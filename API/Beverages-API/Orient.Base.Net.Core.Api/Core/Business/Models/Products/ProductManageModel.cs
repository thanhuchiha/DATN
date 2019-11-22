using Orient.Base.Net.Core.Api.Core.Business.IoC;
using Orient.Base.Net.Core.Api.Core.DataAccess.Repositories.Base;
using Orient.Base.Net.Core.Api.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Orient.Base.Net.Core.Api.Core.Business.Models.Products
{
    public class ProductManageModel : IValidatableObject
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Price { get; set; }

        public string Image { get; set; }

        public Guid[] CategoryIds { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var categoryRepository = IoCHelper.GetInstance<IRepository<Category>>();

            if (CategoryIds == null || CategoryIds.Length == 0)
            {
                yield return new ValidationResult("Category not be null", new string[] { "CategoryIds" });
            }

            if (!CategoryIds.All(x => categoryRepository.GetAll().Select(y => y.Id).Contains(x)))
            {
                yield return new ValidationResult("Invalid Category", new string[] { "CategoryIds" });
            }
        }

        public void SetDataToModel(Product product)
        {
            product.Name = Name;
            product.Description = Description;
            product.Price = Price;
            product.Image = Image;
        }
    }
}
