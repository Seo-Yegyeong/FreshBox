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
    public class OutboundLogViewModel : ViewModelBase
    {
        private OutboundLog _outboundLog = new();

        public OutboundLog OutboundLog
        {
            get => _outboundLog;
            set
            {
                _outboundLog = value;
                OnPropertyChanged();
            }
        }

        public int ProductId
        {
            get => _outboundLog.ProductId;
            set
            {
                if (_outboundLog.ProductId != value)
                {
                    _outboundLog.ProductId = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Quantity
        {
            get => _outboundLog.Quantity;
            set
            {
                if (_outboundLog.Quantity != value)
                {
                    _outboundLog.Quantity = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime OutboundAt
        {
            get => _outboundLog.OutboundAt;
            set
            {
                if (_outboundLog.OutboundAt != value)
                {
                    _outboundLog.OutboundAt = value;
                    OnPropertyChanged();
                }
            }
        }

        public int MemberId
        {
            get => _outboundLog.MemberId;
            set
            {
                if (_outboundLog.MemberId != value)
                {
                    _outboundLog.MemberId = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
