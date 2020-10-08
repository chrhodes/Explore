using System.Threading.Tasks;

using VNC.Core.Mvvm;

namespace FriendOrganizer.Presentation.Friend06.ViewModels
{
    public interface IFriend06ViewModel : IViewModel
    {
        Task LoadAsync();
    }
}
