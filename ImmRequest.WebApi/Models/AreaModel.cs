using ImmRequest.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ImmRequest.WebApi.Models
{
    public class AreaModel : Model<Area, AreaModel>
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public ICollection<TopicModel> Topics { get; set; }

        public override AreaModel SetModel(Area entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Topics = TopicModel
                .ToModel(entity.Topics)
                .ToList();
            return this;
        }

        public override Area ToDomain()
        {
            return new Area
            {
                Id = Id,
                Name = Name,
                Topics = TopicModel
                .ToEntity(Topics)
                .ToList()
            };
        }
    }
}
