using System;
using System.Collections.Generic;
using System.ComponentModel;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Graphics;
using MyMap.CustomViews;
using MyMap.Droid.CustomViews;
using MyMap.Droid.Service;
using MyMap.Model;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace MyMap.Droid.CustomViews
{

    public class CustomMapRenderer : MapRenderer, GoogleMap.IInfoWindowAdapter, GoogleMap.ISnapshotReadyCallback
    {
        List<CustomPin> customPins;
        public Action<Bitmap> OnSnapshotDone;
        private GoogleMap googleMap;
        public Bitmap Bitmap;
        private FileService service;
        private Context mainContext;
        public CustomMapRenderer(Context context) : base(context)
        {
            mainContext = context;
        }

        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                NativeMap.InfoWindowClick -= OnInfoWindowClick;
            }

            if (e.NewElement != null)
            {
                var formsMap = (CustomMap)e.NewElement;
                customPins = formsMap.CustomPins;
            }


        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == CustomMap.IsMakeSnapshotProperty.PropertyName)
            {
                var res = (sender as CustomMap).IsMakeSnapshot;
                if (res == true)
                    MakeSnapshot();
            }
            if (e.PropertyName == CustomMap.IsUpdateProperty.PropertyName)
            {
                customPins = (sender as CustomMap).CustomPins;
            }
            if (e.PropertyName == CustomMap.CheckThemeProperty.PropertyName)
            {
                ChangeMapTheme();
            }

        }

        protected override void OnMapReady(GoogleMap map)
        {
            base.OnMapReady(map);
            this.googleMap = map;
            ChangeMapTheme();
            NativeMap.InfoWindowClick += OnInfoWindowClick;
            NativeMap.SetInfoWindowAdapter(this);
        }
        private void ChangeMapTheme()
        {
            if (this.googleMap != null)
            {
                if (Application.Current.UserAppTheme == OSAppTheme.Dark)
                {
                    this.googleMap.SetMapStyle(MapStyleOptions.LoadRawResourceStyle(mainContext, Resource.Layout.map_style_night));
                }
                else
                {
                    this.googleMap.SetMapStyle(MapStyleOptions.LoadRawResourceStyle(mainContext, Resource.Layout.map_style_light));
                }
            }
        }
        protected override MarkerOptions CreateMarker(Pin pin)
        {
            Bitmap mutableBitmap = null;
            foreach (var item in customPins)
            {
                if (item.Position == pin.Position)
                {
                    service = new FileService();
                    string path = service.GetPath(item.Icon);
                    Bitmap bitmap = BitmapFactory.DecodeFile(path);
                    mutableBitmap = bitmap.Copy(Bitmap.Config.Argb8888, true);
                    Canvas canvas = new Canvas(mutableBitmap);
                    Paint paint = new Paint();
                    Xamarin.Forms.Color color = Xamarin.Forms.Color.FromHex(item.Color);
                    paint.SetColorFilter(new PorterDuffColorFilter(color.ToAndroid(), PorterDuff.Mode.SrcIn));
                    canvas.DrawBitmap(mutableBitmap, new Matrix(), paint);
                    break;
                }
            }
            var marker = new MarkerOptions();
            marker.SetPosition(new LatLng(pin.Position.Latitude, pin.Position.Longitude));
            marker.SetTitle(pin.Label);
            marker.SetSnippet(pin.Address);
            if (mutableBitmap != null)
            {
                marker.SetIcon(BitmapDescriptorFactory.FromBitmap(mutableBitmap));
            }
            else
            {
                marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.outline_pinMap));
            }
            return marker;
        }
        void OnInfoWindowClick(object sender, GoogleMap.InfoWindowClickEventArgs e)
        {
            var customPin = GetCustomPin(e.Marker);
            if (customPin == null)
            {
                throw new Exception("Custom pin not found");
            }

            //if (!string.IsNullOrWhiteSpace(customPin.Url))
            //{
            //    var url = Android.Net.Uri.Parse(customPin.Url);
            //    var intent = new Intent(Intent.ActionView, url);
            //    intent.AddFlags(ActivityFlags.NewTask);
            //    Android.App.Application.Context.StartActivity(intent);
            //}
        }
        CustomPin GetCustomPin(Marker marker)
        {
            var position = new Position(marker.Position.Latitude, marker.Position.Longitude);
            foreach (var pin in customPins)
            {
                if (pin.Position == position)
                {
                    return pin;
                }
            }
            return null;
        }

        public Android.Views.View GetInfoContents(Marker marker)
        {
            //var inflater = Android.App.Application.Context.GetSystemService(Context.LayoutInflaterService) as Android.Views.LayoutInflater;
            //if (inflater != null)
            //{
            //    Android.Views.View view;

            //    var customPin = GetCustomPin(marker);
            //    if (customPin == null)
            //    {
            //        throw new Exception("Custom pin not found");
            //    }

            //    if (customPin.Name.Equals("Xamarin"))
            //    {
            //        view = inflater.Inflate(Resource.Layout.XamarinMapInfoWindow, null);
            //    }
            //    else
            //    {
            //        view = inflater.Inflate(Resource.Layout.MapInfoWindow, null);
            //    }

            //    var infoTitle = view.FindViewById<TextView>(Resource.Id.InfoWindowTitle);
            //    var infoSubtitle = view.FindViewById<TextView>(Resource.Id.InfoWindowSubtitle);

            //    if (infoTitle != null)
            //    {
            //        infoTitle.Text = marker.Title;
            //    }
            //    if (infoSubtitle != null)
            //    {
            //        infoSubtitle.Text = marker.Snippet;
            //    }

            //    return view;
            //}
            return null;
        }

        public Android.Views.View GetInfoWindow(Marker marker)
        {
            return null;
        }
        public void MakeSnapshot()
        {
            this.googleMap.Snapshot(this);
        }
        public void OnSnapshotReady(Bitmap snapshot)
        {
            Bitmap = snapshot;
        }
    }
}
