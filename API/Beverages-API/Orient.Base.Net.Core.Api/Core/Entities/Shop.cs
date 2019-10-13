using Orient.Base.Net.Core.Api.Core.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Orient.Base.Net.Core.Api.Core.Entities
{
    [Table("Shop")]
    public class Shop : BaseEntity
    {
        public Shop() : base()
        {
        }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        public ShopStatusEnums.Shop ShopStatus { get; set; }

        [StringLength(255)]
        public string Avatar { get; set; }

        public List<CategoryInShop> CategoryInShops { get; set; }

    }
}
