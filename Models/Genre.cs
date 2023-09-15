using System.ComponentModel.DataAnnotations;

namespace MoviesDataBaseApp.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }
        public string GenreName { get; set; }

    }
}
