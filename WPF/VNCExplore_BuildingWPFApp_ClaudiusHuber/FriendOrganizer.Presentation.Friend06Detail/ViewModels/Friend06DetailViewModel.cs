using System.Threading.Tasks;

using VNCExplore_FriendOrganizer.Core.DomainServices;

namespace FriendOrganizer.Presentation.Friend06Detail.ViewModels
{
    class Friend06DetailViewModel : VNC.Core.Mvvm.ViewModelBase, IFriend06DetailViewModel
    {
        private IFriendDataService06 _dataService;

        public Friend06DetailViewModel(
            IFriendDataService06 dataService)
        {
            _dataService = dataService;
        }

        public async Task LoadAsync(int friendId)
        {
            Friend = await _dataService.GetByIdAsync(friendId);
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
