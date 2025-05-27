using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region MVVM 패턴을 위한 네임스페이스
using FreshBox.Models;
using FreshBox.Repository;
using System.Collections.ObjectModel;
using System.ComponentModel;
#endregion


namespace FreshBox.ViewModels
{
    internal class MemberViewModel : ViewModelBase
    {
        private readonly MemberRepository _MemberRepo = new MemberRepository();
        public ObservableCollection<Member> Members { get; set; }

        private Member _member = new();

        public Member Member
        {
            get => _member;
            set
            {
                _member = value;
                OnPropertyChanged();
            }
        }

        public string Username
        {
            get => _member.Username;
            set
            {
                if (_member.Username != value)
                {
                    _member.Username = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Password
        {
            get => _member.Password;
            set
            {
                if (_member.Password != value)
                {
                    _member.Password = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Name
        {
            get => _member.Name;
            set
            {
                if (_member.Name != value)
                {
                    _member.Name = value;
                    OnPropertyChanged();
                }
            }
        }

        // 다른 속성들도 같은 방식으로 구현 가능


        public MemberViewModel()
        {
            Members = new ObservableCollection<Member>(_MemberRepo.GetAllMembers());
        }
    }
}
