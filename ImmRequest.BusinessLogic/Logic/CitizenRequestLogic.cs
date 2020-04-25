using ImmRequest.BusinessLogic.Exceptions;
using ImmRequest.BusinessLogic.Helpers;
using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.BusinessLogic.Resources;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.DataAccess.Interfaces.Exceptions;
using ImmRequest.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.BusinessLogic.Logic
{
    public class CitizenRequestLogic : ILogic<CitizenRequest>
    {
        private IRepository<CitizenRequest> citizenRequestRepository;

        private IValidator<CitizenRequest> citizenRequestValidator;

        public CitizenRequestLogic(IRepository<CitizenRequest> citizenRequestRepository, IValidator<CitizenRequest> citizenRequestValidator)
        {
            this.citizenRequestRepository = citizenRequestRepository;
            this.citizenRequestValidator = citizenRequestValidator;
        }

        public void Create(CitizenRequest objectToCreate)
        {
            if (citizenRequestValidator.IsValid(objectToCreate))
            {
                citizenRequestRepository.Insert(objectToCreate);
            }
        }

        public void Delete(long id)
        {
            try
            {
                citizenRequestRepository.Delete(id);
            }
            catch (DatabaseNotFoundException exception)
            {
                LogicHelpers.WarnIfNotFound(exception, BusinessResource.Action_Delete,
                    BusinessResource.Entity_Request);
            }
        }

        public CitizenRequest Get(long Id)
        {
            var request =  citizenRequestRepository.Get(Id);
            LogicHelpers.WarnIfNotFound(request, BusinessResource.Action_Get, 
                BusinessResource.Entity_Request);
            return request;
        }

        public ICollection<CitizenRequest> GetAll()
        {
            return citizenRequestRepository.GetAll();
        }

        public void Update(CitizenRequest objectToUpdate)
        {
            try
            {
                if (citizenRequestValidator.IsValid(objectToUpdate))
                {
                    citizenRequestRepository.Update(objectToUpdate);
                }
            }
            catch (DatabaseNotFoundException exception)
            {
                LogicHelpers.WarnIfNotFound(exception, BusinessResource.Action_Update,
                    BusinessResource.Entity_Request);
            }

        }
    }
}
