using LearnCpp.Models;
using System;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;

namespace LearnCpp.ViewModels
{
    [QueryProperty(nameof(MeaningId), nameof(MeaningId))]
    public class WordDetailViewModel: BaseViewModel
    {
        private int meaningId;
        private string word;
        private string meaning;

        public Meaning meaningWord = new Meaning();

        private string btnTxt;
        public string BtnTxt
        {
            get => btnTxt;
            set => SetProperty(ref btnTxt, value);
        }

        public ICommand FavoriteBtn { get => new Command(FavoriteCommand); }
        private async void FavoriteCommand(object obj)
        {
            if (meaningWord.IsFavorite)
            {
                meaningWord.IsFavorite = false;
                await App.Database.SaveMeaning(meaningWord);

                BtnTxt = "Добавить в избранное";
            }
            else
            {
                meaningWord.IsFavorite = true;
                await App.Database.SaveMeaning(meaningWord);
                BtnTxt = "Удалить из избранного";
            }
        }

        public string Word
        {
            get => word;
            set => SetProperty(ref word, value);
        }

        public string WordMeaning
        {
            get => meaning;
            set => SetProperty(ref meaning, value);
        }

        public int MeaningId
        {
            get => meaningId;
            set
            {
                meaningId = value;
                LoadMeaningId(value);
            }
        }

        public async void LoadMeaningId(int meaningId)
        {
            try
            {
                await App.Database.CreateMeaning();

                meaningWord = await App.Database.GetMeaning(meaningId);

                Word = (await App.Database.GetWord(meaningWord.WordFK)).Word;
                WordMeaning = (await App.Database.GetWord(meaningWord.MeaningFK)).Word;
                
                meaningWord.DateViewed = DateTime.Now;
                await App.Database.SaveMeaning(meaningWord);

                if (meaningWord.IsFavorite) BtnTxt = "Удалить из избранного";
                else BtnTxt = "Добавить в избранное";
            }
            catch (Exception)
            {
                Debug.WriteLine("Не удалось загрузить страницу");
            }
        }
    }
}
