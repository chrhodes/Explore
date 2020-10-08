using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using FriendOrganizer.Presentation.Friend10B.Views;
using Prism.Events;

using VNC.Core.Mvvm;

using VNCExplore_FriendOrganizer.Core.Events;

using VNCExplore_FriendOrganizer.Core.DomainServices;
using VNCExplore_FriendOrganizer.Core.Events;

namespace FriendOrganizer.Presentation.Friend10B.ViewModels
{
    public class Friend10BViewModel : ViewModelBase, IFriend10BViewModel
    {
        private IFriendLookupDataService10 _dataService;
        private IEventAggregator _eventAggregator;

        //private IFriend10BDetail _friend10BDetail;
        //Func<IFriend10BDetailViewModel> _friend10DetailViewModelCreator;

        private static int _instanceCountVM = 0;

        public Friend10BViewModel(
                IFriendLookupDataService10 friendLookupDataService
                ,IEventAggregator eventAggregator
                //,IFriend10BDetail friend10BDetail
                //Func<IFriend10BDetailViewModel> friend10DetailViewModelCreator
            )
        {
            _instanceCountVM++;
            _dataService = friendLookupDataService;
            _eventAggregator = eventAggregator;
            Friend10Bs = new ObservableCollection<NavigationItem10BViewModel>();

            _eventAggregator.GetEvent<AfterFriendSavedEvent08>()
                .Subscribe(AfterFriendSaved);

            //_eventAggregator.GetEvent<OpenFriendDetailViewEvent10B>()
            //    .Subscribe(OnOpenFriendDetailView);

            ////_friend10BDetail = friend10BDetail;
            //_friend10DetailViewModelCreator = friend10DetailViewModelCreator;
        }
        
        //public IFriend10BDetail Friend10BDetail
        //{
        //    get { return _friend10BDetail; }
        //    set { _friend10BDetail = value; }
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

        //    newVM.View = Friend10BDetail;

        //    //newVM.View = ((Views.Friend10B)View).Friend10BDetail;

        //    //var fd = _friend10BDetail as Friend10BDetail;

        //    //fd.ViewModel = vm;
        //}

        private void AfterFriendSaved(AfterFriendSavedEventArgs08 obj)
        {
            var lookupItem = Friend10Bs.Single(l => l.Id == obj.Id);
            lookupItem.DisplayMember = obj.DisplayMember;
        }

        public async Task LoadAsync()
        {
            var lookup = await _dataService.GetFriendLookupAsync();
            Friend10Bs.Clear();

            foreach (var item in lookup)
            {
                Friend10Bs.Add(
                    new NavigationItem10BViewModel(item.Id, item.DisplayMember, _eventAggregator));
            }
        }

        public ObservableCollection<NavigationItem10BViewModel> Friend10Bs { get; }

        //public IView View
        //{
        //    get;
        //    set;
        //}

        //NavigationItem10BViewModel _selectedFriend10B;

        //public NavigationItem10BViewModel SelectedFriend10B
        //{
        //    get { return _selectedFriend10B; }
        //    set
        //    {
        //        _selectedFriend10B = value;
        //        OnPropertyChanged();

        //        if (_selectedFriend10B != null)
        //        {
        //            _eventAggregator.GetEvent<OpenFriendDetailViewEvent10B>()
        //                .Publish(_selectedFriend10B.Id);
        //        }
        //    }
        //}
    }
}
