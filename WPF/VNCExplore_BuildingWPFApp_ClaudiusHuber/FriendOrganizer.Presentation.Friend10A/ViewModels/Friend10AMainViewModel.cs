using System;
using System.Threading.Tasks;

using Prism.Events;

using VNC.Core.Mvvm;

using VNCExplore_FriendOrganizer.Core.Events;

using VNCExplore_FriendOrganizer.Core.Services;

namespace FriendOrganizer.Presentation.Friend10A.ViewModels
{
    public class Friend10AMainViewModel : ViewModelBase
    {
        private IEventAggregator _eventAggregator;
        private Func<IFriend10ADetailViewModel> _friend10ADetailViewModelCreator;
        private IFriend10ADetailViewModel _friend10ADetailViewModel;
        private IMessageDialogService _messageDialogService;

        public IFriend10AViewModel Friend10AViewModel { get; }

        public Friend10AMainViewModel(
            IFriend10AViewModel friendViewModel,
            Func<IFriend10ADetailViewModel> friendDetailViewModelCreator,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService)
        {
            _eventAggregator = eventAggregator;
            _friend10ADetailViewModelCreator = friendDetailViewModelCreator;
            _messageDialogService = messageDialogService;

            _eventAggregator.GetEvent<OpenFriendDetailViewEvent10A>()
                .Subscribe(OnOpenFriendDetailView);

            Friend10AViewModel = friendViewModel;
        }

        public IFriend10ADetailViewModel Friend10ADetailViewModel
        {
            get
            {
                return _friend10ADetailViewModel;
            }
            private set
            {
                _friend10ADetailViewModel = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadAsync()
        {
            await Friend10AViewModel.LoadAsync();
        }

        private async void OnOpenFriendDetailView(int friendId)
        {
            if (Friend10ADetailViewModel != null && Friend10ADetailViewModel.HasChanges)
            {
                var result = _messageDialogService.ShowOkCancelDialog("You've made changes. Navigate away?", "Question");
                if (result == MessageDialogResult.Cancel)
                {
                    return;
                }
            }

            Friend10ADetailViewModel = _friend10ADetailViewModelCreator();
            await Friend10ADetailViewModel.LoadAsync(friendId);
        }
    }
}
