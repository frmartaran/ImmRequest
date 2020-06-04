using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.BusinessLogic.Helpers.Inputs
{
    public class AreaImporterInput
    {
        public IRepository<Area> AreaRepository { get; set; }
        public IRepository<Topic> TopicRepository { get; set; }
        public IValidator<Area> AreaValidator { get; set; }
        public IValidator<Topic> TopicValidator { get; set; }
        public IValidator<TopicType> TopicTypeValidator { get; set; }
        public AreaImporterInput(IRepository<Area> areaRepository,
            IRepository<Topic> topicRepository, IValidator<Area> areaValidator,
            IValidator<Topic> topicValidator, IValidator<TopicType> typeValidator)
        {
            AreaValidator = areaValidator;
            TopicValidator = topicValidator;
            TopicTypeValidator = typeValidator;
            AreaRepository = areaRepository;
            TopicRepository = topicRepository;
        }
    }
}
