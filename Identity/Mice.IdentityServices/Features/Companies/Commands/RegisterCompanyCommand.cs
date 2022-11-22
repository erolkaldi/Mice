using MediatR;
using Mice.IdentityContext;
using Mice.IdentityModels.DtoModels;
using Mice.IdentityModels.EntityModels;
using Mice.IdentityServices.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mice.IdentityServices.Features.Companies.Commands
{
    public class RegisterCompanyCommand :IRequest<ActionResponse<string>>
    {
        public string Email { get; set; } = "";
        public string CompanyName { get; set; } = "";
        public string Phone { get; set; } = "";
        public string UserEmail { get; set; } = "";
        public string UserPhone { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Password { get; set; } = "";
        public string ConfirmPassword { get; set; } = "";
    }

    public class RegisterCompanyCommandHandler :IRequestHandler<RegisterCompanyCommand, ActionResponse<string>>
    {
        private IdentityDbContext _db;

        public RegisterCompanyCommandHandler(IdentityDbContext db)
        {
            _db = db;
        }

        public async Task<ActionResponse<string>> Handle(RegisterCompanyCommand cmd,CancellationToken cancellationToken)
        {
            ActionResponse<string> response = new ActionResponse<string>();
            try
            {
                if (cmd.Password!=cmd.ConfirmPassword)
                {
                    response.Success = false;
                    response.Message = "ConfirmPassword does not match";
                    return response;
                }
                bool exists = _db.Companies.Any(p => p.Email == cmd.Email);
                if (exists)
                {
                    response.Success = false;
                    response.Message = "Email already exists for a company";
                    return response;
                }
                exists = _db.CompanyUsers.Any(p => p.Email == cmd.UserEmail);
                if (exists)
                {
                    response.Success = false;
                    response.Message = "Your email is already connected with a company";
                    return response;
                }
                TokenHelper tokenHelper = new TokenHelper();
                Company company = new Company();
                company.Id = Guid.NewGuid();
                company.Name = cmd.CompanyName;
                company.Email = cmd.Email;
                company.EmailVerified = false;
                company.Phone = cmd.Phone;
                company.CreateDate= DateTime.Now;
                company.CreateUser = cmd.UserEmail;
                _db.Companies.Add(company);
                CompanyUser companyUser = new CompanyUser();
                companyUser.Id = Guid.NewGuid();
                companyUser.CompanyId = company.Id;
                companyUser.CreateDate = DateTime.Now;
                companyUser.CreateUser = cmd.UserEmail;
                companyUser.Email = cmd.UserEmail;
                companyUser.FirstName = cmd.FirstName;
                companyUser.LastName = cmd.LastName;
                companyUser.Phone = cmd.UserPhone;
                companyUser.Password =tokenHelper.HashPassword(cmd.Password);
                _db.CompanyUsers.Add(companyUser);
                _db.SaveChanges();
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
