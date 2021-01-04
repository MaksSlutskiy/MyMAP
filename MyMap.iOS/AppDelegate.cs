using Foundation;
using Prism;
using Prism.Ioc;
using UIKit;
using Xamarin.Forms;

namespace MyMap.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Forms.SetFlags(new string[] { "RadioButton_Experimental", "IndicatorView_Experimental", "AppTheme_Experimental" });

            Rg.Plugins.Popup.Popup.Init();
            Xamarin.FormsMaps.Init();
            Forms.Init();
            LoadApplication(new App(new iOSInitializer()));
            UINavigationBar.Appearance.TintColor = UIColor.FromRGB(255, 255, 255);
            UINavigationBar.Appearance.Translucent = false;
            UINavigationBar.Appearance.TitleTextAttributes = new UIStringAttributes
            {
                ForegroundColor = UIColor.White
            };

            UINavigationBar.Appearance.LargeTitleTextAttributes = new UIStringAttributes
            {
                ForegroundColor = UIColor.White
            };

            UINavigationBar.Appearance.BackgroundColor = UIColor.FromRGB(29, 131, 72);
            return base.FinishedLaunching(app, options);
        }
    }

    public class iOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}
