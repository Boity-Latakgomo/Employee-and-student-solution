using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ULProject.Controls;
using ULProject.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(NonlinedBorderedPicker), typeof(NonlinedBorderedPickerAndroid))]
namespace ULProject.Droid
{
    [Obsolete]
    class NonlinedBorderedPickerAndroid : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                //Control.SetBackgroundResource(Resource.Layout.rounded_shape);
                var gradientDrawable = new GradientDrawable();
                gradientDrawable.SetCornerRadius(10f);
                gradientDrawable.SetStroke(2, Android.Graphics.Color.Rgb(176, 32, 121));
                // The following code set the background color of an entry
                //gradientDrawable.SetColor(Android.Graphics.Color.Rgb(73, 145, 253));
                Control.SetBackground(gradientDrawable);

                Control.SetPadding(16, Control.PaddingTop, 16,
                    Control.PaddingBottom);
            }
        }
    }
}