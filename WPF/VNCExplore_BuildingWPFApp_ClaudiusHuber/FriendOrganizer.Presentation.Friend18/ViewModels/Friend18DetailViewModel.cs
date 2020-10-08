using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using FriendOrganizer.Domain;
using FriendOrganizer.Presentation.Friend18.ModelWrappers;
using FriendOrganizer.UI.ModelWrappers;

using Prism.Commands;
using Prism.Events;
using VNC.Core.Events;
using VNCExplore_FriendOrganizer.Core.DomainServices;
using VNCExplore_FriendOrganizer.Core.Events;
using VNCExplore_FriendOrganizer.Core.Services;

namespace FriendOrganizer.Presentation.Friend18.ViewModels
{
    internal class Friend18DetailViewModel : DetailViewModelBase18, IFriend18DetailViewModel
    {

        private static int _instanceCountDVM = 100;
        private Friend18Wrapper _friend;
        private Friend18PhoneNumberWrapper _selectedPhoneNumber;
        private IFriendRepository16 _friendRepository;
        readonly IProgrammingLanguageLookupDataService12 _programmingLanguageLookupDataService;

        public ICommand AddPhoneNumberCommand { get; }
        public ICommand RemovePhoneNumberCommand { get; }

        public ObservableCollection<LookupItem> ProgrammingLanguages { get; }
        public ObservableCollection<Friend18PhoneNumberWrapper> PhoneNumbers { get; }

        public Friend18DetailViewModel(
            IFriendRepository16 friendRepository,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService,
            IProgrammingLanguageLookupDataService12 programmingLanguageLookupDataService)
            : base(eventAggregator, messageDialogService)
        {
            _friendRepository = friendRepository;
            _programmingLanguageLookupDataService = programmingLanguageLookupDataService;

            eventAggregator.GetEvent<AfterCollectionSavedEvent19>()
                .Subscribe(AfterCollectionSaved);

            AddPhoneNumberCommand = new DelegateCommand(
                OnAddPhoneNumberExecute);

            RemovePhoneNumberCommand = new DelegateCommand(
                OnRemovePhoneNumberExecute, OnRemovePhoneNumberCanExecute);

            ProgrammingLanguages = new ObservableCollection<LookupItem>();
            PhoneNumbers = new ObservableCollection<Friend18PhoneNumberWrapper>();
        }

        private async void AfterCollectionSaved(AfterCollectionSavedEventArgs args)
        {
            if (args.ViewModelName == nameof(ProgrammingLanguage18DetailViewModel))
            {
                await LoadProgrammingLanguagesLookupAsync();
            }
        }

        public Friend18Wrapper Friend
        {
            get { return _friend; }
            private set
            {
                _friend = value;
                OnPropertyChanged();
            }
        }

        public Friend18PhoneNumberWrapper SelectedPhoneNumber
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

            Id = friend.Id;

            InitializeFriend(friend);

            InitializeFriendPhoneNumbers(friend.PhoneNumbers);

            await LoadProgrammingLanguagesLookupAsync();
        }

        //public override async Task LoadAsync(int? friendId)
        //{
        //    var friend = friendId.HasValue
        //        ? await _friendRepository.FindByIdAsync(friendId.Value)
        //        : CreateNewFriend();

        //    Id = friend.Id;

        //    InitializeFriend(friend);

        //    InitializeFriendPhoneNumbers(friend.PhoneNumbers);

        //    await LoadProgrammingLanguagesLookupAsync();
        //}

        private void InitializeFriend(Domain.Friend15 friend)
        {
            Friend = new Friend18Wrapper(friend);
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

                if (e.PropertyName == nameof(Friend.FirstName)
                    || e.PropertyName == nameof(Friend.LastName))
                {
                    SetTitle();
                }
            };

            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();

            // Little trick to trigger the validation when creating new entries
            if (Friend.Id == 0)
            {
                Friend.FirstName = "";
            }

            SetTitle();
        }

        private void SetTitle()
        {
            Title = $"{Friend.FirstName} {Friend.LastName}";
        }

        private void InitializeFriendPhoneNumbers(ICollection<FriendPhoneNumber13> phoneNumbers)
        {
            foreach (var wrapper in PhoneNumbers)
            {
                wrapper.PropertyChanged -= Friend18PhoneNumberWrapper_PropertyChanged;
            }
            PhoneNumbers.Clear();
            foreach (var friendPhoneNumber in phoneNumbers)
            {
                var wrapper = new Friend18PhoneNumberWrapper(friendPhoneNumber);
                PhoneNumbers.Add(wrapper);
                wrapper.PropertyChanged += Friend18PhoneNumberWrapper_PropertyChanged;
            }
        }

        private void Friend18PhoneNumberWrapper_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!HasChanges)
            {
                HasChanges = _friendRepository.HasChanges();
            }
            if (e.PropertyName == nameof(Friend18PhoneNumberWrapper.HasErrors))
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
            Id = Friend.Id;
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
                MessageDialogService.ShowInfoDialog(
                    $"{Friend.FirstName} {Friend.LastName} can't be deleted.  As this friend is part of at least one meeting");
                return;
            }
            var result = MessageDialogService.ShowOkCancelDialog(
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
            var newNumber = new Friend18PhoneNumberWrapper(new FriendPhoneNumber13());
            newNumber.PropertyChanged += Friend18PhoneNumberWrapper_PropertyChanged;
            PhoneNumbers.Add(newNumber);
            Friend.Model.PhoneNumbers.Add(newNumber.Model);
            newNumber.Number = ""; // Trigger validation :-)
        }

        private void OnRemovePhoneNumberExecute()
        {
            SelectedPhoneNumber.PropertyChanged -= Friend18PhoneNumberWrapper_PropertyChanged;
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
