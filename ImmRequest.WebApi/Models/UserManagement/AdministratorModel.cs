using ImmRequest.Domain.UserManagement;
using ImmRequest.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImmRequest.WebApi.Models.UserManagement
{
    public class AdministratorModel : IModel<Administrator, AdministratorModel>
    {
        public long Id { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public Administrator ToDomain()
        {
            throw new NotImplementedException();
        }

        public AdministratorModel ToModel(Administrator entity)
        {
            throw new NotImplementedException();
        }
    }
}
