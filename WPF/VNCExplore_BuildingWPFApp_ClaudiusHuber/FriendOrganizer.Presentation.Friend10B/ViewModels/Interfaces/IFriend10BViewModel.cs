using System.Threading.Tasks;

using VNC.Core.Mvvm;

namespace FriendOrganizer.Presentation.Friend10B.ViewModels
{
    public interface IFriend10BViewModel : IViewModel
    {
        Task LoadAsync();
    }
}
