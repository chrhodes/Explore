﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using Prism.Commands;
using Prism.Events;
using VNC;
using VNC.Core.Events;
using VNC.Core.Mvvm;

using VNCExplore_FriendOrganizer.Core.Services;

namespace FriendOrganizer.Presentation.Friend.ViewModels
{
    public class FriendMainViewModel : ViewModelBase
    {
        private IEventAggregator _eventAggregator;
        private Func<IFriendDetailViewModel> _friendDetailViewModelCreator;
        private Func<IMeetingDetailViewModel> _meetingDetailViewModelCreator;
        private Func<IProgrammingLanguageDetailViewModel> _programmingLanguageDetailViewModelCreator;
        private IDetailViewModel _selectedDetailViewModel;
        private IMessageDialogService _messageDialogService;

        public ICommand CreateNewDetailCommand { get; }

        public ICommand OpenSingleDetailViewCommand { get; }

        // N.B. This is public so View.Xaml can bind to it.
        public INavigationViewModel NavigationViewModel { get; }

        public FriendMainViewModel(
            INavigationViewModel navigationViewModel,
            Func<IFriendDetailViewModel> friendDetailViewModelCreator,
            Func<IMeetingDetailViewModel> meetingDetailViewModelCreator,
            Func<IProgrammingLanguageDetailViewModel> programmingLanguageDetailViewModelCreator,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_APPNAME);

            _eventAggregator = eventAggregator;
            _friendDetailViewModelCreator = friendDetailViewModelCreator;
            _meetingDetailViewModelCreator = meetingDetailViewModelCreator;
            _programmingLanguageDetailViewModelCreator = programmingLanguageDetailViewModelCreator;
            _messageDialogService = messageDialogService;

            DetailViewModels = new ObservableCollection<IDetailViewModel>();

            _eventAggregator.GetEvent<OpenDetailViewEvent>()
                .Subscribe(OnOpenDetailView);

            _eventAggregator.GetEvent<AfterDetailDeletedEvent>()
                .Subscribe(AfterDetailDeleted);

            _eventAggregator.GetEvent<AfterDetailClosedEvent>()
                .Subscribe(AfterDetailClosed);

            CreateNewDetailCommand = new DelegateCommand<Type>(
                OnCreateNewDetailExecute);

            OpenSingleDetailViewCommand = new DelegateCommand<Type>(
                OnOpenSingleDetailExecute);

            NavigationViewModel = navigationViewModel;

            Log.CONSTRUCTOR("Exit", Common.LOG_APPNAME, startTicks);
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
            Int64 startTicks = Log.VIEWMODEL("(FriendMainViewModel) Enter", Common.LOG_APPNAME);

            await NavigationViewModel.LoadAsync();

            Log.VIEWMODEL("(FriendMainViewModel) Exit", Common.LOG_APPNAME, startTicks);
        }

        private int _nextNewItemId = 0;

        private void OnCreateNewDetailExecute(Type viewModelType)
        {
            Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_APPNAME);

            OnOpenDetailView(
                new OpenDetailViewEventArgs
                {
                    Id = _nextNewItemId--,  // Ids in DB > 0.  Can now create multiple new items
                    ViewModelName = viewModelType.Name
                });

            Log.EVENT_HANDLER("Exit", Common.LOG_APPNAME, startTicks);
        }

        private async void OnOpenDetailView(OpenDetailViewEventArgs args)
        {
            Int64 startTicks = Log.EVENT_HANDLER("(FriendMainViewModel) Enter", Common.LOG_APPNAME);

            var detailViewModel = DetailViewModels
                    .SingleOrDefault(vm => vm.Id == args.Id
                    && vm.GetType().Name == args.ViewModelName);

            if (detailViewModel == null)
            {
                switch (args.ViewModelName)
                {
                    case nameof(FriendDetailViewModel):
                        detailViewModel = _friendDetailViewModelCreator();
                        break;

                    case nameof(MeetingDetailViewModel):
                        detailViewModel = _meetingDetailViewModelCreator();
                        break;

                    case nameof(ProgrammingLanguageDetailViewModel):
                        detailViewModel = _programmingLanguageDetailViewModelCreator();
                        break;
                }

                // Verify item still exists (may have been deleted)

                try
                {
                     await detailViewModel.LoadAsync(args.Id);
                }
                catch
                {
                    _messageDialogService.ShowInfoDialog(
                        "Cannot load the entity, it may have been deleted" +
                        " by another user.  Updating Navigation");
                    await NavigationViewModel.LoadAsync();
                    return;
                }

                DetailViewModels.Add(detailViewModel);
            }

            SelectedDetailViewModel = detailViewModel;

            Log.EVENT_HANDLER("(FriendMainViewModel) Exit", Common.LOG_APPNAME, startTicks);
        }

        void AfterDetailDeleted(AfterDetailDeletedEventArgs args)
        {
            Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_APPNAME);

            RemoveDetailViewModel(args.Id, args.ViewModelName);

            Log.EVENT_HANDLER("Exit", Common.LOG_APPNAME, startTicks);
        }

        void AfterDetailClosed(AfterDetailClosedEventArgs args)
        {
            Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_APPNAME);

            RemoveDetailViewModel(args.Id, args.ViewModelName);

            Log.EVENT_HANDLER("Exit", Common.LOG_APPNAME, startTicks);
        }

        private void RemoveDetailViewModel(int id, string viewModelName)
        {
            Int64 startTicks = Log.VIEWMODEL("Enter", Common.LOG_APPNAME);

            var detailViewModel = DetailViewModels
                .SingleOrDefault(vm => vm.Id == id
                && vm.GetType().Name == viewModelName);

            if (detailViewModel != null)
            {
                DetailViewModels.Remove(detailViewModel);
            }

            Log.VIEWMODEL("Exit", Common.LOG_APPNAME, startTicks);
        }

        void OnOpenSingleDetailExecute(Type viewModelType)
        {
            Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_APPNAME);

            OnOpenDetailView(
                new OpenDetailViewEventArgs
                {
                    Id = -1,
                    ViewModelName = viewModelType.Name
                });

            Log.EVENT_HANDLER("Exit", Common.LOG_APPNAME, startTicks);
        }
    }
}
