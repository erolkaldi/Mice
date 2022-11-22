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

namespace Mice.IdentityServices.Features.Customers.Commands
{
    public class CustomerLoginCommand : IRequest<ActionResponse<Token>>
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public string ConfirmPassword { get; set; } = "";
    }

    public class CustomerLoginCommandHandler : IRequestHandler<CustomerLoginCommand, ActionResponse<Token>>
    {
        private IdentityDbContext _db;
        private readonly IConfiguration _configuration;
        public CustomerLoginCommandHandler(IdentityDbContext db, IConfiguration configuration)
        {
            _configuration = configuration;
            _db = db;
        }

        public async Task<ActionResponse<Token>> Handle(CustomerLoginCommand command, CancellationToken cancellationToken)
        {
            ActionResponse<Token> response = new ActionResponse<Token>();
            try
            {
                if (command.Password != command.ConfirmPassword)
                {
                    response.Success = false;
                    response.Message = "ConfirmPassword does not match";
                    return response;
                }
                Customer customer = _db.Customers.Where(p => p.Email == command.Email).FirstOrDefault();
                if (customer == null)
                {
                    response.Success = false;
                    response.Message = "User not found";
                    return response;
                }
                
                if (!customer.EmailVerified)
                {
                    response.Success = false;
                    response.Message = "Email not verified";
                    return response;
                }
                TokenHelper tokenHelper = new TokenHelper();
                if (customer.Password != tokenHelper.HashPassword(command.Password))
                {
                    
                    response.Success = false;
                    response.Message = "Password is wrong";
                    return response;
                }
                response.Data = tokenHelper.GenerateCustomerToken(customer, _configuration["Jwt:CustomerKey"], _configuration["Jwt:Issuer"]);
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
