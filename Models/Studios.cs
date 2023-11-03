using System.ComponentModel.DataAnnotations;

namespace MoviesDataBaseApp.Models
{
    public class Studios
    {
        [Key]
        public int Id { get; set; }

        public string StudioName { get; set; }

        public string? CEO { get; set; }
    }
}
