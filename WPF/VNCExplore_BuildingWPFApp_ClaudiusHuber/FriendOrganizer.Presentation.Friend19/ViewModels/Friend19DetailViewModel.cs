using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using FriendOrganizer.Domain;
using FriendOrganizer.Presentation.Friend19.ModelWrappers;
using FriendOrganizer.UI.ModelWrappers;

using Prism.Commands;
using Prism.Events;
using VNC.Core.Events;
using VNCExplore_FriendOrganizer.Core.DomainServices;
using VNCExplore_FriendOrganizer.Core.Events;
using VNCExplore_FriendOrganizer.Core.Services;

namespace FriendOrganizer.Presentation.Friend19.ViewModels
{
    internal class Friend19DetailViewModel : DetailViewModelBase19, IFriend19DetailViewModel
    {

        private static int _instanceCountDVM = 100;
        private Friend19Wrapper _friend;
        private Friend19PhoneNumberWrapper _selectedPhoneNumber;
        private IFriendRepository19 _friendRepository;
        readonly IProgrammingLanguageLookupDataService12 _programmingLanguageLookupDataService;

        public ICommand AddPhoneNumberCommand { get; }
        public ICommand RemovePhoneNumberCommand { get; }

        public ObservableCollection<LookupItem> ProgrammingLanguages { get; }
        public ObservableCollection<Friend19PhoneNumberWrapper> PhoneNumbers { get; }

        public Friend19DetailViewModel(
            IFriendRepository19 friendRepository,
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
            PhoneNumbers = new ObservableCollection<Friend19PhoneNumberWrapper>();
        }

        private async void AfterCollectionSaved(AfterCollectionSavedEventArgs args)
        {
            if (args.ViewModelName == nameof(ProgrammingLanguage19DetailViewModel))
            {
                await LoadProgrammingLanguagesLookupAsync();
            }
        }

        public Friend19Wrapper Friend
        {
            get { return _friend; }
            private set
            {
                _friend = value;
                OnPropertyChanged();
            }
        }

        public Friend19PhoneNumberWrapper SelectedPhoneNumber
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

        private void InitializeFriend(Domain.Friend19 friend)
        {
            Friend = new Friend19Wrapper(friend);
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
                wrapper.PropertyChanged -= Friend19PhoneNumberWrapper_PropertyChanged;
            }
            PhoneNumbers.Clear();
            foreach (var friendPhoneNumber in phoneNumbers)
            {
                var wrapper = new Friend19PhoneNumberWrapper(friendPhoneNumber);
                PhoneNumbers.Add(wrapper);
                wrapper.PropertyChanged += Friend19PhoneNumberWrapper_PropertyChanged;
            }
        }

        private void Friend19PhoneNumberWrapper_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!HasChanges)
            {
                HasChanges = _friendRepository.HasChanges();
            }
            if (e.PropertyName == nameof(Friend19PhoneNumberWrapper.HasErrors))
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
            await SaveWithOptimisticConcurrencyAsync(_friendRepository.UpdateAsync,
              () =>
              {
                  HasChanges = _friendRepository.HasChanges();
                  Id = Friend.Id;
                  RaiseDetailSavedEvent(Friend.Id, $"{Friend.FirstName} {Friend.LastName}");
              });

            // Refactored into base class

            //try
            //{
            //    await _friendRepository.UpdateAsync();
            //}
            //catch (DbUpdateConcurrencyException ex)
            //{
            //    var databaseValues = ex.Entries.Single().GetDatabaseValues();

            //    if (databaseValues == null)
            //    {
            //        MessageDialogService.ShowInfoDialog(
            //            "The entity has been deleted by another user.  Cannot continue.");
            //        RaiseDetailDeletedEvent(Id);
            //        return;
            //    }

            //    var result = MessageDialogService.ShowOkCancelDialog("The entity has been changed in" +
            //        " the meantime by someone else.  Click OK to save your changes anyway, click Cancel" +
            //        " to reload th entity from the database.", "Question");

            //    if (result == MessageDialogResult.OK)   // Client Wins
            //    {
            //        // Update the original values with database-values
            //        var entry = ex.Entries.Single();
            //        entry.OriginalValues.SetValues(entry.GetDatabaseValues());
            //        await _friendRepository.UpdateAsync();
            //    }
            //    else  // Database Wins
            //    {
            //        // Reload entity from database
            //        await ex.Entries.Single().ReloadAsync();
            //        await LoadAsync(Friend.Id);
            //    }
            //}            

            //HasChanges = _friendRepository.HasChanges();
            //Id = Friend.Id;
            //RaiseDetailSavedEvent(Friend.Id, $"{Friend.FirstName} {Friend.LastName}");
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
            var newNumber = new Friend19PhoneNumberWrapper(new FriendPhoneNumber13());
            newNumber.PropertyChanged += Friend19PhoneNumberWrapper_PropertyChanged;
            PhoneNumbers.Add(newNumber);
            Friend.Model.PhoneNumbers.Add(newNumber.Model);
            newNumber.Number = ""; // Trigger validation :-)
        }

        private void OnRemovePhoneNumberExecute()
        {
            SelectedPhoneNumber.PropertyChanged -= Friend19PhoneNumberWrapper_PropertyChanged;
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
        private Domain.Friend19 CreateNewFriend()
        {
            var friend = new Domain.Friend19();
            _friendRepository.Add(friend);
            return friend;
        }
    }
}
