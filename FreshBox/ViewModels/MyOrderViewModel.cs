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
        public ObservableCollection<MyOrder> MyOrders { get; set; } = new();
        [ObservableProperty]
        private MyOrder? newOrder; // 새로 추가할 주문을 위한 속성
        [ObservableProperty]
        private MyOrder? selectedOrder;
        // 이때 ?(Question Mark)를 사용하는 이유? 초기에는 아무 것도 선택되지 않았을 수 있기 때문에, Nullable 타입으로 선언한 거야!
        [ObservableProperty]
        private DateTime inputOrderDate = DateTime.Now; // 주문 날짜는 현재 날짜로 초기화
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
        private void LoadMyOrders() // 이때 ProductID를 가지고 Product_name을 가져와서 출력해주자!
        {
            MyOrders = [.. _repository.GetAllOrders()];
            // 여기서 [.. ] 문법은 컬렉션 표현식이며, spread 연산자라고 한다.
            // _repository.GetAllOrders()에서 얻은 컬렉션을 새로운 List로 복사하여 대입하는 효과
        }
        [RelayCommand]
        private void AddMyOrder()
        {
            MessageBox.Show("AddMyOrder() called");
            if (SelectedOrder == null)
            {
                MessageBox.Show("주문을 선택해주세요.");
                return;
            }
            MessageBox.Show($"{SelectedOrder.Id}, {SelectedOrder.Order_date}, {SelectedOrder.ProductId}, {SelectedOrder.Quantity}");
            if (string.IsNullOrWhiteSpace(InputProductName) || InputQuantity <= 0)
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
            _repository.InsertMyOrder(newOrder);
            LoadMyOrders(); // 새로 추가한 주문을 반영하기 위해 목록을 다시 불러옴
            InputProductName = string.Empty; // 주문 추가 후 입력 필드 초기화
            InputQuantity = 0; // 주문 추가 후 입력 필드 초기화
        }
        [RelayCommand]
        private void InboundMyOrder()
        {
            if (SelectedOrder == null)
            {
                MessageBox.Show("어떤 주문에 대한 입고인지 항목을 선택해주세요.");
                return;
            }
            //사용자가 입력한 입고 건의 ID를 찾기
            int inputProductId = _repository.GetProductIdByName(InputProductName!);
            //선택한 입고 주문과 입고 건을 비교
            // # 일치할 경우
            if (SelectedOrder.ProductId == inputProductId && SelectedOrder.Quantity == InputQuantity)
            {
                MessageBox.Show("입고 정보가 일치합니다! 입고 처리를 진행합니다.");
                // #1. Inbound_log에 기록 추가하기
                // #2. my_order 테이블에서 해당 주문 삭제하기
                // #3. 유통기한 생성해서 Expiration_log에 기록 추가하기

                /*
                 다음을 고려하기
                    입고 레퍼지토리에서 insert메서드 만들때
                    유통기한 테이블도 같이 하나의 트랜젝션으로 묶어서 같이 되게 하셔야해요!
                    입고 기록테이블이랑 유통기한테이블 하나의 트랜잭션으로 묶어서 처리!
                    둘 다 성공해야 commit되게요
                    하나라도 실패하면 롤백처리
                 */

                MessageBox.Show("입고 처리가 완료되었습니다.");
                LoadMyOrders();
            }
            // # 상품 불일치
            else if (SelectedOrder.ProductId != inputProductId)
            {
                var ans = MessageBox.Show("상품이 다릅니다! 그대로 입고 처리하시려면 Yes를, 반품 처리하시려면 No를 클릭하세요.", "상품 불일치", MessageBoxButton.YesNo);
                if (ans == MessageBoxResult.Yes)
                {
                    MessageBox.Show("입고 처리를 진행합니다.");
                    // #1. Inbound_log에 기록 추가하기
                    // #2. Product에 stock 수량 추가하기
                }
                else
                {
                    // Nothing to do here
                    MessageBox.Show("반품 처리가 완료되었습니다.");
                }
                return;
            }
            // # 수량 불일치
            else if (SelectedOrder.Quantity != InputQuantity)
            {
                var ans = MessageBox.Show($"수량이 일치하지 않습니다! \r\n주문한 수량: {SelectedOrder.Quantity}\r\n입고된 수량: {InputQuantity}\r\n그대로 입고 처리하시려면 Yes를, 반품 처리하시려면 No를 클릭하세요.", "수량 불일치 알림", MessageBoxButton.YesNo);
                if (ans == MessageBoxResult.Yes)
                {
                    MessageBox.Show("입고 처리를 진행합니다.");
                    // #1. Inbound_log에 기록 추가하기
                    // #2. Product에 stock 수량 추가하기
                }
                else
                {
                    // Nothing to do here
                    MessageBox.Show("반품 처리가 완료되었습니다.");
                }
                return;
            }
            //string message = $"주문 ID: {SelectedOrder.Id}\n" +
            //                 $"주문 날짜: {SelectedOrder.Order_date}\n" +
            //                 $"상품 ID: {SelectedOrder.ProductId}\n" +
            //                 $"수량: {SelectedOrder.Quantity}\n" +
            //                 "입고 처리를 진행합니다.";
        }
    }
}