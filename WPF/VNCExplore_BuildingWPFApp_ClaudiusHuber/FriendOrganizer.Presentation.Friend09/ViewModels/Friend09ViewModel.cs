using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using Prism.Events;
using System;
using VNC.Core.Mvvm;

using VNCExplore_FriendOrganizer.Core.Events;

using VNCExplore_FriendOrganizer.Core.DomainServices;
using VNCExplore_FriendOrganizer.Core.Events;

namespace FriendOrganizer.Presentation.Friend09.ViewModels
{
    public class Friend09ViewModel : ViewModelBase, IFriend09ViewModel, IViewModel
    {
        private IFriendLookupDataService06 _dataService;
        private IEventAggregator _eventAggregator;
        private static int _instanceCountVM = 0;

        public Friend09ViewModel(
                IFriendLookupDataService06 Friend06LookupDataService,
                IEventAggregator eventAggregator)
        {
            _dataService = Friend06LookupDataService;
            _eventAggregator = eventAggregator;
            Friend09s = new ObservableCollection<NavigationItem08ViewModel>();
            _eventAggregator.GetEvent<AfterFriendSavedEvent08>().Subscribe(AfterFriendSaved);

            _instanceCountVM++;
        }

        private void AfterFriendSaved(AfterFriendSavedEventArgs08 obj)
        {
            var lookupItem = Friend09s.Single(l => l.Id == obj.Id);
            lookupItem.DisplayMember = obj.DisplayMember;
        }

        
        public int InstanceCountVM
        {
            get { return _instanceCountVM; }
            set
            {
                if (_instanceCountVM == value)
                    return;
                _instanceCountVM = value;
                OnPropertyChanged();
            }
        }
        

        public async Task LoadAsync()
        {
            var lookup = await _dataService.GetFriendLookupAsync();
            Friend09s.Clear();

            foreach (var item in lookup)
            {
                Friend09s.Add(new NavigationItem08ViewModel(item.Id, item.DisplayMember));
            }
        }

        public ObservableCollection<NavigationItem08ViewModel> Friend09s { get; }

        public IView View
        {
            get;
            set;
        }

        NavigationItem08ViewModel _selectedFriend09;

        public NavigationItem08ViewModel SelectedFriend09
        {
            get { return _selectedFriend09; }
            set
            {
                _selectedFriend09 = value;
                OnPropertyChanged();

                if (_selectedFriend09 != null)
                {
                    _eventAggregator.GetEvent<OpenFriendDetailViewEvent09>()
                        .Publish(_selectedFriend09.Id);
                }
            }
        }
    }
}
