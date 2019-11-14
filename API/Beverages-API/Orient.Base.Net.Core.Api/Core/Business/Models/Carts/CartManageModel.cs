using Orient.Base.Net.Core.Api.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Orient.Base.Net.Core.Api.Core.Business.Models.Carts
{
    public class CartManageModel
    {
        [Required]
        public int Quantity_Product { get; set; }

        [Required]
        public string Total { get; set; }

        public void SetDataToModel(Cart cart)
        {
            cart.Quantity_Product = Quantity_Product;
            cart.Total = Total;
        }
    }
}
