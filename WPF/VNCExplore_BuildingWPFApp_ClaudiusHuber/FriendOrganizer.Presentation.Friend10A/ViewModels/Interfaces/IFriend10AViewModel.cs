using System.Threading.Tasks;

using VNC.Core.Mvvm;

namespace FriendOrganizer.Presentation.Friend10A.ViewModels
{
    public interface IFriend10AViewModel : IViewModel
    {
        Task LoadAsync();
    }
}
