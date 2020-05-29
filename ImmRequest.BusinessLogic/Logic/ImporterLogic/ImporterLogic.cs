using ImmRequest.BusinessLogic.Extentions;
using ImmRequest.BusinessLogic.Helpers.Automapper;
using ImmRequest.BusinessLogic.Helpers.Inputs;
using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.BusinessLogic.Validators;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.Domain;
using ImmRequest.Domain.Fields;
using ImmRequest.Importer.Interfaces;
using ImmRequest.Importer.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace ImmRequest.BusinessLogic.Logic.ImporterLogic
{
    public class ImporterLogic : IImporterLogic
    {
        private const string IMPORTERS_FOLDER = @"~\..\Importers";
        private const string IMPORT_METHOD_NAME = "Import";
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
            var normalizedPath = NormalizePath(IMPORTERS_FOLDER);
            var allImporters = GetAllImporters(normalizedPath);
            var allNames = allImporters
                .Select(im => im.Name.SplitByCamelCase())
                .ToList();
            return allNames;
        }

        private List<Type> GetAllImporters(string normalizedPath)
        {
            var allImporters = new List<Type>();
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
                allImporters.AddRange(importers);

            }
            return allImporters;
        }

        private string NormalizePath(string pathToNormalize)
        {
            var currentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var normalizedPath = Path.Combine(currentDirectory, pathToNormalize);
            return normalizedPath;
        }

        public void Import(string importerName, string path)
        {
            var importerType = FindImporter(importerName);
            var areas = InvokeImport(path, importerType);
            var mappedAreas = Map(areas);
            SaveImportedElements(mappedAreas);
        }

        private void SaveImportedElements(ICollection<Area> mappedAreas)
        {
            foreach (var area in mappedAreas)
            {
                foreach (var topic in area.Topics)
                {
                    topic.Area = area;
                    TopicValidator.IsValid(topic);
                    foreach (var type in topic.Types)
                    {
                        type.ParentTopic = topic;
                        type.AllFields = new List<BaseField>();
                        TopicTypeValidator.IsValid(type);
                    }
                }
                if (AreaValidator.IsValid(area))
                {
                    AreaRepository.Insert(area);
                    AreaRepository.Save();
                }
            }
        }

        private ICollection<Area> Map(ICollection<IArea> areas)
        {
            var mapper = MapperProfile.GetMapper();
            var mappedAreas = mapper.Map<ICollection<IArea>, ICollection<Area>>(areas);
            return mappedAreas;
        }

        private ICollection<IArea> InvokeImport(string path, Type importerType)
        {
            var importer = Activator.CreateInstance(importerType, path);
            var methodInfo = importerType.GetMethod(IMPORT_METHOD_NAME);
            var result = methodInfo.Invoke(importer, new object[] { });
            var areas = result as ICollection<IArea>;
            return areas;
        }

        private Type FindImporter(string importerName)
        {
            var normalizedPath = NormalizePath(IMPORTERS_FOLDER);
            var allImporters = GetAllImporters(normalizedPath);
            var normalizedName = Regex.Replace(importerName, @"\s+", "").ToUpper();
            var importerType = allImporters
                .Where(t => t.Name.ToUpper().Equals(normalizedName))
                .FirstOrDefault();
            return importerType;
        }
    }
}
