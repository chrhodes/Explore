using System.Threading.Tasks;

using VNC.Core.Mvvm;

namespace FriendOrganizer.Presentation.Friend10.ViewModels
{
    public interface IFriend10ViewModel : IViewModel
    {
        Task LoadAsync();
    }
}
