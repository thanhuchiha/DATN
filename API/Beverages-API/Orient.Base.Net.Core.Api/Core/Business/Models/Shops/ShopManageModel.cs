using Orient.Base.Net.Core.Api.Core.Entities;
using Orient.Base.Net.Core.Api.Core.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orient.Base.Net.Core.Api.Core.Business.Models.Shops
{
    public class ShopManageModel
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public string FeeShip { get; set; }

        public ShopStatusEnums.Status Status { get; set; }

        public string AvatarUrl { get; set; }

        public void SetDataToModel(Shop shop)
        {
            shop.Name = Name;
            shop.Address = Address;
            shop.Description = Description;
            shop.FeeShip = FeeShip;
            shop.Status = Status;
            shop.AvatarUrl = AvatarUrl;
        }
    }
}
