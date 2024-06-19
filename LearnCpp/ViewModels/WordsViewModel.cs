using LearnCpp.Models;
using LearnCpp.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LearnCpp.ViewModels
{
    public class WordsViewModel: BaseViewModel
    {
        private int height;
        public int Height
        {
            get => height;
            set => SetProperty(ref height, value);
        }

        private Meaning selectedMeaning;
        public Meaning SelectedMeaning
        {
            get => selectedMeaning;
            set
            {
                SetProperty(ref selectedMeaning, value);
                OnMeaningSelected(value);
            }
        }
        public ObservableCollection<Meaning> Meanings { get; }
        public Command OpenSettingsCommand { get; }
        public Command<Meaning> MeaningTapped { get; }
        public Command LoadWordsCommand { get; }

        public WordsViewModel()
        {
            Title = "Избранное";
            Meanings = new ObservableCollection<Meaning>();
            LoadWordsCommand = new Command(async() => await ExecuteLoadWordsCommand());
            MeaningTapped = new Command<Meaning>(OnMeaningSelected);
            Height = 0;
        }

        async void OnMeaningSelected(Meaning meaning)
        {
            if (meaning == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(WordDetailPage)}?{nameof(WordDetailViewModel.MeaningId)}={meaning.MeaningID}");
        }

        async Task ExecuteLoadWordsCommand()
        {
            IsBusy = true;

            try
            {
                Meanings.Clear();

                var words = (await App.Database.GetAllMeaningWithChildren()).Where(x=>x.IsFavorite == true);
                if (words.Count() <= 0)
                {

                    Height = 1000;
                }
                else
                {
                    Height = 0;
                    foreach (var meaning in words)
                    {
                        Meanings.Add(meaning);
                    }
                }

            }
            catch (Exception)
            {
                Debug.WriteLine("Не удалось загрузить страницу");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedMeaning = null;
        }
    }
}
