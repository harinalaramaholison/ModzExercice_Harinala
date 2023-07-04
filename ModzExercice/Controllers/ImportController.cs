using Microsoft.AspNetCore.Mvc;
using ModzExercice.WebApp.Models;

namespace ModzExercice.WebApp.Controllers
{
    public class ImportController : Controller
    {
        public IActionResult Index()
        {
            if (ModelState.IsValid) 
            {

            }
            return View();
        }

        public IActionResult Index(ImportViewModel model)
        {
            if (ModelState.IsValid) 
            {
                try
                {
                    using (var reader = new StreamReader(model.File.OpenReadStream()))
                    {
                        
                    }
                }
                catch (Exception e)
                {

                    throw;
                }
            }
            return View();
        }
    }
}
