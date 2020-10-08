using System.Threading.Tasks;

using VNC.Core.Mvvm;

namespace FriendOrganizer.Presentation.Friend06Detail.ViewModels
{
    public interface IFriend06DetailViewModel : IViewModel
    {
        Task LoadAsync(int friendId);
    }
}
