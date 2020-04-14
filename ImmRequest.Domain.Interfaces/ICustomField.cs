using System.Collections.Generic;

namespace ImmRequest.Domain.Interfaces
{
    public interface ICustomField
    {
        string Name { get; set; }

        long ParentTypeId { get; set; }

        void SetRange(List<string> values);

        T ValueToDataType<T>(string value) where T : class;

        bool Validate(string value);

    }
}