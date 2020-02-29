using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookMyMealAPI.Model.Entity
{
    public class ProfileModel
    {
     
        [Key]
        [Required]
        public string Email { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }
    }
}
