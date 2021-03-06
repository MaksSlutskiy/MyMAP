using System;
using System.ComponentModel;

namespace MyMap.Model
{
    public class Category : INotifyPropertyChanged
    {
        public int Id { get; set; }
        private string _name;
        private string _color;

        public string Color
        {
            get => _color;
            set
            {
                _color = value;
                OnPropertyChanged("Color");
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
