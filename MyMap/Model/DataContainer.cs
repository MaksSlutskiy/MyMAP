using System;
using MyMap.Interface;

namespace MyMap.Model
{
    public class DataContainer : IDataContainer
    {
        #region Methods
        public async void AddValue(string key, object value)
        {
            try
            {
                App.Current.Properties.Add(key, value);
                await App.Current.SavePropertiesAsync();
            }
            catch (Exception ex)
            {
            }
        }

        public async void AddValueXML(string key, string recipeXML)
        {
            try
            {
                App.Current.Properties.Add(key, recipeXML);
                await App.Current.SavePropertiesAsync();
            }
            catch (Exception ex)
            {
            }
        }

        public object GetValue(string key)
        {
            object value = null;
            try
            {

                if (App.Current.Properties.ContainsKey(key))
                {
                    value = App.Current.Properties[key];
                }
            }
            catch (Exception ex)
            {
            }
            return value;
        }
        public string GetValueXML(string key)
        {
            string xml = null;
            try
            {

                if (App.Current.Properties.ContainsKey(key))
                {
                    xml = App.Current.Properties[key].ToString();
                }
            }
            catch (Exception ex)
            {
            }
            return xml;
        }

        public async void RemoveValue(string key)
        {
            try
            {
                App.Current.Properties.Remove(key);
                await App.Current.SavePropertiesAsync();

            }
            catch (Exception ex)
            {
            }
        }

        #endregion
    }
}
