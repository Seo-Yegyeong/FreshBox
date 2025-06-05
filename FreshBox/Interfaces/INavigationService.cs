using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshBox.Interfaces
{
    /// <summary>
    /// 화면 간 네비게이션(이동)을 담당하는 서비스 인터페이스입니다.
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        /// 지정한 뷰 이름으로 화면을 이동합니다.
        /// </summary>
        /// <param name="viewName">이동할 뷰의 이름(키)입니다.</param>
        void NavigateTo(string viewName);

        /// <summary>
        /// 이전 화면으로 되돌아갑니다.
        /// </summary>
        void GoBack();
    }

}
