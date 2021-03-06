using System;
using System.ComponentModel;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Views;
using MyMap.CustomViews;
using MyMap.Droid.CustomViews;
using Xamarin.Forms.Platform.Android;

[assembly: Xamarin.Forms.ExportRenderer(typeof(ExtendedViewCell), typeof(ExtendedViewCellRenderer))]
namespace MyMap.Droid.CustomViews
{
    public class ExtendedViewCellRenderer : ViewCellRenderer
    {
        private View _cellCore;
        private Drawable _unselectedBackground;

        private bool _selected;

        private void Cell_LongClick(object sender, View.LongClickEventArgs e)
        {
            var view = sender as Xamarin.Forms.INativeElementView;
            var extendedViewCell = view.Element as ExtendedViewCell;
            //var command = extendedViewCell.Command;
            //command?.Execute(extendedViewCell.CommandParameter);
        }

        protected override View GetCellCore(Xamarin.Forms.Cell item,
                                                          View convertView,
                                                          ViewGroup parent,
                                                          Context context)
        {
            _cellCore = base.GetCellCore(item, convertView, parent, context);
            //_cellCore.LongClick += Cell_LongClick;
            //_cellCore.Click += Cell_Click;

            _selected = false;
            _unselectedBackground = _cellCore.Background;

            return _cellCore;
        }

        private void Cell_Click(object sender, System.EventArgs e)
        {
            //throw new System.NotImplementedException();
        }

        protected override void OnCellPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            base.OnCellPropertyChanged(sender, args);
            if (args.PropertyName == "IsSelected")
            {
                _selected = !_selected;

                if (_selected)
                {
                    var extendedViewCell = sender as ExtendedViewCell;
                    _cellCore.SetBackgroundColor(extendedViewCell.SelectedBackgroundColor.ToAndroid());
                }
                else
                {
                    _cellCore.SetBackground(_unselectedBackground);
                }
            }
        }
    }
}
