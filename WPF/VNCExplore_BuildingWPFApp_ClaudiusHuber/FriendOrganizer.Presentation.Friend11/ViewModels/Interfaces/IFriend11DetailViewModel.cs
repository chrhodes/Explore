using System.Threading.Tasks;

using VNC.Core.Mvvm;

namespace FriendOrganizer.Presentation.Friend11.ViewModels
{
    public interface IFriend11DetailViewModel : IViewModel
    {
        bool HasChanges { get; }
        Task LoadAsync(int? id);
    }
}
