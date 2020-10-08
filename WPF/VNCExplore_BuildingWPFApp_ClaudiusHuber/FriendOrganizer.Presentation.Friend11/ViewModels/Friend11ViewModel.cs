using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using FriendOrganizer.Presentation.Friend11.Views;
using Prism.Events;

using VNC.Core.Mvvm;

using VNCExplore_FriendOrganizer.Core.Events;

using VNCExplore_FriendOrganizer.Core.DomainServices;
using VNCExplore_FriendOrganizer.Core.Events;

namespace FriendOrganizer.Presentation.Friend11.ViewModels
{
    public class Friend11ViewModel : ViewModelBase, IFriend11ViewModel
    {
        private IFriendLookupDataService10 _dataService;
        private IEventAggregator _eventAggregator;

        private static int _instanceCountVM = 0;
        public ObservableCollection<NavigationItem11ViewModel> Friend11s { get; }

        public Friend11ViewModel(
                IFriendLookupDataService10 friendLookupDataService
                ,IEventAggregator eventAggregator)
        {
            _instanceCountVM++;
            _dataService = friendLookupDataService;
            _eventAggregator = eventAggregator;
            Friend11s = new ObservableCollection<NavigationItem11ViewModel>();

            _eventAggregator.GetEvent<AfterFriendSavedEvent11>()
                .Subscribe(AfterFriendSaved);

            _eventAggregator.GetEvent<AfterFriendDeletedEvent11>()
                .Subscribe(AfterFriendDeleted);
        } 

        public int InstanceCountVM
        {
            get { return _instanceCountVM; }
            set { _instanceCountVM = value; }
        }
        public async Task LoadAsync()
        {
            var lookup = await _dataService.GetFriendLookupAsync();
            Friend11s.Clear();

            foreach (var item in lookup)
            {
                Friend11s.Add(
                    new NavigationItem11ViewModel(item.Id, item.DisplayMember, _eventAggregator));
            }
        }

        private void AfterFriendSaved(AfterFriendSavedEventArgs11 args)
        {
            var lookupItem = Friend11s.SingleOrDefault(l => l.Id == args.Id);
            if (lookupItem == null)
            {
                Friend11s.Add(new NavigationItem11ViewModel(args.Id, args.DisplayMember, _eventAggregator));
            }
            else
            {
                lookupItem.DisplayMember = args.DisplayMember;
            }        
        }

        private void AfterFriendDeleted(int friendId)
        {
            var friend = Friend11s.SingleOrDefault(f => f.Id == friendId);

            if (friend != null)
            {
                Friend11s.Remove(friend);
            }

        }
    }
}
