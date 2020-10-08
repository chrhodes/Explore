using System.Collections.ObjectModel;
using System.Threading.Tasks;
using FriendOrganizer.Domain;
using Prism.Mvvm;
using VNCExplore_FriendOrganizer.Core.DomainServices;

namespace VNCExplore_FriendOrganizer.ViewModels
{
    public class MainWindowDxLayoutViewModel : BindableBase //, IMainWindowDxLayoutViewModel
    {
        public string Title = "VNCExplore BuildingWPF Application Using Entity Framework";

        private IFriendDataService04 _friendDataService04;
        private IFriend _selectedFriend04;

        private IFriendDataService05 _friendDataService05;
        private IFriend _selectedFriend05;

        // TODO(crhodes)
        // Figure out why this is getting called twice

        public MainWindowDxLayoutViewModel(
            IFriendDataService04 friendDataService04,
            IFriendDataService05 friendDataService05)
        {
            Friends04 = new ObservableCollection<IFriend>();
            _friendDataService04 = friendDataService04;

            Friends05 = new ObservableCollection<IFriend>();
            _friendDataService05 = friendDataService05;
        }

        //public void Load()
        //{
        //    var friends04 = _friendDataService04.GetAll();
        //    Friends04.Clear();

        //    foreach (var friend in friends04)
        //    {
        //        Friends04.Add(friend);
        //    }

        //    var friends05 = _friendDataService05.GetAll();
        //    Friends05.Clear();

        //    foreach (var friend in friends05)
        //    {
        //        Friends05.Add(friend);
        //    }
        //}

        public async Task LoadAsync()
        {
            var friends04 = _friendDataService04.GetAll();
            Friends04.Clear();

            foreach (var friend in friends04)
            {
                Friends04.Add(friend);
            }

            var friends05 = await _friendDataService05.GetAllAsync();
            Friends05.Clear();

            foreach (var friend in friends05)
            {
                Friends05.Add(friend);
            }
        }

        public ObservableCollection<IFriend> Friends04 { get; set; }

        public IFriend SelectedFriend04
        {
            get { return _selectedFriend04; }
            set
            {
                _selectedFriend04 = value;
                RaisePropertyChanged();
                //OnPropertyChanged();
            }
        }

        public ObservableCollection<IFriend> Friends05 { get; set; }

        public IFriend SelectedFriend05
        {
            get { return _selectedFriend05; }
            set
            {
                _selectedFriend05 = value;
                RaisePropertyChanged();
                //OnPropertyChanged();
            }
        }
    }
}
