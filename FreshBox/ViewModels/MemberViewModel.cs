using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreshBox.Models;
using FreshBox.Repository;
using System.Collections.ObjectModel;
using System.ComponentModel;
#endregion

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FreshBox.Services;


#region #3. CommunityToolkit.Mvvm 라이브러리
/*
 * - Microsoft가 관리하는 공식 MVVM 보조 라이브러리.
 * - MVVM 구조를 간결하고 안전하게 작성할 수 있도록 도와주는 도구!
 * 
 * [핵심 기능]
 * - ObservalbeProperty : 자동 속성 구현
 * - RelayCommand : 커맨드 자동 생성
 * - ObservalbeObject : ViewModel 베이스 클래스
 * 
 * [설정 방법]
 * 1. ViewModel 클래스에 필요한 using 선언.
 * using CommunityToolkit.Mvvm.ComponentModel;
 * using CommunityToolkit.Mvvm.Input;
 * 
 * 2. ViewModel 클래스가 ObservableObject를 상속
 * public partial class -- 일단 패스
 * 
 * 3. 속성 자동 구현: [ObservalbeProperty]
 * ㄴ View <-> ViewModel 간 데이터 자동 연동
 * 
 * 4. 명령 자동 생성: [RelayCommand]
 * ㄴ 버튼 클릭 등 UI 이벤트를 ViewModel 메서드로 연결
 */
#endregion

namespace FreshBox.ViewModels
{
    // MVVM Toolkit에서 제공하는 ObservableObject를 상속받음
    public partial class MemberViewModel : ObservableObject
    { // ㄴ ObservableObject 상속받는 이유 : 속성과 UI 사이의 데이터 바인딩이 자동으로 동작하게 만들기 위해서.
      // ㄴ 꼭, partial class 이여야 함.
        /* ObservableObject 클래스
          MVVM Toolkit에서 제공하는 클래스
          INotifyPropertyChanged를 자동으로 구현해줌
          즉, 속성이 바뀌면 화면에 자동 반영이 가능해짐
         */

        public ObservableCollection<Member> member { get; } = new();
        // get; => 읽기 전용 속성
        // 이 컬렉션을 생성자에서 따로 초기화하지 않고, 바로 선언과 동시에 인스턴스 생성.
        /*
         * ObservableCollection
         * ㄴ UI와 연결될 수 있는 컬렉션(List)
         * ㄴ 변화가 있을 시 UI에 자동 반영되도록 설계된 클래스
         * ㄴ 일반 List<T>는 데이터가 바뀌어도 UI가 모름.
         */

        private readonly MemberService memberSvc = new MemberService();

        // MVVM Toolkit 방식: 자동으로 PropertyChanged 발생시켜주는 속성
        // [ObservableProperty]를 쓰면 이 필드와 연결된 속성이 자동 생성됨
        // // 1. Username이라는 값을 바인딩해서 사용할 수 있게 만들어준다
        [ObservableProperty]
        private string username; // 이 필드를 바탕으로 자동으로 속성 만들어줘라는 뜻
        // ㄴ 뷰의 textBoxusername과 바인딩됨
        // 사용자가 입력할 때 마다 자동으로 바인딩된 속성(ViewModel의 Username이 바뀜) 값이 변경됨
        /*
         [ObservableProperty]
         자동으로 username이라는 속성을 생성해주고, 자동으로 화면과 연결해 줄 수 있다.
         */

        [ObservableProperty]
        private string duplicateCheckResult; 

        // 흐름 요약
        /*
            1. 사용자가 textBoxusername에 값을 입력
            2. 사용자가 커서를 다른 곳을 클릭함 -> LostFocus 이벤트 발생
            3. LostFocus 이벤트가  ViewModel의 CheckDuplicateCommand를 호출함
            4. ViewModel이 현재 Username 값을 기준으로 중복체크
            5. 중복 여부에 따라 결과를 DuplicateCheckResult에 표시
         */
        // 2. LostFocus 시 실행할 명령(textBoxusername이 포커스를 잃었을때 실행할 명령
        [RelayCommand]
        private void checkDulicateUsername() {
            string resultText = memberSvc.CheckUsernameDuplicate(username);
            duplicateCheckResult = resultText;

        }
        /* [RelayCommand]
            버튼 클릭이나 이벤트 발생할 때 실행되는 **명령(동작)**을 만들어주는 역할
            .xaml 파일에서 Command="{Binding checkDulicateUsernameCommand}" 이렇게 사용할 수 있음
            뒤에 Command가 붙은 이름은 자동 생성
         */

        /* 
         * WPF 기본 TextBox는 LostFocus에 Command를 직접 바인딩 못한다
         * Microsoft.Xaml.Behaviors.Wpf 라는 NuGet 패키지를 설치하고,
         * Interaction.Behaviors를 써서 우회적으로 바인딩 
         *
         * 네임스페이스에 
         * xmlns:i="http://schemas.microsoft.com/xaml/behaviors"를 포함시켜야 한다
         * Interaction.Behaviors : 이벤트(예: LostFocus)를 ViewModel의 명령(Command)과 연결해주는 중간 장치
         */

                
    }
}
