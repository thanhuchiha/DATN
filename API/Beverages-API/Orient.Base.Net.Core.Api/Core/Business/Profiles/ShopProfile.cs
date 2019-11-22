using AutoMapper;
using Orient.Base.Net.Core.Api.Core.Business.Models.Shops;
using Orient.Base.Net.Core.Api.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orient.Base.Net.Core.Api.Core.Business.Profiles
{
    public class ShopProfile : Profile
    {

        public ShopProfile()
        {
            CreateMap<Shop, ShopManageModel>().ReverseMap();
        }
    }
}
