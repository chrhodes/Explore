using VNC.Core.Mvvm;
using PrismDemo.Business;

namespace ModuleInterfaces
{
    public interface IPersonDetailsViewModel : IViewModel
    {
        Person SelectedPerson { get; set; }
    }
}
