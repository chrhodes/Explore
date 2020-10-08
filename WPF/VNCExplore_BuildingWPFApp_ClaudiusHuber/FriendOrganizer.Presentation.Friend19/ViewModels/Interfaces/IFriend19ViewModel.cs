using System.Threading.Tasks;

using VNC.Core.Mvvm;

namespace FriendOrganizer.Presentation.Friend19.ViewModels
{
    public interface IFriend19ViewModel : IViewModel
    {
        Task LoadAsync();
    }
}
