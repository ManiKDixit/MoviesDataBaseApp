using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace MoviesDataBaseApp.Models
{
    public class AppUser : IdentityUser
    {

        public string? WatchList { get; set; }
        public string? Favorites { get; set; }
        public string? ProfileImageUrl { get; internal set; }
    }
}
