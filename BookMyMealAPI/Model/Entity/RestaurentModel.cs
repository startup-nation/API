using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookMyMealAPI.Model.Entity
{
    public class RestaurentModel
    {
        [Key]
        [Required]
        public int ID { get; set; }

        [Required]
        public string RestaurantEmail { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }

        public string ImageUrl { get; set; }


        public string OwnerEmail { get; set; }
        public ApplicationUserModel applicationUser { get; set; }

        public RestaurentRequestModel restaurentRequest { get; set; }
    }
}
