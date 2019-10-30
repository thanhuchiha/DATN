using Orient.Base.Net.Core.Api.Core.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Orient.Base.Net.Core.Api.Core.Entities
{
    [Table("Product")]
    public class Product : BaseEntity
    {
        public Product() : base()
        {

        }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [StringLength(255)]
        public string Price { get; set; }

        [StringLength(255)]
        public string FeeShip { get; set; }

        public ProductStatusEnums.Product ProductStatus { get; set; }

        public List<Image> Images { get; set; }

        public List<Comment> Comments { get; set; }

        public List<ProductInCategory> ProductInCategories { get; set; }
    }
}
