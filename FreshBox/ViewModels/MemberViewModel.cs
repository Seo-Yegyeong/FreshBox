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
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
#endregion


namespace FreshBox.ViewModels
{
    public partial class MemberViewModel : ObservableObject
    {
        public ObservableCollection<Member> Members { get; } = new();

        [ObservableProperty]
        private Member? selectedMember;

        public MemberViewModel()
        {
            // Null 참조 발생 -> 초기화가 필요합니다! 안 그럼 xaml에서 <vm:MemberViewModel.Members> 바인딩이 실패합니다!
            LoadMembers();
        }

        [RelayCommand]
        private void LoadMembers()
        {
            Members.Clear();
            Members.Add(new Member
            {
                Id = 1,
                Username = "johndoe",
                Password = "hashedpassword",
                Name = "John Doe",
                Role = "employee",
                Phone = "010-1234-5678",
                Email = "john@example.com",
                BirthDate = new System.DateTime(1990, 1, 1),
                CreatedAt = System.DateTime.Now,
                HireDate = System.DateTime.Today
            });
        }
    }
}
