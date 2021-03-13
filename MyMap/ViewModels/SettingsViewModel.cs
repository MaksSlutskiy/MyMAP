using System;
using MyMap.Helps;
using MyMap.Model;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace MyMap.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private DelegateCommand _advertisingCommand;
        private DelegateCommand _aboutAppCommand;
        private DelegateCommand _themeChangecommand;
        private DelegateCommand _languageChangecommand;
        private DelegateCommand _contactscommand;
        private DelegateCommand _backCommand;
        private DataContainer dataContainer;
        private bool _isRefreshView;

        public bool IsRefreshView
        {
            get => _isRefreshView;
            set
            {
                SetProperty(ref _isRefreshView, value);
            }
        }
        public DelegateCommand BackCommand =>
           _backCommand ?? (_backCommand = new DelegateCommand(async () =>
           {
               var nav = new NavigationParameters();
               nav.Add("CheckTheme", true);
               await NavigationService.GoBackAsync(nav);
           }));
        public DelegateCommand AboutAppCommand =>
            _aboutAppCommand ?? (_aboutAppCommand = new DelegateCommand(() =>
            {
                NavigateAsync("AboutAppPage");
            }));

        public DelegateCommand AdvertisingCommand =>
            _advertisingCommand ?? (_advertisingCommand = new DelegateCommand(() => NavigateAsync("AdvertisingPage")));

        public DelegateCommand LanguageChangecommand =>
           _languageChangecommand ?? (_languageChangecommand = new DelegateCommand(() =>
               NavigateAsync("LanguageDialogPage")));
        public DelegateCommand Contactscommand =>
          _contactscommand ?? (_contactscommand = new DelegateCommand(() =>
             NavigateAsync("ContactsPage")));

        public DelegateCommand ThemeChangecommand =>
            _themeChangecommand ?? (_themeChangecommand = new DelegateCommand(() =>
            {
                if (Application.Current.UserAppTheme == OSAppTheme.Dark)
                    Application.Current.UserAppTheme = OSAppTheme.Light;
                else
                    Application.Current.UserAppTheme = OSAppTheme.Dark;
                SaveTheme();
            }));

        public SettingsViewModel(INavigationService navigationService, IPageDialogService pageDialogService) :
            base(navigationService, pageDialogService)
        {
            this.Title = Translator.TranslatorInstance["Test"];
            dataContainer = new DataContainer();
        }
        private void SaveTheme()
        {
            dataContainer.RemoveValue(App.Info.ThemeKey);
            dataContainer.AddValue(App.Info.ThemeKey, Application.Current.UserAppTheme.ToString());
        }
    }
}
