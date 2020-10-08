using System.Windows.Input;

using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

using VNCExplore_FriendOrganizer.Core.Events;

namespace FriendOrganizer.Presentation.Friend12.ViewModels
{
    public class NavigationItem12ViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;

        private string _displayMember;

        public NavigationItem12ViewModel(
            int id, string displayMember,
            IEventAggregator eventAggregator)
        {
            Id = id;
            DisplayMember = displayMember;
            _eventAggregator = eventAggregator;

            OpenFriend12DetailViewCommand = new DelegateCommand(OnOpenFriend12DetailView);
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

        public ICommand OpenFriend12DetailViewCommand { get; }

        private void OnOpenFriend12DetailView()
        {
            _eventAggregator.GetEvent<OpenFriendDetailViewEvent12>()
                  .Publish(Id);
        }
    }
}

