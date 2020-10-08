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
    public class Navigation15ViewModel : ViewModelBase, INavigation15ViewModel
    {
        private IFriendLookupDataService10 _friendLookupDataService;
        private IMeetingLookupDataService15 _meetingLookupDataService;
        private IEventAggregator _eventAggregator;

        private static int _instanceCountVM = 0;
        public ObservableCollection<NavigationItem15ViewModel> Friend15s { get; }
        public ObservableCollection<NavigationItem15ViewModel> Meeting15s { get; }

        public Navigation15ViewModel(
                IEventAggregator eventAggregator,
                IFriendLookupDataService10 friendLookupDataService,
                IMeetingLookupDataService15 meetingLookupDataService)
        {
            _instanceCountVM++;
            _eventAggregator = eventAggregator;

            _friendLookupDataService = friendLookupDataService;
            _meetingLookupDataService = meetingLookupDataService;

            Friend15s = new ObservableCollection<NavigationItem15ViewModel>();
            Meeting15s = new ObservableCollection<NavigationItem15ViewModel>();

            Friend15s.Add(
                new NavigationItem15ViewModel(0, "friend",
                nameof(Friend15DetailViewModel),
                _eventAggregator));

            Meeting15s.Add(
                new NavigationItem15ViewModel(0, "meeting",
                nameof(Meeting15DetailViewModel),
                _eventAggregator));

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
            var lookupF = await _friendLookupDataService.GetFriendLookupAsync();
            Friend15s.Clear();

            foreach (var item in lookupF)
            {
                Friend15s.Add(
                    new NavigationItem15ViewModel(item.Id, item.DisplayMember, 
                    nameof(Friend15DetailViewModel),
                    _eventAggregator));
            }

            var lookupM = await _meetingLookupDataService.GetMeetingLookupAsync();
            Meeting15s.Clear();

            foreach (var item in lookupM)
            {
                Meeting15s.Add(
                    new NavigationItem15ViewModel(item.Id, item.DisplayMember,
                    nameof(Meeting15DetailViewModel),
                    _eventAggregator));
            }
        }

        private void AfterDetailSaved(AfterDetailSavedEventArgs args)
        {
            switch (args.ViewModelName)
            {
                case nameof(Friend15DetailViewModel):
                    AfterDetailSaved(Friend15s, args);
                    break;

                case nameof(Meeting15DetailViewModel):
                    AfterDetailSaved(Meeting15s, args);
                    break;

                default:
                    throw new System.Exception($"AfterDetailSaved(): ViewModel {args.ViewModelName} not mapped.");
            }
        }

        private void AfterDetailSaved(ObservableCollection<NavigationItem15ViewModel> items,
            AfterDetailSavedEventArgs args)
        {
            var lookupItem = items.SingleOrDefault(l => l.Id == args.Id);

            if (lookupItem == null)
            {
                items.Add(new NavigationItem15ViewModel(args.Id, args.DisplayMember,
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
                case nameof(Friend15DetailViewModel):
                    AfterDetailDeleted(Friend15s, args);
                    break;

                case nameof(Meeting15DetailViewModel):
                    AfterDetailDeleted(Meeting15s, args);
                    break;

                default:
                    throw new System.Exception($"AfterDetailDeleted(): ViewModel {args.ViewModelName} not mapped.");
            }
        }

        void AfterDetailDeleted(ObservableCollection<NavigationItem15ViewModel> items,
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
