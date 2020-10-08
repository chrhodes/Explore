using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using FriendOrganizer.Presentation.Friend13.Views;
using Prism.Events;

using VNC.Core.Mvvm;

using VNCExplore_FriendOrganizer.Core.Events;

using VNCExplore_FriendOrganizer.Core.DomainServices;

namespace FriendOrganizer.Presentation.Friend13.ViewModels
{
    public class Friend13ViewModel : ViewModelBase, IFriend13ViewModel
    {
        private IFriendLookupDataService10 _dataService;
        private IEventAggregator _eventAggregator;

        private static int _instanceCountVM = 0;
        public ObservableCollection<NavigationItem13ViewModel> Friend13s { get; }

        public Friend13ViewModel(
                IFriendLookupDataService10 friendLookupDataService,
                IEventAggregator eventAggregator)
        {
            _instanceCountVM++;
            _dataService = friendLookupDataService;
            _eventAggregator = eventAggregator;
            Friend13s = new ObservableCollection<NavigationItem13ViewModel>();

            _eventAggregator.GetEvent<AfterFriendSavedEvent13>()
                .Subscribe(AfterFriendSaved);

            _eventAggregator.GetEvent<AfterFriendDeletedEvent13>()
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
            Friend13s.Clear();

            foreach (var item in lookup)
            {
                Friend13s.Add(
                    new NavigationItem13ViewModel(item.Id, item.DisplayMember, _eventAggregator));
            }
        }

        private void AfterFriendSaved(AfterFriendSavedEventArgs13 args)
        {
            var lookupItem = Friend13s.SingleOrDefault(l => l.Id == args.Id);

            if (lookupItem == null)
            {
                Friend13s.Add(new NavigationItem13ViewModel(args.Id, args.DisplayMember, _eventAggregator));
            }
            else
            {
                lookupItem.DisplayMember = args.DisplayMember;
            }        
        }

        private void AfterFriendDeleted(int friendId)
        {
            var friend = Friend13s.SingleOrDefault(f => f.Id == friendId);

            if (friend != null)
            {
                Friend13s.Remove(friend);
            }

        }
    }
}
