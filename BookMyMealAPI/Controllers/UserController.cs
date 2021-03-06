﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BookMyMealAPI.Data;
using BookMyMealAPI.Model.Entity;
using BookMyMealAPI.Model.Request;
using BookMyMealAPI.Model.Settings;
using BookMyMealAPI.Services.Email;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BookMyMealAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private UserManager<ApplicationUserModel> _userManager;
        private SignInManager<ApplicationUserModel> _singInManager;
        private readonly ApplicationSettings _appSettings;
        private readonly APIDbContext _context;
        public UserController(UserManager<ApplicationUserModel> userManager, SignInManager<ApplicationUserModel> signInManager,IOptions<ApplicationSettings> appSettings,APIDbContext context)
        {
            _userManager = userManager;
            _singInManager = signInManager;
            _appSettings = appSettings.Value;
            _context = context;
        }


        /// <summary>
        /// Can be access by Unauthorize users
        /// </summary>
        [HttpPost]
        [Route("Customer/Registration")]
        public async Task<Object> CustomerRegistration(CustomerRegistrationModel model)
        {
            var usr = await _userManager.FindByEmailAsync(model.Email);
            if (usr != null)
            {
                return BadRequest(new { message = "User Already Exists" });
            }

            var Role = "Customer";
            var applicationUser = new ApplicationUserModel()
            {
                UserName = model.Email,
                Email = model.Email
            };

            ProfileModel profile = new ProfileModel()
            {
                Email = model.Email,
                Name = "Name",
                PhoneNumber="+880100000000000"

            };

            try
            {
                var result = await _userManager.CreateAsync(applicationUser, model.Password);
                await _userManager.AddToRoleAsync(applicationUser, Role);

                _context.Profile.Add(profile);
                await _context.SaveChangesAsync();

                if (result.Succeeded)
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(applicationUser);
                    var callbackUrl = Url.Action(("ConfirmEmail"), "User", new { userId = applicationUser.Id, code = code }, Request.Scheme);

                    EmailSender emailSender = new EmailSender();
                    emailSender.sendVerificationEmail(model.Email, callbackUrl);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Can be access by Unauthorizeduser
        /// </summary>
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginRequestModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                
                if (! await _userManager.IsEmailConfirmedAsync(user))
                {
                    return BadRequest(new { message = "Email not Confirmed" });
                }
                var role = await _userManager.GetRolesAsync(user);
                IdentityOptions _options = new IdentityOptions();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserID",user.Id.ToString()),
                      //  new Claim("Email",user.Email.ToString()),
                        new Claim(_options.ClaimsIdentity.RoleClaimType,role.FirstOrDefault())
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Ok(new { token });
            }
            else
                return BadRequest(new { message = "Username or password is incorrect." });
        }


        /// <summary>
        /// Can be access by Unauthorized users     
        /// </summary>
        [HttpPost]
        [Route("Restaurent/Registration")]
        public async Task<Object> RestaurentRegistration(RestaurantRegistrationModel model)
        {
            var usr = await _userManager.FindByEmailAsync(model.OwnerEmail);
            if ( usr!= null)
            {
                return BadRequest(new { message = "User Already Exists" });
            }

            var Role = "RestaurantOwner";
            var applicationUser = new ApplicationUserModel()
            {
                UserName = model.OwnerEmail,
                Email = model.OwnerEmail
            };

            ProfileModel profile = new ProfileModel()
            {
                Email = model.OwnerEmail,
                Name = model.OwnerName,
                PhoneNumber = model.OwnerPhoneNumber

            };

            RestaurentModel restaurent = new RestaurentModel()
            {
                OwnerEmail = model.OwnerEmail,
                RestaurantEmail = model.RestaurantEmail,
                Phone = model.RestaurantPhone,
                Name = model.RestaurantName,
                Location = model.ResturantAddress

            };

            try
            {
                var result = await _userManager.CreateAsync(applicationUser, model.Password);
                await _userManager.AddToRoleAsync(applicationUser, Role);

                _context.Profile.Add(profile);
                _context.Restaurent.Add(restaurent);
                await _context.SaveChangesAsync();

                var resturantID = _context.Restaurent.Where(r => r.OwnerEmail == model.OwnerEmail).FirstOrDefault().ID;

                RestaurentRequestModel restaurentRequest = new RestaurentRequestModel()
                {
                    IsVerified = false,
                    RegistrationDateTime = DateTime.Now,
                    RestaurentID=resturantID
                    
                };

                _context.RestaurentRequest.Add(restaurentRequest);
                await _context.SaveChangesAsync();

                if (result.Succeeded)
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(applicationUser);
                    var callbackUrl = Url.Action(("ConfirmEmail"), "User", new { userId = applicationUser.Id, code = code }, Request.Scheme);

                    EmailSender emailSender = new EmailSender();
                    emailSender.sendVerificationEmail(model.OwnerEmail, callbackUrl);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        /// <summary>
        /// Can be access by Unauthorized users. This feature is used to verify email address
        /// </summary>
        [HttpGet]
        public async Task<Object> ConfirmEmailAsync(string userId,string code)
        {
            if(userId==null || code == null)
            {
                return BadRequest(new { message = "Somting is missing" });
            }

            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.ConfirmEmailAsync(user, code);

            if (result.Succeeded)
            {
                return Ok(new { Message = "Verification Succeeded" });
            }
            else
            {
                return Ok(new { Message = "Verification Failed" });
            }
        }

    }
}
