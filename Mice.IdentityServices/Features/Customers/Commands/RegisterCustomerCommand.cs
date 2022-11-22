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

namespace Mice.IdentityServices.Features.Customers.Commands
{
    public class RegisterCustomerCommand : IRequest<ActionResponse<string>>
    {
        public string Email { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Phone { get; set; } = "";
        public string BirthDate { get; set; } = "";
        public string Password { get; set; } = "";
        public string ConfirmPassword { get; set; } = "";
    }

    public class RegisterCustomerCommandHandler : IRequestHandler<RegisterCustomerCommand, ActionResponse<string>>
    {
        private IdentityDbContext _db;

        public RegisterCustomerCommandHandler(IdentityDbContext db)
        {
            _db = db;
        }

        public async Task<ActionResponse<string>> Handle(RegisterCustomerCommand cmd, CancellationToken cancellationToken)
        {
            ActionResponse<string> response = new ActionResponse<string>();
            try
            {
                if (cmd.Password != cmd.ConfirmPassword)
                {
                    response.Success = false;
                    response.Message = "ConfirmPassword does not match";
                    return response;
                }
                bool exists = _db.Customers.Any(p => p.Email == cmd.Email);
                if (exists)
                {
                    response.Success = false;
                    response.Message = "Email already exists for a customer";
                    return response;
                }
                
                TokenHelper tokenHelper = new TokenHelper();
                Customer customer = new Customer();
                customer.Id = Guid.NewGuid();
                customer.Email = cmd.Email;
                customer.EmailVerified = false;
                customer.Phone = cmd.Phone;
                customer.Password =tokenHelper.HashPassword(cmd.Password);
                customer.BirthDate = Convert.ToDateTime(cmd.BirthDate);
                customer.FirstName = cmd.FirstName;
                customer.LastName = cmd.LastName;
                customer.CreateDate = DateTime.Now;
                customer.CreateUser = cmd.Email;
                _db.Customers.Add(customer);
                
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
