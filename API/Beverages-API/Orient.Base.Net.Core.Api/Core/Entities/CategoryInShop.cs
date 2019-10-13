using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Orient.Base.Net.Core.Api.Core.Entities
{
    [Table("CategoryInShop")]
    public class CategoryInShop : BaseEntity
    {
        public CategoryInShop() : base()
        {

        }

        public Guid CategoryId { get; set; }
        
        public Category Category { get; set; }

        public Guid ShopId { get; set; }

        public Shop Shop { get; set; }
    }
}
