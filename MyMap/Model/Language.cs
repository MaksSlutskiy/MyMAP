using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyMap.Model
{
    public class Language : INotifyPropertyChanged
    {
        private LanguageEnum _name;
        private string _reduction;
        private bool _isChange;

        public void Notify([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public LanguageEnum Name
        {
            get => _name;
            set
            {
                _name = value;
                Notify("Name");
            }
        }

        public string Reduction
        {
            get => _reduction;
            set
            {
                _reduction = value;
                Notify("Reduction");
            }
        }

        public bool IsChange
        {
            get => _isChange;
            set
            {
                _isChange = value;
                Notify("IsChange");
            }
        }
    }

    public enum LanguageEnum
    {
        Русский,
        Українська,
        English
    }
}
