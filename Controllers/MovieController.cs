using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesDatabase.Data;
using MoviesDataBaseApp.Models;

namespace MoviesDataBaseApp.Controllers
{
    public class MovieController : Controller
    {


        private readonly ApplicationDbContext _context;


        public MovieController(ApplicationDbContext context)
        {
            _context = context; //Injecting the DB here
        }

        public IActionResult Index()
        {
            var movies = _context.Movies.ToList();
            return View(movies);
        }

        public async Task<IActionResult> Detail(int id)
        {

            Movie movie = await _context.Movies.Include(director => director.Director)
     .Include(genre => genre.Genre).Include(award => award.Award).Include(studios => studios.Studios).FirstOrDefaultAsync(i => i.Id == id);
            return View(movie);
        }

    }
}
