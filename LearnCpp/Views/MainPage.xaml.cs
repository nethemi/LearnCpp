using LearnCpp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LearnCpp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : ContentPage
	{
        MainViewModel viewModel;
        public MainPage ()
		{
			InitializeComponent ();

            BindingContext = viewModel = new MainViewModel ();
        }

        protected override void OnAppearing()
        {
            viewModel.OnAppearing();
            base.OnAppearing();
        }
    }
}