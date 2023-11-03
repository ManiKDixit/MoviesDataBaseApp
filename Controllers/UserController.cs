using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoviesDataBaseApp.Interfaces;
using MoviesDataBaseApp.Models;
using MoviesDataBaseApp.ViewModels;

namespace MoviesDataBaseApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IPhotoService _photoService;

        public UserController(IUserRepository userRepository, UserManager<AppUser> userManager, IPhotoService photoService)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _photoService = photoService;
        }

        [HttpGet("users")]
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllUsers();
            List<UserViewModel> result = new List<UserViewModel>();
            foreach (var user in users)
            {
                var userViewModel = new UserViewModel()
                {
                    Id = user.Id,
                    WatchList = user.WatchList,
                    Favorites = user.Favorites,
                    ProfileImageUrl = user.ProfileImageUrl,
                  
                    UserName = user.UserName,

                };
                result.Add(userViewModel);
            }
            return View(result);
        }




        [HttpGet]
        public async Task<IActionResult> Detail(string id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return RedirectToAction("Index", "Users");
            }

            var userDetailViewModel = new UserDetailViewModel()
            {
                Id = user.Id,
                WatchList = user.WatchList,
                Favorites = user.Favorites,
                UserName = user.UserName,
                ProfileImageUrl = user.ProfileImageUrl

            };
            return View(userDetailViewModel);
        }
    }
}
