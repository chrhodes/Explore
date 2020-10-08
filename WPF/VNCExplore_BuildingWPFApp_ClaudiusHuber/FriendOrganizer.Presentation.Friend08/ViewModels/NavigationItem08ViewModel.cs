using Prism.Mvvm;

namespace FriendOrganizer.Presentation.Friend08.ViewModels
{
    public class NavigationItem08ViewModel : BindableBase
    {
        string _displayMember;

        public NavigationItem08ViewModel(int id, string displayMember)
        {
            Id = id;
            DisplayMember = displayMember;
        }

        public int Id { get; set; }

        public string DisplayMember
        {
            get { return _displayMember; }
            set
            {
                if (_displayMember == value)
                    return;
                _displayMember = value;
                RaisePropertyChanged();
            }
        }
    }
}

