using System.ComponentModel.DataAnnotations;

namespace ModzExercice.WebApp.CustomValidation
{
    public class MyFileExtension : ValidationAttribute
    {
        public string? AllowedExtensions { get; set; }
        public override bool IsValid(object? value)
        {
            IFormFile? myFile = value as IFormFile;

            if (myFile != null)
            {                
                string fileExt = Path.GetExtension(myFile.FileName);
                fileExt = fileExt.TrimStart('.');

                return AllowedExtensions.Contains(fileExt);
            }
            return true;
        }
    }
}
