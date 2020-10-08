using System.Threading.Tasks;

using VNC.Core.Mvvm;

namespace FriendOrganizer.Presentation.Friend11.ViewModels
{
    public interface IFriend11ViewModel : IViewModel
    {
        Task LoadAsync();
    }
}
