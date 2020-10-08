using System.Threading.Tasks;

using VNC.Core.Mvvm;

namespace FriendOrganizer.Presentation.Friend08.ViewModels
{
    public interface IFriend08DetailViewModel : IViewModel
    {
        Task LoadAsync(int id);
    }
}
