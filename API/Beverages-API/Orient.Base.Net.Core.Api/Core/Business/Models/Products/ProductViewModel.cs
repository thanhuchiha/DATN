using Orient.Base.Net.Core.Api.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orient.Base.Net.Core.Api.Core.Business.Models.Products
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {

        }

        public ProductViewModel(Product product) : this()
        {
            if (product != null)
            {
                Id = product.Id;
                Name = product.Name;
                Description = product.Description;
                Price = product.Price;
                FeeShip = product.FeeShip;
            }
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Price { get; set; }

        public string FeeShip { get; set; }
    }
}
