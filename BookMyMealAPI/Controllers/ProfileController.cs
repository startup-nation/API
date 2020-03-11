using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookMyMealAPI.Data;
using BookMyMealAPI.Model.Entity;
using BookMyMealAPI.Model.Request;
using BookMyMealAPI.Model.Response;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookMyMealAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProfileController : ControllerBase
    {
        private UserManager<ApplicationUserModel> _userManager;
        private readonly APIDbContext _context;

        public ProfileController(UserManager<ApplicationUserModel> userManager, APIDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles ="Customer")]
        public async Task<Object> GetUserProfile()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            var profile = await _context.Profile.FindAsync(user.Email);

            if (profile == null)
            {
                return NotFound();
            }

            ProfileResponseModel response = new ProfileResponseModel()
            {
                Email = profile.Email,
                Name = profile.Name,
                PhoneNumber = profile.PhoneNumber
            };

            return response;
        }


        [HttpPost]
        [Route("Update")]
        [Authorize(Roles = "Customer")]
        public async Task<Object> UpdatePofile(ProfileUpdateRequestModel updateRequestModel)
        {
            ProfileUpdateResponseModel responseModel = new ProfileUpdateResponseModel();
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            ProfileModel profile = await _context.Profile.FindAsync(user.Email);
            if (profile == null)
            {
                return NotFound();
            }

            profile.Name = updateRequestModel.Name;
            profile.PhoneNumber = updateRequestModel.PhoneNumber;
            _context.Entry(profile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                responseModel.IsSuccess = true;
            }
            catch (Exception)
            {
                responseModel.IsSuccess = false;
                return responseModel;
            }

            responseModel.Profile = profile;

            

            return responseModel;
        }






    }
}
