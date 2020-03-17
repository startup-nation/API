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
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private UserManager<ApplicationUserModel> _userManager;
        private readonly APIDbContext _context;

        public AdminController(UserManager<ApplicationUserModel> userManager, APIDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }



        [HttpGet]
        [Route("Admin/GetAllRestaurentRequest")]
        
        public async Task<ActionResult<IEnumerable<RestaurentRequestModel>>> GetAllRestaurentRequest()
        {
            return await _context.RestaurentRequest.ToListAsync();
            
        }


        [HttpPost]
        [Route("Admin/UpdateResaurentRequest")]
        public async Task<Object> UpdatePofile(UpdateRestaurentRequestModel updateRestaurentRequest)
        {
            UpdateResponse responseModel = new UpdateResponse();

            RestaurentRequestModel restautentRequest = await _context.RestaurentRequest.FindAsync(updateRestaurentRequest.ID);

            if (restautentRequest == null)
            {
                return NotFound();
            }



            restautentRequest.IsVerified = updateRestaurentRequest.IsVerified;
            restautentRequest.Comment = updateRestaurentRequest.Comment;
 
            _context.Entry(restautentRequest).State = EntityState.Modified;

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

            responseModel.UpdatedObject = restautentRequest;



            return responseModel;
        }



    }
}
