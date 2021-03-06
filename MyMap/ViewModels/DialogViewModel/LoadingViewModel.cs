using System;
using Prism.Navigation;

namespace MyMap.ViewModels.DialogViewModel
{
    public class LoadingViewModel : ViewModelBase
    {
        private string _titleProgress;

        public string TitleProgress
        {
            get => _titleProgress;
            set => SetProperty(ref _titleProgress, value);
        }

        public LoadingViewModel(INavigationService navigationService)
            : base(navigationService) { }
    }
}
