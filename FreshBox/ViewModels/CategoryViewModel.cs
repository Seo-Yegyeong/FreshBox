using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FreshBox.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FreshBox.ViewModels
{
    public class CategoryViewModel : ViewModelBase
    {
        private Category _category = new();

        public Category Category
        {
            get => _category;
            set
            {
                _category = value;
                OnPropertyChanged();
            }
        }

        public string CategoryName
        {
            get => _category.CategoryName;
            set
            {
                if (_category.CategoryName != value)
                {
                    _category.CategoryName = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
