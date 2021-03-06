using System;
using System.Globalization;
using Xamarin.Essentials;
namespace MyMap
{
    public class AppModelInfo
    {
        public static string DatabaseName = "MyMap.db";

        public string ProductName
        {
            get => AppInfo.Name;
        }

        public string ProductVersion
        {
            get => AppInfo.VersionString;
        }
        public DeviceIdiom DeviceType
        {
            get { return DeviceInfo.Idiom; }
        }
        public DevicePlatform Device
        {
            get { return DeviceInfo.Platform; }
        }
        private CultureInfo _currentUICulture = null;
        public CultureInfo CurrentUICulture
        {
            get
            {
                if (_currentUICulture == null)
                {
                    var nameUICulture = Preferences.Get("CultureInfo", "ru-RU");//("CultureInfo", "en");
                    _currentUICulture = new CultureInfo(nameUICulture);
                }
                return _currentUICulture;
            }
            set
            {
                if (value != null && !value.Equals(_currentUICulture))
                {
                    Preferences.Set("CultureInfo", _currentUICulture.Name);
                    _currentUICulture = value;
                }
            }
        }
        public bool IsSafeAreaSupport
        {
            get
            {
                var display = DeviceDisplay.MainDisplayInfo;
                var displayHeight = display.Height;
                switch (displayHeight)
                {
                    //Width
                    case 1668:/*2388*/
                        return true;
                    case 1640:/*2360*/
                        return true;
                    case 2048:/*2732*/
                        return true;
                    case 1284:/*2778*/
                        return true;
                    case 1125:/*2436*/
                        return true;
                    case 1170:/*2532*/
                        return true;
                    case 1242:/*2688*/
                        return true;
                    case 828:/*1792*/
                        return true;

                    //Height
                    case 2388:
                        return true;
                    case 2360:
                        return true;
                    case 2732:
                        return true;
                    case 2778:
                        return true;
                    case 2436:
                        return true;
                    case 2532:
                        return true;
                    case 2688:
                        return true;
                    case 1792:
                        return true;
                    default:
                        return false;
                }
            }
        }
        public string LanguageKey { get => "languageKey"; }
        public string ThemeKey { get => "themeKey"; }

        public string LatitudeKey { get => "latitudeKey"; }
        public string LongitudeKey { get => "longitudeKey"; }

        public string DegreesLatKey { get => "degreesLatKey"; }
        public string DegreesLonKey { get => "degreesLonKey"; }
    }
}
