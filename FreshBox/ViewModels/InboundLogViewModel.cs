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
    public class InboundLogViewModel : ViewModelBase
    {
        private InboundLog _inboundLog = new();

        public InboundLog InboundLog
        {
            get => _inboundLog;
            set
            {
                _inboundLog = value;
                OnPropertyChanged();
            }
        }

        public int ProductId
        {
            get => _inboundLog.ProductId;
            set
            {
                if (_inboundLog.ProductId != value)
                {
                    _inboundLog.ProductId = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Quantity
        {
            get => _inboundLog.Quantity;
            set
            {
                if (_inboundLog.Quantity != value)
                {
                    _inboundLog.Quantity = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime InboundAt
        {
            get => _inboundLog.InboundAt;
            set
            {
                if (_inboundLog.InboundAt != value)
                {
                    _inboundLog.InboundAt = value;
                    OnPropertyChanged();
                }
            }
        }

        public int? MemberId
        {
            get => _inboundLog.MemberId;
            set
            {
                if (_inboundLog.MemberId != value)
                {
                    _inboundLog.MemberId = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
