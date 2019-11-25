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

        [StringLength(512)]
        [Required]
        public string Name { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public string FeeShip { get; set; }

        public ShopStatusEnums.Status Status { get; set; }

        public string AvatarUrl { get; set; }

        public List<UserInShop> UserInShops { get; set; }

        public List<ProductInShop> ProductInShops { get; set; }

        public List<Comment> Comments { get; set; }
    }
}
