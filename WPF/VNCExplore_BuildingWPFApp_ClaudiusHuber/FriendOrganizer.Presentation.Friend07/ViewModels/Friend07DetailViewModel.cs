using System.Threading.Tasks;

using Prism.Events;

using VNC.Core.Mvvm;

using VNCExplore_FriendOrganizer.Core.Events;

using VNCExplore_FriendOrganizer.Core.DomainServices;

namespace FriendOrganizer.Presentation.Friend07.ViewModels
{
    class Friend07DetailViewModel : ViewModelBase, IFriend07DetailViewModel
    {
        private IFriendDataService06 _dataService;
        private IEventAggregator _eventAggregator;

        public Friend07DetailViewModel(
                IFriendDataService06 dataService,
                IEventAggregator eventAggregator)
        {
            _dataService = dataService;
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<OpenFriendDetailViewEvent07>()
                .Subscribe(OnOpenFriendDetailView);
        }

        private async void OnOpenFriendDetailView(int typeId)
        {
            await LoadAsync(typeId);
        }

        public async Task LoadAsync(int id)
        {
            Friend = await _dataService.GetByIdAsync(id);
        }

        private Domain.Friend05 _friend;

        public Domain.Friend05 Friend
        {
            get { return _friend; }
            private set
            {
                _friend = value;
                OnPropertyChanged();
            }
        }
    }
}
