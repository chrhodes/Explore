using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Prism.Events;

using VNC.Core.Mvvm;

using VNCExplore_FriendOrganizer.Core.Events;

using VNCExplore_FriendOrganizer.Core.DomainServices;

namespace FriendOrganizer.Presentation.Friend07.ViewModels
{
    public class Friend07ViewModel : ViewModelBase, IFriend07ViewModel, IViewModel
    {
        private IFriendLookupDataService06 _dataService;
        private IEventAggregator _eventAggregator;

        public Friend07ViewModel(
                IFriendLookupDataService06 Friend06LookupDataService,
                IEventAggregator eventAggregator)
        {
            _dataService = Friend06LookupDataService;
            _eventAggregator = eventAggregator;
            Friend07s = new ObservableCollection<NavigationItemViewModel>();
        }

        public async Task LoadAsync()
        {
            var lookup = await _dataService.GetFriendLookupAsync();
            Friend07s.Clear();

            foreach (var item in lookup)
            {
                Friend07s.Add(new NavigationItemViewModel(item.Id, item.DisplayMember));
            }
        }

        public ObservableCollection<NavigationItemViewModel> Friend07s { get; }

        public IView View
        {
            get;
            set;
        }

        NavigationItemViewModel _selectedFriend07;

        public NavigationItemViewModel SelectedFriend07
        {
            get { return _selectedFriend07; }
            set
            {
                _selectedFriend07 = value;
                OnPropertyChanged();

                if (_selectedFriend07 != null)
                {
                    _eventAggregator.GetEvent<OpenFriendDetailViewEvent07>()
                        .Publish(_selectedFriend07.Id);
                }
            }
        }
    }
}
