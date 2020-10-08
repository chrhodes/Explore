using System;
using System.Threading.Tasks;
using System.Windows.Input;

using Prism.Commands;
using Prism.Events;

using VNC.Core.Events;
using VNC.Core.Mvvm;
using VNCExplore_FriendOrganizer.Core.Events;
using VNCExplore_FriendOrganizer.Core.Services;

namespace FriendOrganizer.Presentation.Friend16.ViewModels
{
    public class Friend16MainViewModel : ViewModelBase
    {
        private IEventAggregator _eventAggregator;
        private Func<IFriend16DetailViewModel> _friend16DetailViewModelCreator;
        private Func<IMeeting16DetailViewModel> _meeting16DetailViewModelCreator;
        private IDetailViewModel _detailViewModel;
        private IMessageDialogService _messageDialogService;

        public ICommand CreateNewDetailCommand { get; }

        // N.B. This is public so View.Xaml can bind to it.
        //public IFriend16ViewModel Friend16ViewModel { get; }
        public INavigation16ViewModel Navigation16ViewModel { get; }

        public Friend16MainViewModel(
            INavigation16ViewModel navigationViewModel,
            Func<IFriend16DetailViewModel> friendDetailViewModelCreator,
            Func<IMeeting16DetailViewModel> meetingDetailViewModelCreator,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService)
        {
            _eventAggregator = eventAggregator;
            _friend16DetailViewModelCreator = friendDetailViewModelCreator;
            _meeting16DetailViewModelCreator = meetingDetailViewModelCreator;
            _messageDialogService = messageDialogService;

            _eventAggregator.GetEvent<OpenDetailViewEvent16>()
                .Subscribe(OnOpenDetailView);

            _eventAggregator.GetEvent<AfterDetailDeletedEvent16>()
                .Subscribe(AfterDetailDeleted);

            CreateNewDetailCommand = new DelegateCommand<Type>(OnCreateNewDetailExecute);

            //Friend16ViewModel = friendViewModel;
            Navigation16ViewModel = navigationViewModel;
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
            await Navigation16ViewModel.LoadAsync();
            //await Friend16ViewModel.LoadAsync();
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
                case nameof(Friend16DetailViewModel):
                    DetailViewModel = _friend16DetailViewModelCreator();
                    break;

                case nameof(Meeting16DetailViewModel):
                    DetailViewModel = _meeting16DetailViewModelCreator();
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
