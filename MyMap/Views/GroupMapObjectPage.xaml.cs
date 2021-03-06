using System;
using System.Collections.Generic;
using MyMap.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace MyMap.Views
{
    public partial class GroupMapObjectPage : ContentPage
    {
        public GroupMapObjectPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (App.Info.Device == DevicePlatform.iOS)
            this.Grid.Margin = new Thickness(0, 40, 0, 0);
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
                this.SourceItems.Margin = new Thickness(size, 0, size, 0);
                //this.MainGrid.Margin = new Thickness(0, safeInsets.Top, 0, 0);
                this.BottomBar.Margin = new Thickness(0, 0, 0, 21);
            }
        }
        void CheckBox_CheckedChanged(System.Object sender, Xamarin.Forms.CheckedChangedEventArgs e)
        {
            GroupMapObjectViewModel model = BindingContext as GroupMapObjectViewModel;

            if (model != null)
                model.RefreshGroupEnabled((sender as CheckBox).IsChecked);
        }
    }
}
