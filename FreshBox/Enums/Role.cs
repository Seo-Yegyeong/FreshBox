using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshBox.Enums
{
    /// <summary>
    /// MEMBER 테이블의 role컬럼 타입
    /// </summary>
    public enum Role
    {
        None,      // 0, 로그아웃 상태, 기본값
        Employee, // 1, 사원
        Admin // 2, 관리자
    }
}
