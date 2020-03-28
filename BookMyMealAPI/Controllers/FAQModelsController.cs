using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookMyMealAPI.Data;
using BookMyMealAPI.Model.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using BookMyMealAPI.Model.Request;
using BookMyMealAPI.Model.Response;

namespace BookMyMealAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    [Produces("application/json")]
    public class FAQModelsController : ControllerBase
    {
        private readonly APIDbContext _context;

        public FAQModelsController(APIDbContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Can be access by Unauthorized User
        /// </summary>
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FAQModel>>> GetfAQModels()
        {
            return await _context.fAQModels.ToListAsync();
        }


        /// <summary>
        /// Can be access by Unauthorized User
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<FAQModel>> GetFAQModel(int id)
        {
            var fAQModel = await _context.fAQModels.FindAsync(id);

            if (fAQModel == null)
            {
                return NotFound();
            }

            return fAQModel;
        }

        /// <summary>
        /// Can be access by Admin
        /// </summary>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin")]     
        [HttpPut("{id}")]
        public async Task<Object> PutFAQModel(int id, FAQModel fAQModel)
        {
            UpdateResponse responseModel = new UpdateResponse()
            {
                IsSuccess = false,
                UpdatedObject=null
            };
           

            if (id != fAQModel.ID)
            {
                return BadRequest();
            }

             _context.Entry(fAQModel).State = EntityState.Modified;


            try
            {
                await _context.SaveChangesAsync();

                responseModel.IsSuccess = true;
                responseModel.UpdatedObject = fAQModel;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FAQModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return responseModel;
        }

        /// <summary>
        /// Can be access by Admin
        /// </summary>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<FAQModel>> PostFAQModel(NewFAQRequestModel fAQRequestModel)
        {
            FAQModel fAQModel = new FAQModel()
            {
                Question=fAQRequestModel.Question,
                Solution=fAQRequestModel.Solution
            };

            _context.fAQModels.Add(fAQModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFAQModel", new { id = fAQModel.ID }, fAQModel);
        }

        /// <summary>
        /// Can be access by Admin
        /// </summary>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<DeleteResponseModel>> DeleteFAQModel(int id)
        {
            DeleteResponseModel deleteResponese = new DeleteResponseModel()
            {
                IsSuccess = false
            };
            var fAQModel = await _context.fAQModels.FindAsync(id);
            if (fAQModel == null)
            {
                return NotFound();
            }

            _context.fAQModels.Remove(fAQModel);
            await _context.SaveChangesAsync();
            deleteResponese.IsSuccess = true;
            

            return deleteResponese ;
        }

        private bool FAQModelExists(int id)
        {
            return _context.fAQModels.Any(e => e.ID == id);
        }
    }
}
