using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MoviesDataBaseApp.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        //[Key]
        //public string Id { get; set; }

        public string Name { get; set; }

        [ForeignKey("Genre")]
        public int? GenreId { get; set; }

        public string? Image { get; set; }

        public Genre Genre { get; set; }

        [ForeignKey("Director")]
        public int? DirectorId { get; set; }

        public Director Director { get; set; }

        [ForeignKey("Studios")]
        public int? StudiosId { get; set; }

        public Studios Studios { get; set; }

        public string Language { get; set; }

        [ForeignKey("Award")]
        public int? AwardId { get; set; }
        public Award? Award { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string? Description { get; set; }

        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
