using System.Threading.Tasks;
using DryIoc;
using Microsoft.EntityFrameworkCore;
using MyMap.Controls;
using MyMap.Interface;
using MyMap.IRepository;
using MyMap.Model;
using MyMap.Moduls;
using MyMap.Service;
using MyMap.ViewModels;
using MyMap.ViewModels.DialogViewModel;
using MyMap.Views;
using MyMap.Views.DialogViews;
using Prism;
using Prism.Ioc;
using Prism.Plugin.Popups;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using SqliteApp.Standart.Abstructions;
using SqliteApp.Standart.Context;
using SqliteApp.Standart.Entites;
using SqliteApp.Standart.Interface;
using SqliteApp.Standart.Repositories;
using SqliteApp.Standart.UnitOfWork;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace MyMap
{
    public partial class App
    {
        public static DbManagerModule DBModulManager { get; private set; }

        public static AppModelInfo Info { get; private set; }

        public App() : this(null)
        {
        }
        public App(IPlatformInitializer initializer) : base(initializer)
        {
        }

        private async void GetData()
        {
            await Task.Run(() =>
            {
               
                var container = new SimpleInjector.Container();
                container.Options.ResolveUnregisteredConcreteTypes = true;
               container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
                container.Options.EnableAutoVerification = false;
                container.Register<IRepository<MapObject>, MapObjectRepository>();
                container.Register<IService<MapObjectPin>, MapObjectService>();
                container.Register<DbContext, MobileContext>();
                DBModulManager = new DbManagerModule(container.GetInstance<IService<MapObjectPin>>());
            });
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            Info = new AppModelInfo();
            GetData();
            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<SideBarPopupPage, SideBarViewModel>();

            containerRegistry.RegisterForNavigation<EditPinDialogPage, EditPinDialogViewModel>();

            containerRegistry.RegisterPopupNavigationService();

           

        }
    }
}
