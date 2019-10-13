using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Orient.Base.Net.Core.Api.Core.Entities
{
    [Table("Comment")]
    public class Comment : BaseEntity
    {
        public Comment() : base()
        {

        }

        [StringLength(255)]
        public string Content { get; set; }

        public User User { get; set; }

        public Product Product { get; set; }
    }
}
