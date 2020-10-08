using System.Collections.ObjectModel;
using System.Threading.Tasks;

using FriendOrganizer.Domain;

using VNC.Core.Mvvm;

using VNCExplore_FriendOrganizer.Core.DomainServices;

namespace FriendOrganizer.Presentation.Friend06.ViewModels
{
    public class Friend06ViewModel : ViewModelBase, IFriend06ViewModel
    {
        private IFriendLookupDataService06 _friendLookupService;

        public Friend06ViewModel(
            IFriendLookupDataService06 friendLookupService)
        {
            _friendLookupService = friendLookupService;
            Friends06 = new ObservableCollection<LookupItem>();
        }

        public async Task LoadAsync()
        {
            var lookup = await _friendLookupService.GetFriendLookupAsync();
            Friends06.Clear();

            foreach (var item in lookup)
            {
                Friends06.Add(item);
            }
        }

        public ObservableCollection<LookupItem> Friends06 { get; }

        public IView View
        {
            get;
            set;
        }
    }
}
