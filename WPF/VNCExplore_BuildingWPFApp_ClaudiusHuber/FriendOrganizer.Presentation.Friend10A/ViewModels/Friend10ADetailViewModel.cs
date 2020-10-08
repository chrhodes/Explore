using System.Threading.Tasks;
using System.Windows.Input;

using FriendOrganizer.UI.Wrapper;

using Prism.Commands;
using Prism.Events;

using VNC.Core.Mvvm;

using VNCExplore_FriendOrganizer.Core.DomainServices;
using VNCExplore_FriendOrganizer.Core.Events;

namespace FriendOrganizer.Presentation.Friend10A.ViewModels
{
    class Friend10ADetailViewModel : ViewModelBase, IFriend10ADetailViewModel
    {

        private static int _instanceCountDVM = 100;
        private IEventAggregator _eventAggregator;
        private Friend10AWrapper _friend;
        private IFriendRepository10 _friendRepository;
        bool _hasChanges;

        public Friend10ADetailViewModel(
                IFriendRepository10 friendRepository,
                IEventAggregator eventAggregator)
        {
            _instanceCountDVM++;
            _friendRepository = friendRepository;
            _eventAggregator = eventAggregator;

            //_eventAggregator.GetEvent<OpenFriend07DetailViewEvent>()
            //    .Subscribe(OnOpenFriendDetailView);

            SaveCommand = new DelegateCommand(
                OnSaveExecute, OnSaveCanExecute);
        }

        public Friend10AWrapper Friend
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

        public ICommand SaveCommand { get; }

        public async Task LoadAsync(int friendId)
        {
            var friend = await _friendRepository.FindByIdAsync(friendId);

            Friend = new Friend10AWrapper(friend);
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
        }

        async void OnSaveExecute()
        {
            await _friendRepository.UpdateAsync();

            HasChanges = _friendRepository.HasChanges();

            // Tell the List that we have updated something
            _eventAggregator.GetEvent<AfterFriendSavedEvent08>()
                .Publish(new AfterFriendSavedEventArgs08
                {
                    Id = Friend.Id,
                    DisplayMember = $"{Friend.FirstName} {Friend.LastName}"
                });
        }

        bool OnSaveCanExecute()
        {
            return Friend != null && !Friend.HasErrors && HasChanges;
        }
    }
}
