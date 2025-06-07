using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FreshBox.Enums;
using FreshBox.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FreshBox.ViewModels
{
    /// <summary>
    /// WorkMenuView와 바인딩 된 뷰모델 클래스
    /// </summary>
    public partial class WorkMenuViewModel : ObservableObject
    {
        // 싱글턴으로 관리
        private static WorkMenuViewModel? instance;
        public static WorkMenuViewModel Instance
        {
            get
            {
                if (instance == null) {
                    instance = new WorkMenuViewModel();
                }  
                return instance;
            }
        }

        // 생성자는 private으로 외부 생성 제한
        private WorkMenuViewModel()
        {
            // 초기화 로직
        }

        // 권한 갱신 후 UI 갱신 메서드
        public void RefreshRoleProperties()
        {
            OnPropertyChanged(nameof(IsCommonButtonVisible));
            OnPropertyChanged(nameof(IsAdminVisible));
        }


        // 프로퍼티 선언 ---------------------------

        /// <summary>
        /// 로그인한 사용자의 권한(읽기 전용 속성)
        /// 이 값을 읽으려고 하면 내부에서 LoginSession.GetInstance().LoggedInRole 값을 반환함
        /// LoginSession 싱글턴 객체의 LoggedInRole 값을 그대로 제공해주는 역할
        /// </summary>
        public Role LoggedInRole
        {
            get
            {
                return LoginSession.GetInstance().LoggedInRole;
            }
        }

        // 사원 및 관리자 공통 버튼 표시
        public Visibility IsCommonButtonVisible => (LoggedInRole == Role.Employee || LoggedInRole == Role.Admin) ? Visibility.Visible : Visibility.Collapsed;
        
        // 관리자 전용 버튼 표시
        public Visibility IsAdminVisible => LoggedInRole == Role.Admin ? Visibility.Visible : Visibility.Collapsed;

        /*
         * WPF에서 UI 요소의 Visibility 속성
         * Collapsed: 요소가 화면에 안 보이고, 자리도 차지하지 않음
            → UI에서 완전히 사라진 상태
         * Hidden: 요소가 안 보이지만, 공간은 차지함
         * Visible: 요소가 보임
         */


        // 화면 전환 메서드 선언 -----------------------------------

        /// <summary>
        /// 뒤로가기 버튼 클릭 시 호출됨
        /// 이전에 보았던 화면으로 돌아감
        /// </summary>
        [RelayCommand]
        private void GoToBack() {
            ViewNavigationService.Instance.GoBack();
        }

        /// <summary>
        /// 출퇴근찍기 버튼 클릭시 호출됨
        /// CheckInOutView로 화면 전환
        /// </summary>
        [RelayCommand]
        private void GoToCheckInOutView() {
            ViewNavigationService.Instance.NavigateTo("CheckInOut");
            // 주의) 메인윈도우 뒷코드에 등록이 먼저 되어있어야 함
        }

        /// <summary>
        /// 내 출퇴근 기록 확인 버튼 클릭시 호출됨
        /// MyWorkLogView로 화면 전환
        /// </summary>
        [RelayCommand]
        private void GoToMyWorkLogView()
        {
            ViewNavigationService.Instance.NavigateTo("MyWorkLog");
            // 주의) 메인윈도우 뒷코드에 등록이 먼저 되어있어야 함
        }

        /// <summary>
        /// 관리자용 : 회사 고유 휴일 등록 버튼 클릭시 호출됨
        /// CompanyHolidayView로 화면 전환
        /// </summary>
        [RelayCommand]
        private void GoToCompanyHolidayView()
        {
            ViewNavigationService.Instance.NavigateTo("CompanyHoliday");
            // 주의) 메인윈도우 뒷코드에 등록이 먼저 되어있어야 함
        }


        /// <summary>
        /// 관리자용 : 출퇴근 기준 시간 설정 버튼 클릭시 호출됨
        /// WorkTimeSettignsView로 화면 전환
        /// </summary>
        [RelayCommand]
        private void GoToWorkTimeSettignsView()
        {
            ViewNavigationService.Instance.NavigateTo("WorkTimeSettigns");
            // 주의) 메인윈도우 뒷코드에 등록이 먼저 되어있어야 함
        }

    }
}
