using LearnCpp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LearnCpp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WordDetailPage : ContentPage
	{
		public WordDetailPage ()
		{
			InitializeComponent ();
            BindingContext = new WordDetailViewModel();
        }
    }
}