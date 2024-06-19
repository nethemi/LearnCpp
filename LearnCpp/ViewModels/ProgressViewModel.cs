using LearnCpp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LearnCpp.ViewModels
{
    public class ResultTopic
    {
        public string TopicTitle { get; set; }
        public string Result { get; set; }
    }

    public class ProgressViewModel : BaseViewModel
    {
        public ObservableCollection<IGrouping<string, TaskByTopic>> TasksList { get; }
        public ObservableCollection<ResultTopic> ResultList { get; }

        public Command LoadCommand { get; }
        public ProgressViewModel() 
        {
            Title = "Успеваемость";

            TasksList = new ObservableCollection<IGrouping<string, TaskByTopic>>();
            ResultList = new ObservableCollection<ResultTopic>();

            LoadCommand = new Command(async () => await LoadTaskListAsync());
        }

        private async Task LoadTaskListAsync()
        {
            IsBusy = true;
            try
            {
                TasksList.Clear();

                var taskList = (await App.Database.TaskByTopicWithChildren()).GroupBy(x => x.topic.TopicTitle);

                foreach (var item in taskList)
                {
                    TasksList.Add(item);
                }

                var tasks = await App.Database.GetAllTasksByTopic();
                var topics = await App.Database.GetAllTopics();
                ResultList.Clear();

                foreach (var topic in topics)
                {
                    List<TaskByTopic> results = new List<TaskByTopic>();
                    foreach (var task in tasks)
                    {
                        if (task.TopicFK == topic.TopicID) results.Add(task);
                    }

                    int countTasks = results.Count;
                    var doneTask = results.Where(x => x.IsDone).ToList();
                    int countDone = doneTask.Count;
                    int result = 0;

                    if (countDone > countTasks) break;

                    countDone--;

                    while (countDone >= 0)
                    {
                        if (doneTask[countDone].Result) result++;

                        countDone--;
                    }

                    ResultList.Add(new ResultTopic { TopicTitle = topic.TopicTitle, Result = "Оценка: " + ((result * 100) / countTasks) });
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
        }
    }
}
