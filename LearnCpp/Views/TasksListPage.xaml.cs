using LearnCpp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LearnCpp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TasksListPage : ContentPage
	{
		TaskListViewModel viewModel;
		public TasksListPage ()
		{
			InitializeComponent ();

			BindingContext = viewModel = new TaskListViewModel ();
		}

        protected override void OnAppearing()
        {
            viewModel.OnAppearing();
			base.OnAppearing();
        }
    }
}