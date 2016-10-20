using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using MilSim.Models;

namespace MilSim.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public HomeController(UserManager<ApplicationUser> userManager,
                            SignInManager<ApplicationUser> signInManager ) {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
           
            return View();
        }

        public async Task<IActionResult> About() {
            if( HttpContext.User.Identity.IsAuthenticated ) {
                //update various steam fields on the user
                var user = await _userManager.GetUserAsync( HttpContext.User );
                //TODO GET STEAM APIs
            }
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
