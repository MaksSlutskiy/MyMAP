using System;
using System.IO;
using CoreGraphics;
using Foundation;
using MyMap.Interface;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;
[assembly: Xamarin.Forms.DependencyAttribute(typeof(MyMap.iOS.Service.ViewToImageService))]
namespace MyMap.iOS.Service
{
    public class ViewToImageService : IViewToImageService
    {
        public byte[] GetImage(View viewTest)
        {
            var renderer = Platform.GetRenderer(viewTest);
            Platform.SetRenderer(viewTest, renderer);
            var viewRenderer = renderer.NativeView;
            var mapRenderer = (MapRenderer)viewRenderer;
            var mapView = mapRenderer.Control;

            var rendererImage = new UIGraphicsImageRenderer(mapView.Bounds.Size);
            var image = rendererImage.CreateImage((context) => mapView.DrawViewHierarchy(mapView.Bounds, true));
            
            byte[] byteArray;
            using (NSData imageData = image.AsPNG())
            {
                byteArray = new Byte[imageData.Length];
                System.Runtime.InteropServices.Marshal.Copy(imageData.Bytes, byteArray, 0, Convert.ToInt32(imageData.Length));
            }
            //var source = Xamarin.Forms.ImageSource.FromStream(() => new MemoryStream(byteArray));
            return byteArray;

        }
    }
}
