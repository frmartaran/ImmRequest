using System;
using System.Collections.Generic;
using ImmRequest.Domain.Interfaces;

namespace ImmRequest.Domain
{
    public class Area : IIdentifiable
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<Topic> Topics { get; set; }
    }
}