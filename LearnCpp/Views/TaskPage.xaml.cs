using LearnCpp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LearnCpp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TaskPage : ContentPage
	{
		public TaskPage ()
		{
			InitializeComponent ();

			BindingContext = new TaskViewModel ();
		}
    }
}