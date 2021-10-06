using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ULProject
{
    public class GlobalResources
    {
        public static void Initialise()
        {
            Application.Current.Resources = new ResourceDictionary();
            InitialiseGlobalColors();
            InitialiseButtonStyle();
        }

        private static void InitialiseGlobalColors()
        {
            Application.Current.Resources.Add("PrimaryDarkTextColor", Color.FromHex("#141414"));
            Application.Current.Resources.Add("PrimaryLightTextColor", Color.FromHex("#F8EEF5"));
            Application.Current.Resources.Add("EntryBackground", Color.FromHex("#E4DFED"));
            Application.Current.Resources.Add("PrimaryColor", Color.FromHex("#2C1D4D"));
            Application.Current.Resources.Add("PinkTextColor", Color.FromHex("#B02079"));
            Application.Current.Resources.Add("LighterPrimaryColor", Color.FromHex("#CFCBD6"));
            Application.Current.Resources.Add("PurpleColor", Color.FromHex("#2C1D4D"));
        }

        private static void InitialiseButtonStyle()
        {
            // Solid buttton
            Application.Current.Resources.Add("SolidButton", new Style(typeof(Button))
            {
                Setters =
                {
                    new Setter {Property = Button.CornerRadiusProperty, Value = 12},
                    new Setter {Property = Button.BorderWidthProperty, Value = 4},
                    new Setter {Property = VisualElement.BackgroundColorProperty, Value = Color.Brown},
                    new Setter {Property = Button.BorderColorProperty, Value = Color.Black},
                    new Setter {Property = View.MarginProperty, Value = new Thickness(20, 24, 20, 8)}
                }
            });

            // bordered button
        }
    }
}
