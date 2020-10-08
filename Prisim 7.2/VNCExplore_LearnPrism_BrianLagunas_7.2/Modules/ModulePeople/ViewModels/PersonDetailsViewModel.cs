using VNC.Core.Mvvm;
using ModuleInterfaces;

namespace ModulePeopleEventAggregation
{
    public class PersonDetailsViewModel : ViewModelBase, IPersonDetailsViewModel
    {
        public PersonDetailsViewModel() { }

        private Person _SelectedPerson;
        public Person SelectedPerson
        {
            get { return _SelectedPerson; }
            set
            {
                _SelectedPerson = value;
                OnPropertyChanged("SelectedPerson");
            }
        }

        PrismDemo.Business.Person IPersonDetailsViewModel.SelectedPerson { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    }
}
