using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshBox.Models
{
    public class MyOrder
    {
        public int Id { get; set; }
        public DateTime Order_date { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        // Join한 Product 이름 표시용
        //public string? ProductName { get; set; }
    }
}
