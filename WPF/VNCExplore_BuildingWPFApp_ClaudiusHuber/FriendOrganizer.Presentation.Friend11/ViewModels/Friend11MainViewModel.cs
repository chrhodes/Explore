using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using Prism.Events;

using VNC.Core.Mvvm;

using VNCExplore_FriendOrganizer.Core.Events;
using VNCExplore_FriendOrganizer.Core.Services;

namespace FriendOrganizer.Presentation.Friend11.ViewModels
{
    public class Friend11MainViewModel : ViewModelBase
    {
        private IEventAggregator _eventAggregator;
        private Func<IFriend11DetailViewModel> _Friend11DetailViewModelCreator;
        private IFriend11DetailViewModel _Friend11DetailViewModel;
        private IMessageDialogService _messageDialogService;

        public ICommand AddNewFriendCommand { get; }

        public IFriend11ViewModel Friend11ViewModel { get; }

        public Friend11MainViewModel(
            IFriend11ViewModel friendViewModel,
            Func<IFriend11DetailViewModel> friendDetailViewModelCreator,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService)
        {
            _eventAggregator = eventAggregator;
            _Friend11DetailViewModelCreator = friendDetailViewModelCreator;
            _messageDialogService = messageDialogService;

            _eventAggregator.GetEvent<OpenFriendDetailViewEvent11>()
                .Subscribe(OnOpenFriendDetailView);

            _eventAggregator.GetEvent<AfterFriendDeletedEvent11>()
                .Subscribe(AfterFriendDeleted);

            AddNewFriendCommand = new DelegateCommand(OnAddNewFriendExecute);

            Friend11ViewModel = friendViewModel;
        }

        public IFriend11DetailViewModel Friend11DetailViewModel
        {
            get
            {
                return _Friend11DetailViewModel;
            }
            private set
            {
                _Friend11DetailViewModel = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadAsync()
        {
            await Friend11ViewModel.LoadAsync();
        }

        private void OnAddNewFriendExecute()
        {
            OnOpenFriendDetailView(null);
        }

        private async void OnOpenFriendDetailView(int? friendId)
        {
            if (Friend11DetailViewModel != null && Friend11DetailViewModel.HasChanges)
            {
                var result = _messageDialogService.ShowOkCancelDialog("You've made changes. Navigate away?", "Question");
                if (result == MessageDialogResult.Cancel)
                {
                    return;
                }
            }

            Friend11DetailViewModel = _Friend11DetailViewModelCreator();
            await Friend11DetailViewModel.LoadAsync(friendId);
        }
        void AfterFriendDeleted(int friendId)
        {
            // Hide the DetailViewModel when friend deleted
            Friend11DetailViewModel = null;
        }
    }
}
