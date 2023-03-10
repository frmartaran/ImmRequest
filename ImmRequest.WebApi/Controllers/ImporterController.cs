using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ImmRequest.BusinessLogic.Exceptions;
using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.WebApi.Filters;
using ImmRequest.WebApi.Resources;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImmRequest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [AuthorizationFilter]
    [EnableCors("CorsPolicy")]
    public class ImporterController : ControllerBase
    {
        private IImporterLogic Logic { get; set; }

        public ImporterController(IImporterLogic logic)
        {
            Logic = logic;

        }
        /// <summary>
        /// Permite a un administrardor realizar ingresar nuevas areas usando un importador dado.
        /// </summary>
        /// <param name="importer"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Import([FromForm] string importer, [FromForm] IFormFile file)
        {
            if (file == null)
                return BadRequest(WebApiResource.Importer_FileNull);

            try
            {
                var mainPath = AppDomain.CurrentDomain.BaseDirectory;
                var today = DateTime.Now.ToString("yyyy-MM-dd");
                var path = mainPath + $"{today}-Uploads\\";

                Directory.CreateDirectory(path);
                var filePath = Path.Combine(path, file.FileName);
                using (var newFile = System.IO.File.Create(filePath))
                {
                    file.CopyToAsync(newFile).Wait();
                    newFile.Close();
                }

                Logic.Import(importer, filePath);
                return Ok(WebApiResource.Import_Successful);
            }
            catch (ValidationException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (BusinessLogicException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (DeveloperException exception)
            {
                return StatusCode(500, exception.Message);
            }

        }

        /// <summary>
        /// Retorna la lista de importadores en el sistema.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Get()
        {
            var allImporters = Logic.GetImporterOptions();

            return Ok(allImporters);
        }
    }
}