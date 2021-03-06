using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyMap.CustomViews
{
    public class ExtendedViewCell : ViewCell
    {
        public static readonly BindableProperty SelectedBackgroundColorProperty =
            BindableProperty.Create("SelectedBackgroundColor",
                                    typeof(Color),
                                    typeof(ExtendedViewCell),
                                    Color.Default);

        public Color SelectedBackgroundColor
        {
            get { return (Color)GetValue(SelectedBackgroundColorProperty); }
            set { SetValue(SelectedBackgroundColorProperty, value); }
        }

       

        public static readonly BindableProperty CommandProperty =
            BindableProperty.CreateAttached("Command", typeof(ICommand), typeof(ExtendedViewCell), (object)null);

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.CreateAttached("CommandParameter", typeof(object), typeof(ExtendedViewCell), (object)null);

        public object CommandParameter
        {
            get => (object)GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }
    }
}
