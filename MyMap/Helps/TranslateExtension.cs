using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyMap.Helps
{
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension<BindingBase>
    {
        /// <summary>
        /// Match to the name of the label into .resx file
        /// </summary>
        public string Text { get; set; }

        public TranslateExtension()
        {
        }

        public BindingBase ProvideValue(IServiceProvider serviceProvider)
        {
            var binding = new Binding
            {
                Mode = BindingMode.OneWay,
                Path = $"[{Text}]",
                Source = Translator.TranslatorInstance,
            };
            return binding;
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return ProvideValue(serviceProvider);
        }

    }
}
