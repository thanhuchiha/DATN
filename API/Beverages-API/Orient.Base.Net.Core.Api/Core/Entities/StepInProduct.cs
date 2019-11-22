using Orient.Base.Net.Core.Api.Core.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Orient.Base.Net.Core.Api.Core.Entities
{
    [Table("StepInProduct")]
    public class StepInProduct
    {
        public StepInProduct() : base()
        {
            
        }

        [StringLength(512)]
        [Required]
        public string Name { get; set; }

        public StatusEnums.Status Status { get; set; }

        public Guid ProductId { get; set; }

        public Product Product { get; set; }
    }
}
