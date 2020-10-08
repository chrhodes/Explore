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
    public class Navigation17ViewModel : ViewModelBase, INavigation17ViewModel
    {
        private IFriendLookupDataService10 _friendLookupDataService;
        private IMeetingLookupDataService15 _meetingLookupDataService;
        private IEventAggregator _eventAggregator;

        private static int _instanceCountVM = 0;
        public ObservableCollection<NavigationItem17ViewModel> Friend17s { get; }
        public ObservableCollection<NavigationItem17ViewModel> Meeting17s { get; }

        public Navigation17ViewModel(
                IEventAggregator eventAggregator,
                IFriendLookupDataService10 friendLookupDataService,
                IMeetingLookupDataService15 meetingLookupDataService)
        {
            _instanceCountVM++;
            _eventAggregator = eventAggregator;

            _friendLookupDataService = friendLookupDataService;
            _meetingLookupDataService = meetingLookupDataService;

            Friend17s = new ObservableCollection<NavigationItem17ViewModel>();
            Meeting17s = new ObservableCollection<NavigationItem17ViewModel>();

            //Friend17s.Add(
            //    new NavigationItem17ViewModel(0, "friend",
            //    nameof(Friend17DetailViewModel),
            //    _eventAggregator));

            //Meeting17s.Add(
            //    new NavigationItem17ViewModel(0, "meeting",
            //    nameof(Meeting17DetailViewModel),
            //    _eventAggregator));

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
            var lookupF = await _friendLookupDataService.GetFriendLookupAsync();
            Friend17s.Clear();

            foreach (var item in lookupF)
            {
                Friend17s.Add(
                    new NavigationItem17ViewModel(item.Id, item.DisplayMember, 
                    nameof(Friend17DetailViewModel),
                    _eventAggregator));
            }

            var lookupM = await _meetingLookupDataService.GetMeetingLookupAsync();
            Meeting17s.Clear();

            foreach (var item in lookupM)
            {
                Meeting17s.Add(
                    new NavigationItem17ViewModel(item.Id, item.DisplayMember,
                    nameof(Meeting17DetailViewModel),
                    _eventAggregator));
            }
        }

        private void AfterDetailSaved(AfterDetailSavedEventArgs args)
        {
            switch (args.ViewModelName)
            {
                case nameof(Friend17DetailViewModel):
                    AfterDetailSaved(Friend17s, args);
                    break;

                case nameof(Meeting17DetailViewModel):
                    AfterDetailSaved(Meeting17s, args);
                    break;

                default:
                    throw new System.Exception($"AfterDetailSaved(): ViewModel {args.ViewModelName} not mapped.");
            }
        }

        private void AfterDetailSaved(ObservableCollection<NavigationItem17ViewModel> items,
            AfterDetailSavedEventArgs args)
        {
            var lookupItem = items.SingleOrDefault(l => l.Id == args.Id);

            if (lookupItem == null)
            {
                items.Add(new NavigationItem17ViewModel(args.Id, args.DisplayMember,
                    args.ViewModelName,
                    _eventAggregator));
            }
            else
            {
                lookupItem.DisplayMember = args.DisplayMember;
            }
        }

        private void AfterDetailDeleted(AfterDetailDeletedEventArgs args)
        {
            switch (args.ViewModelName)
            {
                case nameof(Friend17DetailViewModel):
                    AfterDetailDeleted(Friend17s, args);
                    break;

                case nameof(Meeting17DetailViewModel):
                    AfterDetailDeleted(Meeting17s, args);
                    break;

                default:
                    throw new System.Exception($"AfterDetailDeleted(): ViewModel {args.ViewModelName} not mapped.");
            }
        }

        void AfterDetailDeleted(ObservableCollection<NavigationItem17ViewModel> items,
            AfterDetailDeletedEventArgs args)
        {
            var lookupItem = items.SingleOrDefault(f => f.Id == args.Id);

            if (lookupItem != null)
            {
                items.Remove(lookupItem);
            }
        }
    }
}
