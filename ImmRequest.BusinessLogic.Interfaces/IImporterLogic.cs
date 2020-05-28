using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.BusinessLogic.Interfaces
{
    public interface IImporterLogic
    {
        List<string> GetImporterOptions();

        void Import(string path);

        IValidator<T> FindValidatorFor<T>() where T : class;

    }
}
