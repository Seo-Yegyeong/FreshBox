using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshBox.Enums
{
    /// <summary>
    /// WORK_LOG 테이블의 status컬럼 타입
    /// </summary>
    public enum WorkStatus
    {
        Normal,     // 정상 출근
        Late,       // 지각
        EarlyLeave, // 조퇴
        Absent,     // 결근
        Vacation    // 휴가
    }
}
