using System.Threading.Tasks;

using VNC.Core.Mvvm;

namespace FriendOrganizer.Presentation.Friend17.ViewModels
{
    public interface IFriend17ViewModel : IViewModel
    {
        Task LoadAsync();
    }
}
