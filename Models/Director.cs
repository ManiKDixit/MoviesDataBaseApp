using System.ComponentModel.DataAnnotations;

namespace MoviesDataBaseApp.Models
{
    public class Director
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }
    }
}
