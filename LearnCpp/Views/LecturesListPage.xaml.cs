using LearnCpp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LearnCpp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LecturesListPage : ContentPage
	{
		LectureListViewModel viewModel;
        public LecturesListPage ()
		{
			InitializeComponent ();

            BindingContext = viewModel = new LectureListViewModel(); 
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.OnAppearing();
        }
    }
}