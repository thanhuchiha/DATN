using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Orient.Base.Net.Core.Api.Core.Entities
{
    [Table("Image")]
    public class Image : BaseEntity
    {
        public Image() : base()
        {

        }

        [StringLength(255)]
        public string Url { get; set; }
    }
}
