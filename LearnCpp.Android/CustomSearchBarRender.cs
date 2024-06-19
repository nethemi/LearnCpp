using Android.Content;
using Android.Widget;
using LearnCpp.Droid;
using LearnCpp.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(CustomSerachBar), typeof(CustomSearchBarRender))]
namespace LearnCpp.Droid
{
    public class CustomSearchBarRender: SearchBarRenderer
    {
        public CustomSearchBarRender(Context context) : base(context) { }
        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                base.OnElementChanged(e);
                var icon = Control?.FindViewById(Context.Resources.GetIdentifier("android:id/search_mag_icon", null, null));
                (icon as ImageView)?.SetColorFilter(Color.Accent.ToAndroid());
            }
        }
    }
}