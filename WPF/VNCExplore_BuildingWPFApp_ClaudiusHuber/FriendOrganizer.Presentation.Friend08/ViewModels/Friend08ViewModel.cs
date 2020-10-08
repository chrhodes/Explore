using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using Prism.Events;

using VNC.Core.Mvvm;

using VNCExplore_FriendOrganizer.Core.Events;

using VNCExplore_FriendOrganizer.Core.DomainServices;
using VNCExplore_FriendOrganizer.Core.Events;

namespace FriendOrganizer.Presentation.Friend08.ViewModels
{
    public class Friend08ViewModel : ViewModelBase, IFriend08ViewModel, IViewModel
    {
        private IFriendLookupDataService06 _dataService;
        private IEventAggregator _eventAggregator;

        public Friend08ViewModel(
                IFriendLookupDataService06 Friend06LookupDataService,
                IEventAggregator eventAggregator)
        {
            _dataService = Friend06LookupDataService;
            _eventAggregator = eventAggregator;
            Friend08s = new ObservableCollection<NavigationItem08ViewModel>();
            _eventAggregator.GetEvent<AfterFriendSavedEvent08>().Subscribe(AfterFriendSaved);
        }

        private void AfterFriendSaved(AfterFriendSavedEventArgs08 obj)
        {
            var lookupItem = Friend08s.Single(l => l.Id == obj.Id);
            lookupItem.DisplayMember = obj.DisplayMember;
        }

        public async Task LoadAsync()
        {
            var lookup = await _dataService.GetFriendLookupAsync();
            Friend08s.Clear();

            foreach (var item in lookup)
            {
                Friend08s.Add(new NavigationItem08ViewModel(item.Id, item.DisplayMember));
            }
        }

        public ObservableCollection<NavigationItem08ViewModel> Friend08s { get; }

        public IView View
        {
            get;
            set;
        }

        NavigationItem08ViewModel _selectedFriend08;

        public NavigationItem08ViewModel SelectedFriend08
        {
            get { return _selectedFriend08; }
            set
            {
                _selectedFriend08 = value;
                OnPropertyChanged();

                if (_selectedFriend08 != null)
                {
                    _eventAggregator.GetEvent<OpenFriendDetailViewEvent08>()
                        .Publish(_selectedFriend08.Id);
                }
            }
        }
    }
}
