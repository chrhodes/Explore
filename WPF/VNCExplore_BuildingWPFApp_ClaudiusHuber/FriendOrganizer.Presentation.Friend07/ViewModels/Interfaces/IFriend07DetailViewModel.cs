using System.Threading.Tasks;

using VNC.Core.Mvvm;

namespace FriendOrganizer.Presentation.Friend07.ViewModels
{
    public interface IFriend07DetailViewModel : IViewModel
    {
        Task LoadAsync(int id);
    }
}
