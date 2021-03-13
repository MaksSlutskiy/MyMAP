using System;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Essentials;

namespace MyMap.ViewModels
{
    public class AboutAppViewModel : ViewModelBase
    {
        private DelegateCommand _backCommand;
        public AboutAppViewModel(INavigationService navigationService, IPageDialogService pageDialogService) :
            base(navigationService, pageDialogService)
        {
        }
        public string Version
        {
            get
            {
                return AppInfo.VersionString;
            }
        }
        public DelegateCommand BackCommand =>
          _backCommand ?? (_backCommand = new DelegateCommand(async () =>
          {
              await NavigationService.GoBackAsync();
          }));
    }
}
