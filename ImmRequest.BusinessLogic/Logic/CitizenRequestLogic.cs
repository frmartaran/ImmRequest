using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.DataAccess.Interfaces;
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

            }
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public CitizenRequest Get(long Id)
        {
            throw new NotImplementedException();
        }

        public ICollection<CitizenRequest> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(CitizenRequest objectToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
