using LearnCpp.Models;
using LearnCpp.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;

namespace LearnCpp.ViewModels
{
    public class LectureListViewModel : BaseViewModel
    {
        private Lecture selectedLecture;
        public Lecture SelectedLecture
        {
            get => selectedLecture;
            set
            {
                SetProperty(ref selectedLecture, value);
                OpenLecture(value);
            }
        }

        public ObservableCollection<IGrouping<string, LectureByTopic>> LectureList { get; }
        public Command<Lecture> TapLecture { get; }

        public LectureListViewModel()
        {
            Title = "Лекции";

            LectureList = new ObservableCollection<IGrouping<string, LectureByTopic>>();
            LoadLectureListAsync();

            TapLecture = new Command<Lecture>(OpenLecture);
        }

        private async void LoadLectureListAsync()
        {
            try
            {
                var lectureList = (await App.Database.LectureByTopicWithChildren()).GroupBy(x => x.topic.TopicTitle);

                foreach (var item in lectureList)
                {
                    LectureList.Add(item);
                }
            }
            catch (Exception)
            {
                Debug.WriteLine("Не удалось загрузить страницу");
            }
        }

        private async void OpenLecture(Lecture lecture)
        {
            if (lecture == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(LecturePage)}?{nameof(LectureViewModel.LectureTopic)}={lecture.LectureTopic}");

        }

        public void OnAppearing()
        {
            SelectedLecture = null;
        }

    }
}
