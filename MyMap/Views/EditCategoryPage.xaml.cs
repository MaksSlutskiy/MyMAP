using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace MyMap.Views
{
    public partial class EditCategoryPage : ContentPage
    {
        public EditCategoryPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (App.Info.Device == DevicePlatform.iOS)
                this.MainGrid.Margin = new Thickness(0, 40, 0, 0);
            SetSafaZoneMargin();
        }
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            SetSafaZoneMargin();
        }
        private void SetSafaZoneMargin()
        {
            if (App.Info.Device == DevicePlatform.iOS && App.Info.IsSafeAreaSupport)
            {
                this.BoxView.IsVisible = true;
                var safeInsets = On<iOS>().SafeAreaInsets();
                double size = 0;
                if (safeInsets.Right > 0)
                    size = safeInsets.Right;
                else
                    size = safeInsets.Left;
                this.SourceItems.Margin = new Thickness(size, 0, size, 71);
                //this.MainGrid.Margin = new Thickness(0, safeInsets.Top, 0, 0);
                this.BottomBar.Margin = new Thickness(0, 0, 0, 21);
            }
        }
    }
}
