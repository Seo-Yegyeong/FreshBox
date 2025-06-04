using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FreshBox.Models;
using FreshBox.Repository;


namespace FreshBox.ViewModels
{
    public partial class ProductViewModel : ObservableObject
    {
        private readonly ProductRepository _repository;
        public ObservableCollection<Product> Products { get; set; } = new();

        [ObservableProperty]
        private Product? selectedProduct;

        [ObservableProperty]
        private Product? newProduct; // 새로 추가할 상품을 위한 속성


        [RelayCommand]
        private void LoadProducts()
        {
            Products = new ObservableCollection<Product>(_repository.GetAllProducts());
        }

        private void AddNewProduct()
        {
            if (newProduct != null)
            {
                _repository.InsertProduct(newProduct);
                LoadProducts(); // 새로 추가한 상품을 반영하기 위해 다시 로드
                NewProduct = new Product(); // 새 상품 입력 필드를 초기화
            }
        }
    }
}