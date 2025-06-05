using FreshBox.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

#region 화면 전환에 사용할 뷰 네비게이션 서비스 클래스 - 싱글턴으로 관리함 
/* 
     WPF 앱에서 화면 전환(페이지 이동)을 담당함.
    <사용법>
    1. 최초 앱 실행 시, Initialize()로 ContentControl을 지정하고 초기화
        ex) ViewNavigationService.Initialize(MainWindow의 ContentControl)
    2. 사용할 모든 뷰(UserControl)를 등록
        -> RegisterView(string viewName, UserControl view) 호출
    3. 뷰 전환 시 이름으로 호출
        -> NavigateTo(string viewName) 호출
    4. 뒤로 가기
        -> navigationService.GoBack();

    <주의사항>
    - 모든 뷰모델이 공통 인스턴스를 사용하므로 반드시 싱글턴이어야 함
    - Initialize()를 호출하지 않으면 예외 발생함
    - NavigateTo() 호출 전에 반드시 RegisterView()로 뷰를 등록해야 한다
    - 등록되지 않은 뷰 이름을 넘기면 예외가 발생
        NavigationService.NavigateTo("뷰이름") 호출 시 등록되지 않은 뷰 이름이면 
        throw new ArgumentException(...)으로 예외를 발생
        -> 호출부에서 이걸 try-catch로 안전하게 감싸서 처리해야 함
 */
#endregion

namespace FreshBox.Services
{
    /// <summary>
    /// 뷰 이름을 키로 하여 UserControl 인스턴스를 관리하고
    /// 화면 전환을 담당하는 네비게이션 서비스(화면 바꿔 끼우는 걸 해주는 조립 관리자)
    /// => 싱글턴으로 관리해야함
    /// 이유 : 모든 뷰모델이 공통된 뷰네비게이션서비스를 써야해서
    /// 새로운 뷰모델 만들 때마다 서비스 새로 생성하면,
    /// 각각 다른 인스턴스를 쓰게 돼서, 작동이 제대로 되지 않음
    /// 싱글턴으로 관리하면 뷰모델 어디서든 동일한 인스턴스 접근 가능!
    /// => 앱 전역에서 하나의 인스턴스를 공유하여 뷰 전환을 일관되게 처리함
    /// </summary>
    public class ViewNavigationService : INavigationService
    { // INavigationService 인터페이스를 상속받아 구현하는 클래스

        // 화면 전환 대상 컨트롤 (주로 ContentControl)
        // 실제 화면 전환이 일어나는 컨트롤
        // -> 예를 들어, MainWindow.xaml에서 ContentControl이 여기에 해당
        private ContentControl _contentControl;

        // 등록된 뷰들을 이름과 UserControl 인스턴스 쌍으로 저장
        // 뷰 이름과 해당 뷰(UserControl) 인스턴스를 저장하는 딕셔너리
        private readonly Dictionary<string, UserControl> _views = new();

        // 뒤로가기 기능을 위한 뷰 이름 기록 스택
        // 사용자가 화면을 이동할 때 이전 화면 이름을 저장
        // 이전에 보였던 뷰 이름을 저장하는 스택 (뒤로가기용)
        private readonly Stack<string> _history = new();

 
        // 현재 ContentControl에 표시되고 있는 뷰 이름을 저장
        private string _currentViewName;

        // 싱글턴 인스턴스 (정적 필드)
        private static ViewNavigationService _instance;

        // 생성자 외부 호출 방지 (private)
        private ViewNavigationService() { }


        /// <summary>
        /// 싱글턴 인스턴스에 접근하기 위한 프로퍼티
        /// 이 프로퍼티를 통해 어디서든 ViewNavigationService 인스턴스를 사용할 수 있음
        /// 
        /// <주의>
        /// - 반드시 Initialize()를 먼저 호출해서 _instance를 초기화해야 함
        /// - 초기화 전에 Instance에 접근하면 예외 발생 (InvalidOperationException)
        ///   => 앱 시작 시점에 Initialize() 호출하는 코드를 반드시 넣어야 함
        /// </summary>
        public static ViewNavigationService Instance
        {
            get
            {
                // 아직 초기화되지 않았으면 예외 발생
                if (_instance == null)
                    throw new InvalidOperationException("ViewNavigationService가 아직 초기화되지 않았습니다. Initialize()를 먼저 호출하세요.");

                // 초기화된 인스턴스 반환
                return _instance;
            }
        }

