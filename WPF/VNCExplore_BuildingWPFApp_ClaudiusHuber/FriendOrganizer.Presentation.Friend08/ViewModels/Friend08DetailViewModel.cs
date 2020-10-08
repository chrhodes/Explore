using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using Prism.Events;

using VNC.Core.Mvvm;

using VNCExplore_FriendOrganizer.Core.Events;

using VNCExplore_FriendOrganizer.Core.DomainServices;
using VNCExplore_FriendOrganizer.Core.Events;

namespace FriendOrganizer.Presentation.Friend08.ViewModels
{
    class Friend08DetailViewModel : ViewModelBase, IFriend08DetailViewModel
    {
        private IFriendDataService08 _dataService;
        private IEventAggregator _eventAggregator;

        public Friend08DetailViewModel(
                IFriendDataService08 dataService,
                IEventAggregator eventAggregator)
        {
            _dataService = dataService;
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<OpenFriendDetailViewEvent08>()
                .Subscribe(OnOpenFriendDetailView);

            SaveCommand = new DelegateCommand(
                OnSaveExecute, OnSaveCanExecute);
        }

        private async void OnOpenFriendDetailView(int typeId)
        {
            await LoadAsync(typeId);
        }

        public async Task LoadAsync(int id)
        {
            Friend = await _dataService.FindByIdAsync(id);
        }

        private Domain.Friend05 _friend;

        public Domain.Friend05 Friend
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
            await _dataService.UpdateAsync(Friend);

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
            // Check if Customer is valid
            return true;
        }
    }
}
