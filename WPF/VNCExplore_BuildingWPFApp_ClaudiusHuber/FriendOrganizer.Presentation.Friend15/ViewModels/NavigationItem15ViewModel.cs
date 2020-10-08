﻿using System.Windows.Input;

using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

using VNC.Core.Events;
using VNCExplore_FriendOrganizer.Core.Events;

namespace FriendOrganizer.Presentation.Friend15.ViewModels
{
    public class NavigationItem15ViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;

        private string _displayMember;
        private string _detailViewModelName;

        public NavigationItem15ViewModel(
            int id, 
            string displayMember,
            string detailViewModelName,
            IEventAggregator eventAggregator)
        {
            Id = id;
            DisplayMember = displayMember;
            _detailViewModelName = detailViewModelName;
            _eventAggregator = eventAggregator;

            OpenDetailViewCommand = new DelegateCommand(OnOpenDetailViewExecute);
        }

        public int Id { get; set; }

        public string DisplayMember
        {
            get { return _displayMember; }
            set
            {
                if (_displayMember == value)
                    return;
                _displayMember = value;
                RaisePropertyChanged();
            }
        }

        public ICommand OpenDetailViewCommand { get; }

        private void OnOpenDetailViewExecute()
        {
            _eventAggregator.GetEvent<OpenDetailViewEvent15>()
                  .Publish
                    (
                        new OpenDetailViewEventArgs
                        {
                            Id = Id,
                            ViewModelName = _detailViewModelName
                        }
                    );
        }
    }
}

