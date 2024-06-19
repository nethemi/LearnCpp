using LearnCpp.Models;
using LearnCpp.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;
using static Android.Resource;

namespace LearnCpp.ViewModels
{
    [QueryProperty(nameof(TaskFk), nameof(TaskFk))]
    public class MeaningInTaskViewModel: BaseViewModel
    {
        private int height;
        public int Height
        {
            get => height;
            set => SetProperty(ref height, value);
        }

        public ObservableCollection<Meaning> Meanings { get; }
        public MeaningInTaskViewModel()
        {
            Meanings = new ObservableCollection<Meaning>();
        }

        private int taskFK;
        public int TaskFk
        {
            get => TaskFk;
            set
            {
                taskFK = value;
                LoadMeaningList(value);
            }
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
        public Command<Meaning> MeaningTapped => new Command<Meaning>(OnMeaningSelected);
        private async void OnMeaningSelected(Meaning meaning)
        {
            if (meaning == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(WordDetailPage)}?{nameof(WordDetailViewModel.MeaningId)}={meaning.MeaningID}");
        }

        private async void LoadMeaningList(int id)
        {
            try
            {
                Meanings.Clear();
                await App.Database.CreateMeaning();
                await App.Database.CreateMeaningInTask();

                var wordMeaning = (await App.Database.GetAllMeaningInTask()).Where(x => x.TaskFK == id);
                var words = await App.Database.GetAllMeaningWithChildren();

                if (wordMeaning.Count() <= 0)
                {

                    Height = 1000;
                }
                else
                {
                    Height = 0;
                    foreach (var meaning in words)
                    {
                        foreach (var word in wordMeaning)
                        {
                            if (meaning.MeaningID == word.MeaningFK) Meanings.Add(meaning);
                        }
                    }
                }

            }
            catch (Exception)
            {
                Debug.WriteLine("Не удалось загрузить страницу");
            }
        }
    }
}
