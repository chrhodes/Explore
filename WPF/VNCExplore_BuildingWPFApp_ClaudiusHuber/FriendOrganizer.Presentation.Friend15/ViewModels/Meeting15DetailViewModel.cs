using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

using FriendOrganizer.Domain;
using FriendOrganizer.DomainServices.Repositories;
using FriendOrganizer.Presentation.Friend15.ModelWrappers;

using Prism.Commands;
using Prism.Events;

using VNC.Core.Mvvm;
using VNCExplore_FriendOrganizer.Core.DomainServices;
using VNCExplore_FriendOrganizer.Core.Services;

namespace FriendOrganizer.Presentation.Friend15.ViewModels
{
    internal class Meeting15DetailViewModel : DetailViewModelBase15, IMeeting15DetailViewModel
    {

        private static int _instanceCountDVM = 100;
        private Meeting15Wrapper _meeting;
        private IMeetingRepository15 _meetingRepository;
        private IMessageDialogService _messageDialogService;

        public ICommand AddPhoneNumberCommand { get; }
        public ICommand RemovePhoneNumberCommand { get; }

        public ObservableCollection<LookupItem> ProgrammingLanguages { get; }
        public ObservableCollection<Friend15PhoneNumberWrapper> PhoneNumbers { get; }

        public Meeting15DetailViewModel(
            IMeetingRepository15 meetingRepository,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService)
            : base(eventAggregator)
        {
            _messageDialogService = messageDialogService;
            _meetingRepository = meetingRepository;
        }

        public Meeting15Wrapper Meeting
        {
            get { return _meeting; }
            private set
            {
                _meeting = value;
                OnPropertyChanged();
            }
        }

        public override async Task LoadAsync(int meetingId)
        {
            var meeting = meetingId > 0
                ? await _meetingRepository.FindByIdAsync(meetingId)
                : CreateNewMeeting();

            InitializeMeeting(meeting);
        }

        private void InitializeMeeting(Domain.Meeting15 meeting)
        {
            Meeting = new Meeting15Wrapper(meeting);

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
            };

            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();

            if (Meeting.Id == 0)
            {
                Meeting.Title = ""; // Force Validation error
            }
        }

        protected override async void OnSaveExecute()
        {
            await _meetingRepository.UpdateAsync();

            HasChanges = _meetingRepository.HasChanges();
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
            var result = _messageDialogService.ShowOkCancelDialog(
                $"Do you really want to delete the meeting {Meeting.Title}?", "Question");
            if (result == MessageDialogResult.OK)
            {
                _meetingRepository.Remove(Meeting.Model);
                await _meetingRepository.UpdateAsync();
                RaiseDetailDeletedEvent(Meeting.Id);
            }
        }

        private Domain.Meeting15 CreateNewMeeting()
        {
            var meeting = new Domain.Meeting15
            {
                DateFrom = DateTime.Now.Date,
                DateTo = DateTime.Now.Date
            };

            _meetingRepository.Add(meeting);
            return meeting;
        }
    }
}
