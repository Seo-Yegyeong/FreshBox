using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshBox.Models
{
    public enum DefectiveStatus
    {
        미확인,
        보류,
        승인,
        취소
    }

    public class DefectiveLog
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int MemberId { get; set; }
        public string Reason { get; set; } = string.Empty;
        public DateTime ReportedAt { get; set; }
        public DefectiveStatus Status { get; set; }
        public int AdminId { get; set; }
    }
}
