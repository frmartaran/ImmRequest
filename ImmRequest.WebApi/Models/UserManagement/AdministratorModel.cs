using ImmRequest.Domain.UserManagement;
using ImmRequest.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace ImmRequest.WebApi.Models.UserManagement
{
    public class AdministratorModel : Model<Administrator, AdministratorModel>
    {
        public long? Id { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public override Administrator ToDomain()
        {
            var administrator = new Administrator
            {
                Email = this.Email,
                UserName = Username,
                Password = Password
            };

            if (Id.HasValue)
                administrator.Id = Id.Value;

            return administrator;

        }

        public override AdministratorModel SetModel(Administrator entity)
        {
            this.Id = entity.Id;
            this.Username = entity.UserName;
            this.Password = entity.Password;
            this.Email = entity.Email;

            return this;
        }
    }
}
