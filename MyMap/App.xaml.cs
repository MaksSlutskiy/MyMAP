using System.Globalization;
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
        private DataContainer dataContainer;
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
                container.Register<IRepository<CategoryDb>, CategoryRepository>();
                container.Register<IService<MapObjectPin>, MapObjectService>();
                container.Register<IService<Category>, CategoryService>();
                container.Register<DbContext, MobileContext>();
                DBModulManager = new DbManagerModule(container.GetInstance<IService<MapObjectPin>>(), container.GetInstance<IService<Category>>());
            });
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            Info = new AppModelInfo();
            dataContainer = new DataContainer();
            GetData();
            GetLocalization();
            GetTheme();
            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<SideBarPopupPage, SideBarViewModel>();
            containerRegistry.RegisterForNavigation<GroupMapObjectPage, GroupMapObjectViewModel>();
            containerRegistry.RegisterForNavigation<EditCategoryPage, EditCategoryViewModel>();
            containerRegistry.RegisterForNavigation<SettingPage, SettingsViewModel>();

            containerRegistry.RegisterForNavigation<EditPinDialogPage, EditPinDialogViewModel>();
            containerRegistry.RegisterForNavigation<ShareMapDialogPage, ShareMapDialogViewModel>();
            containerRegistry.RegisterForNavigation<EditCategoryDialogPage, EditCategoryDialogViewModel>();
            containerRegistry.RegisterForNavigation<LanguageDialogPage, LanguageDialogViewModel>();
            containerRegistry.RegisterPopupNavigationService();

           

        }
        private void GetLocalization()
        {
            try
            {
                var language = dataContainer.GetValue(App.Info.LanguageKey);
                if (language != null)
                {
                    CultureInfo selectedCulture = new CultureInfo(language.ToString());
                    CultureInfo.DefaultThreadCurrentCulture = selectedCulture;
                    CultureInfo.DefaultThreadCurrentUICulture = selectedCulture;
                    CultureInfo.CurrentCulture = selectedCulture;
                    CultureInfo.CurrentUICulture = selectedCulture;
                }
            }
            catch { }

        }
        private void GetTheme()
        {
            try
            {
                var theme = dataContainer.GetValue(App.Info.ThemeKey).ToString();
                if (theme != null)
                {
                    if (theme == "Dark")
                        Application.Current.UserAppTheme = OSAppTheme.Dark;
                    else
                        Application.Current.UserAppTheme = OSAppTheme.Light;
                }
            }
            catch { }
        }
    }
}
