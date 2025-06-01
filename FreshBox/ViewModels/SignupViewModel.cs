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
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;


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
    public partial class SignUpViewModel : ObservableObject
    { // ㄴ ObservableObject 상속받는 이유 : 속성과 UI 사이의 데이터 바인딩이 자동으로 동작하게 만들기 위해서.
      // ㄴ 꼭, partial class 이여야 함.
        /* ObservableObject 클래스
          MVVM Toolkit에서 제공하는 클래스
          INotifyPropertyChanged를 자동으로 구현해줌
          즉, 속성이 바뀌면 화면에 자동 반영이 가능해짐
         */

        // public ObservableCollection<Member> Members { get; } = new();
        // get; => 읽기 전용 속성만 있음
        // 이 컬렉션을 생성자에서 따로 초기화하지 않고, 바로 선언과 동시에 인스턴스 생성.
        // Member 객체들을 담는 리스트
        // = new();	초기화. 비어있는 컬렉션으로 시작
        /*
         * ObservableCollection
         * ㄴ UI와 연결될 수 있는 컬렉션(List)
         * ㄴ 변화가 있을 시 UI에 자동 반영되도록 설계된 클래스
         * ㄴ 일반 List<T>는 데이터가 바뀌어도 UI가 모름.
         */

        // 서비스 객체 생성
        private readonly SignUpService signUpSvc = new SignUpService();



        //입력 필드 바인딩 (자동 PropertyChanged)-------------------------------

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
        // string.Empty;로 초기화 시켜 놓는 이유 : 예외 방지, XAML 바인딩에서 Null이 안 예쁨 등
        // "" 빈문자열과 같음(명시적으로 빈 문자열임을 나타내는 표준 상수)

        [ObservableProperty]
        private string pwd = string.Empty; // 비밀번호

        [ObservableProperty]
        private string confirmPwd = string.Empty; // 비밀번호 확인

        [ObservableProperty]
        private string memberName = string.Empty; // 사용자 이름

        [ObservableProperty]
        private DateTime birthDate;

        [ObservableProperty]
        private string birthDateString = string.Empty;  // 생년월일문자열

        [ObservableProperty]
        private string phoneNumber = string.Empty; // 연락처

        [ObservableProperty]
        private string email = string.Empty; // 이메일

        [ObservableProperty]
        private string hireDateString = string.Empty;  // "yyyy-MM-dd" 형식으로 받는다고 가정


        //유효성 결과 메세지를 담는 속성(UI에 표시용)-------------------------------
        [ObservableProperty]
        private string usernameValidationMessage = string.Empty; // username 검증 결과
        // ㄴ DuplicateCheckResultTextBlock의 text속성과 바인딩 되어있음 (유효성,중복 검사 결과 출력용)

        [ObservableProperty]
        private string pwdValidationMessage = string.Empty;

        [ObservableProperty]
        private string pwdConfirmValidationMessage = string.Empty;

        [ObservableProperty]
        private string memberNameValidationMessage = string.Empty;

        [ObservableProperty]
        private string birthDateValidationMessage = string.Empty;

        [ObservableProperty]
        private string phoneNumValidationMessage = string.Empty;

        [ObservableProperty]
        private string emailValidationMessage = string.Empty;

        [ObservableProperty]
        private string hireDateValidationMessage = string.Empty;

        // 유효 여부 (true -> 유효) --------------------------------
        // 사용자 ID 유효 여부
        [ObservableProperty]
        private bool isUsernameValid;

        // 비밀번호 유효 여부
        [ObservableProperty]
        private bool isPwdValid;

        // 비밀번호 확인 일치 여부
        [ObservableProperty]
        private bool isConfirmPwdValid;

        // 사용자 이름 유효 여부
        [ObservableProperty]
        private bool isMemberNameValid;

        // 생년월일 유효 여부
        [ObservableProperty]
        private bool isBirthDateValid;

        // 연락처 유효 여부
        [ObservableProperty]
        private bool isPhoneNumberValid;

        // 이메일 유효 여부
        [ObservableProperty]
        private bool isEmailValid;

        // 입사일 유효 여부
        [ObservableProperty]
        private bool isHireDateValid;

        // 모든 조건이 true여야 회원가입 가능
        // 각 필드는 개별 유효성 검사 결과 (true = 통과, false = 실패)
        // 모든 항목이 통과(true)해야만 CanRegister가 true가 되어 회원가입 가능 상태가 됨
        public bool CanRegister =>
            IsUsernameValid &&         // 아이디 형식이 올바른가? & 중복이 없는가? & 필수 입력
            IsPwdValid &&              // 비밀번호 형식이 올바른가? & 필수 입력
            IsConfirmPwdValid &&       // 비밀번호 확인이 일치하는가? & 필수 입력
            IsMemberNameValid &&       // 이름이 올바르게 입력되었는가? & 필수 입력
            IsBirthDateValid &&        // 생년월일 형식이 올바른가? & 필수 입력
            IsPhoneNumberValid &&      // 연락처 형식이 올바른가? & 중복이 없는가? & 필수 입력
            IsEmailValid &&            // 이메일 형식이 올바른가? & 중복이 없는가? & 필수 입력
            IsHireDateValid;           // 입사일 형식이 올바른가? (선택이라 null도 괜찮음)


        //생성자 ---------------------------------------------
        public SignUpViewModel()
        {
            // 기본 생성자 - 디자이너가 필요로 함
        }


        // 메서드 정의 ---------------------------------------

        // 유효성 검사는 || 단축 평가로 인해 각 조건을 if문으로 분리해서 각각 개별 검사 로직으로 진행됨
        // 이렇게 해야 각각의 조건이 실패했을 때 적절한 처리가 가능하고,
        // 어떤 조건에서 실패했는지 명확히 알 수 있다.

        // 사용자 아이디(username) 유효성 & 중복 검사 흐름 요약
        /*
            1. 사용자가 UsernameTextBox에 값을 변경할 때마다 메서드 자동 호출
            2. 입력제한 검사(12자) & 유효성 검사 함 
            3. 통과 시 DB에 같은 username이 있는지 검사하는 중복 체크 함수 호출
            3. 중복 여부에 따라 결과를 DuplicateCheckResultTextBlock의 
                text속성과 바인딩 된 duplicateCheckResult에 표시
         */
        //[ObservableProperty] 속성을 쓰면, 빌드 시 Source Generator가 자동으로 코드를 생성
        // 값이 바뀔 때마다 이 함수를 자동으로 호출 시켜줌
        // username이 바뀔때마다 자동으로 호출되는 메서드
        partial void OnUsernameChanged(string value)
        {

            // 12자 넘으면 잘라내기
            if (value.Length > 12)
            {
                IsUsernameValid = false;
                Username = value.Substring(0, 12).Trim(); // 재할당되며 OnUsernameChanged 재호출됨
                return; // 뒤에 로직 실행 안 하도록 바로 종료
            }

            // 아이디 유효성 검사
            // @를 붙이는 이유 : \(백슬레시) 이스케이프 문자 이런것 때문에 사용함
            // 이스케이프를 무시하고 문자 그대로 인식하라고 컴파일러에게 알려주는 역할

            string pattern = @"^[A-Za-z0-9]{6,12}$";
            // ㄴ 정규표현식 : 6~12자의 영문 대소문자와 숫자사용
            //^가 붙으면 문자열 맨 처음부터 딱 맞아야 한다는 의미
            // ^ 없으면, 문자열 중간에 영어 10~20자가 있으면 매칭될 수 있음
            // ^와 $를 함께 쓰면 "전체 문자열이 이 패턴이어야 한다"는 뜻

            // 빈 문자열 또는 공백만 있는지 검사
            if (string.IsNullOrWhiteSpace(value))
            {
                IsUsernameValid = false;
                UsernameValidationMessage = "아이디를 입력해주세요.";
                return; // 뒤에 로직 실행 안 하도록 바로 종료
            }

            // 공백이 포함되었는지 검사
            if (value.Contains(' '))
            {
                IsUsernameValid = false;
                UsernameValidationMessage = "아이디에는 공백이 포함될 수 없습니다.";
                return;
            }

            if (!Regex.IsMatch(value, pattern))
            {
                IsUsernameValid = false;
                UsernameValidationMessage = "❌ 6~12자의 영문 대소문자와 숫자만 사용할 수 있습니다.";
                return; // 뒤에 로직 실행 안 하도록 바로 종료
            }

            // 유효성 통과한 경우 → 중복 체크 시도
            CheckUsernameDuplicate();


        }

        /// <summary>
        /// 입력한 Username의 중복 여부를 검사하는 메서드
        /// </summary>
        private void CheckUsernameDuplicate()
        {
            try
            {

                bool isDuplicate = signUpSvc.IsUsernameDuplicate(Username);

                if (isDuplicate) // 중복 된 아이디 있음
                {
                    IsUsernameValid = false; // username이 유효합니까? 아니요
                    //UI에 중복 메시지 처리
                    UsernameValidationMessage = "❌ 사용할 수 없는 아이디입니다. 다른 아이디를 입력해 주세요.";
                }
                else // 중복된 아이디 없음
                {
                    IsUsernameValid = true; // username이 유효함
                    //UI에 사용 가능 메시지 처리
                    UsernameValidationMessage = "✅ 사용하실 수 있는 ID입니다.";
                }
            }

            catch (Exception ex)
            { // 에러
                Debug.WriteLine($"Service error: {ex.Message}");
                IsUsernameValid = false; // 에러 시, DB 확인 불가하므로)) username이 유효하지 않음으로 처리
                // UI에 네트워크, DB 연결 문제 등 예외 상황에 대한 사용자 안내 메시지
                UsernameValidationMessage = "⚠️ Error : 서버 오류가 발생했습니다. 잠시 후 다시 시도해 주세요.";
            }
        }

        //Pwd 속성이 변경될 때마다 자동 호출되는 함수
        partial void OnPwdChanged(string value)
        {
            // value가 20자를 초과하면, 앞 20자만 남긴 뒤 Pwd에 재할당
            // → 이때 다시 OnPwdChanged가 호출되지만 20자이므로 무한루프되지 않는다.
            if (value.Length > 20)
            { // 비밀번호 입력이 20자를 초과하면 실행
                IsPwdValid = false; // 유효성 검사 결과 : 유효하지 않음
                Pwd = value.Substring(0, 20).Trim();
                // 원본 문자열로 부터 0번째 인덱스부터 시작해서 20글자를 복사해, 앞 뒤 공백 제거후
                // 새 문자열을 만들어서 pwd에 저장
                return; // 뒤에 로직 실행 안 하도록 바로 종료
            }

            // 글자 수 20 이하일 때 실행됨, 유효성 검사 실행
            //비밀번호는 영문, 숫자, 특수문자 중 2가지 이상 조합하여 10~20자로 설정
            ValidatePassword(value);

            // 비밀번호 확인과 일치하는지 검사
            ValidatePasswordMatch();
        }

        /// <summary>
        /// 비밀번호 유효성 검사 메서드
        /// - 길이: 10 ~ 20자
        /// - 영문, 숫자, 특수문자 중 최소 2가지 조합
        /// </summary>
        /// <param name="value">검사할 비밀번호 문자열</param>
        private void ValidatePassword(string value)
        {

            // 빈문자열 또는 공백으로만 이루어졌는지 검사
            if (string.IsNullOrWhiteSpace(value))
            {
                IsPwdValid = false;
                PwdValidationMessage = "비밀번호를 입력해주세요.";
                return; // 뒤에 로직 실행 안 하도록 바로 종료
            }

            // 공백이 포함 되는지 검사
            if (value.Contains(' '))
            {
                IsPwdValid = false;
                PwdValidationMessage = "비밀번호에 공백이 포함될 수 없습니다.";
                return; 
            }

            //길이 조건 검사(10 ~ 20자)
            if (value.Length < 10 || value.Length > 20)
            {
                IsPwdValid = false;
                PwdValidationMessage = "비밀번호는 10자 이상 20자 이하로 입력해주세요.";
                return; // 해당 메서드 종료
            }

            // 조합 조건 검사(영문, 숫자, 특수문자 중 2가지 이상 포함)
            int typeCount = 0;

            if (Regex.IsMatch(value, "[A-Za-z]"))
                typeCount++;       // 영문
            if (Regex.IsMatch(value, "[0-9]"))
                typeCount++;           // 숫자
            if (Regex.IsMatch(value, "[^A-Za-z0-9]"))
                typeCount++;    // 특수문자

            if (typeCount < 2)
            {
                IsPwdValid = false;
                PwdValidationMessage = "영문, 숫자, 특수문자 중 최소 2가지 조합이 필요합니다.";
                return; // 뒤에 로직 실행 안 하도록 바로 종료
            }

            // 모든 조건을 만족할 경우 실행 됨
            PwdValidationMessage = "✅ 사용 가능한 비밀번호입니다.";
            IsPwdValid = true;

        }

        /// <summary>
        /// ConfirmPwd 속성 값이 변경될 때마다 자동으로 호출되는 partial 메서드
        /// </summary>
        /// <param name="value">사용자가 비밀번호 확인 입력란에 입력한 새 값</param>
        partial void OnConfirmPwdChanged(string value)
        {
            
            // 빈문자열 또는 공백으로만 이루어졌는지 검사
            if (string.IsNullOrWhiteSpace(value))
            {
                IsConfirmPwdValid = false;
                PwdConfirmValidationMessage = "비밀번호를 다시 한번 입력해주세요.";
                return;
            }

            // 공백이 포함 되었는지 검사
            if (value.Contains(' ')) {
                IsConfirmPwdValid = false;
                PwdConfirmValidationMessage = "공백은 포함될 수 없습니다.";
                return;
            }

            // 비밀번호 일치 여부 검사
            ValidatePasswordMatch();
        }

        /// <summary>
        /// 비밀번호와 비밀번호 확인이 일치하는지 검사하는 메서드
        /// </summary>
        private void ValidatePasswordMatch()
        {
            if (Pwd != ConfirmPwd)
            {
                IsConfirmPwdValid = false;
                PwdConfirmValidationMessage = "비밀번호가 일치하지 않습니다.";
            }
            else
            {
                PwdConfirmValidationMessage = "✅ 비밀번호가 일치합니다.";
                IsConfirmPwdValid = true;
            }
        }

        /// <summary>
        /// MemberName 속성 값이 변경될 때마다 자동으로 호출되는 partial 메서드
        /// </summary>
        /// <param name="value">변경된 MemberName의 새 값</param>
        partial void OnMemberNameChanged(string value)
        {
            if (value.Length > 10) // 입력값이 10자 이상이면 실행
            {
                IsMemberNameValid = false; // 유효하지 않음
                MemberName = value.Substring(0, 10).Trim(); // 원본 문자열의 인덱스0~10까지의 새 문자열을 만들어 MemberName에 저장
                return; // 뒤의 로직 실행 되지 않도록 종료
            }

            ValidateMemberName(value);

        }

        // 이름 유효성 검사
        private void ValidateMemberName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            { // value가 빈문자열 또는 공백으로만 이루어졌을 경우 실행
                IsMemberNameValid = false; // 유효하지 않음
                MemberNameValidationMessage = "이름을 입력해주세요.";
                return;
            }

            //공백을 포함하는지 검사
            if (value.Contains(' ')) {
                IsMemberNameValid= false;
                MemberNameValidationMessage = "이름에 공백을 포함할 수 없습니다.";
                return;
            }

            string pattern = @"^[가-힣A-Za-z]{1,10}$";
            // 한글(가-힣) 또는 영문 대/소문자,최소 1글자, 최대 10글자
            // 전체가 이 문자들로만 구성되어야 함(^,$사용)

            //Regular Expression(정규 표현식), 유효성 검사
            if (!Regex.IsMatch(value,pattern)) {
                IsMemberNameValid = false;
                MemberNameValidationMessage = "이름은 한글, 영문자만 가능합니다(최대 10자까지)";
                return; // 뒤의 로직 실행되지 않도록 종료함
            }

            MemberNameValidationMessage = "";
            IsMemberNameValid = true;

        }
        #region  Regex(정규 표현식) 주요 메서드
        /*
            Regex.IsMatch(string input, string pattern)
             ㄴ 입력 문자열이 패턴에 맞는지 참/거짓으로 반환
             ㄴ 용도: 입력값 유효성 검사할 때 가장 많이 씀

            Regex.Matches(string input, string pattern)
             ㄴ 입력 문자열에서 패턴과 일치하는 모든 부분 문자열을 컬렉션으로 반환
             ㄴ 용도: 여러 개 매칭되는 부분 전부 다 뽑아내고 싶을 때
             ㄴ 예) 문서에서 모든 이메일 주소 추출, 모든 숫자 찾기

            Regex.Replace(string input, string pattern, string replacement)
             ㄴ 패턴과 일치하는 부분 문자열을 대체하여 새로운 문자열 반환
             ㄴ 용도: 민감 정보 마스킹, 문자열 포맷 변환, 불필요 문자 제거할 때
             ㄴ 예) 전화번호 형식 통일, 주민번호 일부 * 처리

            Regex.Split(string input, string pattern)
             ㄴ 패턴을 기준으로 입력 문자열을 분할하여 문자열 배열 반환
             ㄴ 용도: 복잡한 구분자(콤마+세미콜론 등)로 문자열 분할할 때
             ㄴ 예) CSV 등 파일 파싱, 로그 라인 분리
            
            Regex.Count(string input, string pattern)
             ㄴ .NET 7부터 도입, 정규 표현식에 매칭되는 부분의 개수를 반환
             ㄴ 용도: 텍스트에서 특정 단어가 몇 번 등장하는지 세고 싶을 때 사용
         */
        #endregion

        /// <summary>
        /// 뷰에 바인딩 된 BirthDateString값이 변할 때마다 자동으로 호출되는 메서드
        /// </summary>
        /// <param name="value">사용자가 입력한 생년월일</param>
        partial void OnBirthDateStringChanged(string value)
        {
            // 입력 제한 8자
            if (value.Length > 8) {  // 8자 이상 입력 시 실행
                IsBirthDateValid = false;
                BirthDateString = value.Substring(0,8);
                // 원본 문자열 인덱스 0부터 8까지의 새 문자열을 만들어 BirthDateString저장
                // BirthDateString저장 시 값 변경되므로 재귀 호출되지만,
                // 8자로 된 값이 저장되므로 해당 if문의 내부는 실행되지 않음(무한루프 되지 않음)
                return;
            }

            ValidateBirthDateString(value); // 유효성 검사 메서드 호출
        }

        // 생년월일 유효성 검사 
        private void ValidateBirthDateString(string value) {
            
            // 빈문자열 또는 공백으로만 이루어졌는지 검사(공백 포함된것은 못잡음)
            if (string.IsNullOrWhiteSpace(value)) { 
                IsBirthDateValid= false; // 유효하지 않음
                BirthDateValidationMessage = "생년월일을 입력해주세요. (예시) 19980801";
                //사용자 UI에 안내 메세지 띄움
                return;
            }

            // 공백을 포함하는지 검사
            if (value.Contains(' ')) {
                IsBirthDateValid = false;
                BirthDateValidationMessage = "공백을 포함할 수 없습니다.";
                return;
            }

            // 형식 검사, 문자열 전체가 숫자 8자리인지 검사
            // ^ : 문자열 시작, $ : 문자열 끝, \d : 숫자 0~9, {8} : 8자리
            if (!Regex.IsMatch(value, @"^\d{8}$")) {
                IsBirthDateValid = false;
                BirthDateValidationMessage = "8자리의 년월일 형식으로 입력해주세요. (예 19980801)";
                return;
            }

            // 날짜 유효성 검사(yyyyMMdd 형식에 맞는지, 실제 존재하는 날짜인지 검사)
            // 시간 분(mm)과 구분하기 위해 날짜의 월 부분은 대문자 MM으로 표시
            // TryParseExact은 입력 문자열이 "yyyyMMdd" 형식에 맞는지 시도
            // 성공하면 birthDate에 DateTime 객체로 변환해 저장
            // 실패하면 false 반환 (실패 시 birthDate는 기본값 저장 0001-01-01 00:00:00)
            // 이상한 값이 birthDate에 저장되지만, 어차피 이 경우 return으로 빠져나가므로 문제가 되진 않음
            // 제대로 입력한 경우 ture반환하고 birthDate에 변환된 날짜가 저장되고
            // if (!true) → if (false)가 되므로 해당 if문은 실행 되지 않음
            if (!DateTime.TryParseExact(value, "yyyyMMdd", null,
                System.Globalization.DateTimeStyles.None, out DateTime birthDate)) {

                IsBirthDateValid = false; // 유효하지 않음
                BirthDateValidationMessage = "유효하지 않은 생년월일입니다.";
                return; // 유효하지 않으면 메시지 출력 후 종료
            }

            // 현재 날짜 기준으로 미래 날짜인지 검사( || 사용시 단축평가 되므로 따로 분리함)
            if (birthDate > DateTime.Today) {
                IsBirthDateValid = false; // 유효하지 않음
                BirthDateValidationMessage = "유효하지 않은 생년월일입니다.(미래날짜불가)";
                return; // 유효하지 않으면 메시지 출력 후 종료
            }

            BirthDateValidationMessage = "";
            BirthDate = birthDate; // 유효성 처리 끝낸 입력문자열을 생년월일 필드에 저장시킴 
            IsBirthDateValid = true; // 유효처리
        }

        /// <summary>
        /// 뷰에 바인딩 된 PhoneNumber 값 변경 시 자동으로 호출되는 메서드
        /// </summary>
        /// <param name="value">사용자 입력값</param>
        partial void OnPhoneNumberChanged(string value)
        {
            // 입력 제한(최대 11자)
            if (value.Length > 11) { 
                IsPhoneNumberValid = false;
                PhoneNumber = value.Substring(0, 11).Trim();
                // 원본 문자열의 인덱스 0부터 11까지 새문자열로 만들고 앞뒤 공백을 제거하여
                // PhoneNumber에 저장 // PhoneNumber 값 변경되어 재귀호출 되지만
                // 11자이기 때문에 해당 if문의 내부는 실행되지 않음
                return;
            }

            // 유효성 검사 메서드 호출
            ValidatePhoneNum(value);

        }

        // 휴대폰 번호 유효성 검사
        private void ValidatePhoneNum(string value) {
            // 빈문자열 또는 공백으로만 이루어졌는지 검사
            if (string.IsNullOrWhiteSpace(value)) {
                IsPhoneNumberValid = false;
                PhoneNumValidationMessage = "연락처를 입력해주세요.";
                return;
            }

            // 공백이나 하이픈이 포함되어 있으면 유효하지 않다고 판단
            if (value.Contains(' ') || value.Contains('-')) {
                IsPhoneNumberValid = false;
                PhoneNumValidationMessage = 
                    "휴대폰 번호는 숫자만 입력해주세요. 하이픈(-), 공백은 사용할 수 없습니다.";
                return; // 밑의 로직이 실행되지 않도록 해당 메서드를 종료함
            }

            // 숫자만 추출 (하이픈, 공백 등 제거)
            string digitsOnly = Regex.Replace(value, @"[^\d]", "");
            // ㄴ value 문자열에서 숫자(0-9)가 아닌 모든 문자를 제거
            // ㄴ 숫자가 아닌 문자를 모두 빈 문자열 ""로 바꿔버림
            //  @"[^\d]" 정규식은 숫자가 아닌(^가 부정) 
            // "010-1234-5678" → "01012345678" 이렇게 숫자만 추출

            // 한국 휴대폰 번호 길이 제한 (10자리 또는 11자리)
            if (digitsOnly.Length != 10 && digitsOnly.Length != 11) {
                // 입력 번호가 10자리 또는 11자리가 아닌 경우 실행
                IsPhoneNumberValid = false; // 유효하지 않음
                PhoneNumValidationMessage = "휴대폰 번호는 10자리 또는 11자리의 숫자만 입력이 가능합니다.";
                return; // 유효하지 않으므로 밑의 로직이 실행되지 않도록 종료 시킴
            }

            // 휴대폰 번호 시작 패턴 (010, 011, 016, 017, 018, 019)
            // 이 뒤로 숫자 7자리 또는 8자리가 뒤따라야 함(즉, 전체 길이는 10자리 또는 11자리)
            if (!Regex.IsMatch(digitsOnly, @"^(010|011|016|017|018|019)\d{7,8}$")) {
                IsPhoneNumberValid = false; // 유효하지 않음
                PhoneNumValidationMessage = "유효하지 않는 형식의 휴대폰 번호입니다.";
                return;
            }

            // DB에 중복된 번호가 있는지 검사 
            bool isDuplicate = signUpSvc.IsPhoneDuplicate(digitsOnly);
            // true : 중복있음(사용불가) , false : 중복 없음(사용가능)

            if (isDuplicate) { // 중복이라 사용불가 
               IsPhoneNumberValid = false; // 유효하지 않음
               PhoneNumValidationMessage = "입력하신 전화번호가 이미 등록되어 있습니다. \n" +
                    "문제가 지속되면 고객센터로 문의해 주세요.";
                return;
            }

            PhoneNumValidationMessage = "사용 가능한 번호 입니다.";
            IsPhoneNumberValid = true;
        }

    }// 클래스 끝
}// 네임스페이스 끝
