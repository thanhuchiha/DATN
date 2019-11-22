using Orient.Base.Net.Core.Api.Core.Business.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orient.Base.Net.Core.Api.Core.Business.Models.Shops
{
    public class ShopRequestGetAllModel : RequestListViewModel
    {
        public ShopRequestGetAllModel() : base()
        {

        }

        public string Query { get; set; }

        public bool? IsActive { get; set; }
    }
}
