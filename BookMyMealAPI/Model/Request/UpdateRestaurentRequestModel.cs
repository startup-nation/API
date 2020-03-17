using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookMyMealAPI.Model.Request
{
    public class UpdateRestaurentRequestModel
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public Boolean IsVerified { get; set; }

        public string Comment { get; set; }
    }
}
