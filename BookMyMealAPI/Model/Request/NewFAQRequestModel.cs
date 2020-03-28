using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookMyMealAPI.Model.Request
{
    public class NewFAQRequestModel
    {
        [Required]
        public string Question { get; set; }

        [Required]
        public string Solution { get; set; }
    }
}
