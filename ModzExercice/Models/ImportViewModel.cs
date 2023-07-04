using ModzExercice.WebApp.CustomValidation;
using System.ComponentModel.DataAnnotations;

namespace ModzExercice.WebApp.Models
{
    public class ImportViewModel
    {
        [Required(ErrorMessage = "Please select a file.")]
        [DataType(DataType.Upload)]
        //[FileExtensions(ErrorMessage = "Must choose .csv file.", Extensions = "csv")]
        [MyFileExtension(ErrorMessage = "Must select .csv file!", AllowedExtensions = "csv")]
        public IFormFile? File { get; set; }
    }
}
