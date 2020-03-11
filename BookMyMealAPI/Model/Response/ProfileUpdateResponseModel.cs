using BookMyMealAPI.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMyMealAPI.Model.Response
{
    public class ProfileUpdateResponseModel
    {
        public bool IsSuccess { get; set; }

        public ProfileModel Profile { get; set; }
    }
}
