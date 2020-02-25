using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMyMealAPI.Model.Request
{
    public class RegistrationModel
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
