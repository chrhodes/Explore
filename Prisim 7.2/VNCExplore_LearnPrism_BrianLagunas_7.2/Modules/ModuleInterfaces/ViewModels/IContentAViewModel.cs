using VNC.Core.Mvvm;

namespace ModuleInterfaces
{
    // This is for ViewModel 1st approaches
    public interface IContentAViewModel : IViewModel
    {
        string Message { get; set; }
    }

    public interface IContentAViewModel_V1: IViewModel
    {
        string Message { get; set; }
    }

    public interface IContentAViewModel_VM1 : IViewModel
    {
        string Message { get; set; }
    }
}
