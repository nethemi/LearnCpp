using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using LearnCpp.Helpers;
using Android.Graphics.Drawables;
using Android.Content;
using LearnCpp.Droid;

[assembly: ExportRenderer(typeof(CustomEditor), typeof(CustomEditorRender))]
namespace LearnCpp.Droid
{
    class CustomEditorRender : EditorRenderer
    {
        public CustomEditorRender(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                GradientDrawable gd = new GradientDrawable();
                gd.SetColor(global::Android.Graphics.Color.Transparent);
                this.Control.SetBackgroundDrawable(gd);
            }
        }
    }
}