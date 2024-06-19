using LearnCpp.Droid;
using Xamarin.Forms;
using static LearnCpp.ViewModels.LectureViewModel;


[assembly: Dependency(typeof(BaseUrl))]
namespace LearnCpp.Droid
{
    public class BaseUrl : IBaseUrl
    {
        public string Get()
        {
            return "file:///android_asset/";
        }
    }
}