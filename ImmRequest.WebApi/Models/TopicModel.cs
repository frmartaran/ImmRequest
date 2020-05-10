using ImmRequest.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImmRequest.WebApi.Models
{
    public class TopicModel : Model<Topic, TopicModel>
    {
        public long Id { get; set; }

        public long AreaId { get; set; }

        public string Name { get; set; }

        public ICollection<TypeModel> Types { get; set; }

        public override TopicModel SetModel(Topic entity)
        {
            Id = entity.Id;
            AreaId = entity.Id;
            Name = entity.Name;
            Types = TypeModel
                .ToModel(entity.Types)
                .ToList();
            return this;
        }

        public override Topic ToDomain()
        {
            return new Topic
            {
                Id = Id,
                AreaId = AreaId,
                Name = Name,
                Types = TypeModel
                    .ToEntity(Types)
                    .ToList()
            };
        }
    }
}
