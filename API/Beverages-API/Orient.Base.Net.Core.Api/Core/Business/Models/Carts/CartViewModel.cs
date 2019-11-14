using Orient.Base.Net.Core.Api.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orient.Base.Net.Core.Api.Core.Business.Models.Carts
{
    public class CartViewModel
    {
        public CartViewModel()
        {

        }

        public CartViewModel(Cart cart) : this()
        {
            if (cart != null)
            {
                Id = cart.Id;
                Quantity_Product = cart.Quantity_Product;
                Total = cart.Total;
            }
        }

        public Guid Id { get; set; }

        public int Quantity_Product { get; set; }

        public string Total { get; set; }
    }
}
