﻿using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using Prism.Commands;
using Prism.Events;

using VNC.Core.Events;
using VNC.Core.Mvvm;

using VNCExplore_FriendOrganizer.Core.Events;
using VNCExplore_FriendOrganizer.Core.Services;

namespace FriendOrganizer.Presentation.Friend19.ViewModels
{
    public abstract class DetailViewModelBase19 : ViewModelBase, IDetailViewModel
    {
        private string _title;
        private int _id;
        protected readonly IEventAggregator EventAggregator;
        protected readonly IMessageDialogService MessageDialogService;
        private bool _hasChanges;
        private static int _instanceCountDVM = 0;

        public DetailViewModelBase19(
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService)
        {
            _instanceCountDVM++;
            EventAggregator = eventAggregator;
            MessageDialogService = messageDialogService;

            SaveCommand = new DelegateCommand(
                OnSaveExecute, OnSaveCanExecute);

            DeleteCommand = new DelegateCommand(
                OnDeleteExecute);

            CloseDetailViewCommand = new DelegateCommand(
                OnCloseDetailViewExecute);
        }

        public ICommand SaveCommand { get; private set; }

        public ICommand DeleteCommand { get; private set; }

        public ICommand CloseDetailViewCommand { get; private set; }

        public int Id
        {
            get { return _id; }
            protected set
            {
                //if (_id == value)
                //    return;
                _id = value;
                //OnPropertyChanged();
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

        public abstract Task LoadAsync(int id);
        //public abstract Task LoadAsync(int? id);

        protected abstract void OnDeleteExecute();

        protected abstract bool OnSaveCanExecute();

        protected abstract void OnSaveExecute();

        protected virtual void RaiseCollectionSavedEvent()
        {
            EventAggregator.GetEvent<AfterCollectionSavedEvent19>()
                .Publish(new AfterCollectionSavedEventArgs
                {
                    ViewModelName = this.GetType().Name
                });
        }

        protected virtual void OnCloseDetailViewExecute()
        {
            if (HasChanges)
            {
                var result = MessageDialogService.ShowOkCancelDialog(
                    "You've made changes.  Close this item?", "Question");

                if (result == MessageDialogResult.Cancel)
                {
                    return;
                }
            }

            EventAggregator.GetEvent<AfterDetailClosedEvent19>()
                .Publish(new AfterDetailClosedEventArgs
                {
                    Id = this.Id,
                    ViewModelName = this.GetType().Name
                });
        }

        protected virtual void RaiseDetailDeletedEvent(int modelId)
        {
            EventAggregator.GetEvent<AfterDetailDeletedEvent19>()
                .Publish(new AfterDetailDeletedEventArgs
                {
                    Id = modelId,
                    ViewModelName = this.GetType().Name
                });
        }

        protected virtual void RaiseDetailSavedEvent(int modelId, string displayMember)
        {
            EventAggregator.GetEvent<AfterDetailSavedEvent19>()
                .Publish(new AfterDetailSavedEventArgs
                {
                    Id = modelId,
                    DisplayMember = displayMember,
                    ViewModelName = this.GetType().Name
                });
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

        public string Title
        {
            get { return _title; }
            set
            {
                if (_title == value)
                    return;
                _title = value;
                OnPropertyChanged();
            }
        }

        protected async Task SaveWithOptimisticConcurrencyAsync(Func<Task> saveFunc, Action afterSaveAction)
        {
            try
            {
                await saveFunc();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var databaseValues = ex.Entries.Single().GetDatabaseValues();

                if (databaseValues == null)
                {
                    MessageDialogService.ShowInfoDialog(
                        "The entity has been deleted by another user.  Cannot continue.");
                    RaiseDetailDeletedEvent(Id);
                    return;
                }

                var result = MessageDialogService.ShowOkCancelDialog(
                    "The entity has been changed by someone else." +
                    " Click OK to save your changes anyway; Click Cancel" +
                    " to reload the entity from the database.", "Question");

                if (result == MessageDialogResult.OK)   // Client Wins
                {
                    // Update the original values with database-values
                    var entry = ex.Entries.Single();
                    entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                    await saveFunc();
                }
                else  // Database Wins
                {
                    // Reload entity from database
                    await ex.Entries.Single().ReloadAsync();
                    await LoadAsync(Id);
                }
            }

            // Do anything that needs to occur after saving

            afterSaveAction();
        }
    }
}
