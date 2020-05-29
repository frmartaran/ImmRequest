using ImmRequest.BusinessLogic.Extentions;
using ImmRequest.BusinessLogic.Helpers.Inputs;
using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.BusinessLogic.Validators;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.Domain;
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
        private const string IMPORTER_ASSEMBLY_PATH = @"~\..\Importers";
        private IRepository<Area> AreaRepository { get; set; }
        private IRepository<Topic> TopicRepository { get; set; }
        private IValidator<Area> AreaValidator { get; set; }
        private IValidator<Topic> TopicValidator { get; set; }
        private IValidator<TopicType> TopicTypeValidator { get; set; }

        public ImporterLogic(AreaImporterInput input)
        {
            AreaRepository = input.AreaRepository;
            TopicRepository = input.TopicRepository;
            AreaValidator = input.AreaValidator;
            TopicValidator = input.TopicValidator;
            TopicTypeValidator = input.TopicTypeValidator;
        }

        public List<string> GetImporterOptions()
        {
            var normalizedPath = NormalizePath(IMPORTER_ASSEMBLY_PATH);
            var allImporters = new List<string>();
            var allFiles = Directory.GetFiles(normalizedPath, "*.dll");
            foreach (var dll in allFiles)
            {
                var assembly = Assembly.LoadFile(dll);
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
                allImporters.AddRange(allNames);

            }
            return allImporters;
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
