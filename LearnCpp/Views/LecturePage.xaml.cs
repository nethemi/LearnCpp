using LearnCpp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LearnCpp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LecturePage : ContentPage
	{
        public LecturePage ()
		{
			InitializeComponent ();
            BindingContext = new LectureViewModel();
        }
    }
}