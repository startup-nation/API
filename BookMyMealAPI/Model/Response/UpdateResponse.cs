﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMyMealAPI.Model.Response
{
    public class UpdateResponse
    {
        public bool IsSuccess { get; set; }

        public Object UpdatedObject { get; set; }
    }
}
