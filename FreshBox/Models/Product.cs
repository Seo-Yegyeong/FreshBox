using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using FreshBox.Enums;

namespace FreshBox.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public int Stock { get; set; }
        public StorageTemp StorageTemp { get; set; }
        public int WarehouseId { get; set; }
    }
}
