using LearnCpp.Models;
using LearnCpp.Views;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LearnCpp.ViewModels
{
    [QueryProperty(nameof(TaskId), nameof(TaskId))]
    public class TaskViewModel : BaseViewModel
    {
        #region task
        private int taskId;
        private string description;
        private string sampleInput;
        private string taskKey;
        private bool taskTest;
        public int TaskId
        {
            get => taskId;
            set
            {
                taskId = value;
                LoadTaskTopic(value);
            }
        }
        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public string SampleInput
        {
            get => sampleInput;
            set => SetProperty(ref sampleInput, value);
        }
        public string TaskKey
        {
            get => taskKey;
            set => SetProperty(ref taskKey, value);
        }

        public bool TaskTest
        {
            get => taskTest;
            set => SetProperty(ref taskTest, value);
        }
        #endregion

        #region hide 
        private int heightTest;
        public int HeightTest
        {
            get => heightTest;
            set => SetProperty(ref heightTest, value);
        }

        private int heightTask;
        public int HeightTask
        {
            get => heightTask;
            set => SetProperty(ref heightTask, value);
        }
        #endregion

        #region test
        private string valueOne;
        public string ValueOne
        {
            get => valueOne;
            set => SetProperty(ref valueOne, value);
        }

        private string valueTwo;
        public string ValueTwo
        {
            get => valueTwo;
            set => SetProperty(ref valueTwo, value);
        }

        private string valueThree;
        public string ValueThree
        {
            get => valueThree;
            set => SetProperty(ref valueThree, value);
        }

        private string valueFour;
        public string ValueFour
        {
            get => valueFour;
            set => SetProperty(ref valueFour, value);
        }

        private bool checkOne;
        public bool CheckOne
        {
            get => checkOne;
            set
            {
                checkOne = value;
                SetProperty(ref checkOne, value);
            }
        }
        private bool checkTwo;
        public bool CheckTwo
        {
            get => checkTwo;
            set
            {
                checkTwo = value;
                SetProperty(ref checkTwo, value);
            }
        }
        private bool checkThree;
        public bool CheckThree
        {
            get => checkThree;
            set
            {
                checkThree = value;
                SetProperty(ref checkThree, value);
            }
        }

        private bool checkFour;
        public bool CheckFour
        {
            get => checkFour;
            set
            {
                checkFour = value;
                SetProperty(ref checkFour, value);
            }
        }

        #endregion

        #region taskSolve
        private string resultColor;
        public string ResultColor
        {
            get => resultColor;
            set => SetProperty(ref  resultColor, value);    
        }
        private string taskValue;
        public string TaskValue
        {
            get => taskValue;
            set => SetProperty(ref taskValue, value);
        }

        private string taskResult;
        public string TaskResult
        {
            get => taskResult;
            set => SetProperty(ref taskResult, value);
        }

        private string btnTxt;
        public string BtnTxt
        {
            get => btnTxt;
            set => SetProperty(ref btnTxt, value);
        }

        private string taskText;
        public string TaskText
        {
            get => taskResult;
            set => SetProperty(ref taskResult, value);
        }
        #endregion

        public ICommand LoadMeaningsCommand => new Command<Meaning>(OpenMeaningsInTask);

        private async void OpenMeaningsInTask(object obj) => await Shell.Current.GoToAsync($"{nameof(MeaningInTaskPage)}?{nameof(MeaningInTaskViewModel.TaskFk)}={TaskId}");

        private async void LoadTaskTopic(int id)
        {
            try
            {
                await App.Database.CreateTask();
                await App.Database.CreateTaskByTopic();

                var practiceTask = await App.Database.GetTask(id);
                var taskDone = (await App.Database.GetAllTasksByTopic()).Where(x => x.TaskFK == id).FirstOrDefault();

                Title = practiceTask.TaskTopic;
                Description = practiceTask.TaskDescription;
                SampleInput = practiceTask.SampleInput;
                TaskKey = practiceTask.TaskKey;
                TaskTest = practiceTask.IsTest;

                if (TaskTest)
                {
                    HeightTest = 500;
                    HeightTask = 0;

                    await App.Database.CreateTestTask();
                    var tests = (await App.Database.GetAllTest()).Where(x => x.TaskFK == id).ToList();

                    ValueOne = tests[0].TestValue;
                    ValueTwo = tests[1].TestValue;
                    ValueThree = tests[2].TestValue;
                    ValueFour = tests[3].TestValue;

                    
                }
                else
                {
                    HeightTask = 500;
                    HeightTest = 0;

                    if (taskDone.TopicFK != 1)
                    {
                        TaskText = @"
#include <iostream>

int main() 
{                                        
    // Write C++ code here
    std::cout << ""Hello world!"";

    return 0;
}";
                    }
                }

                if (!taskDone.IsDone)
                {
                    BtnTxt = "Ответить";
                }
                else
                {
                    BtnTxt = "Решить снова";

                    if (!TaskTest) TaskText = taskDone.Solution;

                    if (taskDone.Result)
                    {
                        TaskResult = "Верно!";
                        ResultColor = "#97F99A";
                    }
                    else
                    {
                        TaskResult = "Неправильно..";
                        ResultColor = "#F99A97";
                    }
                }
            }
            catch (Exception)
            {
                Debug.WriteLine("Не удалось загрузить задание");
            }
        }

        public Command<string> CheckValue { get => new Command<string>(ExecuteCheckValue); }

        private void ExecuteCheckValue(object obj)
        {
            if (string.IsNullOrEmpty(obj.ToString()))
                return;

            TaskValue = (string)obj;

            if (TaskValue == ValueOne) CheckOne = true;
            if (TaskValue == ValueTwo) CheckTwo = true;
            if (TaskValue == ValueThree) CheckThree = true;
            if (TaskValue == ValueFour) CheckFour = true;
        }

        public ICommand ResultBtn { get => new Command(ResultCommand); }
        private async void ResultCommand(object obj)
        {
            var task = (await App.Database.GetAllTasksByTopic()).Where(x => x.TaskFK == TaskId).FirstOrDefault();
            if (BtnTxt == "Ответить")
            {
                if (task.TopicFK != 1) SolveTaskCode(task);
                else SolveTaskTest(task);
            }
            else
            {
                ResultColor = "Transparent";
                TaskResult = "";

                task.Solution = "";
                task.Result = false;
                task.IsDone = false;
                await App.Database.SaveTaskByTopic(task);

                if (task.TopicFK == 1) TaskText = " ";
                else TaskText = @"
#include <iostream>

int main() 
{                                        
    // Write C++ code here
    std::cout << ""Hello world!"";

    return 0;
}";

                BtnTxt = "Ответить";
            }
        }


        private async void SolveTaskTest(TaskByTopic task)
        {
            
            task.IsDone = true;
            task.DateCopmplition = DateTime.Now;

            if (!TaskTest)
            {
                TaskValue = TaskText;
                task.Solution = TaskText;
            }
            else task.Solution = TaskValue;

            if (string.IsNullOrEmpty(TaskValue))
            {
                TaskResult = "Выберите или напишите ответ";
                return;
            }

            if (TaskValue == TaskKey)
            {
                task.Result = true;
                TaskResult = "Верно!";
                ResultColor = "#97F99A";
            }
            else
            {
                task.Result = false;
                TaskResult = "Неправильно..";
                ResultColor = "#F99A97";
            }

            await App.Database.SaveTaskByTopic(task);
            BtnTxt = "Решить снова";
        }

        private async void SolveTaskCode(TaskByTopic task)
        {
            if (string.IsNullOrEmpty(TaskText))
            {
                TaskResult = "Выберите или напишите ответ";
                return;
            }

            BtnTxt = "Решаем...";

            task.Solution = TaskText;
            task.IsDone = true;
            task.DateCopmplition = DateTime.Now;

            TaskValue = await CompileAndRunCode(TaskText);
            if (TaskValue == TaskKey)
            {
                task.Result = true;
                TaskResult = "Верно!";
                ResultColor = "#97F99A";
            }
            else
            {
                task.Result = false;
                TaskResult = "Неправильно..";
                ResultColor = "#F99A97";
            }

            await App.Database.SaveTaskByTopic(task);
            BtnTxt = "Решить снова";
        }

        private async Task<string> CompileAndRunCode(string code)
        {
            string clientId = "8e5e2492d297f6700919659b99078d3b";
            string clientSecret = "94dc1f26775f8e4a75d86bbec2053c90dd052211bd12b85c4e9c560945f7c9aa";
            string apiUrl = "https://api.jdoodle.com/v1/execute";

            var requestBody = new
            {
                script = code,
                stdin = SampleInput,
                language = "cpp17",
                versionIndex = "0",
                clientId = clientId,
                clientSecret = clientSecret
            };

            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var responseObject = JsonConvert.DeserializeObject<JDoodleResponse>(responseContent);
                    return responseObject.output;
                }
                else
                {
                    return "Ошибка компиляции или выполнения.";
                }
            }
        }
    }

    public class JDoodleResponse
    {
        public string output { get; set; }
        public string statusCode { get; set; }
        public string memory { get; set; }
        public string cpuTime { get; set; }
        public string error { get; set; }
    }

}
