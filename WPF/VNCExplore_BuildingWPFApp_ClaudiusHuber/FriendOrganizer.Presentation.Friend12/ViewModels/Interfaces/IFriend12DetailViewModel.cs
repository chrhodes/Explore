using System.Threading.Tasks;

using VNC.Core.Mvvm;

namespace FriendOrganizer.Presentation.Friend12.ViewModels
{
    public interface IFriend12DetailViewModel : IViewModel
    {
        bool HasChanges { get; }
        Task LoadAsync(int? id);
    }
}
