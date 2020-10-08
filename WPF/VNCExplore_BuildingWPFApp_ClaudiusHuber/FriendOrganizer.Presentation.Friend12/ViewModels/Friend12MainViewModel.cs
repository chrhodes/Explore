using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using Prism.Events;

using VNC.Core.Mvvm;

using VNCExplore_FriendOrganizer.Core.Events;
using VNCExplore_FriendOrganizer.Core.Services;

namespace FriendOrganizer.Presentation.Friend12.ViewModels
{
    public class Friend12MainViewModel : ViewModelBase
    {
        private IEventAggregator _eventAggregator;
        private Func<IFriend12DetailViewModel> _Friend12DetailViewModelCreator;
        private IFriend12DetailViewModel _Friend12DetailViewModel;
        private IMessageDialogService _messageDialogService;

        public ICommand AddNewFriendCommand { get; }

        public IFriend12ViewModel Friend12ViewModel { get; }

        public Friend12MainViewModel(
            IFriend12ViewModel friendViewModel,
            Func<IFriend12DetailViewModel> friendDetailViewModelCreator,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService)
        {
            _eventAggregator = eventAggregator;
            _Friend12DetailViewModelCreator = friendDetailViewModelCreator;
            _messageDialogService = messageDialogService;

            _eventAggregator.GetEvent<OpenFriendDetailViewEvent12>()
                .Subscribe(OnOpenFriendDetailView);

            _eventAggregator.GetEvent<AfterFriendDeletedEvent12>()
                .Subscribe(AfterFriendDeleted);

            AddNewFriendCommand = new DelegateCommand(OnAddNewFriendExecute);

            Friend12ViewModel = friendViewModel;
        }

        public IFriend12DetailViewModel Friend12DetailViewModel
        {
            get
            {
                return _Friend12DetailViewModel;
            }
            private set
            {
                _Friend12DetailViewModel = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadAsync()
        {
            await Friend12ViewModel.LoadAsync();
        }

        private void OnAddNewFriendExecute()
        {
            OnOpenFriendDetailView(null);
        }

        private async void OnOpenFriendDetailView(int? friendId)
        {
            if (Friend12DetailViewModel != null && Friend12DetailViewModel.HasChanges)
            {
                var result = _messageDialogService.ShowOkCancelDialog("You've made changes. Navigate away?", "Question");
                if (result == MessageDialogResult.Cancel)
                {
                    return;
                }
            }

            Friend12DetailViewModel = _Friend12DetailViewModelCreator();
            await Friend12DetailViewModel.LoadAsync(friendId);
        }
        void AfterFriendDeleted(int friendId)
        {
            // Hide the DetailViewModel when friend deleted
            Friend12DetailViewModel = null;
        }
    }
}
