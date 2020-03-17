using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookMyMealAPI.Model.Entity
{
    public class RestaurentRequestModel
    {
        [Key]
        [Required]
        public int ID { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime RegistrationDateTime { get; set; }


        public string Comment { get; set; }

        [Required]
        public Boolean IsVerified { get; set; }
        public int RestaurentID { get; set; }
        public RestaurentModel restaurent { get; set; }

    }
}
