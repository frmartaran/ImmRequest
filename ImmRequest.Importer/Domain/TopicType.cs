﻿using ImmRequest.Importer.Interfaces.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImmRequest.Importer.Domain
{
    public class TopicType : IType
    {

        [JsonConstructor]
        public TopicType(ICollection<Field> fields)
        {
            Fields = fields.Cast<IField>().ToList();
        }
        public string Name { get; set; }
        public ICollection<IField> Fields { get; set; }
    }
}
