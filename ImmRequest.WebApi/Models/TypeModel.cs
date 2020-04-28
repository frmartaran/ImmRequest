using ImmRequest.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImmRequest.WebApi.Models
{
    public class TypeModel : Model<TopicType, TypeModel>
    {
        public string Name { get; set; }


        public override TypeModel SetModel(TopicType entity)
        {
            Name = entity.Name;
            return this;
        }

        public override TopicType ToDomain()
        {
            throw new NotImplementedException();
        }
    }
}
