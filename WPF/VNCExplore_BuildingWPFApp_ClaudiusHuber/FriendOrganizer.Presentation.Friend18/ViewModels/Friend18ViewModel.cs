using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using Prism.Events;

using VNC.Core.Events;
using VNC.Core.Mvvm;

using VNCExplore_FriendOrganizer.Core.DomainServices;
using VNCExplore_FriendOrganizer.Core.Events;

namespace FriendOrganizer.Presentation.Friend18.ViewModels
{
    public class Friend18ViewModel : ViewModelBase, IFriend18ViewModel
    {
        private IFriendLookupDataService10 _dataService;
        private IEventAggregator _eventAggregator;

        private static int _instanceCountVM = 0;
        public ObservableCollection<NavigationItem18ViewModel> Friend18s { get; }

        public Friend18ViewModel(
                IFriendLookupDataService10 friendLookupDataService,
                IEventAggregator eventAggregator)
        {
            _instanceCountVM++;
            _dataService = friendLookupDataService;
            _eventAggregator = eventAggregator;
            Friend18s = new ObservableCollection<NavigationItem18ViewModel>();

            _eventAggregator.GetEvent<AfterDetailSavedEvent18>()
                .Subscribe(AfterDetailSaved);

            _eventAggregator.GetEvent<AfterDetailDeletedEvent18>()
                .Subscribe(AfterDetailDeleted);
        } 

        public int InstanceCountVM
        {
            get { return _instanceCountVM; }
            set { _instanceCountVM = value; }
        }

        public async Task LoadAsync()
        {
            var lookup = await _dataService.GetFriendLookupAsync();
            Friend18s.Clear();

            foreach (var item in lookup)
            {
                Friend18s.Add(
                    new NavigationItem18ViewModel(item.Id, item.DisplayMember, 
                    nameof(Friend18DetailViewModel),
                    _eventAggregator));
            }
        }

        private void AfterDetailSaved(AfterDetailSavedEventArgs args)
        {
            switch (args.ViewModelName)
            {
                case nameof(Friend18DetailViewModel):
                    var lookupItem = Friend18s.SingleOrDefault(l => l.Id == args.Id);

                    if (lookupItem == null)
                    {
                        Friend18s.Add(new NavigationItem18ViewModel(args.Id, args.DisplayMember,
                            nameof(Friend18DetailViewModel),
                            _eventAggregator));
                    }
                    else
                    {
                        lookupItem.DisplayMember = args.DisplayMember;
                    }
                    break;
            }
        }

        private void AfterDetailDeleted(AfterDetailDeletedEventArgs args)
        {
            switch (args.ViewModelName)
            {
                case nameof(Friend18DetailViewModel):
                     var friend = Friend18s.SingleOrDefault(f => f.Id == args.Id);

                    if (friend != null)
                    {
                        Friend18s.Remove(friend);
                    }                   
                    break;
            }
        }
    }
}
