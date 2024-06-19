using LearnCpp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LearnCpp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MeaningInLecturePage : ContentPage
	{
		public MeaningInLecturePage()
		{
			InitializeComponent ();
			BindingContext = new MeaningInLectureViewModel();
		}
	}
}