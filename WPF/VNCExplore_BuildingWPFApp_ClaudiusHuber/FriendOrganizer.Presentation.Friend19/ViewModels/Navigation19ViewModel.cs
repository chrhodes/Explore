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
    public class Navigation19ViewModel : ViewModelBase, INavigation19ViewModel
    {
        private IFriendLookupDataService10 _friendLookupDataService;
        private IMeetingLookupDataService15 _meetingLookupDataService;
        private IEventAggregator _eventAggregator;

        private static int _instanceCountVM = 0;
        public ObservableCollection<NavigationItem19ViewModel> Friend19s { get; }
        public ObservableCollection<NavigationItem19ViewModel> Meeting19s { get; }

        public Navigation19ViewModel(
                IEventAggregator eventAggregator,
                IFriendLookupDataService10 friendLookupDataService,
                IMeetingLookupDataService15 meetingLookupDataService)
        {
            _instanceCountVM++;
            _eventAggregator = eventAggregator;

            _friendLookupDataService = friendLookupDataService;
            _meetingLookupDataService = meetingLookupDataService;

            Friend19s = new ObservableCollection<NavigationItem19ViewModel>();
            Meeting19s = new ObservableCollection<NavigationItem19ViewModel>();

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
            var lookupF = await _friendLookupDataService.GetFriendLookupAsync();
            Friend19s.Clear();

            foreach (var item in lookupF)
            {
                Friend19s.Add(
                    new NavigationItem19ViewModel(item.Id, item.DisplayMember, 
                    nameof(Friend19DetailViewModel),
                    _eventAggregator));
            }

            var lookupM = await _meetingLookupDataService.GetMeetingLookupAsync();
            Meeting19s.Clear();

            foreach (var item in lookupM)
            {
                Meeting19s.Add(
                    new NavigationItem19ViewModel(item.Id, item.DisplayMember,
                    nameof(Meeting19DetailViewModel),
                    _eventAggregator));
            }
        }

        private void AfterDetailSaved(AfterDetailSavedEventArgs args)
        {
            switch (args.ViewModelName)
            {
                case nameof(Friend19DetailViewModel):
                    AfterDetailSaved(Friend19s, args);
                    break;

                case nameof(Meeting19DetailViewModel):
                    AfterDetailSaved(Meeting19s, args);
                    break;

                default:
                    throw new System.Exception($"AfterDetailSaved(): ViewModel {args.ViewModelName} not mapped.");
            }
        }

        private void AfterDetailSaved(ObservableCollection<NavigationItem19ViewModel> items,
            AfterDetailSavedEventArgs args)
        {
            var lookupItem = items.SingleOrDefault(l => l.Id == args.Id);

            if (lookupItem == null)
            {
                items.Add(new NavigationItem19ViewModel(args.Id, args.DisplayMember,
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
                case nameof(Friend19DetailViewModel):
                    AfterDetailDeleted(Friend19s, args);
                    break;

                case nameof(Meeting19DetailViewModel):
                    AfterDetailDeleted(Meeting19s, args);
                    break;

                default:
                    throw new System.Exception($"AfterDetailDeleted(): ViewModel {args.ViewModelName} not mapped.");
            }
        }

        void AfterDetailDeleted(ObservableCollection<NavigationItem19ViewModel> items,
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
