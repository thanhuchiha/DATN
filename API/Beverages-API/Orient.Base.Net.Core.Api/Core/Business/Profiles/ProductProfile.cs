using AutoMapper;
using Orient.Base.Net.Core.Api.Core.Business.Models.Products;
using Orient.Base.Net.Core.Api.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orient.Base.Net.Core.Api.Core.Business.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductManageModel>().ReverseMap();
        }
    }
}
