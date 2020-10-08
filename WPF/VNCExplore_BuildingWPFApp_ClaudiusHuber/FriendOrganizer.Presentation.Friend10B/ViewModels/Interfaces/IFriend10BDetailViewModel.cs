using System.Threading.Tasks;

using VNC.Core.Mvvm;

namespace FriendOrganizer.Presentation.Friend10B.ViewModels
{
    public interface IFriend10BDetailViewModel : IViewModel
    {
        bool HasChanges { get; }
        Task LoadAsync(int id);
    }
}
