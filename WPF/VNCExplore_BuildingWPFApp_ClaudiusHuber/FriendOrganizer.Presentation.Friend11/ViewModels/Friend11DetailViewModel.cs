using System.Threading.Tasks;
using System.Windows.Input;

using FriendOrganizer.UI.Wrapper;

using Prism.Commands;
using Prism.Events;
using System;
using VNC.Core.Mvvm;

using VNCExplore_FriendOrganizer.Core.DomainServices;
using VNCExplore_FriendOrganizer.Core.Events;
using FriendOrganizer.Domain;

namespace FriendOrganizer.Presentation.Friend11.ViewModels
{
    internal class Friend11DetailViewModel : ViewModelBase, IFriend11DetailViewModel
    {

        private static int _instanceCountDVM = 100;
        private IEventAggregator _eventAggregator;
        private Friend11Wrapper _friend;
        private IFriendRepository10 _friendRepository;
        private bool _hasChanges;

        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }

        public Friend11DetailViewModel(
                IFriendRepository10 friendRepository,
                IEventAggregator eventAggregator)
        {
            _instanceCountDVM++;
            _friendRepository = friendRepository;
            _eventAggregator = eventAggregator;

            SaveCommand = new DelegateCommand(
                OnSaveExecute, OnSaveCanExecute);

            DeleteCommand = new DelegateCommand(
                OnDeleteExecute, OnDeleteCanExecute);
        }

        public Friend11Wrapper Friend
        {
            get { return _friend; }
            private set
            {
                _friend = value;
                OnPropertyChanged();
            }
        }

        public bool HasChanges
        {
            get { return _hasChanges; }
            set
            {
                if (_hasChanges != value)
                {
                    _hasChanges = value;
                    OnPropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public int InstanceCountDVM
        {
            get { return _instanceCountDVM; }
            set
            {
                if (_instanceCountDVM == value)
                    return;
                _instanceCountDVM = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadAsync(int? friendId)
        {
            var friend = friendId.HasValue
                ? await _friendRepository.FindByIdAsync(friendId.Value)
                : CreateNewFriend();

            Friend = new Friend11Wrapper(friend);
            Friend.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _friendRepository.HasChanges();
                }

                if (e.PropertyName == nameof(Friend.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();

            // Little trick to trigger the validation when creating new entries
            if (Friend.Id == 0)
            {
                Friend.FirstName = "";
            }
        }

        private async void OnSaveExecute()
        {
            await _friendRepository.UpdateAsync();

            HasChanges = _friendRepository.HasChanges();

            // Tell the List that we have updated something
            _eventAggregator.GetEvent<AfterFriendSavedEvent11>()
                .Publish(new AfterFriendSavedEventArgs11
                {
                    Id = Friend.Id,
                    DisplayMember = $"{Friend.FirstName} {Friend.LastName}"
                });
        }

        private bool OnSaveCanExecute()
        {
            return Friend != null && !Friend.HasErrors && HasChanges;
        }

        private bool OnDeleteCanExecute()
        {
            return true;
        }
        private async void OnDeleteExecute()
        {
            _friendRepository.Remove(Friend.Model);
           await  _friendRepository.UpdateAsync();

            _eventAggregator.GetEvent<AfterFriendDeletedEvent11>()
                .Publish(Friend.Id);
        }

        private Friend05 CreateNewFriend()
        {
            var friend = new Friend05();
            _friendRepository.Add(friend);
            return friend;
        }
    }
}
