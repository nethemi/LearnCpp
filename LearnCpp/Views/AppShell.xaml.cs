using Xamarin.Forms;

namespace LearnCpp.Views
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(SettingPage), typeof(SettingPage));

            Routing.RegisterRoute(nameof(LecturePage), typeof(LecturePage));
            Routing.RegisterRoute(nameof(TaskPage), typeof(TaskPage));
            Routing.RegisterRoute(nameof(MeaningInLecturePage), typeof(MeaningInLecturePage));
            Routing.RegisterRoute(nameof(MeaningInTaskPage), typeof(MeaningInTaskPage));

            Routing.RegisterRoute(nameof(WordDetailPage), typeof(WordDetailPage));
            
        }

    }
}
