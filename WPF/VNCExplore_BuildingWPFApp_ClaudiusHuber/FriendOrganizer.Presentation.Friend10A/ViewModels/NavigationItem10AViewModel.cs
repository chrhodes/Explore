using System.Windows.Input;

using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

using VNCExplore_FriendOrganizer.Core.Events;

namespace FriendOrganizer.Presentation.Friend10A.ViewModels
{
    public class NavigationItem10AViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;

        private string _displayMember;

        public NavigationItem10AViewModel(
            int id, string displayMember,
            IEventAggregator eventAggregator)
        {
            Id = id;
            DisplayMember = displayMember;
            _eventAggregator = eventAggregator;

            OpenFriend10ADetailViewCommand = new DelegateCommand(OnOpenFriend10ADetailView);
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

        public ICommand OpenFriend10ADetailViewCommand { get; }

        private void OnOpenFriend10ADetailView()
        {
            _eventAggregator.GetEvent<OpenFriendDetailViewEvent10A>()
                  .Publish(Id);
        }
    }
}

