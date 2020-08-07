using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VROOM.Models;
using VROOM.Models.DTOs;

namespace VROOM.Controllers
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public RegisterController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost, ActionName("Register")]
        public IActionResult Register([Bind("Email, Password")] RegisterDTO registerDTO)
        {

            return RedirectToAction("Index", "Login");

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
