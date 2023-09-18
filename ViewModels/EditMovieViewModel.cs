using MoviesDataBaseApp.Models;
using System.ComponentModel.DataAnnotations;

namespace MoviesDataBaseApp.ViewModels
{
    public class EditMovieViewModel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public IFormFile? Image { get; set; }
        public string? URL { get; set; }

        public Genre Genre { get; set; }

        public Director Director { get; set; }

        public Studios Studios { get; set; }

        public string Language { get; set; }

        public Award? Award { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string? Description { get; set; }
    }
}
