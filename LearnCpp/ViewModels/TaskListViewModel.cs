using LearnCpp.Models;
using LearnCpp.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;

namespace LearnCpp.ViewModels
{
    public class TaskListViewModel : BaseViewModel
    {
        private PracticeTask selectedTask;
        public PracticeTask SelectedTask
        {
            get => selectedTask;
            set
            {
                SetProperty(ref selectedTask, value);
                OpenTask(value);
            }
        }

        public ObservableCollection<IGrouping<string, TaskByTopic>> TasksList { get; }
        public Command<PracticeTask> TapTask { get; }


        public TaskListViewModel() {
            Title = "Задания";

            TasksList = new ObservableCollection<IGrouping<string, TaskByTopic>>();
            LoadTaskListAsync();

            TapTask = new Command<PracticeTask>(OpenTask);
        }

        private async void LoadTaskListAsync()
        {
            try
            {
                var taskList = (await App.Database.TaskByTopicWithChildren()).GroupBy(x => x.topic.TopicTitle);

                foreach (var item in taskList)
                {
                    TasksList.Add(item);
                }
            }
            catch (Exception)
            {
                Debug.WriteLine("Не удалось загрузить страницу");
            }
        }

        private async void OpenTask(PracticeTask task)
        {
            if (task == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(TaskPage)}?{nameof(TaskViewModel.TaskId)}={task.TaskID}");

        }

        public void OnAppearing()
        {
            SelectedTask = null;
        }
    }
}
