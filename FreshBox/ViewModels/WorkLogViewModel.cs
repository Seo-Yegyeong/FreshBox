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
    public class WorkLogViewModel : ViewModelBase
    {
        private WorkLog _workLog = new();

        public WorkLog WorkLog
        {
            get => _workLog;
            set
            {
                _workLog = value;
                OnPropertyChanged();
            }
        }

        public int MemberId
        {
            get => _workLog.MemberId;
            set
            {
                if (_workLog.MemberId != value)
                {
                    _workLog.MemberId = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime? CheckIn
        {
            get => _workLog.CheckIn;
            set
            {
                if (_workLog.CheckIn != value)
                {
                    _workLog.CheckIn = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime? CheckOut
        {
            get => _workLog.CheckOut;
            set
            {
                if (_workLog.CheckOut != value)
                {
                    _workLog.CheckOut = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
