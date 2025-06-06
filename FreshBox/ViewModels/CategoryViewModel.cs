using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FreshBox.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FreshBox.Services;
using System.Windows;

namespace FreshBox.ViewModels
{
    public partial class CategoryViewModel : ObservableObject
    {
        private readonly CategoryService productService;

        [ObservableProperty]
        private ObservableCollection<Category> categories = [];

        [ObservableProperty]
        private string? selectedCategoryId; // 선택된 카테고리

        [ObservableProperty]
        private int categoryId; // 카테고리 ID

        [ObservableProperty]
        private string categoryName = string.Empty; // 카테고리 이름

        public CategoryViewModel()
        {
            productService = new CategoryService();
            LoadCategories();
        }

        [RelayCommand]
        private void LoadCategories()
        {
            categories = productService.LoadCategoriesService();
        }
    }
}
