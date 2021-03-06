using System;
using System.IO;
using Android.Graphics;
using Android.Widget;
using MyMap.Droid.CustomViews;
using MyMap.Droid.Service;
using MyMap.Interface;
using Xamarin.Forms;
using Xamarin.Forms.Maps.Android;
using Xamarin.Forms.Platform.Android;

[assembly: Dependency(typeof(ViewToImageService))]
namespace MyMap.Droid.Service
{
    public class ViewToImageService : IViewToImageService
    {
        public byte[] GetImage(View viewTest)
        {

            //var renderer = Platform.GetRenderer(viewTest);
            //Platform.SetRenderer(viewTest, renderer);
            //var viewRenderer = renderer.NativeView;
            //var mapRenderer = (MapRenderer)viewRenderer;
            //var mapView = mapRenderer.Control;

            //var rendererImage = new UIGraphicsImageRenderer(mapView.Bounds.Size);
            //var image = rendererImage.CreateImage((context) => mapView.DrawViewHierarchy(mapView.Bounds, true));

            //byte[] byteArray;
            //using (NSData imageData = image.AsPNG())
            //{
            //    byteArray = new Byte[imageData.Length];
            //    System.Runtime.InteropServices.Marshal.Copy(imageData.Bytes, byteArray, 0, Convert.ToInt32(imageData.Length));
            //}
            var renderer = viewTest.GetRenderer();
            Platform.SetRenderer(viewTest, renderer);
            var viewRenderer = renderer.View;
            var mapRenderer = (CustomMapRenderer)viewRenderer;

            //var mapView = mapRenderer.Control;

            Bitmap bitmap = Bitmap.CreateBitmap(mapRenderer.Bitmap.Width, mapRenderer.Bitmap.Height, Bitmap.Config.Argb8888);
            Canvas canvas = new Canvas(bitmap);
            canvas.DrawBitmap(mapRenderer.Bitmap, new Matrix(), null);
            byte[] bitmapData;
            using (var stream = new MemoryStream())
            {
                bitmap.Compress(Bitmap.CompressFormat.Png, 100, stream);
                bitmapData = stream.ToArray();
            }
            //var source = Xamarin.Forms.ImageSource.FromStream(() => new MemoryStream(bitmapData));
            return bitmapData;

        }
        //private void SnapshotDone(Bitmap bitmap)
        //{
        //    Bitmap bmOverlay = Bitmap.CreateBitmap(bitmap.Width, bitmap.Height, bitmap.GetConfig());
        //    Canvas canvas = new Canvas(bmOverlay);
        //    canvas.DrawBitmap(bitmap, new Matrix(), null);
        //    bitMap = bmOverlay;
        //}
    }

}
