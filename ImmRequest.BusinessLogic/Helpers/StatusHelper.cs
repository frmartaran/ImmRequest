using ImmRequest.Domain.Enums;
using System.Collections.Generic;

namespace ImmRequest.BusinessLogic.Helpers
{
    public class StatusHelper
    {
        public static ICollection<RequestStatus> NextStatuses(RequestStatus status)
        {
            var nextStatuses = new List<RequestStatus>();
            if (status == RequestStatus.Created)
            {
                nextStatuses.Add(RequestStatus.OnRevision);
            }
            else if (status == RequestStatus.OnRevision)
            {
                nextStatuses.Add(RequestStatus.Acepted);
                nextStatuses.Add(RequestStatus.Declined);
            }
            else if (status == RequestStatus.Acepted || status == RequestStatus.Declined)
            {
                nextStatuses.Add(RequestStatus.Ended);
            }
            else if (status == RequestStatus.Ended)
            {
                nextStatuses.Add(RequestStatus.Ended);
            }
            return nextStatuses;
        }

        public static ICollection<RequestStatus> PreviousStatuses(RequestStatus status)
        {
            var previousStatuses = new List<RequestStatus>();
            if (status == RequestStatus.Created)
            {
                previousStatuses.Add(RequestStatus.Created);
            }
            else if (status == RequestStatus.OnRevision)
            {
                previousStatuses.Add(RequestStatus.Created);
            }
            else if (status == RequestStatus.Acepted || status == RequestStatus.Declined)
            {
                previousStatuses.Add(RequestStatus.OnRevision);
            }
            else if (status == RequestStatus.Ended)
            {
                previousStatuses.Add(RequestStatus.Acepted);
                previousStatuses.Add(RequestStatus.Declined);
            }
            return previousStatuses;
        }
    }
}
