using ImmRequest.DataAccess.Interfaces;
using ImmRequest.Domain;
using ImmRequest.Domain.Fields;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.BusinessLogic.Extensions
{
    public class CitizenRequestValidatorInput
    {
        public IRepository<Area> AreaRepository { get; set; }

        public IRepository<Topic> TopicRepository { get; set; }

        public IRepository<TopicType> TopicTypeRepository { get; set; }

        public IRepository<BaseField> FieldRepository { get; set; }
    }
}
