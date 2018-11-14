using CoreAnimation;
using CoreGraphics;
using smartCubes.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Entry), typeof(MyEntryRenderer))]
namespace smartCubes.iOS
{
    public class MyEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.BorderStyle = UITextBorderStyle.None;
                // do whatever you want to the UITextField here!
                /*CALayer border = new CALayer();
                float width = 2.0f;
                border.BorderColor = Color.Blue.ToCGColor();
                border.Frame = new CGRect(0, 40, 400, 2.0f);
                border.BorderWidth = width;

                Control.Layer.AddSublayer(border);
                Control.Layer.MasksToBounds = true;*/


            }
        }
    }
}