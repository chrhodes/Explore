using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using FriendOrganizer.Presentation.Friend12.Views;
using Prism.Events;

using VNC.Core.Mvvm;

using VNCExplore_FriendOrganizer.Core.Events;

using VNCExplore_FriendOrganizer.Core.DomainServices;

namespace FriendOrganizer.Presentation.Friend12.ViewModels
{
    public class Friend12ViewModel : ViewModelBase, IFriend12ViewModel
    {
        private IFriendLookupDataService10 _dataService;
        private IEventAggregator _eventAggregator;

        private static int _instanceCountVM = 0;
        public ObservableCollection<NavigationItem12ViewModel> Friend12s { get; }

        public Friend12ViewModel(
                IFriendLookupDataService10 friendLookupDataService
                ,IEventAggregator eventAggregator)
        {
            _instanceCountVM++;
            _dataService = friendLookupDataService;
            _eventAggregator = eventAggregator;
            Friend12s = new ObservableCollection<NavigationItem12ViewModel>();

            _eventAggregator.GetEvent<AfterFriendSavedEvent12>()
                .Subscribe(AfterFriendSaved);

            _eventAggregator.GetEvent<AfterFriendDeletedEvent12>()
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
            Friend12s.Clear();

            foreach (var item in lookup)
            {
                Friend12s.Add(
                    new NavigationItem12ViewModel(item.Id, item.DisplayMember, _eventAggregator));
            }
        }

        private void AfterFriendSaved(AfterFriendSavedEventArgs12 args)
        {
            var lookupItem = Friend12s.SingleOrDefault(l => l.Id == args.Id);

            if (lookupItem == null)
            {
                Friend12s.Add(new NavigationItem12ViewModel(args.Id, args.DisplayMember, _eventAggregator));
            }
            else
            {
                lookupItem.DisplayMember = args.DisplayMember;
            }        
        }

        private void AfterFriendDeleted(int friendId)
        {
            var friend = Friend12s.SingleOrDefault(f => f.Id == friendId);

            if (friend != null)
            {
                Friend12s.Remove(friend);
            }

        }
    }
}
