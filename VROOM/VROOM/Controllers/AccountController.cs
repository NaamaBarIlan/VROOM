using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using VROOM.Models;
using VROOM.Models.DTOs;

namespace VROOM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private SignInManager<ApplicationUser> _signInManager;

        private IConfiguration _config;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = configuration;
        }

        // api/account/register
        [HttpPost, Route("register")]
        [Authorize (Policy = "SilverLevel")]
        public async Task<IActionResult> Register(RegisterDTO register)
        {
            ApplicationUser user = new ApplicationUser()
            {
                Email = register.Email,
                UserName = register.Email,
                FirstName = register.FirstName,
                LastName = register.LastName
            };

            // Create the user
            var result = await _userManager.CreateAsync(user, register.Password);

            if (result.Succeeded)
            {
                if(user.Email == _config["CEOEmail"])
                {
                    register.Role = ApplicationRoles.CEO;
                    await _userManager.AddToRoleAsync(user, ApplicationRoles.CEO);
                }
                else 
                {
                    if(User.IsInRole("Office Manager"))
                    {
                        if(register.Role == ApplicationRoles.Employee)
                        {
                            await _userManager.AddToRoleAsync(user, register.Role);
                        }
                        else
                        {
                            return BadRequest();
                        }
                    }
                    else if(User.IsInRole("CEO"))
                    {
                        await _userManager.AddToRoleAsync(user, register.Role);
                    }

                }

                //sign the user in if it was successful.
                await _signInManager.SignInAsync(user, false);

                return Ok();
            }

            return BadRequest("Invalid Registration");

        }

        // api/account/login
        [HttpPost, Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, false);
            if (result.Succeeded)
            {
                // look the user up
                var user = await _userManager.FindByEmailAsync(login.Email);

                // User is our identity "Principle"

                var identityRole = await _userManager.GetRolesAsync(user);

                var token = CreateToken(user, identityRole.ToList());

                // make them a token based on their account

                // send that JWT token back to the user

                // log the user in
                return Ok(new
                {
                    jwt = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });

            }

            return BadRequest("Invalid attempt");
        }

        // api/account/assign/role
        [HttpPost, Route("assign/role")]
        [Authorize(Policy = "SilverLevel")]
        public async Task AssignRoleToUser(AssignRoleDTO assignment)
        {
            var user = await _userManager.FindByEmailAsync(assignment.Email);

            string assignedRole = GetRole(assignment);

            //if ((User.IsInRole("Office Manager") && assignment.Role != "Employee"))
            //{
            //    return BadRequest("Invalid Registration");
            //}

            await _userManager.AddToRoleAsync(user, assignedRole);
        }

        private JwtSecurityToken CreateToken(ApplicationUser user, List<string> role)
        {

            var authClaims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName),
                new Claim("UserId", user.Id)
                //optional, add fav-color claim
            };

            foreach (var item in role)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, item));
            }

            var token = AuthenticateToken(authClaims);

            return token;
        }

        private JwtSecurityToken AuthenticateToken(List<Claim> claims)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWTKey"]));

            var token = new JwtSecurityToken(
                issuer: _config["JWTIssuer"],
                audience: _config["JWTIssuer"],
                expires: DateTime.UtcNow.AddHours(24),
                claims: claims,
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        private string GetRole(AssignRoleDTO assignRoleDTO)
        {
            string role = "";
            switch (assignRoleDTO.Role.ToLower())
            {
                case "ceo":
                    role = ApplicationRoles.CEO;
                    break;
                case "office manager":
                    role = ApplicationRoles.OfficeManager;
                    break;
                case "employee":
                    role = ApplicationRoles.Employee;
                    break;
                default:
                    break;
            }

            return role;
        }
    }
}
