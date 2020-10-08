using System.Threading.Tasks;

using VNC.Core.Mvvm;

namespace FriendOrganizer.Presentation.Friend15.ViewModels
{
    public interface IFriend15ViewModel : IViewModel
    {
        Task LoadAsync();
    }
}
