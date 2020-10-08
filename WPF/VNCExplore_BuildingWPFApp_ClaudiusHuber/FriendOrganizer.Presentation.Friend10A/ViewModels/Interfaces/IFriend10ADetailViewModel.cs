using System.Threading.Tasks;

using VNC.Core.Mvvm;

namespace FriendOrganizer.Presentation.Friend10A.ViewModels
{
    public interface IFriend10ADetailViewModel : IViewModel
    {
        bool HasChanges { get; }
        Task LoadAsync(int id);
    }
}
