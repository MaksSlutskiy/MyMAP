using MyMap.Controls;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace MyMap.ViewModels
{
    public class ViewModelBase : BindableBase, IInitialize, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; private set; }
        protected IPageDialogService PageDialogService { get; private set; }
        protected IEventAggregator EventAggregator { get; private set; }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        private bool _isNavigating = true;
        protected bool IsNavigating
        {
            get => _isNavigating;
            set => SetProperty(ref _isNavigating, value);
        }

        protected ViewModelBase()
        {
        }

        protected ViewModelBase(INavigationService navigationService) : this()
        {
            NavigationService = navigationService;
        }

        protected ViewModelBase(INavigationService navigationService, IEventAggregator eventAggregator) : this(navigationService)
        {
            EventAggregator = eventAggregator;
        }

        protected ViewModelBase(INavigationService navigationService, IPageDialogService pageDialogService) : this(navigationService)
        {
            PageDialogService = pageDialogService;
        }

        protected ViewModelBase(INavigationService navigationService, IEventAggregator eventAggregator, IPageDialogService pageDialogService) : this(navigationService, eventAggregator)
        {
            PageDialogService = pageDialogService;
        }


        public virtual void Initialize(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
            IsNavigating = false;
        }

        public virtual void Destroy()
        {

        }
        protected Task NavigateAsync(string name, NavigationParameters parameters = null, bool? useModalNavigation = null, bool animated = true)
        {
            if (_isNavigating)
                return Task.CompletedTask;
            IsNavigating = true;
            try { NavigationService.NavigateAsync(name, parameters, useModalNavigation, animated); IsNavigating = false; return Task.CompletedTask; }
            catch { return Task.CompletedTask; }
        }
    }

    public abstract class ViewModelToolbarBase : ViewModelBase
    {
        private DelegateCommand<object> _itemSelectionChangedCommand;
        private ObservableCollection<ViewItem> _toolbarItems;

        public ObservableCollection<ViewItem> ToolbarItems
        {
            get => _toolbarItems;
            set => SetProperty(ref _toolbarItems, value);
        }

        //protected IDialogService DialogService { get; private set; }

        public DelegateCommand<object> ItemSelectionChangedCommand =>
            _itemSelectionChangedCommand ?? (_itemSelectionChangedCommand = new DelegateCommand<object>((parameter) => ToolbarItemClicked(parameter)));

        protected ViewModelToolbarBase(INavigationService navigationService, IPageDialogService pageDialogService) :
            base(navigationService, pageDialogService)
        {
        }

        //protected ViewModelToolbarBase(INavigationService navigationService, IPageDialogService pageDialogService, IDialogService dialogService) :
        //    base(navigationService, pageDialogService)
        //{
        //    DialogService = dialogService;
        //}

        protected ViewModelToolbarBase(INavigationService navigationService, IEventAggregator eventAggregator, IPageDialogService pageDialogService) :
            base(navigationService, eventAggregator, pageDialogService)
        {
        }

        protected abstract void ToolbarItemClicked(object parameter);
    }
}
