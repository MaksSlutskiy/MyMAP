using System.IO;
using MyMap.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace MyMap.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            SetSafaZoneMargin();
        }
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            SetSafaZoneMargin();
        }
        private void SetSafaZoneMargin()
        {
            var res = On<iOS>().UsingSafeArea();
            if (App.Info.Device == DevicePlatform.iOS && App.Info.IsSafeAreaSupport)
            {
                this.BoxView.IsVisible = true;
                var safeInsets = On<iOS>().SafeAreaInsets();
                double size = 0;
                if (safeInsets.Right > 0)
                    size = safeInsets.Right;
                else
                    size = safeInsets.Left;
                //this.MainGrid.Margin = new Thickness(0, safeInsets.Top, 0, 0);
                this.BottomBar.Margin = new Thickness(0, 0, 0, 21);
            }
        }


    }
}
