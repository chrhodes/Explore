using System;
using System.Threading.Tasks;
using System.Windows.Input;

using Prism.Commands;
using Prism.Events;

using VNC.Core.Events;
using VNC.Core.Mvvm;
using VNCExplore_FriendOrganizer.Core.Events;
using VNCExplore_FriendOrganizer.Core.Services;

namespace FriendOrganizer.Presentation.Friend15.ViewModels
{
    public class Friend15MainViewModel : ViewModelBase
    {
        private IEventAggregator _eventAggregator;
        private Func<IFriend15DetailViewModel> _friend15DetailViewModelCreator;
        private Func<IMeeting15DetailViewModel> _meeting15DetailViewModelCreator;
        private IDetailViewModel _detailViewModel;
        private IMessageDialogService _messageDialogService;

        public ICommand CreateNewDetailCommand { get; }

        // N.B. This is public so View.Xaml can bind to it.
        //public IFriend15ViewModel Friend15ViewModel { get; }
        public INavigation15ViewModel Navigation15ViewModel { get; }

        public Friend15MainViewModel(
            INavigation15ViewModel navigationViewModel,
            Func<IFriend15DetailViewModel> friendDetailViewModelCreator,
            Func<IMeeting15DetailViewModel> meetingDetailViewModelCreator,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService)
        {
            _eventAggregator = eventAggregator;
            _friend15DetailViewModelCreator = friendDetailViewModelCreator;
            _meeting15DetailViewModelCreator = meetingDetailViewModelCreator;
            _messageDialogService = messageDialogService;

            _eventAggregator.GetEvent<OpenDetailViewEvent15>()
                .Subscribe(OnOpenDetailView);

            _eventAggregator.GetEvent<AfterDetailDeletedEvent15>()
                .Subscribe(AfterDetailDeleted);

            CreateNewDetailCommand = new DelegateCommand<Type>(OnCreateNewDetailExecute);

            //Friend15ViewModel = friendViewModel;
            Navigation15ViewModel = navigationViewModel;
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
            await Navigation15ViewModel.LoadAsync();
            //await Friend15ViewModel.LoadAsync();
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
                case nameof(Friend15DetailViewModel):
                    DetailViewModel = _friend15DetailViewModelCreator();
                    break;

                case nameof(Meeting15DetailViewModel):
                    DetailViewModel = _meeting15DetailViewModelCreator();
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
