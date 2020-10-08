using System.Threading.Tasks;

using VNC.Core.Mvvm;

namespace FriendOrganizer.Presentation.Friend16.ViewModels
{
    public interface IFriend16ViewModel : IViewModel
    {
        Task LoadAsync();
    }
}
