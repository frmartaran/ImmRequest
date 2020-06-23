using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImmRequest.WebApi.Helpers.Inputs
{
    public class CitizenRequestControllerInput
    {
        public ILogic<CitizenRequest> Logic { get; set; }
        public IFinder<Topic> TopicFinder { get; set; }
        public IFinder<Area> AreaFinder { get; set; }

        public CitizenRequestControllerInput(ILogic<CitizenRequest> citizenRequestLogic,
            IFinder<Topic> topicFinder, IFinder<Area> areaFinder)
        {
            Logic = citizenRequestLogic;
            TopicFinder = topicFinder;
            AreaFinder = areaFinder;
        }
    }
}
