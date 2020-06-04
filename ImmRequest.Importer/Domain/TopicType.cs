using ImmRequest.Importer.Extentions;
using ImmRequest.Importer.Interfaces.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImmRequest.Importer.Domain
{
    public class TopicType : IType
    {
        public string Name { get; set; }

    }
}
