using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using FriendOrganizer.Presentation.Friend10A.Views;
using Prism.Events;

using VNC.Core.Mvvm;

using VNCExplore_FriendOrganizer.Core.Events;

using VNCExplore_FriendOrganizer.Core.DomainServices;
using VNCExplore_FriendOrganizer.Core.Events;

namespace FriendOrganizer.Presentation.Friend10A.ViewModels
{
    public class Friend10AViewModel : ViewModelBase, IFriend10AViewModel
    {
        private IFriendLookupDataService10 _dataService;
        private IEventAggregator _eventAggregator;

        //private IFriend10ADetail _friend10ADetail;
        //Func<IFriend10ADetailViewModel> _friend10DetailViewModelCreator;

        private static int _instanceCountVM = 0;

        public Friend10AViewModel(
                IFriendLookupDataService10 lookupDataService
                ,IEventAggregator eventAggregator
                //,IFriend10ADetail friend10ADetail
                //Func<IFriend10ADetailViewModel> friend10DetailViewModelCreator
            )
        {
            _instanceCountVM++;
            _dataService = lookupDataService;
            _eventAggregator = eventAggregator;
            Friend10As = new ObservableCollection<NavigationItem10AViewModel>();

            _eventAggregator.GetEvent<AfterFriendSavedEvent08>()
                .Subscribe(AfterFriendSaved);

            //_eventAggregator.GetEvent<OpenFriendDetailViewEvent10A>()
            //    .Subscribe(OnOpenFriendDetailView);

            ////_friend10ADetail = friend10ADetail;
            //_friend10DetailViewModelCreator = friend10DetailViewModelCreator;
        }
        
        //public IFriend10ADetail Friend10ADetail
        //{
        //    get { return _friend10ADetail; }
        //    set { _friend10ADetail = value; }
        //}

        public int InstanceCountVM
        {
            get { return _instanceCountVM; }
            set { _instanceCountVM = value; }
        }

        //private async void OnOpenFriendDetailView(int friendId)
        //{
        //    // Create a new DetailViewModel and load the friend

        //    var newVM = _friend10DetailViewModelCreator();
            
        //    await newVM.LoadAsync(friendId);

        //    newVM.View = Friend10ADetail;

        //    //newVM.View = ((Views.Friend10A)View).Friend10ADetail;

        //    //var fd = _friend10ADetail as Friend10ADetail;

        //    //fd.ViewModel = vm;
        //}

        private void AfterFriendSaved(AfterFriendSavedEventArgs08 obj)
        {
            var lookupItem = Friend10As.Single(l => l.Id == obj.Id);
            lookupItem.DisplayMember = obj.DisplayMember;
        }

        public async Task LoadAsync()
        {
            var lookup = await _dataService.GetFriendLookupAsync();
            Friend10As.Clear();

            foreach (var item in lookup)
            {
                Friend10As.Add(
                    new NavigationItem10AViewModel(item.Id, item.DisplayMember, _eventAggregator));
            }
        }

        public ObservableCollection<NavigationItem10AViewModel> Friend10As { get; }

        //public IView View
        //{
        //    get;
        //    set;
        //}

        NavigationItem10AViewModel _selectedFriend10A;

        public NavigationItem10AViewModel SelectedFriend10A
        {
            get { return _selectedFriend10A; }
            set
            {
                _selectedFriend10A = value;
                OnPropertyChanged();

                if (_selectedFriend10A != null)
                {
                    _eventAggregator.GetEvent<OpenFriendDetailViewEvent10A>()
                        .Publish(_selectedFriend10A.Id);
                }
            }
        }
    }
}
