using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using FriendOrganizer.Domain;
using FriendOrganizer.Presentation.Friend16.ModelWrappers;
using FriendOrganizer.UI.ModelWrappers;

using Prism.Commands;
using Prism.Events;

using VNC.Core.Mvvm;

using VNCExplore_FriendOrganizer.Core.DomainServices;
using VNCExplore_FriendOrganizer.Core.Services;

namespace FriendOrganizer.Presentation.Friend16.ViewModels
{
    internal class Friend16DetailViewModel : DetailViewModelBase16, IFriend16DetailViewModel
    {

        private static int _instanceCountDVM = 100;
        private Friend16Wrapper _friend;
        private Friend16PhoneNumberWrapper _selectedPhoneNumber;
        private IFriendRepository16 _friendRepository;
        readonly IProgrammingLanguageLookupDataService12 _programmingLanguageLookupDataService;
        private IMessageDialogService _messageDialogService;

        public ICommand AddPhoneNumberCommand { get; }
        public ICommand RemovePhoneNumberCommand { get; }

        public ObservableCollection<LookupItem> ProgrammingLanguages { get; }
        public ObservableCollection<Friend16PhoneNumberWrapper> PhoneNumbers { get; }

        public Friend16DetailViewModel(
            IFriendRepository16 friendRepository,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService,
            IProgrammingLanguageLookupDataService12 programmingLanguageLookupDataService)
            : base(eventAggregator)
        {
            _messageDialogService = messageDialogService;
            _friendRepository = friendRepository;
            _programmingLanguageLookupDataService = programmingLanguageLookupDataService;

            AddPhoneNumberCommand = new DelegateCommand(
                OnAddPhoneNumberExecute);

            RemovePhoneNumberCommand = new DelegateCommand(
                OnRemovePhoneNumberExecute, OnRemovePhoneNumberCanExecute);

            ProgrammingLanguages = new ObservableCollection<LookupItem>();
            PhoneNumbers = new ObservableCollection<Friend16PhoneNumberWrapper>();
        }

        public Friend16Wrapper Friend
        {
            get { return _friend; }
            private set
            {
                _friend = value;
                OnPropertyChanged();
            }
        }

        public Friend16PhoneNumberWrapper SelectedPhoneNumber
        {
            get { return _selectedPhoneNumber; }
            set
            {
                _selectedPhoneNumber = value;
                OnPropertyChanged();
                ((DelegateCommand)RemovePhoneNumberCommand).RaiseCanExecuteChanged();
            }
        }

        public override async Task LoadAsync(int friendId)
        {
            var friend = friendId > 0
                ? await _friendRepository.FindByIdAsync(friendId)
                : CreateNewFriend();

            InitializeFriend(friend);

            InitializeFriendPhoneNumbers(friend.PhoneNumbers);

            await LoadProgrammingLanguagesLookupAsync();
        }

        private void InitializeFriend(Domain.Friend15 friend)
        {
            Friend = new Friend16Wrapper(friend);
            Friend.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _friendRepository.HasChanges();
                }

                if (e.PropertyName == nameof(Friend.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            };

            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();

            // Little trick to trigger the validation when creating new entries
            if (Friend.Id == 0)
            {
                Friend.FirstName = "";
            }
        }

        private void InitializeFriendPhoneNumbers(ICollection<FriendPhoneNumber13> phoneNumbers)
        {
            foreach (var wrapper in PhoneNumbers)
            {
                wrapper.PropertyChanged -= Friend16PhoneNumberWrapper_PropertyChanged;
            }
            PhoneNumbers.Clear();
            foreach (var friendPhoneNumber in phoneNumbers)
            {
                var wrapper = new Friend16PhoneNumberWrapper(friendPhoneNumber);
                PhoneNumbers.Add(wrapper);
                wrapper.PropertyChanged += Friend16PhoneNumberWrapper_PropertyChanged;
            }
        }

        private void Friend16PhoneNumberWrapper_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!HasChanges)
            {
                HasChanges = _friendRepository.HasChanges();
            }
            if (e.PropertyName == nameof(Friend16PhoneNumberWrapper.HasErrors))
            {
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }

        private async Task LoadProgrammingLanguagesLookupAsync()
        {
            ProgrammingLanguages.Clear();

            //ProgrammingLanguages.Add(new NullLookupItem());
            ProgrammingLanguages.Add(new NullLookupItem { DisplayMember = " - " });

            var lookup = await _programmingLanguageLookupDataService
                                .GetProgrammingLanguageLookupAsync();

            foreach (var lookupItem in lookup)
            {
                ProgrammingLanguages.Add(lookupItem);
            }
        }

        protected override async void OnSaveExecute()
        {
            await _friendRepository.UpdateAsync();

            HasChanges = _friendRepository.HasChanges();
            RaiseDetailSavedEvent(Friend.Id, $"{Friend.FirstName} {Friend.LastName}");
        }

        protected override bool OnSaveCanExecute()
        {
            return Friend != null 
                && !Friend.HasErrors
                && PhoneNumbers.All(pn => !pn.HasErrors)
                && HasChanges;
        }

        protected override async void OnDeleteExecute()
        {
            if (await _friendRepository.HasMeetingsAsync(Friend.Id))
            {
                _messageDialogService.ShowInfoDialog(
                    $"{Friend.FirstName} {Friend.LastName} can't be deleted.  As this friend is part of at least one meeting");
                return;
            }
            var result = _messageDialogService.ShowOkCancelDialog(
                "Do you really want to delete the friend?", "Question");
            if (result == MessageDialogResult.OK)
            {
                _friendRepository.Remove(Friend.Model);
                await _friendRepository.UpdateAsync();
                RaiseDetailDeletedEvent(Friend.Id);
            }
        }

        private void OnAddPhoneNumberExecute()
        {
            var newNumber = new Friend16PhoneNumberWrapper(new FriendPhoneNumber13());
            newNumber.PropertyChanged += Friend16PhoneNumberWrapper_PropertyChanged;
            PhoneNumbers.Add(newNumber);
            Friend.Model.PhoneNumbers.Add(newNumber.Model);
            newNumber.Number = ""; // Trigger validation :-)
        }

        private void OnRemovePhoneNumberExecute()
        {
            SelectedPhoneNumber.PropertyChanged -= Friend16PhoneNumberWrapper_PropertyChanged;
            _friendRepository.RemovePhoneNumber(SelectedPhoneNumber.Model);
            PhoneNumbers.Remove(SelectedPhoneNumber);
            SelectedPhoneNumber = null;
            HasChanges = _friendRepository.HasChanges();
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        private bool OnRemovePhoneNumberCanExecute()
        {
            return SelectedPhoneNumber != null;
        }
        private Domain.Friend15 CreateNewFriend()
        {
            var friend = new Domain.Friend15();
            _friendRepository.Add(friend);
            return friend;
        }
    }
}
