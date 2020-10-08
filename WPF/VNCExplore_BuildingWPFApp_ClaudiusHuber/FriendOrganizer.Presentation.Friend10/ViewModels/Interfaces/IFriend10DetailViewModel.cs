using System.Threading.Tasks;

using VNC.Core.Mvvm;

namespace FriendOrganizer.Presentation.Friend10.ViewModels
{
    public interface IFriend10DetailViewModel : IViewModel
    {
        Task LoadAsync(int id);
    }
}
