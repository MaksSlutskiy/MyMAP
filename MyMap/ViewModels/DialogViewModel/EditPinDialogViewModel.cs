using System;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;

namespace MyMap.ViewModels.DialogViewModel
{
    public class EditPinDialogViewModel : ViewModelBase
    {
        public EditPinDialogViewModel(INavigationService navigationService, IEventAggregator eventAggregator, IPageDialogService pageDialogService)
           : base(navigationService, eventAggregator, pageDialogService)
        {
        }
        private DelegateCommand _okCommand;

        public DelegateCommand OkCommand =>
            _okCommand ?? (_okCommand = new DelegateCommand(async () =>
            {
                await NavigationService.GoBackAsync();
            }));
    }
}
