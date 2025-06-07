using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FreshBox.ViewModels;
using FreshBox.Models;
using FreshBox.Services;
using System.Security.Cryptography.X509Certificates;
using FreshBox.Enums;


namespace FreshBox.ViewModels
{
    public partial class ProductViewModel : ObservableObject
    {
        private readonly ProductService productService = new ProductService();
        public ObservableCollection<Product> Products { get; set; } = [];

        // ==================================================================== //
        // 카테고리와 창고를 선택할 수 있는 ViewModel을 포함
        public CategoryViewModel CategorySubVM { get; } = new CategoryViewModel();
        public WarehouseViewModel WarehouseSubVM { get; } = new WarehouseViewModel();
        

        [ObservableProperty]
        private Product? selectedProduct;

        [ObservableProperty]
        private Product? newProduct; // 새로 추가할 상품을 위한 속성

        [ObservableProperty]
        private string productName = string.Empty; // 상품명 입력을 위한 속성

        [ObservableProperty]
        private int productStock; // 수량 입력을 위한 속성

        [ObservableProperty]
        private string? productBarcode; // 바코드 입력을 위한 속성

        [ObservableProperty]
        private StorageTemp storageTemp; // 저장 온도 선택을 위한 속성 (냉장, 냉동, 상온 중 하나를 선택)

        // ==================================================================== //


        private bool isProductNameValid; // 상품명 유효성 검사 결과

        private bool isBarcodeValid; // 바코드 유효성 검사 결과

        private bool isStorageTempValid; // 저장 온도 유효성 검사 결과

        private bool isCategoryValid; // 카테고리 유효성 검사 결과

        private bool isWarehouseValid; // 창고 유효성 검사 결과

        // ==================================================================== //


        public ProductViewModel()
        {
            
        // 상품 목록을 초기화
            LoadProducts();
        }


        // Product에 대해 처리해야 하는 작업? & 고려해야 하는 요소?
        // #1. 상품 전체 데이터 반환
        [RelayCommand]
        private void LoadProducts()
        {
            Products = productService.LoadProductsService();
        }

        // #2. 상품 추가
        // - 유효성 검사하기 (예: 필수 입력값 확인, 중복 체크 등)
        // ProductName (textBox)
        // - 이미 존재하는 상품명인지 확인 => (Service에 입력값 전달 -> DB에 조회 요청 -> 중복 여부 0과 1로 판단)
        // - 상품명은 필수 입력값 => 단순히 ViewModel에서 null 체크만 하면 됨
        public bool CheckNameDuplication(string productName)
        {
            // Service에 입력값 전달 -> DB에 조회 요청 -> 중복 여부 0과 1로 판단
            bool isDuplicated = productService.IsProductNameDuplicated(productName);
            if( isDuplicated )
            {
                isProductNameValid = false; // 중복된 상품명은 유효하지 않음
            }
            else
            {
                isProductNameValid = true; // 중복되지 않은 상품명은 유효함
            }
            return isProductNameValid;
        }

        partial void OnProductNameChanged(string value)
        {
            if (string.IsNullOrEmpty(productName))
                return;
            CheckNameDuplication(value);
        }

        // CategoryID (ComboBox)
        // - 선택된 카테고리가 있는지 확인 => 단순히 ViewModel에서 null 체크만 하면 됨


        // Barcode (textBox - 수기로 입력받기)
        // - 바코드는 필수 입력값 => 단순히 ViewModel에서 null 체크만 하면 됨
        // - 바코드 형식이 올바른지 확인 => (ViewModel에서 정규식으로 형식 검사 -> 올바른 형식인지 0과 1로 판단)
        // - 바코드 중복 여부 확인 => (Service에 입력값 전달 -> DB에 조회 요청 -> 중복 여부 0과 1로 판단)


        // StorageTemp (radioButton)
        // - 냉장, 냉동, 상온 중 하나를 선택해야 함 => 단순히 ViewModel에서 null 체크만 하면 됨


        // 일단 보류
        // WarehouseId (ComboBox)
        // - 선택된 창고가 있는지 확인 => 단순히 ViewModel에서 null 체크만 하면 됨
        // - 냉장, 냉동, 상온과 관련된 창고인지 판별해주어야 함 => 


        [RelayCommand]
        private void GoBack()
        {
            // 뒤로가기 버튼 클릭 시 이전 화면으로 이동
            ViewNavigationService.Instance.GoBack();
        }

        [RelayCommand]
        private void AddProduct()
        {
            //if (newProduct != null)
            //{
            //    productService.AddProductService(newProduct);
            //    LoadProducts(); // 새로 추가한 상품을 반영하기 위해 다시 로드
            //    NewProduct = new Product(); // 새 상품 입력 필드를 초기화
            //}
        }

        // #3. 상품 수정

        // #4. 상품 삭제

        public void AddNewProduct()
        {
            //if (newProduct != null)
            //{
            //    _repository.InsertProduct(newProduct);
            //    LoadProducts(); // 새로 추가한 상품을 반영하기 위해 다시 로드
            //    NewProduct = new Product(); // 새 상품 입력 필드를 초기화
            //}
        }
    }
}