using VNC.Core.Mvvm;
using ModuleInterfaces;
using Prism.Events;
using VNCExplore_LearnPrism_BrianLagunas.Infrastructure;
using System;

namespace ModuleStatusBar
{
    public class StatusBarViewModel : ViewModelBase, IStatusBarViewModel
    {
        IEventAggregator _eventAggregator;

        public StatusBarViewModel(IStatusBar view, IEventAggregator eventAggregator)
            : base(view)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<PersonUpdatedEvent>().Subscribe(PersonUpdated);
        }

        private void PersonUpdated(string obj)
        {
            Message = string.Format("{0} was updated.", obj);
        }

        private string _message = "Ready";
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged("Message");
            }
        }
    }
}

