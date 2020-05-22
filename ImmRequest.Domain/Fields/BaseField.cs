using System;
using System.Collections.Generic;
using ImmRequest.Domain.Enums;
using ImmRequest.Domain.Interfaces;

namespace ImmRequest.Domain.Fields
{
    public abstract class BaseField : ICustomField, IIdentifiable, ISoftDelete
    {
        public long Id { get; set; }
        public abstract DataType InputType { get; }
        public string Name { get; set; }
        public long ParentTypeId { get; set; }
        public TopicType ParentType { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedDate { get; set; }
        public virtual bool IsMultipleSelectEnabled { get; set; }
        public virtual void SetRange(List<string> values) { }

        public abstract bool Validate(List<string> values);
        public virtual bool ValidateRangeValues() { return true; }

        public virtual void UpdateValues(BaseField valuesToUpdate)
        {
            Name = valuesToUpdate.Name;
            ParentType = valuesToUpdate.ParentType;
        }

    }
}