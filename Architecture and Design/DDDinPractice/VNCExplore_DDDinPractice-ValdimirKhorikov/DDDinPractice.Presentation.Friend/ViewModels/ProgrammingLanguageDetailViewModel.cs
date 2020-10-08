﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using FriendOrganizer.DomainServices.Repositories;
using FriendOrganizer.Presentation.Friend.ModelWrappers;
using Prism.Commands;
using Prism.Events;

using VNCExplore_FriendOrganizer.Core.Services;

namespace FriendOrganizer.Presentation.Friend.ViewModels
{
    public class ProgrammingLanguageDetailViewModel 
        : DetailViewModelBase, IProgrammingLanguageDetailViewModel
    {
        IProgrammingLanguageRepository _programmingLanguageRepository;
        private ProgrammingLanguageWrapper _selectedProgrammingLanguage;

        public ObservableCollection<ProgrammingLanguageWrapper> ProgrammingLanguages { get; }

        public ProgrammingLanguageDetailViewModel(
            IEventAggregator eventAggregator, 
            IMessageDialogService messageDialogService,
            IProgrammingLanguageRepository programmingLanguageRepository) 
            : base(eventAggregator, messageDialogService)
        {
            _programmingLanguageRepository = programmingLanguageRepository;
            Title = "Programming Languages";
            ProgrammingLanguages = new ObservableCollection<ProgrammingLanguageWrapper>();

            AddCommand = new DelegateCommand(OnAddExecute);
            RemoveCommand = new DelegateCommand(OnRemoveExecute, OnRemoveCanExecute);
        }

        public ICommand AddCommand { get; }

        public ICommand RemoveCommand { get; }

        public ProgrammingLanguageWrapper SelectedProgrammingLanguage
        {
            get { return _selectedProgrammingLanguage; }
            set
            {
                _selectedProgrammingLanguage = value;
                OnPropertyChanged();
                ((DelegateCommand)RemoveCommand).RaiseCanExecuteChanged();
            }
        }

        public async override Task LoadAsync(int id)
        {
            Id = id;

            foreach (var wrapper in ProgrammingLanguages)
            {
                wrapper.PropertyChanged -= Wrapper_PropertyChanged;
            }

            ProgrammingLanguages.Clear();

            var languages = await _programmingLanguageRepository.GetAllAsync();

            foreach (var model in languages)
            {
                var wrapper = new ProgrammingLanguageWrapper(model);
                wrapper.PropertyChanged += Wrapper_PropertyChanged;
                ProgrammingLanguages.Add(wrapper);
            }
        }

        void Wrapper_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (! HasChanges)
            {
                HasChanges = _programmingLanguageRepository.HasChanges();
            }

            if (e.PropertyName == nameof(ProgrammingLanguageWrapper.HasErrors))
            {
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }


        protected override void OnDeleteExecute()
        {
            throw new NotImplementedException();
        }

        protected override bool OnSaveCanExecute()
        {
            return HasChanges && ProgrammingLanguages.All(p => !p.HasErrors);
        }

        protected async override void OnSaveExecute()
        {
            try
            {
                await _programmingLanguageRepository.UpdateAsync();
                HasChanges = _programmingLanguageRepository.HasChanges();
                RaiseCollectionSavedEvent();
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }

                MessageDialogService.ShowInfoDialog(
                    "Error while saving th entities, " +
                    "the data will be reloaded.  Details: " + ex.Message);
                await LoadAsync(Id);
            }
        }

        void OnAddExecute()
        {
            var wrapper = new ProgrammingLanguageWrapper(new Domain.ProgrammingLanguage());
            wrapper.PropertyChanged += Wrapper_PropertyChanged;
            _programmingLanguageRepository.Add(wrapper.Model);
            ProgrammingLanguages.Add(wrapper);

            wrapper.Name = "";  // Trigger the validation
        }

        private async void OnRemoveExecute()
        {
            var isReferenced =
                await _programmingLanguageRepository.IsReferencedByFriendAsync(
                    SelectedProgrammingLanguage.Id);

            if (isReferenced)
            {
                MessageDialogService.ShowInfoDialog(
                    $"The language {SelectedProgrammingLanguage.Name}" +
                    " can't be removed;  It is referenced by at least one friend");
                return;
            }

            SelectedProgrammingLanguage.PropertyChanged -= Wrapper_PropertyChanged;
            _programmingLanguageRepository.Remove(SelectedProgrammingLanguage.Model);
            ProgrammingLanguages.Remove(SelectedProgrammingLanguage);
            SelectedProgrammingLanguage = null;
            HasChanges = _programmingLanguageRepository.HasChanges();
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        bool OnRemoveCanExecute()
        {
            return SelectedProgrammingLanguage != null;
        }
    }
}
