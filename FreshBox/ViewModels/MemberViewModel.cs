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
    public partial class MemberViewModel : ObservableObject
    { // ㄴ ObservableObject 상속받는 이유 : 속성과 UI 사이의 데이터 바인딩이 자동으로 동작하게 만들기 위해서.
      // ㄴ 꼭, partial class 이여야 함.

        public ObservableCollection<Member> member { get; } = new();
        // get; => 읽기 전용 속성
        // 이 컬렉션을 생성자에서 따로 초기화하지 않고, 바로 선언과 동시에 인스턴스 생성.
        /*
         * ObservableCollection
         * ㄴ UI와 연결될 수 있는 컬렉션(List)
         * ㄴ 변화가 있을 시 UI에 자동 반영되도록 설계된 클래스
         * ㄴ 일반 List<T>는 데이터가 바뀌어도 UI가 모름.
         */

        


                
    }
}
