using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesDatabase.Data;
using MoviesDataBaseApp.Interfaces;
using MoviesDataBaseApp.Models;
using MoviesDataBaseApp.ViewModels;
using System.Net;

namespace MoviesDataBaseApp.Controllers
{
    public class MovieController : Controller
    {


        //private readonly ApplicationDbContext _context;
        private readonly IMovieRepository _movieRepository;
        private readonly IPhotoService _photoService;

        public MovieController(IMovieRepository movieRepository, IPhotoService photoService)
        {
            _movieRepository = movieRepository; //Injecting the DB here
            _photoService = photoService;
        }

        public async Task<IActionResult> Index()
        {
            var movies = await _movieRepository.GetAll();
            return View(movies);
        }

        public async Task<IActionResult> Detail(int id)
        {

            Movie movie =  await _movieRepository.GetByIdAsync(id);
            return View(movie);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddMovieViewModel movieVM)
        {
            if (ModelState.IsValid)
            {
                // return View(club);
                var result = await _photoService.AddPhotoAsync(movieVM.Image);
                var movie = new Movie
                {
                    Name = movieVM.Name,
                    Genre = movieVM.Genre,
                    Director = new Director
                    {
                        Name = movieVM.Director.Name,
                        Age = movieVM.Director.Age,

                    },

                    Studios = new Studios
                    {
                        StudioName = movieVM.Studios.StudioName,
                        CEO = movieVM.Studios.CEO,
                    },

                    Award = new Award
                    {
                        AwardName = movieVM.Award.AwardName,
                        AwardDate = movieVM.Award.AwardDate,
                    },

                    Language = movieVM.Language,
                    Image = result.Url.ToString(),
                    ReleaseDate = movieVM.ReleaseDate,
                    Description = movieVM.Description,

                };

                _movieRepository.Add(movie);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }

            return View(movieVM);

        }



        public async Task<IActionResult> Edit(int id)
        {

            var movie = await _movieRepository.GetByIdAsync(id);
            if (movie == null)
            {
                return View("Error");
            }

            var movieVM = new EditMovieViewModel
            {

                Name = movie.Name,
                Description = movie.Description,
                Language = movie.Language,
                ReleaseDate = movie.ReleaseDate,
                URL = movie.Image,
                Director = movie.Director,
                Studios = movie.Studios,
                Award = movie.Award,
                Genre = movie.Genre,

            };

            return View(movieVM);

        }



        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditMovieViewModel movieVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit Movie Details");
                return View("Edit", movieVM);
            }

            var userMovie = await _movieRepository.GetByIdAsyncNoTracking(id);

            if (userMovie == null)
            {
                return View("Error");
            }

            var photoResult = await _photoService.AddPhotoAsync(movieVM.Image);

            if (photoResult.Error != null)
            {
                ModelState.AddModelError("Image", "Photo upload failed");
                return View(movieVM);
            }

            if (!string.IsNullOrEmpty(userMovie.Image))
            {
                _ = _photoService.DeletePhotoAsync(userMovie.Image);
            }

            var movie = new Movie
            {
                Id = id,
                Name = movieVM.Name,
                Description = movieVM.Description,
                Image = photoResult.Url.ToString(),
                Director = movieVM.Director,
                Award = movieVM.Award,
                Studios = movieVM.Studios,
                //Director = new Director
                //{
                //    Name = movieVM.Director.Name,
                //    Age = movieVM.Director.Age,

                //},
                //Award = new Award
                //{
                //    AwardName = movieVM.Award.AwardName,
                //    AwardDate = movieVM.Award.AwardDate,
                //},
                //Studios = new Studios
                //{
                //    StudioName = movieVM.Studios.StudioName,
                //    CEO = movieVM.Studios.CEO,
                //},
                Language = movieVM.Language,
                ReleaseDate = movieVM.ReleaseDate,
                Genre = movieVM.Genre,
                
                //AddressId = clubVM.AddressId,
                //Address = clubVM.Address,
            };

            _movieRepository.Update(movie);

            return RedirectToAction("Index");
        }
<<<<<<< Updated upstream
=======


        public async Task<IActionResult> Delete(int id)
        {
            var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            string appid = curUserId;
            var movieDetails = await _movieRepository.GetByIdAsync(id,appid);
            if (movieDetails == null) return View("Error");
            return View(movieDetails);

        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            string appid = curUserId;
            var movieDetails = await _movieRepository.GetByIdAsync(id,appid);
            if (movieDetails == null) return View("Error");

            _movieRepository.Delete(movieDetails);
            return RedirectToAction("Index");

        }



        //[HttpPost]
        //public async Task<IActionResult> Edit(int id, EditMovieViewModel movieVM)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        ModelState.AddModelError("", "Failed to edit Movie Details");
        //        return View("Edit", movieVM);
        //    }

        //    var userMovie = await _movieRepository.GetByIdAsyncNoTracking(id);

        //    if (userMovie == null)
        //    {
        //        return View("Error");
        //    }

        //    var photoResult = await _photoService.AddPhotoAsync(movieVM.Image);

        //    if (photoResult.Error != null)
        //    {
        //        ModelState.AddModelError("Image", "Photo upload failed");
        //        return View(movieVM);
        //    }

        //    if (!string.IsNullOrEmpty(userMovie.Image))
        //    {
        //        _ = _photoService.DeletePhotoAsync(userMovie.Image);
        //    }

        //    var movie = new Movie
        //    {
        //        Id = id,
        //        Name = movieVM.Name,
        //        Description = movieVM.Description,
        //        Image = photoResult.Url.ToString(),
        //        Director = movieVM.Director,
        //        Award = movieVM.Award,
        //        Studios = movieVM.Studios,
        //        AppUserId = userMovie.AppUserId,
        //        //Director = new Director
        //        //{
        //        //    Name = movieVM.Director.Name,
        //        //    Age = movieVM.Director.Age,

        //        //},
        //        //Award = new Award
        //        //{
        //        //    AwardName = movieVM.Award.AwardName,
        //        //    AwardDate = movieVM.Award.AwardDate,
        //        //},
        //        //Studios = new Studios
        //        //{
        //        //    StudioName = movieVM.Studios.StudioName,
        //        //    CEO = movieVM.Studios.CEO,
        //        //},
        //        Language = movieVM.Language,
        //        ReleaseDate = movieVM.ReleaseDate,
        //        Genre = movieVM.Genre,

        //        //AddressId = clubVM.AddressId,
        //        //Address = clubVM.Address,
        //    };

        //    _movieRepository.Update(movie);

        //    return RedirectToAction("Index");
        //}
>>>>>>> Stashed changes
    }

}

