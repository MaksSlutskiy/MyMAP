using System;
using System.Globalization;
using Xamarin.Essentials;
namespace MyMap
{
    public class AppModelInfo
    {
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
    }
}
