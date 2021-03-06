using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using MyMap.Helps;
using MyMap.Model;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace MyMap.ViewModels.DialogViewModel
{
    class LanguageDialogViewModel : ViewModelBase
    {
        private DelegateCommand _closeCommand = null;
        private DelegateCommand _saveCommand = null;
        private DelegateCommand _openNotificationCommand = null;
        private ObservableCollection<Language> _languages;
        private DataContainer dataContainer;
        public ObservableCollection<Language> Languages
        {
            get => _languages;
            set
            {
                SetProperty(ref _languages, value);
            }
        }

        public DelegateCommand OpenNotificationCommand =>
                   _openNotificationCommand ?? (_openNotificationCommand = new DelegateCommand(async () =>
                   {
                       //await PageDialogService.DisplayAlertAsync(Translator.TranslatorInstance["Language_Warnings"],
                       // Translator.TranslatorInstance["Language_WarningsText"], Translator.TranslatorInstance["New_InputCertPassword_Ok"]);

                   }));

        public DelegateCommand CloseCommand =>
           _closeCommand ?? (_closeCommand = new DelegateCommand(() =>
           {
               NavigationService.GoBackAsync();
           }));

        public DelegateCommand SaveCommand =>
           _saveCommand ?? (_saveCommand = new DelegateCommand(() =>
           {
               RefreshLocalization();
               NavigationService.GoBackAsync();
           }));


        public LanguageDialogViewModel(INavigationService navigationService, IPageDialogService pageDialogService) :
           base(navigationService, pageDialogService)
        {
            AddLanguages();
            dataContainer = new DataContainer();
        }

        private void AddLanguages()
        {
            _languages = new ObservableCollection<Language>
            {
                new Language { Name = LanguageEnum.Русский, Reduction = "ru-RU", IsChange = false},
                new Language { Name = LanguageEnum.Українська, Reduction = "uk-UA", IsChange = false},
                new Language { Name = LanguageEnum.English, Reduction = "en-US", IsChange = false}
            };
            foreach (var item in _languages)
            {
                if (item.Reduction == CultureInfo.CurrentUICulture.Name)
                {
                    item.IsChange = true;
                }
            }
        }

        private void RefreshLocalization()
        {
            string language = Languages.Where(x => x.IsChange == true).Select(x => x.Reduction).FirstOrDefault();
            if (language != null)
            {
                CultureInfo newCulture = new CultureInfo(language);
                CultureInfo.CurrentUICulture = newCulture;
                dataContainer.RemoveValue(App.Info.LanguageKey);
                dataContainer.AddValue(App.Info.LanguageKey, language);
                Translator.TranslatorInstance.Invalidate();
            }


        }
    }
}
