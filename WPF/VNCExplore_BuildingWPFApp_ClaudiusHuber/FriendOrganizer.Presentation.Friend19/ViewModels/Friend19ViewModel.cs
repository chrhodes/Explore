using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using Prism.Events;

using VNC.Core.Events;
using VNC.Core.Mvvm;

using VNCExplore_FriendOrganizer.Core.DomainServices;
using VNCExplore_FriendOrganizer.Core.Events;

namespace FriendOrganizer.Presentation.Friend19.ViewModels
{
    public class Friend19ViewModel : ViewModelBase, IFriend19ViewModel
    {
        private IFriendLookupDataService10 _dataService;
        private IEventAggregator _eventAggregator;

        private static int _instanceCountVM = 0;
        public ObservableCollection<NavigationItem19ViewModel> Friend19s { get; }

        public Friend19ViewModel(
                IFriendLookupDataService10 friendLookupDataService,
                IEventAggregator eventAggregator)
        {
            _instanceCountVM++;
            _dataService = friendLookupDataService;
            _eventAggregator = eventAggregator;
            Friend19s = new ObservableCollection<NavigationItem19ViewModel>();

            _eventAggregator.GetEvent<AfterDetailSavedEvent19>()
                .Subscribe(AfterDetailSaved);

            _eventAggregator.GetEvent<AfterDetailDeletedEvent19>()
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
            Friend19s.Clear();

            foreach (var item in lookup)
            {
                Friend19s.Add(
                    new NavigationItem19ViewModel(item.Id, item.DisplayMember, 
                    nameof(Friend19DetailViewModel),
                    _eventAggregator));
            }
        }

        private void AfterDetailSaved(AfterDetailSavedEventArgs args)
        {
            switch (args.ViewModelName)
            {
                case nameof(Friend19DetailViewModel):
                    var lookupItem = Friend19s.SingleOrDefault(l => l.Id == args.Id);

                    if (lookupItem == null)
                    {
                        Friend19s.Add(new NavigationItem19ViewModel(args.Id, args.DisplayMember,
                            nameof(Friend19DetailViewModel),
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
                case nameof(Friend19DetailViewModel):
                     var friend = Friend19s.SingleOrDefault(f => f.Id == args.Id);

                    if (friend != null)
                    {
                        Friend19s.Remove(friend);
                    }                   
                    break;
            }
        }
    }
}
