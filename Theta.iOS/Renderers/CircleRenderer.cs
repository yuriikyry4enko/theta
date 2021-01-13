using System;
using System.ComponentModel;
using CoreGraphics;
using Theta.Controls;
using Theta.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


[assembly: ExportRenderer(typeof(Circle), typeof(CircleRenderer))]
namespace Theta.iOS.Renderers
{
    public class CircleRenderer : VisualElementRenderer<Circle>
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            SetNeedsDisplay();
        }

        public override void Draw(CGRect rect)
        {
            if (Element == null) return;

            var circle = Element as Circle;

            using (var context = UIGraphics.GetCurrentContext())
            {
                var path = new CGPath();

                path.AddEllipseInRect(rect);

                context.AddPath(path);
                context.SetFillColor(circle.Color.ToCGColor());

                context.DrawPath(CGPathDrawingMode.Fill);
            }
        }
    }
}