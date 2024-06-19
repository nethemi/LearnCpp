using System;
using System.Globalization;
using Xamarin.Forms;

namespace LearnCpp.Helpers
{
    public class ImageResultConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var img = "icon_process.png";

            if (value is bool val)
            {
                if (val)  img = "icon_true.png";
                else if(!val) img = "icon_false.png";
            }

            return img;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "icon_process.png";
        }
    }
}
