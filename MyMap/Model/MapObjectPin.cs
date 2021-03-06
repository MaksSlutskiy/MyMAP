using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MyMap.Model
{
    public class MapObjectPin : INotifyPropertyChanged
    {
        private bool _isVisible;
        public int Id { get; set; }
        public string _name;
        public string _icon;
        public string _description;
        //public ObservableCollection<Image> Images { get; set; }
        public int _category;
        public double latitude { get; set; }
        public double longitude { get; set; }
        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                _isVisible = value;
                OnPropertyChanged("IsVisible");
            }
        }
        public int Category
        {
            get => _category;
            set
            {
                _category = value;
                OnPropertyChanged("Category");
            }
        }
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        public string Icon
        {
            get => _icon;
            set
            {
                _icon = value;
                OnPropertyChanged("Icon");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
    public enum CategoryEnum
    {
        BeautifulViews = 0,
        Restaurants = 1,
        Bars = 2,
        Cinemas = 3,
        FitnessClubs = 4
    };
}
