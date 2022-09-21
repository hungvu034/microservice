using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Entities
{
    public class Cart
    {
        public Cart()
        {
        }

        public Cart(string userName)
        {
            UserName = userName;
        }

        public decimal TotalPrice  =>
            Items.Sum(item => item.ItemPrice * item.Quantity);

        public string UserName { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>() ; 

        
    }
}