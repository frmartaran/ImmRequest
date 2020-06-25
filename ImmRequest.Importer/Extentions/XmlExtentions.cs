using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ImmRequest.Importer.Extentions
{
    public static class XmlExtentions
    {
        public static List<XmlElement> AsList(this XmlNodeList nodeList)
        {
            return nodeList.Cast<XmlElement>().ToList();
        }
    }
}
