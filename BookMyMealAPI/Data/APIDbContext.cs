using BookMyMealAPI.Model.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMyMealAPI.Data
{
    public class APIDbContext : IdentityDbContext
    {
        public APIDbContext(DbContextOptions<APIDbContext> options) : base(options)
        {

        }

        public DbSet<ApplicationUserModel> applicationUsers { get; set; }
        public DbSet<ProfileModel> Profile { get; set; }

        public DbSet<RestaurentModel> Restaurent { get; set; }

        public DbSet<RestaurentRequestModel> RestaurentRequest { get; set; }

        public DbSet<FAQModel>  fAQModels { get; set; }

    }
}
