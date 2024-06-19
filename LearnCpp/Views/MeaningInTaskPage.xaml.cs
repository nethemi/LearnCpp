using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LearnCpp.ViewModels;

namespace LearnCpp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MeaningInTaskPage : ContentPage
    {
        public MeaningInTaskPage()
        {
            InitializeComponent();
            BindingContext = new MeaningInTaskViewModel();
        }
    }
}