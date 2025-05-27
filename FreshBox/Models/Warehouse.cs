using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshBox.Models
{
    public enum TempControl
    {
        냉동,
        냉장,
        실온
    }

    public class Warehouse
    {
        public int Id { get; set; }
        public string WarehouseName { get; set; } = string.Empty;
        public string? Location { get; set; }
        public TempControl TempControl { get; set; }
        public int? Capacity { get; set; }
    }
}
