using System.Threading.Tasks;

using VNC.Core.Mvvm;

namespace FriendOrganizer.Presentation.Friend13.ViewModels
{
    public interface IFriend13DetailViewModel : IViewModel
    {
        bool HasChanges { get; }
        Task LoadAsync(int? id);
    }
}
