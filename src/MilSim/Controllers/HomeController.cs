using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using MilSim.Models;
using MilSim.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MilSim.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public HomeController(UserManager<ApplicationUser> userManager,
                            SignInManager<ApplicationUser> signInManager, ApplicationDbContext context ) {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public IActionResult Index( )
        {
            return View();
        }

        public async Task<IActionResult> About() {
            if( HttpContext.User.Identity.IsAuthenticated ) {
                //update various steam fields on the user
                var user = await _userManager.GetUserAsync( HttpContext.User );
                //TODO GET STEAM APIs, add AddSteamUser to factory
                SteamFactory sf = new SteamFactory();
                var response = sf.GetSteamUser( user.SteamId );
                JObject steamUser = JsonConvert.DeserializeObject( response ) as JObject;
                var temp = steamUser.GetValue( "response" ).First;
                var t = temp.First;
                var tr = t.First;
                Player s = JsonConvert.DeserializeObject<Player>( t.First.ToString() );
                sf.UpdateSteamUser( user.SteamId, _context );
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