        /// <summary>
        /// ViewNavigationService 싱글턴 인스턴스를 초기화하고 ContentControl을 설정
        /// 이 메서드는 앱 시작 시 한 번만 호출되어야 하며, 
        /// 이후에는 Instance 프로퍼티를 통해 접근해야 함.
        /// <주의>
        /// - 이미 초기화된 상태에서 다시 호출하면 예외 발생 (InvalidOperationException)
        /// - contentControl이 null이면 예외 발생 (ArgumentNullException)
        /// - 이 메서드를 호출하지 않으면 Instance 접근 시 예외가 발생함
        /// - 주로 App.xaml.cs나 MainWindow.xaml.cs에서 호출하는 게 일반적
        /// </summary>
        /// <param name="contentControl">화면 전환이 일어날 대상 컨트롤</param>
        public static void Initialize(ContentControl contentControl)
        {   // 이미 초기화된 경우 예외 발생
            if (_instance != null)
                throw new InvalidOperationException("ViewNavigationService는 이미 초기화되었습니다.");
                // 호출부로 예외던짐

            //초기화 안된 경우 새로운 인스턴스를 생성하고 ContentControl을 설정
            _instance = new ViewNavigationService
            {
                _contentControl = contentControl ?? throw new ArgumentNullException(nameof(contentControl))
                // ?? 연산자는 병합 연산자라고 부름. 왼쪽이 null이면 오른쪽을 실행
                // 왼쪽이 null이 아니면 왼쪽을 사용함
                // 전달받은 contentControl이 null이면 예외를 발생시켜 프로그램이 중단되도록 함
                // => 화면 전환 대상이 될 ContentControl이 반드시 필요하므로 null 허용 불가
                // => nameof(contentControl)을 사용해 어떤 인자가 null인지 명확히 예외 메시지에 표시함
            };
        }

        /// <summary>
        /// 뷰를 등록. 미리 생성한 UserControl을 이름과 함께 등록하여 네비게이션에 사용함
        /// 뷰 이름(key)과 UserControl 인스턴스를 받아서 내부 딕셔너리에 저장
        /// 이렇게 하면 나중에 뷰 이름으로 해당 뷰를 쉽게 찾아 화면에 표시할 수 있음
        /// </summary>
        /// <param name="viewName">뷰를 식별하는 이름</param>
        /// <param name="view">실제 화면에 표시할 UserControl 인스턴스</param>
        public void RegisterView(string viewName, UserControl view)
        {
            // 이미 등록된 뷰 이름이 아니면 새로 등록
            if (!_views.ContainsKey(viewName))
            {
                _views[viewName] = view;
            }
        }

        /// <summary>
        /// 지정한 뷰 이름으로 화면을 전환
        /// 현재 화면은 히스토리에 저장되어 뒤로가기가 가능해짐
        /// </summary>
        /// <param name="viewName">이동할 뷰 이름</param>
        public void NavigateTo(string viewName)
        {
            // 요청한 뷰가 현재 보여지는 뷰와 같으면 아무 동작 안 함
            if (viewName == _currentViewName)
                return; // 이미 현재 뷰면 무시

            // 현재 보여지는 뷰 이름이 있으면 히스토리에 저장 (뒤로가기용)
            if (_currentViewName != null)
                _history.Push(_currentViewName);

            // 등록된 뷰들 중에서 요청한 뷰 이름에 맞는 UserControl 찾기
            if (_views.TryGetValue(viewName, out var view))
            {
                // ContentControl의 내용을 새로운 뷰로 교체 (화면 전환)
                _contentControl.Content = view;
                // 현재 보여지는 뷰 이름 업데이트
                _currentViewName = viewName;
            }
            else
            {    // 등록되지 않은 뷰 이름이면 예외 발생
                throw new ArgumentException($"등록되지 않은 뷰 이름입니다: {viewName}");
            }
        }

        /// <summary>
        /// 이전에 보았던 화면으로 돌아간다
        /// 히스토리가 없으면 아무 동작도 하지 않음
        /// </summary>
        public void GoBack()
        {    // 히스토리에 이전 뷰 이름이 하나라도 있으면
            if (_history.Count > 0)
            {   // 스택에서 마지막 저장된 뷰 이름 꺼내기
                var previousViewName = _history.Pop();
                // 해당 뷰 이름에 맞는 UserControl 찾기
                if (_views.TryGetValue(previousViewName, out var view))
                {    // ContentControl에 이전 뷰 표시 (뒤로가기 동작)
                    _contentControl.Content = view;
                    
                    // 현재 보여지는 뷰 이름 업데이트
                    _currentViewName = previousViewName;
                }
                // 히스토리가 없으면 아무 동작도 안함 (처음 화면일 경우 등)
            }
        }
    }
}