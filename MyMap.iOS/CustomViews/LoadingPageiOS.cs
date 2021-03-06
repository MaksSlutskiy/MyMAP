using System;
using MyMap.CustomViews;
using MyMap.iOS.CustomViews;
using MyMap.Views.DialogViews;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XFPlatform = Xamarin.Forms.Platform.iOS.Platform;

[assembly: Xamarin.Forms.Dependency(typeof(LoadingPageiOS))]
namespace MyMap.iOS.CustomViews
{
    public class LoadingPageiOS : ILoadingPage
    {
        private UIView _nativeView;

        private bool _isInitialized;

        public void InitLoadingPage(ContentPage loadingIndicatorPage)
        {
            if (loadingIndicatorPage != null)
            {
                loadingIndicatorPage.Parent = Xamarin.Forms.Application.Current.MainPage;

                loadingIndicatorPage.Layout(new Rectangle(0, 0,
                    Xamarin.Forms.Application.Current.MainPage.Width,
                    Xamarin.Forms.Application.Current.MainPage.Height));

                var renderer = loadingIndicatorPage.GetOrCreateRenderer();

                _nativeView = renderer.NativeView;

                _isInitialized = true;
            }
        }

        public void ShowLoadingPage()
        {
            if (!_isInitialized)
                InitLoadingPage(new LoadingPage());
            UIApplication.SharedApplication.KeyWindow.AddSubview(_nativeView);
        }

        private void XamFormsPage_Appearing(object sender, EventArgs e)
        {
            var animation = new Animation(callback: d => ((ContentPage)sender).Content.Rotation = d,
                start: ((ContentPage)sender).Content.Rotation,
                end: ((ContentPage)sender).Content.Rotation + 360,
                easing: Easing.Linear);
            animation.Commit(((ContentPage)sender).Content, "RotationLoopAnimation", 16, 800, null, null, () => true);
        }

        public void HideLoadingPage()
        {
            _nativeView.RemoveFromSuperview();
        }
    }
    internal static class PlatformExtension
    {
        public static IVisualElementRenderer GetOrCreateRenderer(this VisualElement bindable)
        {
            var renderer = XFPlatform.GetRenderer(bindable);
            if (renderer == null)
            {
                renderer = XFPlatform.CreateRenderer(bindable);
                XFPlatform.SetRenderer(bindable, renderer);
            }
            return renderer;
        }
    }
}
