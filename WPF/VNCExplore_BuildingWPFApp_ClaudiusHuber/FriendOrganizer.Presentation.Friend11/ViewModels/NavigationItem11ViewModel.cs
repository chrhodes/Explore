using System.Windows.Input;

using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

using VNCExplore_FriendOrganizer.Core.Events;

namespace FriendOrganizer.Presentation.Friend11.ViewModels
{
    public class NavigationItem11ViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;

        private string _displayMember;

        public NavigationItem11ViewModel(
            int id, string displayMember,
            IEventAggregator eventAggregator)
        {
            Id = id;
            DisplayMember = displayMember;
            _eventAggregator = eventAggregator;

            OpenFriend11DetailViewCommand = new DelegateCommand(OnOpenFriend11DetailView);
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

        public ICommand OpenFriend11DetailViewCommand { get; }

        private void OnOpenFriend11DetailView()
        {
            _eventAggregator.GetEvent<OpenFriendDetailViewEvent11>()
                  .Publish(Id);
        }
    }
}

