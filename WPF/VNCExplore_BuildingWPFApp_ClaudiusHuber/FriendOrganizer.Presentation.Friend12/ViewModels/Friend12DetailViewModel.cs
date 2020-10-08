using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

using FriendOrganizer.Domain;
using FriendOrganizer.UI.Wrapper;

using Prism.Commands;
using Prism.Events;

using VNC.Core.Mvvm;

using VNCExplore_FriendOrganizer.Core.DomainServices;
using VNCExplore_FriendOrganizer.Core.Events;

namespace FriendOrganizer.Presentation.Friend12.ViewModels
{
    internal class Friend12DetailViewModel : ViewModelBase, IFriend12DetailViewModel
    {

        private static int _instanceCountDVM = 100;
        private IEventAggregator _eventAggregator;
        private Friend12Wrapper _friend;
        private IFriendRepository12 _friendRepository;
        private bool _hasChanges;
        readonly IProgrammingLanguageLookupDataService12 _programmingLanguageLookupDataService;

        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }

        public ObservableCollection<LookupItem> ProgrammingLanguages { get; }

        public Friend12DetailViewModel(
            IFriendRepository12 friendRepository,
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

            ProgrammingLanguages = new ObservableCollection<LookupItem>();
        }

        public Friend12Wrapper Friend
        {
            get { return _friend; }
            private set
            {
                _friend = value;
                OnPropertyChanged();
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

            await LoadProgrammingLanguagesLookupAsync();
        }

        private void InitializeFriend(Domain.Friend12 friend)
        {
            Friend = new Friend12Wrapper(friend);
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
            _eventAggregator.GetEvent<AfterFriendSavedEvent12>()
                .Publish(new AfterFriendSavedEventArgs12
                {
                    Id = Friend.Id,
                    DisplayMember = $"{Friend.FirstName} {Friend.LastName}"
                });
        }

        private bool OnSaveCanExecute()
        {
            return Friend != null && !Friend.HasErrors && HasChanges;
        }

        private bool OnDeleteCanExecute()
        {
            return true;
        }
        private async void OnDeleteExecute()
        {
            _friendRepository.Remove(Friend.Model);
           await  _friendRepository.UpdateAsync();

            _eventAggregator.GetEvent<AfterFriendDeletedEvent12>()
                .Publish(Friend.Id);
        }

        private Domain.Friend12 CreateNewFriend()
        {
            var friend = new Domain.Friend12();
            _friendRepository.Add(friend);
            return friend;
        }
    }
}
