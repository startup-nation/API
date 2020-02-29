using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookMyMealAPI.Model.Entity;
using BookMyMealAPI.Model.Request;
using BookMyMealAPI.Model.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookMyMealAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private UserManager<ApplicationUserModel> _userManager;

        public UserController(UserManager<ApplicationUserModel> userManager)
        {
            _userManager = userManager;
        }


        [HttpPost]
        [Route("Customer/Registration")]
        //POST : /api/ApplicationUser/Register
        public async Task<Object> Cu(CustomerRegistrationModel model)
        {
            var Role = "Customer";
            var applicationUser = new ApplicationUserModel()
            {
                UserName = model.Email,
                Email = model.Email
            };

            try
            {
                var result = await _userManager.CreateAsync(applicationUser, model.Password);
                await _userManager.AddToRoleAsync(applicationUser, Role);
                return Ok(result);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



    }
}
