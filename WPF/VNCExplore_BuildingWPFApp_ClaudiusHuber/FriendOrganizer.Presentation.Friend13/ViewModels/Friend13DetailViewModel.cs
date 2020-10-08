using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using FriendOrganizer.Domain;
using FriendOrganizer.Presentation.Friend13.Wrapper;
using FriendOrganizer.UI.Wrapper;

using Prism.Commands;
using Prism.Events;

using VNC.Core.Mvvm;

using VNCExplore_FriendOrganizer.Core.DomainServices;
using VNCExplore_FriendOrganizer.Core.Events;

namespace FriendOrganizer.Presentation.Friend13.ViewModels
{
    internal class Friend13DetailViewModel : ViewModelBase, IFriend13DetailViewModel
    {

        private static int _instanceCountDVM = 100;
        private IEventAggregator _eventAggregator;
        private Friend13Wrapper _friend;
        private Friend13PhoneNumberWrapper _selectedPhoneNumber;
        private IFriendRepository13 _friendRepository;
        private bool _hasChanges;
        readonly IProgrammingLanguageLookupDataService12 _programmingLanguageLookupDataService;

        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }

        public ICommand AddPhoneNumberCommand { get; }
        public ICommand RemovePhoneNumberCommand { get; }

        public ObservableCollection<LookupItem> ProgrammingLanguages { get; }
        public ObservableCollection<Friend13PhoneNumberWrapper> PhoneNumbers { get; }

        public Friend13DetailViewModel(
            IFriendRepository13 friendRepository,
            IEventAggregator eventAggregator,
            IProgrammingLanguageLookupDataService12 programmingLanguageLookupDataService)
        {
            _instanceCountDVM++;
            _friendRepository = friendRepository;
            _eventAggregator = eventAggregator;
            _programmingLanguageLookupDataService = programmingLanguageLookupDataService;

            SaveCommand = new DelegateCommand(
                OnSaveExecute, OnSaveCanExecute);

            DeleteCommand = new DelegateCommand(
                OnDeleteExecute, OnDeleteCanExecute);

            AddPhoneNumberCommand = new DelegateCommand(
                OnAddPhoneNumberExecute);
            RemovePhoneNumberCommand = new DelegateCommand(
                OnRemovePhoneNumberExecute, OnRemovePhoneNumberCanExecute);

            ProgrammingLanguages = new ObservableCollection<LookupItem>();
            PhoneNumbers = new ObservableCollection<Friend13PhoneNumberWrapper>();
        }

        public Friend13Wrapper Friend
        {
            get { return _friend; }
            private set
            {
                _friend = value;
                OnPropertyChanged();
            }
        }

        public Friend13PhoneNumberWrapper SelectedPhoneNumber
        {
            get { return _selectedPhoneNumber; }
            set
            {
                _selectedPhoneNumber = value;
                OnPropertyChanged();
                ((DelegateCommand)RemovePhoneNumberCommand).RaiseCanExecuteChanged();
            }
        }

        public bool HasChanges
        {
            get { return _hasChanges; }
            set
            {
                if (_hasChanges != value)
                {
                    _hasChanges = value;
                    OnPropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public int InstanceCountDVM
        {
            get { return _instanceCountDVM; }
            set
            {
                if (_instanceCountDVM == value)
                    return;
                _instanceCountDVM = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadAsync(int? friendId)
        {
            var friend = friendId.HasValue
                ? await _friendRepository.FindByIdAsync(friendId.Value)
                : CreateNewFriend();

            InitializeFriend(friend);

            InitializeFriendPhoneNumbers(friend.PhoneNumbers);

            await LoadProgrammingLanguagesLookupAsync();
        }

        private void InitializeFriend(Domain.Friend13 friend)
        {
            Friend = new Friend13Wrapper(friend);
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
                wrapper.PropertyChanged -= Friend13PhoneNumberWrapper_PropertyChanged;
            }
            PhoneNumbers.Clear();
            foreach (var friendPhoneNumber in phoneNumbers)
            {
                var wrapper = new Friend13PhoneNumberWrapper(friendPhoneNumber);
                PhoneNumbers.Add(wrapper);
                wrapper.PropertyChanged += Friend13PhoneNumberWrapper_PropertyChanged;
            }
        }

        private void Friend13PhoneNumberWrapper_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!HasChanges)
            {
                HasChanges = _friendRepository.HasChanges();
            }
            if (e.PropertyName == nameof(Friend13PhoneNumberWrapper.HasErrors))
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

        private async void OnSaveExecute()
        {
            await _friendRepository.UpdateAsync();

            HasChanges = _friendRepository.HasChanges();

            // Tell the List that we have updated something
            _eventAggregator.GetEvent<AfterFriendSavedEvent13>()
                .Publish(new AfterFriendSavedEventArgs13
                {
                    Id = Friend.Id,
                    DisplayMember = $"{Friend.FirstName} {Friend.LastName}"
                });
        }

        private bool OnSaveCanExecute()
        {
            return Friend != null 
                && !Friend.HasErrors
                && PhoneNumbers.All(pn => !pn.HasErrors)
                && HasChanges;
        }

        private bool OnDeleteCanExecute()
        {
            return true;
        }
        private async void OnDeleteExecute()
        {
            _friendRepository.Remove(Friend.Model);
           await  _friendRepository.UpdateAsync();

            _eventAggregator.GetEvent<AfterFriendDeletedEvent13>()
                .Publish(Friend.Id);
        }

        private void OnAddPhoneNumberExecute()
        {
            var newNumber = new Friend13PhoneNumberWrapper(new FriendPhoneNumber13());
            newNumber.PropertyChanged += Friend13PhoneNumberWrapper_PropertyChanged;
            PhoneNumbers.Add(newNumber);
            Friend.Model.PhoneNumbers.Add(newNumber.Model);
            newNumber.Number = ""; // Trigger validation :-)
        }

        private void OnRemovePhoneNumberExecute()
        {
            SelectedPhoneNumber.PropertyChanged -= Friend13PhoneNumberWrapper_PropertyChanged;
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
        private Domain.Friend13 CreateNewFriend()
        {
            var friend = new Domain.Friend13();
            _friendRepository.Add(friend);
            return friend;
        }
    }
}
