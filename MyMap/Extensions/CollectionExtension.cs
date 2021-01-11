using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MyMap.Extensions
{
    static class CollectionExtension
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> obj)
        {
            return new ObservableCollection<T>(obj);
        }
    }
    
}
