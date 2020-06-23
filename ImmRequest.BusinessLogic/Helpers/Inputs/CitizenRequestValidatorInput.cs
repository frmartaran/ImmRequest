using ImmRequest.DataAccess.Interfaces;
using ImmRequest.Domain;
using ImmRequest.Domain.Fields;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.BusinessLogic.Helpers.Inputs
{
    public class CitizenRequestValidatorInput
    {
        public IRepository<Area> AreaRepository { get; set; }
        public IRepository<Topic> TopicRepository { get; set; }
        public IRepository<TopicType> TopicTypeRepository { get; set; }
        public IRepository<BaseField> FieldRepository { get; set; }
        public IRepository<CitizenRequest> CitizenRequestRepository { get; set; }
        public CitizenRequestValidatorInput(IRepository<Area> areaRepository, IRepository<Topic> topicRepository,
            IRepository<TopicType> topicTypeRepository, IRepository<BaseField> fieldRepository,
            IRepository<CitizenRequest> citizenRequestRepository)
        {
            AreaRepository = areaRepository;
            TopicRepository = topicRepository;
            TopicTypeRepository = topicTypeRepository;
            FieldRepository = fieldRepository;
            CitizenRequestRepository = citizenRequestRepository;
        }
    }
}
