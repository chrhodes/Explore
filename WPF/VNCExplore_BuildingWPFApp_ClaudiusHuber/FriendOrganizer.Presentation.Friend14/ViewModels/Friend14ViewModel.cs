using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using Prism.Events;

using VNC.Core.Events;
using VNC.Core.Mvvm;

using VNCExplore_FriendOrganizer.Core.DomainServices;
using VNCExplore_FriendOrganizer.Core.Events;

namespace FriendOrganizer.Presentation.Friend14.ViewModels
{
    public class Friend14ViewModel : ViewModelBase, IFriend14ViewModel
    {
        private IFriendLookupDataService10 _dataService;
        private IEventAggregator _eventAggregator;

        private static int _instanceCountVM = 0;
        public ObservableCollection<NavigationItem14ViewModel> Friend14s { get; }

        public Friend14ViewModel(
                IFriendLookupDataService10 friendLookupDataService,
                IEventAggregator eventAggregator)
        {
            _instanceCountVM++;
            _dataService = friendLookupDataService;
            _eventAggregator = eventAggregator;
            Friend14s = new ObservableCollection<NavigationItem14ViewModel>();

            _eventAggregator.GetEvent<AfterDetailSavedEvent14>()
                .Subscribe(AfterDetailSaved);

            _eventAggregator.GetEvent<AfterDetailDeletedEvent14>()
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
            Friend14s.Clear();

            foreach (var item in lookup)
            {
                Friend14s.Add(
                    new NavigationItem14ViewModel(item.Id, item.DisplayMember, 
                    nameof(Friend14DetailViewModel),
                    _eventAggregator));
            }
        }

        private void AfterDetailSaved(AfterDetailSavedEventArgs args)
        {
            switch (args.ViewModelName)
            {
                case nameof(Friend14DetailViewModel):
                    var lookupItem = Friend14s.SingleOrDefault(l => l.Id == args.Id);

                    if (lookupItem == null)
                    {
                        Friend14s.Add(new NavigationItem14ViewModel(args.Id, args.DisplayMember,
                            nameof(Friend14DetailViewModel),
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
                case nameof(Friend14DetailViewModel):
                     var friend = Friend14s.SingleOrDefault(f => f.Id == args.Id);

                    if (friend != null)
                    {
                        Friend14s.Remove(friend);
                    }                   
                    break;
            }
        }
    }
}
