using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshBox.Models
{
    public class WarehouseTempLog
    {
        public int Id { get; set; }
        public int WarehouseId { get; set; }
        public decimal Temperature { get; set; }
        public DateTime MeasuredAt { get; set; }
    }
}
