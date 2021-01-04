using System;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;

namespace MyMap.ViewModels
{
    public class SideBarViewModel : ViewModelBase
    {
        private DelegateCommand _addPinCommand;

        public DelegateCommand AddPinCommand =>
            _addPinCommand ?? (_addPinCommand = new DelegateCommand(() =>
            {
                var nav_param = new NavigationParameters();
                nav_param.Add("ActionMap", ActionMap.AddPin);
                NavigationService.GoBackAsync(nav_param);
            }));

        public SideBarViewModel(INavigationService navigationService, IEventAggregator eventAggregator, IPageDialogService pageDialogService)
            : base(navigationService, eventAggregator, pageDialogService)
        {
        }
    }

    public enum ActionMap
    {
        AddPin,
        ClearMap
    }
}
