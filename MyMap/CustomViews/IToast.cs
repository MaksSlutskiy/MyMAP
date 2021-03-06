using System;
using Xamarin.Forms;

namespace MyMap.CustomViews
{
    public interface IToast
    {
        void ShowCustomToast(string message, string image, Color bgColor, Color txtColor, ToastLength toastLength = ToastLength.Short);
        void ShowCustomToast(string message, Color bgColor, Color txtColor, ToastLength toastLength = ToastLength.Short);
    }

    public enum ToastLength
    {
        Short = 0,
        Long = 1
    };
}
