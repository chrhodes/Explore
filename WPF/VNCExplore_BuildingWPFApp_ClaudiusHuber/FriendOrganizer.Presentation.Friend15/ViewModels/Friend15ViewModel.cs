using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using Prism.Events;

using VNC.Core.Events;
using VNC.Core.Mvvm;

using VNCExplore_FriendOrganizer.Core.DomainServices;
using VNCExplore_FriendOrganizer.Core.Events;

namespace FriendOrganizer.Presentation.Friend15.ViewModels
{
    public class Friend15ViewModel : ViewModelBase, IFriend15ViewModel
    {
        private IFriendLookupDataService10 _dataService;
        private IEventAggregator _eventAggregator;

        private static int _instanceCountVM = 0;
        public ObservableCollection<NavigationItem15ViewModel> Friend15s { get; }

        public Friend15ViewModel(
                IFriendLookupDataService10 friendLookupDataService,
                IEventAggregator eventAggregator)
        {
            _instanceCountVM++;
            _dataService = friendLookupDataService;
            _eventAggregator = eventAggregator;
            Friend15s = new ObservableCollection<NavigationItem15ViewModel>();

            _eventAggregator.GetEvent<AfterDetailSavedEvent15>()
                .Subscribe(AfterDetailSaved);

            _eventAggregator.GetEvent<AfterDetailDeletedEvent15>()
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
            Friend15s.Clear();

            foreach (var item in lookup)
            {
                Friend15s.Add(
                    new NavigationItem15ViewModel(item.Id, item.DisplayMember, 
                    nameof(Friend15DetailViewModel),
                    _eventAggregator));
            }
        }

        private void AfterDetailSaved(AfterDetailSavedEventArgs args)
        {
            switch (args.ViewModelName)
            {
                case nameof(Friend15DetailViewModel):
                    var lookupItem = Friend15s.SingleOrDefault(l => l.Id == args.Id);

                    if (lookupItem == null)
                    {
                        Friend15s.Add(new NavigationItem15ViewModel(args.Id, args.DisplayMember,
                            nameof(Friend15DetailViewModel),
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
                case nameof(Friend15DetailViewModel):
                     var friend = Friend15s.SingleOrDefault(f => f.Id == args.Id);

                    if (friend != null)
                    {
                        Friend15s.Remove(friend);
                    }                   
                    break;
            }
        }
    }
}
