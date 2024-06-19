using LearnCpp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LearnCpp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProgressPage : ContentPage
    {
        ProgressViewModel viewModel;

        public ProgressPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new ProgressViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.OnAppearing();
        }
    }
}
