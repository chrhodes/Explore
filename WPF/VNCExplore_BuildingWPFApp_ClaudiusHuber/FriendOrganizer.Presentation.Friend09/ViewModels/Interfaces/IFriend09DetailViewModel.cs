using System.Threading.Tasks;

using VNC.Core.Mvvm;

namespace FriendOrganizer.Presentation.Friend09.ViewModels
{
    public interface IFriend09DetailViewModel : IViewModel
    {
        Task LoadAsync(int id);
    }
}
