using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FreshBox.Models;
using FreshBox.Repository;

namespace FreshBox.ViewModels
{
    public partial class InboundLogViewModel : ObservableObject
    {
        private readonly InboundLogRepository _repository;
        private InboundLog _inboundLog = new();

        public ObservableCollection<InboundLog> InboundLogs { get; set; } = new();

        [ObservableProperty]
        private InboundLog? inboundLog;

    }
}
