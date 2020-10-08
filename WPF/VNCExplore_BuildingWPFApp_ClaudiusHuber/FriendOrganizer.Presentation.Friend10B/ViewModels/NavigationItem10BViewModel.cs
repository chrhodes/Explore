using System.Windows.Input;

using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

using VNCExplore_FriendOrganizer.Core.Events;

namespace FriendOrganizer.Presentation.Friend10B.ViewModels
{
    public class NavigationItem10BViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;

        private string _displayMember;

        public NavigationItem10BViewModel(
            int id, string displayMember,
            IEventAggregator eventAggregator)
        {
            Id = id;
            DisplayMember = displayMember;
            _eventAggregator = eventAggregator;

            OpenFriend10BDetailViewCommand = new DelegateCommand(OnOpenFriend10BDetailView);
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

        public ICommand OpenFriend10BDetailViewCommand { get; }

        private void OnOpenFriend10BDetailView()
        {
            _eventAggregator.GetEvent<OpenFriendDetailViewEvent10B>()
                  .Publish(Id);
        }
    }
}

