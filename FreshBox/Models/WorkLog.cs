using FreshBox.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshBox.Models
{
    /// <summary>
    /// WORK_LOG 테이블과 매핑되는 출퇴근 로그 데이터 모델 클래스
    /// </summary>
    public class WorkLog
    {
        /// <summary>
        /// id 컬럼과 매핑됨 (PK, Auto Increment)
        /// 타입: INT
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// member_id 컬럼과 매핑됨 (FK, NN), 사용자 ID
        /// 타입: INT
        /// FOREIGN KEY -> MEMBER.id
        /// </summary>
        public int MemberId { get; set; }

        /// <summary>
        /// check_in 컬럼과 매핑됨, 출근 시간
        /// 타입: DATETIME
        /// </summary>
        public DateTime? CheckIn { get; set; }

        /// <summary>
        /// check_out 컬럼과 매핑됨, 퇴근 시간
        /// 타입: DATETIME
        /// </summary>
        public DateTime? CheckOut { get; set; }

        /// <summary>
        /// work_date 컬럼과 매핑됨, 출퇴근 기준 날짜
        /// 타입: DATE
        /// </summary>
        public DateTime WorkDate { get; set; }

        /// <summary>
        /// work_duration 컬럼과 매핑됨, 근무 시간(분 단위)
        /// 타입: INT
        /// </summary>
        public int WorkDuration { get; set; }

        /// <summary>
        /// status 컬럼과 매핑됨, 출근 상태 (정상, 지각 등)
        /// 타입: ENUM('Normal', 'Late', 'EarlyLeave', 'Absent', 'Vacation')
        /// </summary>
        public WorkStatus Status { get; set; }
    }
}
}
