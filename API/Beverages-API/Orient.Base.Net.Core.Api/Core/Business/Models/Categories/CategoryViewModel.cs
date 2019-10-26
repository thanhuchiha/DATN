using Orient.Base.Net.Core.Api.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orient.Base.Net.Core.Api.Core.Business.Models.Categories
{
    public class CategoryViewModel
    {
        public CategoryViewModel()
        {

        }

        public CategoryViewModel(Category category) : this()
        {
            if (category != null)
            {
                Id = category.Id;
                Name = category.Name;
            }
        }

        public Guid Id { get; set; }

        public string Name { get; set; }
    }

}
