using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FreshBox.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FreshBox.ViewModels
{
    public class DefectiveLogViewModel : ViewModelBase
    {
        private DefectiveLog _defectiveLog = new();

        public DefectiveLog DefectiveLog
        {
            get => _defectiveLog;
            set
            {
                _defectiveLog = value;
                OnPropertyChanged();
            }
        }

        public int ProductId
        {
            get => _defectiveLog.ProductId;
            set
            {
                if (_defectiveLog.ProductId != value)
                {
                    _defectiveLog.ProductId = value;
                    OnPropertyChanged();
                }
            }
        }

        public int MemberId
        {
            get => _defectiveLog.MemberId;
            set
            {
                if (_defectiveLog.MemberId != value)
                {
                    _defectiveLog.MemberId = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Reason
        {
            get => _defectiveLog.Reason;
            set
            {
                if (_defectiveLog.Reason != value)
                {
                    _defectiveLog.Reason = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime ReportedAt
        {
            get => _defectiveLog.ReportedAt;
            set
            {
                if (_defectiveLog.ReportedAt != value)
                {
                    _defectiveLog.ReportedAt = value;
                    OnPropertyChanged();
                }
            }
        }

        //public string Status
        //{
        //    get => _defectiveLog.Status;
        //    set
        //    {
        //        if (_defectiveLog.Status != value)
        //        {
        //            _defectiveLog.Status = value;
        //            OnPropertyChanged();
        //        }
        //    }
        //}

        //public int AdminId
        //{
        //    get => _defectiveLog.AdminId;
        //    set
        //    {
        //        if (_defectiveLog.AdminId != value)
        //        {
        //            _defectiveLog.AdminId = value;
        //            OnPropertyChanged();
        //        }
        //    }
        //}
    }
}
