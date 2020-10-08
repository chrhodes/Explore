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
    public class Navigation16ViewModel : ViewModelBase, INavigation16ViewModel
    {
        private IFriendLookupDataService10 _friendLookupDataService;
        private IMeetingLookupDataService15 _meetingLookupDataService;
        private IEventAggregator _eventAggregator;

        private static int _instanceCountVM = 0;
        public ObservableCollection<NavigationItem16ViewModel> Friend16s { get; }
        public ObservableCollection<NavigationItem16ViewModel> Meeting16s { get; }

        public Navigation16ViewModel(
                IEventAggregator eventAggregator,
                IFriendLookupDataService10 friendLookupDataService,
                IMeetingLookupDataService15 meetingLookupDataService)
        {
            _instanceCountVM++;
            _eventAggregator = eventAggregator;

            _friendLookupDataService = friendLookupDataService;
            _meetingLookupDataService = meetingLookupDataService;

            Friend16s = new ObservableCollection<NavigationItem16ViewModel>();
            Meeting16s = new ObservableCollection<NavigationItem16ViewModel>();

            //Friend16s.Add(
            //    new NavigationItem16ViewModel(0, "friend",
            //    nameof(Friend16DetailViewModel),
            //    _eventAggregator));

            //Meeting16s.Add(
            //    new NavigationItem16ViewModel(0, "meeting",
            //    nameof(Meeting16DetailViewModel),
            //    _eventAggregator));

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
            var lookupF = await _friendLookupDataService.GetFriendLookupAsync();
            Friend16s.Clear();

            foreach (var item in lookupF)
            {
                Friend16s.Add(
                    new NavigationItem16ViewModel(item.Id, item.DisplayMember, 
                    nameof(Friend16DetailViewModel),
                    _eventAggregator));
            }

            var lookupM = await _meetingLookupDataService.GetMeetingLookupAsync();
            Meeting16s.Clear();

            foreach (var item in lookupM)
            {
                Meeting16s.Add(
                    new NavigationItem16ViewModel(item.Id, item.DisplayMember,
                    nameof(Meeting16DetailViewModel),
                    _eventAggregator));
            }
        }

        private void AfterDetailSaved(AfterDetailSavedEventArgs args)
        {
            switch (args.ViewModelName)
            {
                case nameof(Friend16DetailViewModel):
                    AfterDetailSaved(Friend16s, args);
                    break;

                case nameof(Meeting16DetailViewModel):
                    AfterDetailSaved(Meeting16s, args);
                    break;

                default:
                    throw new System.Exception($"AfterDetailSaved(): ViewModel {args.ViewModelName} not mapped.");
            }
        }

        private void AfterDetailSaved(ObservableCollection<NavigationItem16ViewModel> items,
            AfterDetailSavedEventArgs args)
        {
            var lookupItem = items.SingleOrDefault(l => l.Id == args.Id);

            if (lookupItem == null)
            {
                items.Add(new NavigationItem16ViewModel(args.Id, args.DisplayMember,
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
                case nameof(Friend16DetailViewModel):
                    AfterDetailDeleted(Friend16s, args);
                    break;

                case nameof(Meeting16DetailViewModel):
                    AfterDetailDeleted(Meeting16s, args);
                    break;

                default:
                    throw new System.Exception($"AfterDetailDeleted(): ViewModel {args.ViewModelName} not mapped.");
            }
        }

        void AfterDetailDeleted(ObservableCollection<NavigationItem16ViewModel> items,
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
