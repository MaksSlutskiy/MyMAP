using System;
using Xamarin.Forms;
using Android.Widget;
using MyMap.CustomViews;
using Xamarin.Forms.Platform.Android;
[assembly: Xamarin.Forms.Dependency(typeof(MyMap.Droid.CustomViews.Toast))]
namespace MyMap.Droid.CustomViews
{
    public class Toast : IToast
    {
        private static Android.Widget.Toast _instance;

        public void ShowCustomToast(string message, Color bgColor, Color txtColor, MyMap.CustomViews.ToastLength toastLength = MyMap.CustomViews.ToastLength.Short)
        {
            var length = toastLength == MyMap.CustomViews.ToastLength.Short ? Android.Widget.ToastLength.Short : Android.Widget.ToastLength.Long;
            // To dismiss existing toast, otherwise, the screen will be populated with it if the user do so
            _instance?.Cancel();
            _instance = Android.Widget.Toast.MakeText(Android.App.Application.Context, message, length);
            Android.Views.View tView = _instance.View;
            if (bgColor != null)
                tView.Background.SetColorFilter(bgColor.ToAndroid(), Android.Graphics.PorterDuff.Mode.SrcIn);//Gets the actual oval background of the Toast then sets the color filter

            TextView text = (TextView)tView.FindViewById(Android.Resource.Id.Message);
            if (txtColor != null)
                text.SetTextColor(txtColor.ToAndroid());
            _instance.Show();
        }

        public void ShowCustomToast(string message, string image, Color bgColor, Color txtColor, MyMap.CustomViews.ToastLength toastLength = MyMap.CustomViews.ToastLength.Short)
        {
            var length = toastLength == MyMap.CustomViews.ToastLength.Short ? Android.Widget.ToastLength.Short : Android.Widget.ToastLength.Long;
            // To dismiss existing toast, otherwise, the screen will be populated with it if the user do so
            _instance?.Cancel();
            _instance = Android.Widget.Toast.MakeText(Android.App.Application.Context, message, length);
            Android.Views.View tView = _instance.View;
            if (bgColor != null)
                tView.Background.SetColorFilter(bgColor.ToAndroid(), Android.Graphics.PorterDuff.Mode.SrcIn);//Gets the actual oval background of the Toast then sets the color filter

            TextView text = (TextView)tView.FindViewById(Android.Resource.Id.Message);
            if (txtColor != null)
                text.SetTextColor(txtColor.ToAndroid());
            _instance.Show();
        }
    }
}
