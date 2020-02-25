using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookMyMealAPI.Model.Entity;
using BookMyMealAPI.Model.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookMyMealAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {

        private UserManager<ApplicationUser> _userManager;

        public ApplicationUserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [Route("Registration")]
        [HttpPost]
        public async Task<object> UserRegistration(RegistrationModel registrationModel)
        {
            var applicationUser = new ApplicationUser()
            {
                UserName = registrationModel.Email,
                Email = registrationModel.Email
            };

            try
            {
                var result = await _userManager.CreateAsync(applicationUser, registrationModel.Password);
                return Ok(result);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        

    }
}
