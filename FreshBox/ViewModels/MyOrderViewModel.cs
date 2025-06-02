using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FreshBox.Models;
using System.Collections.ObjectModel;
using FreshBox.Repository;
using System.Windows;

namespace FreshBox.ViewModels
{
    public partial class MyOrderViewModel : ObservableObject
    {
        //[ObservableProperty]
        private readonly MyOrderRepository _repository;

        // 사용자가 View(xaml)쪽에서 입력한 값을 바인딩하는 속성
        [ObservableProperty]
        public ObservableCollection<MyOrder> myOrders = new();

        [ObservableProperty]
        private MyOrder? selectedOrder = new();
        // 이때 ?(Question Mark)를 사용하는 이유? 초기에는 아무 것도 선택되지 않았을 수 있기 때문에, Nullable 타입으로 선언한 거야!

        [ObservableProperty]
        private DateTime inputOrderDate = DateTime.Now; // 주문 날짜는 현재 날짜로 초기화

        [ObservableProperty]
        private int? inputProductId; // 상품 ID 받아서 상품 이름을 찾는 용도

        [ObservableProperty]
        private string? inputProductName; // 상품 이름 받아서 상품 ID를 찾는 용도

        [ObservableProperty]
        private int inputQuantity;

        
        public MyOrderViewModel()
        {
            _repository = new MyOrderRepository();
            LoadMyOrders();
        }



        [RelayCommand]
        public void LoadMyOrders() // 이때 ProductID를 가지고 Product_name을 가져와서 출력해주자!
        {
            MessageBox.Show("Hello");
            MyOrders = [.. _repository.GetAllOrders()];
            // 여기서 [.. ] 문법은 컬렉션 표현식이며, spread 연산자라고 한다.
            // _repository.GetAllOrders()에서 얻은 컬렉션을 새로운 List로 복사하여 대입하는 효과
        }

        [RelayCommand]
        public void AddMyOrder()
        {
            if(string.IsNullOrWhiteSpace(InputProductName) || InputQuantity <= 0)
            {
                // 입력값이 유효하지 않으면 경고 메시지 표시
                Console.WriteLine("상품 이름과 수량을 올바르게 입력해주세요.");
                return;
            }

            MyOrder newOrder = new MyOrder
            {
                Order_date = DateTime.Now,
                ProductId = 0, // 실제 ProductId는 선택된 제품에 따라 설정해야 함!!
                Quantity = InputQuantity
            };

            _repository.AddMyOrder(newOrder);
            LoadMyOrders(); // 새로 추가한 주문을 반영하기 위해 목록을 다시 불러옴
            InputProductName = string.Empty; // 주문 추가 후 입력 필드 초기화
            InputQuantity = 0; // 주문 추가 후 입력 필드 초기화
        }
    }
}
