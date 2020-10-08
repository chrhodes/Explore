using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using FriendOrganizer.Presentation.Friend10.Views;
using Prism.Events;

using VNC.Core.Mvvm;

using VNCExplore_FriendOrganizer.Core.Events;

using VNCExplore_FriendOrganizer.Core.Events;
using VNCExplore_FriendOrganizer.Core.DomainServices;

namespace FriendOrganizer.Presentation.Friend10.ViewModels
{
    public class Friend10ViewModel : ViewModelBase, IFriend10ViewModel
    {
        private IFriendLookupDataService10 _dataService;
        private IEventAggregator _eventAggregator;

        private IFriend10Detail _friend10Detail;
        Func<IFriend10DetailViewModel> _friend10DetailViewModelCreator;

        private static int _instanceCountVM = 0;

        public Friend10ViewModel(
                IFriendLookupDataService10 lookupDataService,
                IEventAggregator eventAggregator,
                //IFriend10Detail friend10Detail,
                Func<IFriend10DetailViewModel> friend10DetailViewModelCreator)
        {
            _instanceCountVM++;
            _dataService = lookupDataService;
            _eventAggregator = eventAggregator;
            Friend10s = new ObservableCollection<NavigationItem10ViewModel>();

            _eventAggregator.GetEvent<AfterFriendSavedEvent08>()
                .Subscribe(AfterFriendSaved);

            _eventAggregator.GetEvent<OpenFriendDetailViewEvent10>()
                .Subscribe(OnOpenFriendDetailView);

            _friend10DetailViewModelCreator = friend10DetailViewModelCreator;
        }

        public int InstanceCountVM
        {
            get { return _instanceCountVM; }
            set { _instanceCountVM = value; }
        }

        private async void OnOpenFriendDetailView(int friendId)
        {
            // Create a new DetailViewModel and load the friend

            var newVM = _friend10DetailViewModelCreator();

            await newVM.LoadAsync(friendId);

            // Grab the detail view from the Module

            var fd = Friend10Module.friend10Detail;

            // and tell the view to use the new ViewModel

            fd.ViewModel = newVM;
        }

        private void AfterFriendSaved(AfterFriendSavedEventArgs08 obj)
        {
            var lookupItem = Friend10s.Single(l => l.Id == obj.Id);
            lookupItem.DisplayMember = obj.DisplayMember;
        }

        public async Task LoadAsync()
        {
            var lookup = await _dataService.GetFriendLookupAsync();
            Friend10s.Clear();

            foreach (var item in lookup)
            {
                Friend10s.Add(new NavigationItem10ViewModel(item.Id, item.DisplayMember));
            }
        }

        public ObservableCollection<NavigationItem10ViewModel> Friend10s { get; }

        //public IView View
        //{
        //    get;
        //    set;
        //}

        NavigationItem10ViewModel _selectedFriend10;

        public NavigationItem10ViewModel SelectedFriend10
        {
            get { return _selectedFriend10; }
            set
            {
                _selectedFriend10 = value;
                OnPropertyChanged();

                if (_selectedFriend10 != null)
                {
                    _eventAggregator.GetEvent<OpenFriendDetailViewEvent10>()
                        .Publish(_selectedFriend10.Id);
                }
            }
        }
    }
}
