using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshBox.Models
{
    public class Category
    {
        public int ID { get; set; }
        public string CategoryName { get; set; }

        public Category(int id, string categoryName)
        {
            ID = id;
            CategoryName = categoryName;
        }
    }
}
