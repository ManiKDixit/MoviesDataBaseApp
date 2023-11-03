using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using MoviesDataBaseApp.Interfaces;
using MoviesDataBaseApp.Models;
using MoviesDataBaseApp.ViewModels;

namespace MoviesDataBaseApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardController(IDashboardRepository dashboardRepository , IPhotoService photoService , IHttpContextAccessor httpContextAccessor)
        {
            _dashboardRepository = dashboardRepository;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;
        }

        private void MapUserEdit(AppUser user, EditUserDashboardViewModel editVm, ImageUploadResult photoResult)
        {
            user.Id = editVm.Id;
            user.UserName = editVm.UserName;
            user.WatchList = editVm.WatchList;
            user.Favorites = editVm.Favorites;
            user.ProfileImageUrl = photoResult.Url.ToString();
        } 


        public async Task<IActionResult> Index()
        {
            var userMovies = await _dashboardRepository.GetAllUserMovies();
            var dashboardViewModel = new DashboardViewModel()
            {
                Movies = userMovies,
            };

            return View(dashboardViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditUserProfile()
        {
            var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var user = await _dashboardRepository.GetUserById(curUserId);
            if(user == null) return View("Error") ;
            var editUserViewModel = new EditUserDashboardViewModel()
            {
                Id = curUserId,
                WatchList = user.WatchList,
                Favorites = user.Favorites,
                ProfileImageUrl = user.ProfileImageUrl
            };
            return View(editUserViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> EditUserProfile(EditUserDashboardViewModel editVM)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to Edit Profile");
                return View("EditUserProfile", editVM);
            }

            AppUser user = await _dashboardRepository.GetByIdNoTracking(editVM.Id);

            if(user.ProfileImageUrl == "" || user.ProfileImageUrl == null)
            {
                var photoResult = await _photoService.AddPhotoAsync(editVM.Image);

                MapUserEdit(user,editVM, photoResult);
                _dashboardRepository.Update(user);
                return RedirectToAction("Index");
            }
            else
            {
                try
                {
                    await _photoService.DeletePhotoAsync(user.ProfileImageUrl);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(editVM);
                }

                var photoResult = await _photoService.AddPhotoAsync(editVM.Image);
                MapUserEdit(user , editVM , photoResult);   
                _dashboardRepository.Update(user);
                return RedirectToAction("Index");
            }
        }

        
    }
}
