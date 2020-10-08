using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using FriendOrganizer.Domain;
using FriendOrganizer.Presentation.Friend19.ModelWrappers;

using Prism.Commands;
using Prism.Events;

using VNC.Core.Events;

using VNCExplore_FriendOrganizer.Core.DomainServices;
using VNCExplore_FriendOrganizer.Core.Events;
using VNCExplore_FriendOrganizer.Core.Services;

namespace FriendOrganizer.Presentation.Friend19.ViewModels
{
    internal class Meeting19DetailViewModel : DetailViewModelBase19, IMeeting19DetailViewModel
    {
        private static int _instanceCountDVM = 100;
        private Meeting19Wrapper _meeting;
        private IMeetingRepository19 _meetingRepository;

        private Domain.Friend19 _selectedAvailableFriend;
        private Domain.Friend19 _selectedAddedFriend;
        private List<Domain.Friend19> _allFriends;

        public Meeting19DetailViewModel(
            IEventAggregator eventAggregator,
            IMeetingRepository19 meetingRepository,
            IMessageDialogService messageDialogService)
            : base(eventAggregator, messageDialogService)
        {
            _meetingRepository = meetingRepository;

            eventAggregator.GetEvent<AfterDetailSavedEvent19>()
                .Subscribe(AfterDetailSaved);

            eventAggregator.GetEvent<AfterDetailDeletedEvent19>()
                .Subscribe(AfterDetailDeleted);

            AddedFriends = new ObservableCollection<Domain.Friend19>();
            AvailableFriends = new ObservableCollection<Domain.Friend19>();

            AddFriendCommand = new DelegateCommand(OnAddFriendExecute, OnAddFriendCanExecute);
            RemoveFriendCommand = new DelegateCommand(OnRemoveFriendExecute, OnRemoveFriendCanExecute);
        }

        private async void AfterDetailSaved(AfterDetailSavedEventArgs args)
        {
            if (args.ViewModelName == nameof(Friend19DetailViewModel))
            {
                // Refresh the context with the updated friend
                await _meetingRepository.ReloadFriendAsync(args.Id);

                _allFriends = await _meetingRepository.GetAllFriendsAsync();

                SetupPicklist();
            }
        }
        private async void AfterDetailDeleted(AfterDetailDeletedEventArgs args)
        {
            if (args.ViewModelName == nameof(Friend19DetailViewModel))
            {
                _allFriends = await _meetingRepository.GetAllFriendsAsync();

                SetupPicklist();
            }
        }

        public Meeting19Wrapper Meeting
        {
            get { return _meeting; }
            private set
            {
                _meeting = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddFriendCommand { get; }

        public ICommand RemoveFriendCommand { get; }

        public ObservableCollection<Domain.Friend19> AddedFriends { get; }

        public ObservableCollection<Domain.Friend19> AvailableFriends { get; }

        public Domain.Friend19 SelectedAvailableFriend
        {
            get { return _selectedAvailableFriend; }
            set
            {
                _selectedAvailableFriend = value;
                OnPropertyChanged();
                ((DelegateCommand)AddFriendCommand).RaiseCanExecuteChanged();
            }
        }

        public Domain.Friend19 SelectedAddedFriend
        {
            get { return _selectedAddedFriend; }
            set
            {
                _selectedAddedFriend = value;
                OnPropertyChanged();
                ((DelegateCommand)RemoveFriendCommand).RaiseCanExecuteChanged();
            }
        }
        public override async Task LoadAsync(int meetingId)
        {
            var meeting = meetingId > 0
                ? await _meetingRepository.FindByIdAsync(meetingId)
                : CreateNewMeeting();

            Id = meeting.Id;

            InitializeMeeting(meeting);

            _allFriends = await _meetingRepository.GetAllFriendsAsync();

            SetupPicklist();
        }

        private void InitializeMeeting(Domain.Meeting19 meeting)
        {
            Meeting = new Meeting19Wrapper(meeting);

            Meeting.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _meetingRepository.HasChanges();
                }

                if (e.PropertyName == nameof(Meeting.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }

                if (e.PropertyName == nameof(Meeting.Title))
                {
                    SetTitle();
                }
            };

            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();

            if (Meeting.Id == 0)
            {
                Meeting.Title = ""; // Force Validation error
            }

            SetTitle();
        }

        private void SetTitle()
        {
            Title = Meeting.Title;
        }

        protected override async void OnSaveExecute()
        {
            await _meetingRepository.UpdateAsync();

            HasChanges = _meetingRepository.HasChanges();
            Id = Meeting.Id;
            RaiseDetailSavedEvent(Meeting.Id, Meeting.Title);
        }

        protected override bool OnSaveCanExecute()
        {
            return Meeting != null 
                && !Meeting.HasErrors
                && HasChanges;
        }

        protected override async void OnDeleteExecute()
        {
            var result = MessageDialogService.ShowOkCancelDialog(
                $"Do you really want to delete the meeting {Meeting.Title}?", "Question");
            if (result == MessageDialogResult.OK)
            {
                _meetingRepository.Remove(Meeting.Model);
                await _meetingRepository.UpdateAsync();
                RaiseDetailDeletedEvent(Meeting.Id);
            }
        }

        private Domain.Meeting19 CreateNewMeeting()
        {
            var meeting = new Domain.Meeting19
            {
                DateFrom = DateTime.Now.Date,
                DateTo = DateTime.Now.Date
            };

            _meetingRepository.Add(meeting);
            return meeting;
        }

        private bool OnRemoveFriendCanExecute()
        {
            return SelectedAddedFriend != null;
        }

        private void OnRemoveFriendExecute()
        {
            var friendToRemove = SelectedAddedFriend;

            Meeting.Model.Friends.Remove(friendToRemove);
            AddedFriends.Remove(friendToRemove);
            AvailableFriends.Add(friendToRemove);
            HasChanges = _meetingRepository.HasChanges();
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        private bool OnAddFriendCanExecute()
        {
            return SelectedAvailableFriend != null;
        }

        private void OnAddFriendExecute()
        {
            var friendToAdd = SelectedAvailableFriend;

            Meeting.Model.Friends.Add(friendToAdd);
            AddedFriends.Add(friendToAdd);
            AvailableFriends.Remove(friendToAdd);
            HasChanges = _meetingRepository.HasChanges();
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        private void SetupPicklist()
        {
            var meetingFriendIds = Meeting.Model.Friends.Select(f => f.Id).ToList();
            var addedFriends = _allFriends.Where(f => meetingFriendIds.Contains(f.Id)).OrderBy(f => f.FirstName);
            var availableFriends = _allFriends.Except(addedFriends).OrderBy(f => f.FirstName);

            AddedFriends.Clear();
            AvailableFriends.Clear();
            foreach (var addedFriend in addedFriends)
            {
                AddedFriends.Add(addedFriend);
            }
            foreach (var availableFriend in availableFriends)
            {
                AvailableFriends.Add(availableFriend);
            }
        }
    }
}
