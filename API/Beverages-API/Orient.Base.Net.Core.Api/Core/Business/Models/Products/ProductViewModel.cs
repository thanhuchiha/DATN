using Orient.Base.Net.Core.Api.Core.Business.Models.Categories;
using Orient.Base.Net.Core.Api.Core.Entities;
using System;
using System.Linq;

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
                Image = product.Image;
                Categories = product.ProductInCategories != null ? product.ProductInCategories.Select(y => new CategoryViewModel(y.Category)).ToArray() : null;
            }
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Price { get; set; }

        public string Image { get; set; }

        public CategoryViewModel[] Categories { get; set; }
    }
}
