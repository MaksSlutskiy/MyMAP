using System;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyMap.Controls
{
    public partial class ColorPickerControl : ContentView
    {
        public event EventHandler<Color> PickedColorChanged;
        private bool isChange;

        public static readonly BindableProperty PickedColorProperty
            = BindableProperty.Create(
                nameof(PickedColor),
                typeof(Color),
                typeof(ColorPickerControl));

        public Color PickedColor
        {
            get { return (Color)GetValue(PickedColorProperty); }
            set { SetValue(PickedColorProperty, value); }
        }

        public readonly static BindableProperty CommandProperty =
            BindableProperty.Create("Command", typeof(ICommand), typeof(ColorPickerControl), null);

        public readonly static BindableProperty ParameterCommandProperty =
            BindableProperty.Create("ParameterCommand", typeof(object), typeof(ColorPickerControl), null);

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public object ParameterCommand
        {
            get => (object)GetValue(ParameterCommandProperty);
            set => SetValue(ParameterCommandProperty, value);
        }


        private SKPoint _lastTouchPoint = new SKPoint();

        public ColorPickerControl()
        {
            InitializeComponent();
        }

        private void SkCanvasView_OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var skImageInfo = e.Info;
            var skSurface = e.Surface;
            var skCanvas = skSurface.Canvas;

            var skCanvasWidth = skImageInfo.Width;
            var skCanvasHeight = skImageInfo.Height;

            skCanvas.Clear(SKColors.White);

            // Draw gradient rainbow Color spectrum 
            using (var paint = new SKPaint())
            {
                paint.IsAntialias = true;

                // Initiate the primary Color list 
                // picked up from Google Web Color Picker 
                var colors = new SKColor[]
                {
                    new SKColor(255, 0, 0), // Red 
     new SKColor(255, 255, 0), // Yellow 
     new SKColor(0, 255, 0), // Green (Lime) 
     new SKColor(0, 255, 255), // Aqua 
     new SKColor(0, 0, 255), // Blue 
     new SKColor(255, 0, 255), // Fuchsia 
     new SKColor(255, 0, 0), // Red 
                };

                // create the gradient shader between Colors 
                using (var shader = SKShader.CreateLinearGradient(
                    new SKPoint(0, 0),
                    new SKPoint(skCanvasWidth, 0),
                    colors,
                    null,
                    SKShaderTileMode.Clamp))
                {
                    paint.Shader = shader;
                    skCanvas.DrawPaint(paint);
                }
            }

            // Draw darker gradient spectrum 
            using (var paint = new SKPaint())
            {
                paint.IsAntialias = true;

                // Initiate the darkened primary color list 
                var colors = new SKColor[]
                {
                    SKColors.Transparent,
                    SKColors.Black
                };

                // create the gradient shader  
                using (var shader = SKShader.CreateLinearGradient(
                    new SKPoint(0, 0),
                    new SKPoint(0, skCanvasHeight),
                    colors,
                    null,
                    SKShaderTileMode.Clamp))
                {
                    paint.Shader = shader;
                    skCanvas.DrawPaint(paint);
                }
            }

            // Represent the color of the current Touch point 
            SKColor touchPointColor;
            if (!isChange)
            {
                touchPointColor = PickedColor.ToSKColor();

            }
            else
            {
                //create the 1x1 bitmap (auto allocates the pixel buffer) 
                using (SKBitmap bitmap = new SKBitmap(skImageInfo))
                {
                    // get the pixel buffer for the bitmap 
                    IntPtr dstpixels = bitmap.GetPixels();

                    // read the surface into the bitmap 
                    skSurface.ReadPixels(skImageInfo,
                        dstpixels,
                        skImageInfo.RowBytes,
                        (int)_lastTouchPoint.X, (int)_lastTouchPoint.Y);

                    // access the color 
                    touchPointColor = bitmap.GetPixel(0, 0);
                }
            }

            // Painting the Touch point 
            using (SKPaint paintTouchPoint = new SKPaint())
            {
                paintTouchPoint.Style = SKPaintStyle.Fill;
                paintTouchPoint.Color = SKColors.White;
                paintTouchPoint.IsAntialias = true;

                // Outer circle (Ring) 
                var outerRingRadius =
                    ((float)skCanvasWidth / (float)skCanvasHeight) * (float)18;
                skCanvas.DrawCircle(
                    _lastTouchPoint.X,
                    _lastTouchPoint.Y,
                    outerRingRadius, paintTouchPoint);

                // Draw another circle with picked color 
                if (!isChange)
                {
                    paintTouchPoint.Color = touchPointColor.WithAlpha(0x0);
                    isChange = true;
                }
                else
                {
                    paintTouchPoint.Color = touchPointColor;
                }

                // Outer circle (Ring) 
                var innerRingRadius =
                    ((float)skCanvasWidth / (float)skCanvasHeight) * (float)12;
                skCanvas.DrawCircle(
                    _lastTouchPoint.X,
                    _lastTouchPoint.Y,
                    innerRingRadius, paintTouchPoint);
            }

            // Set selected color 
            PickedColor = touchPointColor.ToFormsColor();
            PickedColorChanged?.Invoke(this, PickedColor);

            if (Command != null && Command.CanExecute(PickedColor))
            {
                Command.Execute(PickedColor);
            }
        }

        private void SkCanvasView_OnTouch(object sender, SKTouchEventArgs e)
        {
            _lastTouchPoint = e.Location;

            var canvasSize = SkCanvasView.CanvasSize;

            // Check for each touch point XY position to be inside Canvas 
            // Ignore any Touch event ocurred outside the Canvas region  
            if ((e.Location.X > 0 && e.Location.X < canvasSize.Width) &&
                (e.Location.Y > 0 && e.Location.Y < canvasSize.Height))
            {
                e.Handled = true;

                // update the Canvas as you wish 
                SkCanvasView.InvalidateSurface();
            }
        }
    }
}
