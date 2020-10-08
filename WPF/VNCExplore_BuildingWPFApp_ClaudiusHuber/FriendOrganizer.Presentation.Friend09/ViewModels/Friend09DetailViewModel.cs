using System.Threading.Tasks;
using System.Windows.Input;

using FriendOrganizer.UI.Wrapper;
using Prism.Commands;
using Prism.Events;
using System;
using VNC.Core.Mvvm;

using VNCExplore_FriendOrganizer.Core.Events;

using VNCExplore_FriendOrganizer.Core.DomainServices;
using VNCExplore_FriendOrganizer.Core.Events;


namespace FriendOrganizer.Presentation.Friend09.ViewModels
{
    class Friend09DetailViewModel : ViewModelBase, IFriend09DetailViewModel
    {
        private IFriendDataService08 _dataService;
        private IEventAggregator _eventAggregator;
        private Friend09Wrapper _friend;

        private static int _instanceCountDVM = 0;

        public Friend09DetailViewModel(
                IFriendDataService08 dataService,
                IEventAggregator eventAggregator)
        {
            _instanceCountDVM++;
            _dataService = dataService;
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<OpenFriendDetailViewEvent09>()
                .Subscribe(OnOpenFriendDetailView);

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
        

        private async void OnOpenFriendDetailView(int typeId)
        {
            await LoadAsync(typeId);
        }

        public async Task LoadAsync(int friendId)
        {
            var friend = await _dataService.FindByIdAsync(friendId);

            Friend = new Friend09Wrapper(friend);
            Friend.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(Friend.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        public Friend09Wrapper Friend
        {
            get { return _friend; }
            private set
            {
                _friend = value;
                OnPropertyChanged();
            }
        }

        //public async Task LoadAsync(int id)
        //{
        //    Friend = await _dataService.GetByIdAsync(id);
        //}

        //private Domain.Friend05 _friend;

        //public Domain.Friend05 Friend
        //{
        //    get { return _friend; }
        //    private set
        //    {
        //        _friend = value;
        //        OnPropertyChanged();
        //    }
        //}

        public ICommand SaveCommand { get; }

        async void OnSaveExecute()
        {
            await _dataService.UpdateAsync(Friend.Model);

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
