using Microsoft.AspNetCore.Mvc;
using ModzExercice.Services.Interfaces;
using ModzExercice.WebApp.Models;

namespace ModzExercice.WebApp.Controllers
{
    public class ImportController : Controller
    {
        private readonly ILogger<ImportController> logger;
        private readonly IImportService service;

        public ImportController(ILogger<ImportController> logger, IImportService service)
        {
            this.logger = logger;
            this.service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(ImportViewModel model)
        {
            if (ModelState.IsValid) 
            {
                try
                {
                    IFormFile? file = model.File;
                    var serviceFileEntities = service.Process(file.OpenReadStream(), logger);
                    var serviceFile = service.Write(serviceFileEntities);
                    return File(serviceFile, "text/csv", $"{DateTime.Today:yyyyMMdd}-ProcessedFile_" + Path.GetFileNameWithoutExtension(file.FileName) + ".csv");

                }
                catch (Exception e)
                {
                    logger.LogError(e, "Une erreur s'est produite lors du traitement du fichier");
                    return View(nameof(Index));
                }
            }
            return View();
        }
    }
}
