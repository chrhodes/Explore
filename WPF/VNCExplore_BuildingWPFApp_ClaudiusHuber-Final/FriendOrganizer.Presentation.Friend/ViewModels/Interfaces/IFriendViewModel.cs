using System.Threading.Tasks;

using VNC.Core.Mvvm;

namespace FriendOrganizer.Presentation.Friend.ViewModels
{
    public interface IFriendViewModel : IViewModel
    {
        Task LoadAsync();
    }
}
