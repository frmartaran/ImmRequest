using ImmRequest.BusinessLogic.Extentions;
using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.Importer.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ImmRequest.BusinessLogic.Logic.ImporterLogic
{
    public class ImporterLogic : IImporterLogic
    {
        private const string IMPORTER_ASSEMBLY_PATH = @"~\..\ImmRequest.Importer.dll";
        private const string DATA_ACCESS_ASSEMBLY_PATH = @"`\..\ImmRequest.DataAccess.dll";

        public IValidator<T> FindValidatorFor<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public List<string> GetImporterOptions()
        {
            var normalizedPath = NormalizePath(IMPORTER_ASSEMBLY_PATH);
            var assembly = Assembly.LoadFile(normalizedPath);
            var importers = assembly.GetTypes()
                .Where(t => t.GetInterfaces()
                    .Any(i => i.IsGenericType
                        && i.GetGenericTypeDefinition() == typeof(IEntityImporter<,>)
                    )
                )
                .Where(t => !t.GetTypeInfo().IsAbstract)
                .ToList();
            var allNames = importers
                .Select(im => im.Name.SplitByCamelCase())
                .ToList();
            return allNames;
        }

        private static string NormalizePath(string pathToNormalize)
        {
            var currentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var normalizedPath = Path.Combine(currentDirectory, pathToNormalize);
            return normalizedPath;
        }

        public void Import(string path)
        {
            throw new NotImplementedException();
        }
    }
}
