using System.ComponentModel.DataAnnotations;

namespace MoviesDataBaseApp.Models
{
    public class Award
    {
        [Key]
        public int Id { get; set; }

        public string AwardName { get; set; }

        public DateTime AwardDate { get; set; }
    }
}
