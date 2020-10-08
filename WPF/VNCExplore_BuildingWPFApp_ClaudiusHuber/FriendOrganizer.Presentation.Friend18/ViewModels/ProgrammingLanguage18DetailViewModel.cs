using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using FriendOrganizer.DomainServices.Repositories;
using FriendOrganizer.Presentation.Friend18.ModelWrappers;
using Prism.Commands;
using Prism.Events;

using VNCExplore_FriendOrganizer.Core.Services;

namespace FriendOrganizer.Presentation.Friend18.ViewModels
{
    public class ProgrammingLanguage18DetailViewModel 
        : DetailViewModelBase18, IProgrammingLanguage18DetailViewModel
    {
        IProgrammingLanguageRepository18 _programmingLanguageRepository;
        private ProgrammingLanguage18Wrapper _selectedProgrammingLanguage;

        public ObservableCollection<ProgrammingLanguage18Wrapper> ProgrammingLanguages18 { get; }

        public ProgrammingLanguage18DetailViewModel(
            IEventAggregator eventAggregator, 
            IMessageDialogService messageDialogService,
            IProgrammingLanguageRepository18 programmingLanguageRepository) 
            : base(eventAggregator, messageDialogService)
        {
            _programmingLanguageRepository = programmingLanguageRepository;
            Title = "Programming Languages";
            ProgrammingLanguages18 = new ObservableCollection<ProgrammingLanguage18Wrapper>();

            AddCommand = new DelegateCommand(OnAddExecute);
            RemoveCommand = new DelegateCommand(OnRemoveExecute, OnRemoveCanExecute);
        }

        public ICommand AddCommand { get; }

        public ICommand RemoveCommand { get; }

        public ProgrammingLanguage18Wrapper SelectedProgrammingLanguage
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

            foreach (var wrapper in ProgrammingLanguages18)
            {
                wrapper.PropertyChanged -= Wrapper_PropertyChanged;
            }

            ProgrammingLanguages18.Clear();

            var languages = await _programmingLanguageRepository.GetAllAsync();

            foreach (var model in languages)
            {
                var wrapper = new ProgrammingLanguage18Wrapper(model);
                wrapper.PropertyChanged += Wrapper_PropertyChanged;
                ProgrammingLanguages18.Add(wrapper);
            }
        }

        void Wrapper_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (! HasChanges)
            {
                HasChanges = _programmingLanguageRepository.HasChanges();
            }

            if (e.PropertyName == nameof(ProgrammingLanguage18Wrapper.HasErrors))
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
            return HasChanges && ProgrammingLanguages18.All(p => !p.HasErrors);
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
            var wrapper = new ProgrammingLanguage18Wrapper(new Domain.ProgrammingLanguage12());
            wrapper.PropertyChanged += Wrapper_PropertyChanged;
            _programmingLanguageRepository.Add(wrapper.Model);
            ProgrammingLanguages18.Add(wrapper);

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
                    " can't be removed;  It is refrenced by at least one friend");
                return;
            }

            SelectedProgrammingLanguage.PropertyChanged -= Wrapper_PropertyChanged;
            _programmingLanguageRepository.Remove(SelectedProgrammingLanguage.Model);
            ProgrammingLanguages18.Remove(SelectedProgrammingLanguage);
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
