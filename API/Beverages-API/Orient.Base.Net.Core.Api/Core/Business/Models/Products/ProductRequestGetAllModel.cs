﻿using Orient.Base.Net.Core.Api.Core.Business.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orient.Base.Net.Core.Api.Core.Business.Models.Products
{
    public class ProductRequestGetAllModel : RequestListViewModel
    {
        public ProductRequestGetAllModel()
        {

        }

        public string Query { get; set; }

        public bool? IsActive { get; set; }
    }
}
