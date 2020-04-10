using System;
using ImmRequest.Domain.Interfaces;

namespace ImmRequest.Domain.UserManagement
{
    public class Administrator : IIdentifiable
    {
        public long Id { get; set; }

        public string UserName { get; set; }

        public string PassWord { get; set; }

        public string Email { get; set; }
    }
}