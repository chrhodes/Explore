using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using Prism.Events;

using VNC.Core.Events;
using VNC.Core.Mvvm;

using VNCExplore_FriendOrganizer.Core.DomainServices;
using VNCExplore_FriendOrganizer.Core.Events;

namespace FriendOrganizer.Presentation.Friend16.ViewModels
{
    public class Friend16ViewModel : ViewModelBase, IFriend16ViewModel
    {
        private IFriendLookupDataService10 _dataService;
        private IEventAggregator _eventAggregator;

        private static int _instanceCountVM = 0;
        public ObservableCollection<NavigationItem16ViewModel> Friend16s { get; }

        public Friend16ViewModel(
                IFriendLookupDataService10 friendLookupDataService,
                IEventAggregator eventAggregator)
        {
            _instanceCountVM++;
            _dataService = friendLookupDataService;
            _eventAggregator = eventAggregator;
            Friend16s = new ObservableCollection<NavigationItem16ViewModel>();

            _eventAggregator.GetEvent<AfterDetailSavedEvent16>()
                .Subscribe(AfterDetailSaved);

            _eventAggregator.GetEvent<AfterDetailDeletedEvent16>()
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
            Friend16s.Clear();

            foreach (var item in lookup)
            {
                Friend16s.Add(
                    new NavigationItem16ViewModel(item.Id, item.DisplayMember, 
                    nameof(Friend16DetailViewModel),
                    _eventAggregator));
            }
        }

        private void AfterDetailSaved(AfterDetailSavedEventArgs args)
        {
            switch (args.ViewModelName)
            {
                case nameof(Friend16DetailViewModel):
                    var lookupItem = Friend16s.SingleOrDefault(l => l.Id == args.Id);

                    if (lookupItem == null)
                    {
                        Friend16s.Add(new NavigationItem16ViewModel(args.Id, args.DisplayMember,
                            nameof(Friend16DetailViewModel),
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
                case nameof(Friend16DetailViewModel):
                     var friend = Friend16s.SingleOrDefault(f => f.Id == args.Id);

                    if (friend != null)
                    {
                        Friend16s.Remove(friend);
                    }                   
                    break;
            }
        }
    }
}
