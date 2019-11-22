using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Orient.Base.Net.Core.Api.Core.Entities
{
    [Table("ProductInShop")]
    public class ProductInShop : BaseEntity
    {
        public ProductInShop() : base()
        {

        }

        public Guid ProductId { get; set; }

        public Product Product { get; set; }

        public Guid ShopId { get; set; }

        public Shop Shop { get; set; }
    }
}
