using Orient.Base.Net.Core.Api.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Orient.Base.Net.Core.Api.Core.Business.Models.Categories
{

    public class CategoryManageModel
    {
        [Required]
        public string Name { get; set; }

        public void SetDateToModel(Category category)
        {
            category.Name = Name;
        }
    }
}
