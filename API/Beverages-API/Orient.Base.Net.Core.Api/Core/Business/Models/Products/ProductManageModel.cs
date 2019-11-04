using Orient.Base.Net.Core.Api.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orient.Base.Net.Core.Api.Core.Business.Models.Products
{
    public class ProductManageModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Price { get; set; }

        public string FeeShip { get; set; }

        public void SetDataToModel(Product product)
        {
            product.Name = Name;
            product.Description = Description;
            product.Price = Price;
            product.FeeShip = FeeShip;
        }
    }
}
