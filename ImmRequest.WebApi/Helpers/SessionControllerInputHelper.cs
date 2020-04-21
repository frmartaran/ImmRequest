using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImmRequest.WebApi.Helpers
{
    public class SessionControllerInputHelper
    {
        public ISessionLogic Logic { get; set; }

        public IAdministratorLogic AdministratorLogic { get; set; }

        public IContextHelper ContextHelper { get; set; }

        public SessionControllerInputHelper(ISessionLogic logic,
            IAdministratorLogic administratorLogic,
            IContextHelper contextHelper)
        {
            Logic = logic;
            AdministratorLogic = administratorLogic;
            ContextHelper = contextHelper;
        }
    }
}
