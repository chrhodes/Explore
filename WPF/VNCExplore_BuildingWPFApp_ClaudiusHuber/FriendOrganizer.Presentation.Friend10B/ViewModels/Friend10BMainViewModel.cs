using System;
using System.Threading.Tasks;

using Prism.Events;

using VNC.Core.Mvvm;

using VNCExplore_FriendOrganizer.Core.Events;

using VNCExplore_FriendOrganizer.Core.Services;

namespace FriendOrganizer.Presentation.Friend10B.ViewModels
{
    public class Friend10BMainViewModel : ViewModelBase
    {
        private IEventAggregator _eventAggregator;
        private Func<IFriend10BDetailViewModel> _friend10BDetailViewModelCreator;
        private IFriend10BDetailViewModel _friend10BDetailViewModel;
        private IMessageDialogService _messageDialogService;

        public IFriend10BViewModel Friend10BViewModel { get; }

        public Friend10BMainViewModel(
            IFriend10BViewModel friend10BViewModel,
            Func<IFriend10BDetailViewModel> friend10BDetailViewModelCreator,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService)
        {
            _eventAggregator = eventAggregator;
            _friend10BDetailViewModelCreator = friend10BDetailViewModelCreator;
            _messageDialogService = messageDialogService;

            _eventAggregator.GetEvent<OpenFriendDetailViewEvent10B>()
                .Subscribe(OnOpenFriendDetailView);

            Friend10BViewModel = friend10BViewModel;
        }

        public IFriend10BDetailViewModel Friend10BDetailViewModel
        {
            get
            {
                return _friend10BDetailViewModel;
            }
            private set
            {
                _friend10BDetailViewModel = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadAsync()
        {
            await Friend10BViewModel.LoadAsync();
        }

        private async void OnOpenFriendDetailView(int friendId)
        {
            if (Friend10BDetailViewModel != null && Friend10BDetailViewModel.HasChanges)
            {
                var result = _messageDialogService.ShowOkCancelDialog("You've made changes. Navigate away?", "Question");
                if (result == MessageDialogResult.Cancel)
                {
                    return;
                }
            }

            Friend10BDetailViewModel = _friend10BDetailViewModelCreator();
            await Friend10BDetailViewModel.LoadAsync(friendId);
        }
    }
}
