using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoviesDatabase.Data;
using MoviesDataBaseApp.Data;
using MoviesDataBaseApp.Models;
using MoviesDataBaseApp.ViewModels;

namespace MoviesDataBaseApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDbContext _context;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpGet]
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }
            //check if the email is there or not 
            var user = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);

            //if the user exists then
            if (user != null)
            {
                //Check password
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);

                //if the password enetered is correct then sign in 
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Movie");
                    }

                    
                }
                // password incorrect
                TempData["Error"] = "Wrong Credentials . Please try again";
                return View(loginViewModel);

            }

            //User not found
            TempData["Error"] = "User does not exist.  Please try again";
            return View(loginViewModel);
        }


        [HttpGet]
        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }

        [HttpPost]
       public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if(!ModelState.IsValid) 
            {
                return View(registerViewModel);
            }

            var user = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "This email address is already in use . Please try another one";
                return View(registerViewModel);
            }

            var newUser = new AppUser()
            {
                Email = registerViewModel.EmailAddress,
                UserName = registerViewModel.EmailAddress,
            };
            
            var newUserAdd = await _userManager.CreateAsync(newUser, registerViewModel.Password);
             if(newUserAdd.Succeeded)
             {
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
                
             }

            return RedirectToAction("Index", "Movie");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Movie");
        }
    }
}
