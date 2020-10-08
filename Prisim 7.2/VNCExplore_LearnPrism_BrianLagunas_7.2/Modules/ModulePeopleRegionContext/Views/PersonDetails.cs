using System.Windows.Controls;

using Prism.Regions;
using ModuleInterfaces;
using VNC.Core.Mvvm;
using Prism.Common;
using PrismDemo.Business;

namespace ModuleCommunicationPeopleRegionContext
{
    /// <summary>
    /// Interaction logic for PersonDetailsView.xaml
    /// </summary>
    public partial class PersonDetails : UserControl, IPersonDetails
    {
        public PersonDetails(IPersonDetailsViewModel viewModel)
        {
            InitializeComponent();

            ViewModel = viewModel;
            ViewModel.View = this;

            RegionContext.GetObservableContext(this).PropertyChanged += (s, e) =>
                {
                    var context = (ObservableObject<object>)s;
                    // Better not not pass whole person.  Just ID or something.
                    var selectedPerson = (Person)context.Value;
                    (ViewModel as IPersonDetailsViewModel).SelectedPerson = selectedPerson;
                };
        }

        public IViewModel ViewModel
        {
            get { return (IViewModel)DataContext; }
            set { DataContext = value; }
        }
    }
}
