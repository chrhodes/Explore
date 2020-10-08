using System.Threading.Tasks;
using System.Windows.Input;

using FriendOrganizer.UI.Wrapper;

using Prism.Commands;
using Prism.Events;

using VNC.Core.Mvvm;

using VNCExplore_FriendOrganizer.Core.DomainServices;
using VNCExplore_FriendOrganizer.Core.Events;

namespace FriendOrganizer.Presentation.Friend10.ViewModels
{
    class Friend10DetailViewModel : ViewModelBase, IFriend10DetailViewModel
    {
        private IFriendRepository10 _friendRepository;
        private IEventAggregator _eventAggregator;
        private Friend10Wrapper _friend;

        private static int _instanceCountDVM = 0;

        public Friend10DetailViewModel(
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
        
        //private async void OnOpenFriendDetailView(int friendId)
        //{
        //    await LoadAsync(friendId);
        //}

        public async Task LoadAsync(int friendId)
        {
            var friend = await _friendRepository.FindByIdAsync(friendId);

            Friend = new Friend10Wrapper(friend);
            Friend.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(Friend.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        public Friend10Wrapper Friend
        {
            get { return _friend; }
            private set
            {
                _friend = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; }

        async void OnSaveExecute()
        {
            await _friendRepository.UpdateAsync();

            // Tell the Customer that we have updated something
            _eventAggregator.GetEvent<AfterFriendSavedEvent08>()
                .Publish(new AfterFriendSavedEventArgs08
                {
                    Id = Friend.Id,
                    DisplayMember = $"{Friend.FirstName} {Friend.LastName}"
                });
        }

        bool OnSaveCanExecute()
        {
            // TODO(crhodes)
            // Check in addition if friend has changes
            return Friend != null && ! Friend.HasErrors;
        }
    }
}
