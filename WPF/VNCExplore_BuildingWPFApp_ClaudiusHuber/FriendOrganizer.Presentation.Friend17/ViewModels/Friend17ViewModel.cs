using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using Prism.Events;

using VNC.Core.Events;
using VNC.Core.Mvvm;

using VNCExplore_FriendOrganizer.Core.DomainServices;
using VNCExplore_FriendOrganizer.Core.Events;

namespace FriendOrganizer.Presentation.Friend17.ViewModels
{
    public class Friend17ViewModel : ViewModelBase, IFriend17ViewModel
    {
        private IFriendLookupDataService10 _dataService;
        private IEventAggregator _eventAggregator;

        private static int _instanceCountVM = 0;
        public ObservableCollection<NavigationItem17ViewModel> Friend17s { get; }

        public Friend17ViewModel(
                IFriendLookupDataService10 friendLookupDataService,
                IEventAggregator eventAggregator)
        {
            _instanceCountVM++;
            _dataService = friendLookupDataService;
            _eventAggregator = eventAggregator;
            Friend17s = new ObservableCollection<NavigationItem17ViewModel>();

            _eventAggregator.GetEvent<AfterDetailSavedEvent17>()
                .Subscribe(AfterDetailSaved);

            _eventAggregator.GetEvent<AfterDetailDeletedEvent17>()
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
            Friend17s.Clear();

            foreach (var item in lookup)
            {
                Friend17s.Add(
                    new NavigationItem17ViewModel(item.Id, item.DisplayMember, 
                    nameof(Friend17DetailViewModel),
                    _eventAggregator));
            }
        }

        private void AfterDetailSaved(AfterDetailSavedEventArgs args)
        {
            switch (args.ViewModelName)
            {
                case nameof(Friend17DetailViewModel):
                    var lookupItem = Friend17s.SingleOrDefault(l => l.Id == args.Id);

                    if (lookupItem == null)
                    {
                        Friend17s.Add(new NavigationItem17ViewModel(args.Id, args.DisplayMember,
                            nameof(Friend17DetailViewModel),
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
                case nameof(Friend17DetailViewModel):
                     var friend = Friend17s.SingleOrDefault(f => f.Id == args.Id);

                    if (friend != null)
                    {
                        Friend17s.Remove(friend);
                    }                   
                    break;
            }
        }
    }
}
