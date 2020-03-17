using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookMyMealAPI.Model.Request
{
    public class RestaurantRegistrationModel
    {
        [Required]
        public string OwnerName { get; set; }

        [Required]
        public string OwnerEmail { get; set; }

        [Required]
        public string OwnerPhoneNumber { get; set; }

        [Required]
        public string RestaurantName { get; set; }

        [Required]
        public string RestaurantEmail { get; set; }

        [Required]
        public string RestaurantPhone { get; set; }

        [Required]
        public string ResturantAddress { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
