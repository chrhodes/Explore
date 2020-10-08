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
    public class Navigation18ViewModel : ViewModelBase, INavigation18ViewModel
    {
        private IFriendLookupDataService10 _friendLookupDataService;
        private IMeetingLookupDataService15 _meetingLookupDataService;
        private IEventAggregator _eventAggregator;

        private static int _instanceCountVM = 0;
        public ObservableCollection<NavigationItem18ViewModel> Friend18s { get; }
        public ObservableCollection<NavigationItem18ViewModel> Meeting18s { get; }

        public Navigation18ViewModel(
                IEventAggregator eventAggregator,
                IFriendLookupDataService10 friendLookupDataService,
                IMeetingLookupDataService15 meetingLookupDataService)
        {
            _instanceCountVM++;
            _eventAggregator = eventAggregator;

            _friendLookupDataService = friendLookupDataService;
            _meetingLookupDataService = meetingLookupDataService;

            Friend18s = new ObservableCollection<NavigationItem18ViewModel>();
            Meeting18s = new ObservableCollection<NavigationItem18ViewModel>();

            //Friend18s.Add(
            //    new NavigationItem18ViewModel(0, "friend",
            //    nameof(Friend18DetailViewModel),
            //    _eventAggregator));

            //Meeting18s.Add(
            //    new NavigationItem18ViewModel(0, "meeting",
            //    nameof(Meeting18DetailViewModel),
            //    _eventAggregator));

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
            var lookupF = await _friendLookupDataService.GetFriendLookupAsync();
            Friend18s.Clear();

            foreach (var item in lookupF)
            {
                Friend18s.Add(
                    new NavigationItem18ViewModel(item.Id, item.DisplayMember, 
                    nameof(Friend18DetailViewModel),
                    _eventAggregator));
            }

            var lookupM = await _meetingLookupDataService.GetMeetingLookupAsync();
            Meeting18s.Clear();

            foreach (var item in lookupM)
            {
                Meeting18s.Add(
                    new NavigationItem18ViewModel(item.Id, item.DisplayMember,
                    nameof(Meeting18DetailViewModel),
                    _eventAggregator));
            }
        }

        private void AfterDetailSaved(AfterDetailSavedEventArgs args)
        {
            switch (args.ViewModelName)
            {
                case nameof(Friend18DetailViewModel):
                    AfterDetailSaved(Friend18s, args);
                    break;

                case nameof(Meeting18DetailViewModel):
                    AfterDetailSaved(Meeting18s, args);
                    break;

                default:
                    throw new System.Exception($"AfterDetailSaved(): ViewModel {args.ViewModelName} not mapped.");
            }
        }

        private void AfterDetailSaved(ObservableCollection<NavigationItem18ViewModel> items,
            AfterDetailSavedEventArgs args)
        {
            var lookupItem = items.SingleOrDefault(l => l.Id == args.Id);

            if (lookupItem == null)
            {
                items.Add(new NavigationItem18ViewModel(args.Id, args.DisplayMember,
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
                case nameof(Friend18DetailViewModel):
                    AfterDetailDeleted(Friend18s, args);
                    break;

                case nameof(Meeting18DetailViewModel):
                    AfterDetailDeleted(Meeting18s, args);
                    break;

                default:
                    throw new System.Exception($"AfterDetailDeleted(): ViewModel {args.ViewModelName} not mapped.");
            }
        }

        void AfterDetailDeleted(ObservableCollection<NavigationItem18ViewModel> items,
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
