using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.Importer.Tests
{
    public static class TestConstants
    {
        private const string ROOT_PATH = "~\\..\\..\\..\\..\\{0}\\Files\\";

        public static string JsonPath
        {
            get
            {
                return string.Format(ROOT_PATH, "JsonTests");
            }
        }

        public static string XMLPath
        {
            get
            {
                return string.Format(ROOT_PATH, "XMLTests");
            }
        }

    }
}
