using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using Prism.Events;

using VNC.Core.Mvvm;

using VNCExplore_FriendOrganizer.Core.Events;
using VNCExplore_FriendOrganizer.Core.Services;

namespace FriendOrganizer.Presentation.Friend13.ViewModels
{
    public class Friend13MainViewModel : ViewModelBase
    {
        private IEventAggregator _eventAggregator;
        private Func<IFriend13DetailViewModel> _Friend13DetailViewModelCreator;
        private IFriend13DetailViewModel _friend13DetailViewModel;
        private IMessageDialogService _messageDialogService;

        public ICommand AddNewFriendCommand { get; }

        // N.B. This is public so View.Xaml can bind to it.
        public IFriend13ViewModel Friend13ViewModel { get; }

        public Friend13MainViewModel(
            IFriend13ViewModel friendViewModel,
            Func<IFriend13DetailViewModel> friendDetailViewModelCreator,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService)
        {
            _eventAggregator = eventAggregator;
            _Friend13DetailViewModelCreator = friendDetailViewModelCreator;
            _messageDialogService = messageDialogService;

            _eventAggregator.GetEvent<OpenFriendDetailViewEvent13>()
                .Subscribe(OnOpenFriendDetailView);

            _eventAggregator.GetEvent<AfterFriendDeletedEvent13>()
                .Subscribe(AfterFriendDeleted);

            AddNewFriendCommand = new DelegateCommand(OnAddNewFriendExecute);

            Friend13ViewModel = friendViewModel;
        }

        public IFriend13DetailViewModel Friend13DetailViewModel
        {
            get
            {
                return _friend13DetailViewModel;
            }
            private set
            {
                _friend13DetailViewModel = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadAsync()
        {
            await Friend13ViewModel.LoadAsync();
        }

        private void OnAddNewFriendExecute()
        {
            OnOpenFriendDetailView(null);
        }

        private async void OnOpenFriendDetailView(int? friendId)
        {
            if (Friend13DetailViewModel != null && Friend13DetailViewModel.HasChanges)
            {
                var result = _messageDialogService.ShowOkCancelDialog(
                                "You've made changes. Navigate away?", "Question");
                if (result == MessageDialogResult.Cancel)
                {
                    return;
                }
            }

            Friend13DetailViewModel = _Friend13DetailViewModelCreator();
            await Friend13DetailViewModel.LoadAsync(friendId);
        }
        void AfterFriendDeleted(int friendId)
        {
            // Hide the DetailViewModel when friend deleted
            Friend13DetailViewModel = null;
        }
    }
}
