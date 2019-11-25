using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Orient.Base.Net.Core.Api.Core.Entities
{
    [Table("UserInShop")]
    public class UserInShop : BaseEntity
    {
        public UserInShop() : base()
        {

        }

        public Guid UserId { get; set; }

        public User User { get; set; }

        public Guid ShopId { get; set; }

        public Shop Shop { get; set; }
    }
}
