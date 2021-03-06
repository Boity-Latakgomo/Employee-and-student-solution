using CoreGraphics;
using Foundation;
using LearnXaml.Controls;
using LearnXaml.iOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using ULProject.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(NonlinedBorderedEntry), typeof(NonlinedBorderedEntryiOS))]
namespace LearnXaml.iOS
{
    public class NonlinedBorderedEntryiOS : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                Control.Layer.CornerRadius = 20;
                Control.Layer.BorderWidth = 3f;
                Control.Layer.BorderColor = Color.DeepPink.ToCGColor();
                Control.Layer.BackgroundColor = Color.LightGray.ToCGColor();

                Control.LeftView = new UIKit.UIView(new CGRect(0, 0, 10, 0));
                Control.LeftViewMode = UIKit.UITextFieldViewMode.Always;
            }
        }
    }
}