using System.Threading.Tasks;

using VNC.Core.Mvvm;

namespace FriendOrganizer.Presentation.Friend18.ViewModels
{
    public interface IFriend18ViewModel : IViewModel
    {
        Task LoadAsync();
    }
}
