using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreshBox.Models;
using FreshBox.Repository;
using System.Collections.ObjectModel;
using System.ComponentModel;


using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FreshBox.Services;
using System.Text.RegularExpressions;
using System.Diagnostics;


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

        // 서비스 객체 생성
        private readonly MemberService memberSvc = new MemberService();

        // MVVM Toolkit 방식: 자동으로 PropertyChanged 발생시켜주는 속성
        // [ObservableProperty]를 쓰면 이 필드와 연결된 속성이 자동 생성됨
        // // Username이라는 값을 바인딩해서 사용할 수 있게 만들어준다
        [ObservableProperty]
        private string username = string.Empty; // 이 필드를 바탕으로 자동으로 속성 만들어줘라는 뜻
        // ㄴ 뷰의 textBoxusername의 text속성과 바인딩됨
        // 사용자가 입력할 때 마다 자동으로 바인딩된 속성(ViewModel의 Username이 바뀜) 값이 변경됨
        /*
         [ObservableProperty]
         자동으로 username이라는 속성을 생성해주고, 자동으로 화면과 연결해 줄 수 있다.
         */

        [ObservableProperty]
        private string duplicateCheckResult = "";
        // ㄴ textBlockDuplicateCheckResult의 text속성과 바인딩 되어있음(중복 확인 결과 출력)

        // 흐름 요약
        /*
            1. 사용자가 textBoxusername에 값을 입력
            2. 사용자가 값을 변경할 때마다 중복 체크 함수 자동 호출
            3. 중복 여부에 따라 결과를 DuplicateCheckResult에 표시
         */


        //[ObservableProperty] 속성을 쓰면, 빌드 시 Source Generator가 자동으로 코드를 생성
        // 값이 바뀔 때마다 이 함수를 자동으로 호출 시켜줌
        partial void OnUsernameChanged(string value)
        {
            // 12자 넘으면 잘라내기
            if (value.Length > 12) 
            {
                Username = value.Substring(0, 12); // 재할당되며 OnUsernameChanged 재호출됨
                return; // 뒤에 로직 실행 안 하도록 바로 종료
            }

            // 아이디 유효성 검사
            // @를 붙이는 이유 : \(백슬레시) 이스케이프 문자 이런것 때문에 사용함
            // 이스케이프를 무시하고 문자 그대로 인식하라고 컴파일러에게 알려주는 역할
            
            string pattern = @"^[A-Za-z0-9]{6,12}$";
            // ㄴ 정규표현식 : 6~12자의 영문 대소문자와 숫자사용

            if (string.IsNullOrWhiteSpace(Username)) // username textBox가 비어있으면 실행
            {
                DuplicateCheckResult = "아이디를 입력해주세요.";
            }
            else if (Regex.IsMatch(Username, pattern)) {
                CheckUsernameDuplicate(); // DB에 같은 username 있는지 중복 검사 메서드 호출
            }
            else
            {
                DuplicateCheckResult = "❌ 6~12자의 영문 대소문자와 숫자만 사용할 수 있습니다.";
            }


        }

        /// <summary>
        /// 입력한 Username의 중복 여부를 검사하는 메서드
        /// </summary>
        private void CheckUsernameDuplicate() {
            try {

                bool isDuplicate = memberSvc.IsUsernameDuplicate(Username);

                if (isDuplicate) // 중복 된 아이디 있음
                {
                    //UI에 중복 메시지 처리
                    DuplicateCheckResult = "사용할 수 없는 아이디입니다. 다른 아이디를 입력해 주세요.";
                }
                else // 중복된 아이디 없음
                {
                    //UI에 사용 가능 메시지 처리
                    DuplicateCheckResult = "사용하실 수 있는 ID입니다.";
                }
            } 
            
            catch (Exception ex) { // 에러
                Debug.WriteLine($"Service error: {ex.Message}");
                // UI에 네트워크, DB 연결 문제 등 예외 상황에 대한 사용자 안내 메시지
                DuplicateCheckResult = "Error : 서버 오류가 발생했습니다. 잠시 후 다시 시도해 주세요.";
            }
            
           

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
