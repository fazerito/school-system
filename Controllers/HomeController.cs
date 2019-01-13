using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Models;
using SchoolProject.ViewModels;

namespace SchoolProject.Controllers
{
    public class HomeController : Controller
    {
        private SignInManager<Users> _signManager;
        private UserManager<Users> _userManager;

        public HomeController(SignInManager<Users> signManager, UserManager<Users> userManager)
        {
            _userManager = userManager;
            _signManager = signManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

  

        [HttpGet]
        [Route("login")]  
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            { 
                var result = await _signManager.PasswordSignInAsync(model.Login, model.Password, true, false);

                if (result.Succeeded)
                {
                    var user = _userManager.GetUserAsync(HttpContext.User);
                    var usr = User.Identity.Name;
                    using (var context = new SchoolDbContext())
                    {
                        var eeee = context.Users
                            .Where(u => u.Login == usr)
                            .Select(u => u.UserId)
                            .FirstOrDefault();
                    }
                         
                    return RedirectToAction(nameof(Index));
                }
            }

            

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
