using System;

namespace ImmRequest.Domain.Interfaces
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }

        DateTime DeletedDate { get; set; }
    }
}