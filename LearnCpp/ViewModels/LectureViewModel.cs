using Android.Content.Res;
using LearnCpp.Models;
using LearnCpp.Views;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Input;
using VersOne.Epub;
using Xamarin.Forms;

namespace LearnCpp.ViewModels
{
    [QueryProperty(nameof(LectureTopic), nameof(LectureTopic))]
    public class LectureViewModel : BaseViewModel
    {
        private string lectureTopic;
        public string LectureTopic
        {
            get => lectureTopic;
            set
            {
                lectureTopic = value;
                LoadLectureTopic(value);
            }
        }

        public LectureByTopic lecture = new LectureByTopic();
        public ICommand LoadMeaningsCommand => new Command(OpenMeaningsInLecture);
        public HtmlWebViewSource viewSource { get; set; }
        public LectureViewModel() 
        {
            viewSource = new HtmlWebViewSource();
        }

        private async void OpenMeaningsInLecture(object obj) => await Shell.Current.GoToAsync($"{nameof(MeaningInLecturePage)}?{nameof(MeaningInLectureViewModel.LectureFk)}={lecture.LectureFK}");
        public async void LoadLectureTopic(string lectureTopic)
        {
            try
            {
                AssetManager assets = Android.App.Application.Context.Assets;
                Stream stream = assets.Open("LectureCpp.epub");
                EpubBook epubBook = EpubReader.ReadBook(stream);

                foreach (var navigationItem in epubBook.Navigation)
                {
                    foreach(var item in navigationItem.NestedItems)
                    {
                        if (item.Title == lectureTopic)
                        {
                            viewSource.BaseUrl = DependencyService.Get<IBaseUrl>().Get();
                            var html = item.HtmlContentFile.Content;
                            html = Application.Current.RequestedTheme == OSAppTheme.Dark ? html.Replace("light", "dark") : html.Replace("dark", "light");
                            viewSource.Html = html;
                            Title = lectureTopic;

                            await App.Database.CreateLectureByTopic();
                            lecture = (await App.Database.LectureByTopicWithChildren()).Where(x=>x.lecture.LectureTopic == lectureTopic).FirstOrDefault();
                            lecture.DateViewed = DateTime.Now;
                            await App.Database.SaveLectureByTopic(lecture);
                        }
                    }
                }
            }
            catch (Exception)
            {
                Debug.WriteLine("Не удалось загрузить лекцию");
            }
        }
        public interface IBaseUrl { string Get(); }
    }
}
