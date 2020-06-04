using AutoMapper;
using ImmRequest.Domain;
using ImmRequest.Importer.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.BusinessLogic.Helpers.Automapper
{
    public class MapperProfile
    {

        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<IArea, Area>();
                cfg.CreateMap<ITopic, Topic>()
                .ForMember(src => src.AreaId, opt => opt.Ignore());

                cfg.CreateMap<IType, TopicType>()
                .ForMember(src => src.AllFields, opt => opt.Ignore())
                .ForMember(src => src.ParentTopicId, opt => opt.Ignore());

            });
            return config.CreateMapper();
        }
    }
}
