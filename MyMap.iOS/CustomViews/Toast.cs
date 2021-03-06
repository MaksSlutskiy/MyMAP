using System;
using CoreGraphics;
using Foundation;
using MyMap.CustomViews;
using MyMap.iOS.CustomViews;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
[assembly: Xamarin.Forms.Dependency(typeof(Toast))]
namespace MyMap.iOS.CustomViews
{
    public class Toast : IToast
    {
        const double LongDelay = 2;
        const double ShortDelay = 1.5;

        UIView customView = null;
        UILabel label = null;
        UIImageView imageView = null;

        public Toast()
        {
            UIDevice.Notifications.ObserveOrientationDidChange(ChangeRotationCallback);
        }

        private void CreateToast(string message, string image, UIColor backgroundHexColor = null, UIColor textHexColor = null, ToastLength toastLength = ToastLength.Short)
        {
            var delay = toastLength == ToastLength.Short ? ShortDelay : LongDelay;

            var self = GetVisibleViewController();
            var bounds = self.View.Bounds;

            customView = new UIView();
            customView.AccessibilityIdentifier = "customView";
            customView.BackgroundColor = backgroundHexColor;
            customView.Layer.CornerRadius = 15;
            customView.Layer.ShadowColor = UIColor.Black.CGColor;
            customView.Layer.ShadowOpacity = (float)0.8;
            customView.Layer.ShadowRadius = 6;
            customView.Layer.ShadowOffset = new CGSize(width: 4.0, height: 4.0);
            //label
            label = new UILabel();
            label.Text = message;
            label.TextColor = textHexColor;
            label.LineBreakMode = UILineBreakMode.WordWrap;
            label.Lines = 5;
            label.Font = UIFont.SystemFontOfSize(15);
            label.Frame = new CGRect(45, 10, bounds.Width * 0.7 - 50, 0);
            label.SizeToFit();
            //image
            var img = UIImage.FromFile(image).ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
            imageView = new UIImageView(img);
            imageView.TintColor = textHexColor;
            imageView.Frame = new CGRect(10, label.Bounds.Size.Height / 2 - 5, 30, 30);

            customView.Frame = new CGRect(bounds.Width * 0.15, 0, bounds.Width * 0.7, label.Bounds.Size.Height + 20);

            customView.AddSubview(imageView);
            customView.AddSubview(label);
            self.View.AddSubview(customView);

            //Animation
            var pt = customView.Center;
            UIView.Animate(0.14, 0, UIViewAnimationOptions.CurveLinear, () => {
                customView.Center = new CGPoint(pt.X, 100f);
            }, null);
            UIView.Animate(0.4, delay, UIViewAnimationOptions.BeginFromCurrentState, () => { customView.Alpha = 0.0f; }, () => { DismissMessage(); });

            //var animations = new List<CAAnimation>();

            //var animRotate = CAKeyFrameAnimation.FromKeyPath("transform");
            //animRotate.Values = new NSObject[] { NSNumber.FromFloat (-50f), NSNumber.FromFloat (0f)};
            //animRotate.ValueFunction = CAValueFunction.FromName(CAValueFunction.TranslateY);

            //animRotate.Duration = 1.5;
            //animRotate.Speed = 10;

            //customView.Layer.AddAnimation(animRotate, "transform");
        }

        private void ChangeRotationCallback(object sender, NSNotificationEventArgs e)
        {
            if (customView != null)
            {
                var bounds = GetVisibleViewController().View.Bounds;
                label.Frame = new CGRect(45, 10, bounds.Width * 0.7 - 50, 0);
                label.SizeToFit();

                if (imageView != null)
                    imageView.Frame = new CGRect(10, label.Bounds.Size.Height / 2 - 5, 30, 30);

                customView.Frame = new CGRect(bounds.Width * 0.15f, 90f - label.Bounds.Size.Height / 2, bounds.Width * 0.7f, label.Bounds.Size.Height + 20f);
            }
        }

        private void CreateToast(string message, UIColor backgroundHexColor = null, UIColor textHexColor = null, ToastLength toastLength = ToastLength.Short)
        {
            var delay = toastLength == ToastLength.Short ? ShortDelay : LongDelay;

            var self = GetVisibleViewController();
            var bounds = self.View.Bounds;

            customView = new UIView();
            customView.BackgroundColor = backgroundHexColor;
            customView.Layer.CornerRadius = 15;
            customView.Layer.ShadowColor = UIColor.Black.CGColor;
            customView.Layer.ShadowOpacity = (float)0.8;
            customView.Layer.ShadowRadius = 6;
            customView.Layer.ShadowOffset = new CGSize(width: 4.0, height: 4.0);
            //label
            label = new UILabel();
            label.Text = message;
            label.TextColor = textHexColor;
            label.LineBreakMode = UILineBreakMode.WordWrap;
            label.Lines = 5;
            label.Font = UIFont.SystemFontOfSize(15);
            label.Frame = new CGRect(15, 10, bounds.Width * 0.7 - 50, 0);
            label.SizeToFit();

            customView.Frame = new CGRect(bounds.Width * 0.15, 0, bounds.Width * 0.7, label.Bounds.Size.Height + 20);
            customView.AddSubview(label);
            self.View.AddSubview(customView);

            //Animation
            var pt = customView.Center;
            UIView.Animate(0.14, 0, UIViewAnimationOptions.CurveLinear, () => {
                customView.Center = new CGPoint(pt.X, 100f);
            }, null);
            UIView.Animate(0.4, delay, UIViewAnimationOptions.BeginFromCurrentState, () => { customView.Alpha = 0.0f; }, () => { DismissMessage(); });

        }

        private void DismissMessage()
        {
            //alert?.DismissViewController(true, complete);
            //alertDelay?.Dispose();
            customView?.Dispose();
            label?.Dispose();
            imageView?.Dispose();

            customView = null;
            label = null;
            imageView = null;
        }

        private UIViewController GetVisibleViewController()
        {
            try
            {
                var rootController = UIApplication.SharedApplication.KeyWindow.RootViewController;

                switch (rootController.PresentedViewController)
                {
                    case null:
                        return rootController;
                    case UINavigationController controller:
                        return controller.VisibleViewController;
                    case UITabBarController barController:
                        return barController.SelectedViewController;
                    default:
                        return rootController.PresentedViewController;
                }
            }
            catch (Exception)
            {
                return UIApplication.SharedApplication.KeyWindow.RootViewController;
            }
        }

        public void ShowCustomToast(string message, string image, Color bgColor, Color txtColor, ToastLength toastLength = ToastLength.Short)
        {
            CreateToast(message, image, bgColor.ToUIColor(), txtColor.ToUIColor(), toastLength);
        }

        public void ShowCustomToast(string message, Color bgColor, Color txtColor, ToastLength toastLength = ToastLength.Short)
        {
            CreateToast(message, bgColor.ToUIColor(), txtColor.ToUIColor(), toastLength);
        }
    }
}
