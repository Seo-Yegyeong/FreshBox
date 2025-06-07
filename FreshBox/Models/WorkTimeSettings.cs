using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshBox.Models
{
    /// <summary>
    /// 데이터베이스의 work_time_settings테이블과 매핑됨
    /// 출퇴근 기준 시간 설정 모델 클래스
    /// </summary>
    public class WorkTimeSettings
    {
        /// <summary>
        /// 출퇴근 기준 시간 설정 ID (기본 키)
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 출근 가능 시작 시간 (예: 06:00:00) -> 오전 6시부터 출근 찍을 수 있음
        /// </summary>
        public TimeSpan WorkStartFrom { get; set; }
        // TimeSpan은 .NET(C#)에서 시간 간격을 표현하는 자료형
        // DB에서는 컬럼이 TIME타입으로 되어 있기 때문에
        // ToString()할 때 "hh:mm:ss" 포맷을 지정해야 MySQL에서 인식 가능
        /*
         * C# TimeSpan → MySQL TIME
             TimeSpan workTime = new TimeSpan(8, 30, 0); // 8시간 30분
             string timeForMySQL = workTime.ToString(@"hh\:mm\:ss"); // "08:30:00"

         * MySQL TIME → C# TimeSpan
             string dbValue = "07:45:00";
             TimeSpan span = TimeSpan.Parse(dbValue); // OK!
         */

        /// <summary>
        /// 출근 마감 시간 (예: 08:00:00) -> 오전 8시 넘으면 지각으로 처리됨
        /// </summary>
        public TimeSpan WorkStartTo { get; set; }

        /// <summary>
        /// 퇴근 가능 시작 시간 (예: 17:00:00) -> 오후 5시부터 퇴근 가능
        /// </summary>
        public TimeSpan WorkEndFrom { get; set; }

        /// <summary>
        /// 퇴근 마감 시간 (예: 23:59:59) -> 이 시간 지나서는 퇴근 못 찍음
        /// </summary>
        public TimeSpan WorkEndTo { get; set; }

        /// <summary>
        /// 생성 시각 (기본값: 현재 시간) -> 출퇴근 기준 시간 설정한 시각
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// 수정 시각 (수정될 때 자동 갱신됨) -> 출퇴근 기준 시간 수정한 시각
        /// </summary>
        public DateTime UpdatedAt { get; set; }
    }
}
