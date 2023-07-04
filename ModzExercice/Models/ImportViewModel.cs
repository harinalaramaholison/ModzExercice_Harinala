using System.ComponentModel.DataAnnotations;

namespace ModzExercice.WebApp.Models
{
    public class ImportViewModel
    {
        [Required(ErrorMessage = "Please select a file.")]
        [DataType(DataType.Upload)]
        [FileExtensions(ErrorMessage = "Must choose .csv file.", Extensions = "csv")]
        public IFormFile? File { get; set; }
    }
}
