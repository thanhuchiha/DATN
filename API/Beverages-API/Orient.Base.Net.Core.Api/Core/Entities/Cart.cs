using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Orient.Base.Net.Core.Api.Core.Entities
{
    [Table("Cart")]
    public class Cart : BaseEntity
    {
        public Cart() : base()
        {

        }

        public int Quantity_Product { get; set; }

        [StringLength(255)]
        public string Total { get; set; }

        public List<Product> Products { get; set; }
    }
}
