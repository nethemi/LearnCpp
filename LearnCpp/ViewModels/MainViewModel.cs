using LearnCpp.Models;
using LearnCpp.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LearnCpp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region selected items
        private Meaning selectedMeaning;
        private Lecture selectedLecture;

        public Meaning SelectedMeaning
        {
            get => selectedMeaning;
            set
            {
                SetProperty(ref selectedMeaning, value);
                OnMeaningSelected(value);
            }
        }

        public Lecture SelectedLecture
        {
            get => selectedLecture;
            set
            {
                SetProperty(ref selectedLecture, value);
                OpenLecture(value);
            }
        }
        #endregion

        #region items
        public ObservableCollection<Meaning> Meanings { get; }
        public ObservableCollection<Meaning> searchMeaning { get; }
        public ObservableCollection<Lecture> searchLecture { get; }
        
        private string lectureLast;
        public string LectureLast
        {
            get => lectureLast;
            set => SetProperty(ref lectureLast, value);
        }

        private int heightSearch;
        public int HeightSearch
        {
            get => heightSearch;
            set => SetProperty(ref heightSearch, value);
        }

        private int heightLast;
        public int HeightLast
        {
            get => heightLast;
            set => SetProperty(ref heightLast, value);
        }

        private int notWordResult;
        public int NotWordResult
        {
            get => notWordResult;
            set => SetProperty(ref notWordResult, value);
        }

        private int notLectureResult;
        public int NotLectureResult
        {
            get => notLectureResult;
            set => SetProperty(ref notLectureResult, value);
        }

        private int foundWord;
        public int FoundWord
        {
            get => foundWord;
            set => SetProperty(ref foundWord, value);
        }

        private int foundLecture;
        public int FoundLecture
        {
            get => foundLecture;
            set => SetProperty(ref foundLecture, value);
        }


        private string searchText = string.Empty;
        public string SearchText
        {
            get => searchText; 
            set
            {
                if (searchText != value)
                {
                    searchText = value ?? string.Empty;
                    OnPropertyChanged(nameof(SearchText));

                    if (SearchCommand.CanExecute(null))
                    {
                        SearchCommand.Execute(null);
                    }
                }
            }
        }

        #endregion

        #region commands
        public ICommand LoadSettingsCommand { get; }
        public Command LoadLastInfo {  get; }
        public Command<Meaning> MeaningTapped { get; }
        public Command<Lecture> LectureTapped { get; }
        public ICommand TapLecture { get; }

        private ICommand searchCommand;
        public ICommand SearchCommand
        {
            get
            {
                searchCommand = searchCommand ?? new Command(ExecuteSearchCommand, CanExecuteSearchCommand);
                return searchCommand;
            }
        }
        #endregion

        public MainViewModel() 
        {
            Title = "Главная";
            HeightSearch = 0;
            HeightLast = 1000;

            Meanings = new ObservableCollection<Meaning>();
            searchMeaning = new ObservableCollection<Meaning>();
            searchLecture = new ObservableCollection<Lecture>();

            LoadLastInfo = new Command(async () => await ExecuteLoadLastInfo());

            LoadSettingsCommand = new Command(OpenSettings);

            MeaningTapped = new Command<Meaning>(OnMeaningSelected);
            LectureTapped = new Command<Lecture>(OnLectureSelected);
            TapLecture = new Command(OpenLecture);
        }

        private async void OnLectureSelected(Lecture lecture)
        {
            if (lecture == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(LecturePage)}?{nameof(LectureViewModel.LectureTopic)}={lecture.LectureTopic}");
        }

        private async void OpenSettings(object obj)
        {
            await Shell.Current.GoToAsync(nameof(SettingPage));
        }

        private async void OpenLecture(object obj)
        {
            if (String.IsNullOrEmpty(LectureLast))
                return;

            await Shell.Current.GoToAsync($"{nameof(LecturePage)}?{nameof(LectureViewModel.LectureTopic)}={LectureLast}");
        }

        private async void OnMeaningSelected(Meaning meaning)
        {
            if (meaning == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(WordDetailPage)}?{nameof(WordDetailViewModel.MeaningId)}={meaning.MeaningID}");
        }

        private async void ExecuteSearchCommand()
        {
            if (!string.IsNullOrEmpty(SearchText))
            {
                HeightLast = 0;
                HeightSearch = 1000;
                try
                {
                    searchMeaning.Clear();
                    searchLecture.Clear();

                    var words = (await App.Database.GetAllMeaningWithChildren()).Where(x => x.word.Word.ToLower().Contains(SearchText.Trim().ToLower()) || x.meaningWord.Word.Contains(SearchText.Trim()));
                    var lectures = (await App.Database.GetAllLectures()).Where(x=>x.LectureTopic.ToLower().Contains(SearchText.Trim().ToLower()) || x.ChineseTopic.Contains(SearchText.Trim()));

                    if (words.Count() <= 0)
                    {
                        FoundWord = 0;
                        NotWordResult = 100;
                    }
                    else
                    {
                        NotWordResult = 0;
                        FoundWord = 500;
                        foreach (var meaning in words)
                        {
                            searchMeaning.Add(meaning);
                        }
                    }

                    if (lectures.Count() <= 0)
                    {
                        FoundLecture = 0;
                        NotLectureResult = 100;
                    }
                    else
                    {
                        NotLectureResult = 0;
                        FoundLecture = 500;
                        foreach (var lecture in lectures)
                        {
                            searchLecture.Add(lecture);
                        }
                    }
                    
                }
                catch (Exception)
                {
                    Debug.WriteLine("Ничего не найдено");
                }
            }
            else
            {
                HeightSearch = 0;
                HeightLast = 1000;
            }
        }

        private bool CanExecuteSearchCommand() => true;

        async Task ExecuteLoadLastInfo()
        {
            IsBusy = true;
            try
            {
                Meanings.Clear();

                LectureLast = (await App.Database.LectureByTopicWithChildren()).OrderBy(x => x.DateViewed).Last().lecture.LectureTopic;
                var words = (await App.Database.GetAllMeaningWithChildren()).OrderByDescending(x => x.DateViewed).Take(5);

                foreach (var meaning in words)
                {
                    Meanings.Add(meaning);
                }
            }
            catch (Exception)
            {
                Debug.WriteLine("Не удалось загрузить информацию");
            }
            finally 
            { 
                IsBusy = false; 
            }
        }
        
        public void OnAppearing()
        {
            IsBusy = true ;
            SelectedMeaning = null;
        }
    }
}
