using System.Collections.Generic;

namespace ImmRequest.Domain.Interfaces
{
    public interface ICustomField
    {
        string Name { get; set; }

        long ParentTypeId { get; set; }

        void SetRange(List<string> values);

        bool ValidateRangeValues();

        bool Validate(string value);

    }
}