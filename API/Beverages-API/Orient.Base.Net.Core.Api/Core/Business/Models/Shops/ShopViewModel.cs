using Orient.Base.Net.Core.Api.Core.Entities;
using Orient.Base.Net.Core.Api.Core.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orient.Base.Net.Core.Api.Core.Business.Models.Shops
{
    public class ShopViewModel
    {
        public ShopViewModel()
        {

        }

        public ShopViewModel(Shop Shop) : this()
        {
            Id = Shop.Id;
            Name = Shop.Name;
            Address = Shop.Address;
            Description = Shop.Description;
            FeeShip = Shop.FeeShip;
            Status = Shop.Status;
            AvatarUrl = Shop.AvatarUrl;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public string FeeShip { get; set; }

        public ShopStatusEnums.Status Status { get; set; }

        public string AvatarUrl { get; set; }
    }
}
