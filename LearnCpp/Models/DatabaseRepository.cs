using SQLite;
using SQLiteNetExtensionsAsync.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearnCpp.Models
{
    public class DatabaseRepository
    {
        SQLiteAsyncConnection database;

        public DatabaseRepository(string databasePath)
        {
            database = new SQLiteAsyncConnection(databasePath);
        }

        #region language
        public async Task CreateLanguage() => await database.CreateTableAsync<Language>();
        public async Task<List<Language>> GetAllLanguages() => await database.GetAllWithChildrenAsync<Language>();
        public async Task<Language> GetLanguage(int id) => await database.GetAsync<Language>(id);
        public async Task<int> DeleteLanguage(Language item) => await database.DeleteAsync(item);
        public async Task<int> SaveLanguage(Language item)
        {
            if (item.LanguageID != 0)
            {
                await database.UpdateAsync(item);
                return item.LanguageID;
            }
            else
            {
                return await database.InsertAsync(item);
            }
        }
        #endregion

        #region dictionary
        public async Task CreateDictionary() => await database.CreateTableAsync<DictionaryWord>();
        public async Task<List<DictionaryWord>> GetAllWords() => await database.GetAllWithChildrenAsync<DictionaryWord>();
        public async Task<DictionaryWord> GetWord(int id) => await database.GetAsync<DictionaryWord>(id);
        public async Task<int> DeleteWord(DictionaryWord item) => await database.DeleteAsync(item);
        public async Task<int> SaveWord(DictionaryWord item)
        {
            if (item.WordID != 0)
            {
                await database.UpdateAsync(item);
                return item.WordID;
            }
            else
            {
                return await database.InsertAsync(item);
            }
        }
        #endregion

        #region transcription
        public async Task CreateTranscription() => await database.CreateTableAsync<Transcription>();
        public async Task<List<Transcription>> GetAllTranscriptions() => await database.GetAllWithChildrenAsync<Transcription>();
        public async Task<Transcription> GetTranscription(int id) => await database.GetAsync<Transcription>(id);
        public async Task<int> DeleteTranscriprion(Transcription item) => await database.DeleteAsync(item);
        public async Task<int> SaveTranscription(Transcription item)
        {
            if (item.TranscriptionID != 0)
            {
                await database.UpdateAsync(item);
                return item.TranscriptionID;
            }
            else
            {
                return await database.InsertAsync(item);
            }
        }
        #endregion

        #region meaning
        public async Task CreateMeaning() => await database.CreateTableAsync<Meaning>();
        public async Task<Meaning> GetMeaning(int id) => await database.GetAsync<Meaning>(id);
        public async Task<IEnumerable<Meaning>> GetAllMeaning() => await database.Table<Meaning>().ToListAsync();
        public async Task<int> DeleteMeaning(Meaning item) => await database.DeleteAsync(item);
        public async Task<int> SaveMeaning(Meaning item)
        {
            if (item.MeaningID != 0)
            {
                await database.UpdateAsync(item);
                return item.MeaningID;
            }
            else
            {
                return await database.InsertAsync(item);
            }
        }
        public async Task<List<Meaning>> GetAllMeaningWithChildren() => await database.GetAllWithChildrenAsync<Meaning>();
        #endregion

        #region section
        public async Task CreateSection() => await database.CreateTableAsync<Section>();
        public async Task<List<Section>> GetAllSections() => await database.GetAllWithChildrenAsync<Section>();
        public async Task<Section> GetSection(int id) => await database.GetAsync<Section>(id);
        public async Task<int> DeleteSection(Section item) => await database.DeleteAsync(item);
        public async Task<int> SaveSection(Section item)
        {
            if (item.SectionID != 0)
            {
                await database.UpdateAsync(item);
                return item.SectionID;
            }
            else
            {
                return await database.InsertAsync(item);
            }
        }

        #endregion

        #region chapter
        public async Task CreateChapter() => await database.CreateTableAsync<Chapter>();
        public async Task<List<Chapter>> GetAllChapters() => await database.GetAllWithChildrenAsync<Chapter>();
        public async Task<Chapter> GetChapter(int id) => await database.GetAsync<Chapter>(id);
        public async Task<int> DeleteChapter(Chapter item) => await database.DeleteAsync(item);
        public async Task<int> SaveChapter(Chapter item)
        {
            if (item.ChapterID != 0)
            {
                await database.UpdateAsync(item);
                return item.ChapterID;
            }
            else
            {
                return await database.InsertAsync(item);
            }
        }
        #endregion

        #region topic
        public async Task CreateTopic() => await database.CreateTableAsync<Topic>();
        public async Task<IEnumerable<Topic>> GetAllTopics() => await database.Table<Topic>().ToListAsync();
        public async Task<Topic> GetTopic(int id) => await database.GetAsync<Topic>(id);
        public async Task<int> DeleteTopic(Topic item) => await database.DeleteAsync(item);
        public async Task<int> SaveTopic(Topic item)
        {
            if (item.TopicID != 0)
            {
                await database.UpdateAsync(item);
                return item.TopicID;
            }
            else
            {
                return await database.InsertAsync(item);
            }
        }

        public async Task<List<Topic>> GetTopicWithChildren() => await database.GetAllWithChildrenAsync<Topic>();
        #endregion

        #region lecture
        public async Task CreateLecture() => await database.CreateTableAsync<Lecture>();
        public async Task<IEnumerable<Lecture>> GetAllLectures() => await database.Table<Lecture>().ToListAsync();
        public async Task<Lecture> GetLecture(int id) => await database.GetAsync<Lecture>(id);
        public async Task<int> DeleteLecture(Lecture item) => await database.DeleteAsync(item);
        public async Task<int> SaveLecture(Lecture item)
        {
            if (item.LectureID != 0)
            {
                await database.UpdateAsync(item);
                return item.LectureID;
            }
            else
            {
                return await database.InsertAsync(item);
            }
        }
        #endregion

        #region task
        public async Task CreateTask() => await database.CreateTableAsync<PracticeTask>();
        public async Task<List<PracticeTask>> GetAllTasks() => await database.GetAllWithChildrenAsync<PracticeTask>();
        public async Task<PracticeTask> GetTask(int id) => await database.GetAsync<PracticeTask>(id);
        public async Task<int> DeleteTask(PracticeTask item) => await database.DeleteAsync(item);
        public async Task<int> SaveTasks(PracticeTask item)
        {
            if (item.TaskID != 0)
            {
                await database.UpdateAsync(item);
                return item.TaskID;
            }
            else
            {
                return await database.InsertAsync(item);
            }
        }
        #endregion

        #region testTask
        public async Task CreateTestTask() => await database.CreateTableAsync<TaskTest>();
        public async Task<List<TaskTest>> GetAllTest() => await database.GetAllWithChildrenAsync<TaskTest>();
        public async Task<TaskTest> GetTest(int id) => await database.GetAsync<TaskTest>(id);
        public async Task<int> DeleteTest(TaskTest item) => await database.DeleteAsync(item);
        public async Task<int> SaveTest(TaskTest item)
        {
            if (item.TestID != 0)
            {
                await database.UpdateAsync(item);
                return item.TestID;
            }
            else
            {
                return await database.InsertAsync(item);
            }
        }
        #endregion

        #region taskByTopic
        public async Task CreateTaskByTopic() => await database.CreateTableAsync<TaskByTopic>();
        public async Task<IEnumerable<TaskByTopic>> GetAllTasksByTopic() => await database.Table<TaskByTopic>().ToListAsync();
        public async Task<TaskByTopic> GetTaskByTopic(int id) => await database.GetAsync<TaskByTopic>(id);
        public async Task<int> DeleteTaskByTopic(TaskByTopic item) => await database.DeleteAsync(item);
        public async Task<int> SaveTaskByTopic(TaskByTopic item)
        {
            if (item.TaskByTopicID != 0)
            {
                await database.UpdateAsync(item);
                return item.TaskByTopicID;
            }
            else
            {
                return await database.InsertAsync(item);
            }
        }
        public async Task<List<TaskByTopic>> TaskByTopicWithChildren() => await database.GetAllWithChildrenAsync<TaskByTopic>();
        #endregion

        #region lectureByTopic
        public async Task CreateLectureByTopic() => await database.CreateTableAsync<LectureByTopic>();
        public async Task<LectureByTopic> GetLecturByTopic(int id) => await database.GetAsync<LectureByTopic>(id);
        public async Task<int> DeleteLectureByTopic(LectureByTopic item) => await database.DeleteAsync(item);
        public async Task<int> SaveLectureByTopic(LectureByTopic item)
        {
            if (item.LectureByTopicID != 0)
            {
                await database.UpdateAsync(item);
                return item.LectureByTopicID;
            }
            else
            {
                return await database.InsertAsync(item);
            }
        }
        public async Task<List<LectureByTopic>> LectureByTopicWithChildren() => await database.GetAllWithChildrenAsync<LectureByTopic>();
        #endregion

        #region MeaningInLecture
        public async Task CreateMeaningInLecture() => await database.CreateTableAsync<MeaningInLecture>();
        public async Task<IEnumerable<MeaningInLecture>> GetAllMeaningInLecture() => await database.Table<MeaningInLecture>().ToListAsync();
        public async Task<MeaningInLecture> GetMeaningInLecture(int id) => await database.GetAsync<MeaningInLecture>(id);
        public async Task<int> DeleteMeaningInLecture(MeaningInLecture item) => await database.DeleteAsync(item);
        public async Task<int> SaveMeaningInLecture(MeaningInLecture item)
        {
            if (item.MeaningInLectureID != 0)
            {
                await database.UpdateAsync(item);
                return item.MeaningInLectureID;
            }
            else
            {
                return await database.InsertAsync(item);
            }
        }
        public async Task<List<MeaningInLecture>> MeaningInLectureWithChildren() => await database.GetAllWithChildrenAsync<MeaningInLecture>();
        #endregion

        #region MeaningInTask
        public async Task CreateMeaningInTask() => await database.CreateTableAsync<MeaningInTask>();
        public async Task<IEnumerable<MeaningInTask>> GetAllMeaningInTask() => await database.Table<MeaningInTask>().ToListAsync();
        public async Task<MeaningInTask> GetMeaningInTask(int id) => await database.GetAsync<MeaningInTask>(id);
        public async Task<int> DeleteMeaningInTask(MeaningInTask item) => await database.DeleteAsync(item);
        public async Task<int> SaveMeaningInTask(MeaningInTask item)
        {
            if (item.MeaningInTaskID != 0)
            {
                await database.UpdateAsync(item);
                return item.MeaningInTaskID;
            }
            else
            {
                return await database.InsertAsync(item);
            }
        }
        public async Task<List<MeaningInTask>> MeaningInTaskWithChildren() => await database.GetAllWithChildrenAsync<MeaningInTask>();
        #endregion
    }
}
