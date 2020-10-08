using System;

using ModuleInterfaces;

using Prism.Commands;
using Prism.Events;

using VNC.Core.Mvvm;

using VNCExplore_LearnPrism_BrianLagunas.Infrastructure;

namespace ModuleCommunicationPeopleRegionContext
{
    public class PersonDetailsViewModel : ViewModelBase, IPersonDetailsViewModel
    {
        IEventAggregator _eventAggregator;

        public DelegateCommand<PrismDemo.Business.Person> SaveCommand { get; set; }

        public PersonDetailsViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            SaveCommand = new DelegateCommand<PrismDemo.Business.Person>(Save, CanSave);
        }

        private bool CanSave(PrismDemo.Business.Person value)
        {
            if (SelectedPerson != null)
            {
                return SelectedPerson.Error == null;
            }

            return false;
        }

        private void Save(PrismDemo.Business.Person value)
        {
            if (value is null)
            {
                SelectedPerson.LastUpdated = DateTime.Now;
            }
            else
            {
                SelectedPerson.LastUpdated = DateTime.Now.AddYears(value.Age);
            }

            _eventAggregator.GetEvent<PersonUpdatedEvent>().Publish(ViewName);
        }

        private PrismDemo.Business.Person _SelectedPerson;
        public PrismDemo.Business.Person SelectedPerson
        {
            get { return _SelectedPerson; }
            set
            {
                _SelectedPerson = value;
                _SelectedPerson.PropertyChanged += SelectedPerson_PropertyChanged;
                OnPropertyChanged("SelectedPerson");
            }
        }

        private void SelectedPerson_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            // Force calling of CanSave on SaveCommand delegate
            SaveCommand.RaiseCanExecuteChanged();
        }

        public string ViewName
        {
            get
            {
                return string.Format("{0}, {1}", SelectedPerson.LastName, SelectedPerson.FirstName);
            }
        }
    }
}
