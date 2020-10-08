using FriendOrganizer.Domain;
using System.Collections.ObjectModel;

namespace VNCExplore_FriendOrganizer.ViewModels
{
    public interface IMainWindowDxLayoutViewModel
    {
        void Load();

        ObservableCollection<IFriend> Friends04 { get; set; }

        IFriend SelectedFriend04 { get; set; }

        ObservableCollection<IFriend> Friends05 { get; set; }

        IFriend SelectedFriend05 { get; set; }
    }
}
