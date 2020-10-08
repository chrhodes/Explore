using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using Prism.Commands;
using Prism.Events;
using VNC.Core.Events;
using VNC.Core.Mvvm;

using VNCExplore_FriendOrganizer.Core.Events;
using VNCExplore_FriendOrganizer.Core.Services;

namespace FriendOrganizer.Presentation.Friend18.ViewModels
{
    public class Friend18MainViewModel : ViewModelBase
    {
        private IEventAggregator _eventAggregator;
        private Func<IFriend18DetailViewModel> _friend18DetailViewModelCreator;
        private Func<IMeeting18DetailViewModel> _meeting18DetailViewModelCreator;
        private Func<IProgrammingLanguage18DetailViewModel> _programmingLanguage18DetailViewModelCreator;
        private IDetailViewModel _selectedDetailViewModel;
        private IMessageDialogService _messageDialogService;

        public ICommand CreateNewDetailCommand { get; }

        public ICommand OpenSingleDetailViewCommand { get; }

        // N.B. This is public so View.Xaml can bind to it.
        public INavigation18ViewModel Navigation18ViewModel { get; }

        public Friend18MainViewModel(
            INavigation18ViewModel navigationViewModel,
            Func<IFriend18DetailViewModel> friendDetailViewModelCreator,
            Func<IMeeting18DetailViewModel> meetingDetailViewModelCreator,
            Func<IProgrammingLanguage18DetailViewModel> programmingLanguageDetailViewModelCreator,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService)
        {
            _eventAggregator = eventAggregator;
            _friend18DetailViewModelCreator = friendDetailViewModelCreator;
            _meeting18DetailViewModelCreator = meetingDetailViewModelCreator;
            _programmingLanguage18DetailViewModelCreator = programmingLanguageDetailViewModelCreator;
            _messageDialogService = messageDialogService;

            DetailViewModels = new ObservableCollection<IDetailViewModel>();

            _eventAggregator.GetEvent<OpenDetailViewEvent18>()
                .Subscribe(OnOpenDetailView);

            _eventAggregator.GetEvent<AfterDetailDeletedEvent18>()
                .Subscribe(AfterDetailDeleted);

            _eventAggregator.GetEvent<AfterDetailClosedEvent18>()
                .Subscribe(AfterDetailClosed);

            CreateNewDetailCommand = new DelegateCommand<Type>(
                OnCreateNewDetailExecute);

            OpenSingleDetailViewCommand = new DelegateCommand<Type>(
                OnOpenSingleDetailExecute);


            //Friend18ViewModel = friendViewModel;
            Navigation18ViewModel = navigationViewModel;
        }

        public ObservableCollection<IDetailViewModel> DetailViewModels { get; }

        public IDetailViewModel SelectedDetailViewModel
        {
            get
            {
                return _selectedDetailViewModel;
            }
            set
            {
                _selectedDetailViewModel = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadAsync()
        {
            await Navigation18ViewModel.LoadAsync();
            //await Friend18ViewModel.LoadAsync();
        }

        private int _nextNewItemId = 0;

        private void OnCreateNewDetailExecute(Type viewModelType)
        {
            OnOpenDetailView(
                new OpenDetailViewEventArgs
                {
                    Id = _nextNewItemId--,  // Ids in DB > 0.  Can now create multiple new items
                    ViewModelName = viewModelType.Name
                });
        }

        private async void OnOpenDetailView(OpenDetailViewEventArgs args)
        {
            var detailViewModel = DetailViewModels
                    .SingleOrDefault(vm => vm.Id == args.Id
                    && vm.GetType().Name == args.ViewModelName);

            //if (SelectedDetailViewModel != null && SelectedDetailViewModel.HasChanges)
            //{
            //    var result = _messageDialogService.ShowOkCancelDialog(
            //                    "You've made changes. Navigate away?", "Question");
            //    if (result == MessageDialogResult.Cancel)
            //    {
            //        return;
            //    }
            //}

            if (detailViewModel == null)
            {
                switch (args.ViewModelName)
                {
                    case nameof(Friend18DetailViewModel):
                        detailViewModel = _friend18DetailViewModelCreator();
                        break;

                    case nameof(Meeting18DetailViewModel):
                        detailViewModel = _meeting18DetailViewModelCreator();
                        break;

                    case nameof(ProgrammingLanguage18DetailViewModel):
                        detailViewModel = _programmingLanguage18DetailViewModelCreator();
                        break;
                }

                await detailViewModel.LoadAsync(args.Id);
                DetailViewModels.Add(detailViewModel);
            }

            SelectedDetailViewModel = detailViewModel;
        }

        void AfterDetailDeleted(AfterDetailDeletedEventArgs args)
        {
            RemoveDetailViewModel(args.Id, args.ViewModelName);
        }

        void AfterDetailClosed(AfterDetailClosedEventArgs args)
        {
            RemoveDetailViewModel(args.Id, args.ViewModelName);
        }

        private void RemoveDetailViewModel(int id, string viewModelName)
        {
            var detailViewModel = DetailViewModels
                .SingleOrDefault(vm => vm.Id == id
                && vm.GetType().Name == viewModelName);

            if (detailViewModel != null)
            {
                DetailViewModels.Remove(detailViewModel);
            }
        }
        void OnOpenSingleDetailExecute(Type viewModelType)
        {
            OnOpenDetailView(
                new OpenDetailViewEventArgs
                {
                    Id = -1,
                    ViewModelName = viewModelType.Name
                });

        }
    }
}
