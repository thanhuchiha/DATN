using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Orient.Base.Net.Core.Api.Core.Entities
{
    [Table("Item")]
    public class Item : BaseEntity
    {
        public Item() : base()
        {

        }

        public int Quantity { get; set; }

        public Product Product { get; set; }
    }
}
