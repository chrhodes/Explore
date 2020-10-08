using System.Windows.Input;

using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

using VNCExplore_FriendOrganizer.Core.Events;

namespace FriendOrganizer.Presentation.Friend13.ViewModels
{
    public class NavigationItem13ViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;

        private string _displayMember;

        public NavigationItem13ViewModel(
            int id, string displayMember,
            IEventAggregator eventAggregator)
        {
            Id = id;
            DisplayMember = displayMember;
            _eventAggregator = eventAggregator;

            OpenFriend13DetailViewCommand = new DelegateCommand(OnOpenFriend13DetailView);
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

        public ICommand OpenFriend13DetailViewCommand { get; }

        private void OnOpenFriend13DetailView()
        {
            _eventAggregator.GetEvent<OpenFriendDetailViewEvent13>()
                  .Publish(Id);
        }
    }
}

