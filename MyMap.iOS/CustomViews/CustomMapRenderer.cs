using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using CoreGraphics;
using Foundation;
using MapKit;
using MyMap.CustomViews;
using MyMap.Interface;
using MyMap.iOS.CustomViews;
using MyMap.Model;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace MyMap.iOS.CustomViews
{
    public class CustomMapRenderer : MapRenderer
    {
        UIView customPinView;
        List<CustomPin> customPins;
        MKMapView MapView;

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                var nativeMap = Control as MKMapView;
                if (nativeMap != null)
                {
                    nativeMap.RemoveAnnotations(nativeMap.Annotations);
                    nativeMap.GetViewForAnnotation = null;
                    //nativeMap.CalloutAccessoryControlTapped -= OnCalloutAccessoryControlTapped;
                    //nativeMap.DidSelectAnnotationView -= OnDidSelectAnnotationView;
                    //nativeMap.DidDeselectAnnotationView -= OnDidDeselectAnnotationView;
                }
            }

            if (e.NewElement != null)
            {
                var formsMap = (CustomMap)e.NewElement;
                MapView = Control as MKMapView;
                customPins = formsMap.CustomPins;

                MapView.GetViewForAnnotation = GetViewForAnnotation;
                //nativeMap.CalloutAccessoryControlTapped += OnCalloutAccessoryControlTapped;
                //nativeMap.DidSelectAnnotationView += OnDidSelectAnnotationView;
                //nativeMap.DidDeselectAnnotationView += OnDidDeselectAnnotationView;
            }
        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == CustomMap.IsUpdateProperty.PropertyName)
            {
                customPins = ((CustomMap)sender).CustomPins;
                MapView.GetViewForAnnotation = GetViewForAnnotation;
            }
        }

        protected override MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
        {
            MKAnnotationView annotationView = null;
            if (annotation is MKUserLocation)
                return null;
            var customPin = GetCustomPin(annotation as MKPointAnnotation);
            if (customPin == null)
            {
                throw new Exception("Custom pin not found");
            }
            annotationView = mapView.DequeueReusableAnnotation(customPin.Label);
            //if (annotationView == null)
            //{
                annotationView = new MKAnnotationView(annotation, customPin.Label);
                //annotationView.Image = UIImage.FromFile("pin.png");
                Xamarin.Forms.Color color = Xamarin.Forms.Color.FromHex(customPin.Color);
                UIImage image = GetColoredImage(customPin.Icon, color.ToUIColor());
                annotationView.Image = image;

                annotationView.CalloutOffset = new CGPoint(0, 0);
                int width = 40;
                int height = 40;
                annotationView.Frame = new CGRect(0, 0, width, height);

            //}
            annotationView.CanShowCallout = true;
            return annotationView;
        }
        CustomPin GetCustomPin(MKPointAnnotation annotation)
        {
            var position = new Position(annotation.Coordinate.Latitude, annotation.Coordinate.Longitude);
            foreach (var pin in customPins)
            {
                if (pin.Position == position)
                {
                    return pin;
                }
            }
            return null;
        }

        private UIImage GetColoredImage(string path,UIColor color)
        {
            UIImage image = UIImage.FromFile(path);
            UIImage coloredImage = null;
            if (color == null)
                color = UIColor.Black;
            UIGraphics.BeginImageContext(image.Size);
            using (CGContext context = UIGraphics.GetCurrentContext())
            {
                context.TranslateCTM(0, image.Size.Height);
                context.ScaleCTM(1.0f, -1.0f);

                var rect = new RectangleF(0, 0, (float)image.Size.Width, (float)image.Size.Height);

                context.ClipToMask(rect, image.CGImage);
                context.SetFillColor(color.CGColor);
                context.FillRect(rect);

                coloredImage = UIGraphics.GetImageFromCurrentImageContext();
                UIGraphics.EndImageContext();
            }
            return coloredImage;
        }

        //void OnDidSelectAnnotationView(object sender, MKAnnotationViewEventArgs e)
        //{
        //    //CustomMKAnnotationView customView = e.View as CustomMKAnnotationView;
        //    //customPinView = new UIView();

        //    //if (customView.Name.Equals("Xamarin"))
        //    //{
        //        customPinView.Frame = new CGRect(0, 0, 200, 84);
        //        var image = new UIImageView(new CGRect(0, 0, 200, 84));
        //        image.Image = UIImage.FromFile("xamarin.png");
        //        customPinView.AddSubview(image);
        //        customPinView.Center = new CGPoint(0, -(e.View.Frame.Height + 75));
        //        e.View.AddSubview(customPinView);
        //    //}
        //}
        //protected override MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
        //{
        //    MKAnnotationView annotationView = null;

        //    if (annotation is MKUserLocation)
        //        return null;

        //    //var customPin = GetCustomPin(annotation as MKPointAnnotation);
        //    //if (customPin == null)
        //    //{
        //    //    throw new Exception("Custom pin not found");
        //    //}

        //    //annotationView = mapView.DequeueReusableAnnotation(customPin.Name);
        //    //if (annotationView == null)
        //    //{
        //    //    annotationView = new CustomMKAnnotationView(annotation, customPin.Name);
        //    //    annotationView.Image = UIImage.FromFile("pin.png");
        //    //    annotationView.CalloutOffset = new CGPoint(0, 0);
        //    //    annotationView.LeftCalloutAccessoryView = new UIImageView(UIImage.FromFile("monkey.png"));
        //    //    annotationView.RightCalloutAccessoryView = UIButton.FromType(UIButtonType.DetailDisclosure);
        //    //    ((CustomMKAnnotationView)annotationView).Name = customPin.Name;
        //    //    ((CustomMKAnnotationView)annotationView).Url = customPin.Url;
        //    //}
        //    annotationView.CanShowCallout = true;

        //    return annotationView;
        //}
        //void OnCalloutAccessoryControlTapped(object sender, MKMapViewAccessoryTappedEventArgs e)
        //{
        //    //CustomMKAnnotationView customView = e.View as MKAnnotationView;
        //    //if (!string.IsNullOrWhiteSpace(customView.Url))
        //    //{
        //    //    UIApplication.SharedApplication.OpenUrl(new Foundation.NSUrl(customView.Url));
        //    //}
        //}
        //void OnDidDeselectAnnotationView(object sender, MKAnnotationViewEventArgs e)
        //{
        //    if (!e.View.Selected)
        //    {
        //        customPinView.RemoveFromSuperview();
        //        customPinView.Dispose();
        //        customPinView = null;
        //    }
        //}
    }
}
