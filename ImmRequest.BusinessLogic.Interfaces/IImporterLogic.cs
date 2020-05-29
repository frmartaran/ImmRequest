using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.BusinessLogic.Interfaces
{
    public interface IImporterLogic
    {
        List<string> GetImporterOptions();

        void Import(string importerName, string path);
    }
}
