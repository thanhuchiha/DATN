using Orient.Base.Net.Core.Api.Core.Business.Models.Users;
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

        public ShopViewModel(Shop shop) : this()
        {
            if(shop != null)
            {
                Id = shop.Id;
                Name = shop.Name;
                Address = shop.Address;
                Description = shop.Description;
                FeeShip = shop.FeeShip;
                Status = shop.Status;
                AvatarUrl = shop.AvatarUrl;
                Users = shop.UserInShops != null ? shop.UserInShops.Select(y => new UserViewModel(y.User)).ToArray() : null;
            }
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public string FeeShip { get; set; }

        public ShopStatusEnums.Status Status { get; set; }

        public string AvatarUrl { get; set; }

        public UserViewModel[] Users { get; set; }
    }
}
