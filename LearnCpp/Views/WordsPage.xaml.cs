using LearnCpp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LearnCpp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WordsPage : ContentPage
	{
		WordsViewModel viewModel;
		public WordsPage ()
		{
			InitializeComponent ();

			BindingContext = viewModel = new WordsViewModel();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
			viewModel.OnAppearing();
        }
    }
}