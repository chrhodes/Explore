using System;
using System.Threading.Tasks;
using System.Windows.Input;

using Prism.Commands;
using Prism.Events;

using VNC.Core.Events;
using VNC.Core.Mvvm;
using VNCExplore_FriendOrganizer.Core.Events;
using VNCExplore_FriendOrganizer.Core.Services;

namespace FriendOrganizer.Presentation.Friend14.ViewModels
{
    public class Friend14MainViewModel : ViewModelBase
    {
        private IEventAggregator _eventAggregator;
        private Func<IFriend14DetailViewModel> _friend14DetailViewModelCreator;
        private IDetailViewModel _detailViewModel;
        private IMessageDialogService _messageDialogService;

        public ICommand CreateNewDetailCommand { get; }

        // N.B. This is public so View.Xaml can bind to it.
        public IFriend14ViewModel Friend14ViewModel { get; }

        public Friend14MainViewModel(
            IFriend14ViewModel friendViewModel,
            Func<IFriend14DetailViewModel> friendDetailViewModelCreator,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService)
        {
            _eventAggregator = eventAggregator;
            _friend14DetailViewModelCreator = friendDetailViewModelCreator;
            _messageDialogService = messageDialogService;

            _eventAggregator.GetEvent<OpenDetailViewEvent14>()
                .Subscribe(OnOpenDetailView);

            _eventAggregator.GetEvent<AfterDetailDeletedEvent14>()
                .Subscribe(AfterDetailDeleted);

            CreateNewDetailCommand = new DelegateCommand<Type>(OnCreateNewDetailExecute);

            Friend14ViewModel = friendViewModel;
        }

        public IDetailViewModel DetailViewModel
        {
            get
            {
                return _detailViewModel;
            }
            private set
            {
                _detailViewModel = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadAsync()
        {
            await Friend14ViewModel.LoadAsync();
        }

        private void OnCreateNewDetailExecute(Type viewModelType)
        {
            OnOpenDetailView(
                new OpenDetailViewEventArgs
                {
                    ViewModelName = viewModelType.Name
                });
        }

        private async void OnOpenDetailView(OpenDetailViewEventArgs args)
        {
            if (DetailViewModel != null && DetailViewModel.HasChanges)
            {
                var result = _messageDialogService.ShowOkCancelDialog(
                                "You've made changes. Navigate away?", "Question");
                if (result == MessageDialogResult.Cancel)
                {
                    return;
                }
            }

            switch (args.ViewModelName)
            {
                case nameof(Friend14DetailViewModel):
                    DetailViewModel = _friend14DetailViewModelCreator();
                    break;
            }
            
            await DetailViewModel.LoadAsync(args.Id);
        }

        void AfterDetailDeleted(AfterDetailDeletedEventArgs args)
        {
            // Hide the DetailViewModel when friend deleted
            DetailViewModel = null;
        }
    }
}
