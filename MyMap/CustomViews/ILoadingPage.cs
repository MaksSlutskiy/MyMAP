using System;
using Xamarin.Forms;

namespace MyMap.CustomViews
{
    public interface ILoadingPage
    {
        void InitLoadingPage(ContentPage loadingIndicatorPage = null);
        void ShowLoadingPage();
        void HideLoadingPage();
    }
}
