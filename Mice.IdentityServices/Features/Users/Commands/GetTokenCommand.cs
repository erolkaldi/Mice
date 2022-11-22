using MediatR;
using Mice.IdentityContext;
using Mice.IdentityModels.DtoModels;
using Mice.IdentityModels.EntityModels;
using Mice.IdentityServices.Helpers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mice.IdentityServices.Features.Users.Commands
{
    public class GetTokenCommand : IRequest<ActionResponse<Token>>
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }

    public class GetTokenCommandHandler : IRequestHandler<GetTokenCommand, ActionResponse<Token>>
    {
        private IdentityDbContext _db;
        private readonly IConfiguration _configuration;
        public GetTokenCommandHandler(IdentityDbContext db,IConfiguration configuration)
        {
            _configuration = configuration;
            _db = db;
        }

        public async Task<ActionResponse<Token>> Handle(GetTokenCommand command,CancellationToken cancellationToken)
        {
            ActionResponse < Token > response=new ActionResponse<Token> ();
            try
            {
                CompanyUser companyUser = _db.CompanyUsers.Where(p => p.Email == command.Email).FirstOrDefault();
                if (companyUser == null)
                {
                    response.Success = false;
                    response.Message = "User not found";
                    return response;
                }
                if (companyUser.Blocked)
                {
                    response.Success = false;
                    response.Message = "User blocked";
                    return response;
                }
                
                if (!companyUser.EmailVerified)
                {
                    response.Success = false;
                    response.Message = "Email not verified";
                    return response;
                }
                TokenHelper tokenHelper=new TokenHelper();
                if (companyUser.Password != tokenHelper.HashPassword(command.Password))
                {
                    companyUser.RetryCount++;
                    if (companyUser.RetryCount > 10)
                    {
                        companyUser.Blocked = true;
                        _db.Update(companyUser);
                        _db.SaveChanges();
                        response.Success = false;
                        response.Message = "User blocked";
                        return response;
                    }
                    _db.Update(companyUser);
                    _db.SaveChanges();
                    response.Success = false;
                    response.Message = "Password is wrong";
                    return response;
                }
                response.Data=tokenHelper.GenerateToken( companyUser, _configuration["Jwt:Key"], _configuration["Jwt:Issuer"]);
                response.Success = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Internal Server Error";
                response.Exception = ex.Message;
                return response;
            }
        }
    }
}
