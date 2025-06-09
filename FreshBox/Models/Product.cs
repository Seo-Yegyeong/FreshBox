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

        public Product(string productName, int categoryId, string barcode, int stock, StorageTemp storageTemp)
        {
            ProductName = productName;
            CategoryId = categoryId;
            Barcode = barcode;
            Stock = stock;
            StorageTemp = storageTemp;
        }

        // 기본 생성자도 함께 명시적으로 추가 (선택사항이지만 권장)
        public Product() { }
    }
}
