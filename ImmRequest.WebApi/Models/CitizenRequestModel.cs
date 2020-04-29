﻿using ImmRequest.Domain;
using ImmRequest.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ImmRequest.WebApi.Models
{
    public class CitizenRequestModel : Model<CitizenRequest, CitizenRequestModel>
    {
        public long Id { get; set; }

        public string Description { get; set; }

        public string CitizenName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public long AreaId { get; set; }

        public long TopicId { get; set; }

        public long TopicTypeId { get; set; }

        public RequestStatus Status { get; set; }

        public List<RequestFieldValuesModel> Values { get; set; }

        public override CitizenRequestModel SetModel(CitizenRequest entity)
        {
            Id = entity.Id;
            Description = entity.Description;
            CitizenName = entity.CitizenName;
            Email = entity.Email;
            Phone = entity.Phone;
            AreaId = entity.AreaId;
            TopicId = entity.TopicId;
            Status = entity.Status;
            Values = RequestFieldValuesModel
                .ToModel(entity.Values)
                .ToList();
            return this;
        }

        public override CitizenRequest ToDomain()
        {
            return new CitizenRequest
            {
                Description = Description,
                CitizenName = CitizenName,
                Email = Email,
                Phone = Phone,
                AreaId = AreaId,
                TopicId = TopicId,
                TopicTypeId = TopicTypeId,
                Values = RequestFieldValuesModel
                .ToEntity(Values)
                .ToList()
            };
        }
    }
}
