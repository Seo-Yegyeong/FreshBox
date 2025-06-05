using FreshBox.Models;
using FreshBox.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FreshBox.Services
{
    public class CategoryService
    {
        CategoryRepository CategoryRepo = new CategoryRepository();
        public ObservableCollection<Category> LoadCategoriesService()
        {
            MessageBox.Show("LoadCategoriesService() called");
            return new ObservableCollection<Category>(CategoryRepo.GetAllCategories());
        }
    }
}
