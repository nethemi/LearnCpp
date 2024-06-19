using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;

namespace LearnCpp.Models
{
    [Table("Languages")]
    public class Language
    {
        [PrimaryKey, AutoIncrement, Column("LanguageID"), NotNull, Unique]
        public int LanguageID { get; set; }

        [NotNull, Unique]
        public string LanguageName { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public IEnumerable<DictionaryWord> words { get; set; }
    }

    [Table("DictionaryWords")]
    public class DictionaryWord
    {
        [PrimaryKey, AutoIncrement, Column("WordID"), NotNull, Unique]
        public int WordID { get; set; }

        [NotNull, Unique]
        public string Word { get; set; }

        [ForeignKey(typeof(Language)), NotNull]
        public int LanguageFK { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public Language language { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Meaning> meanings { get; set; }
    }

    [Table("Transcriptions")]
    public class Transcription
    {
        [PrimaryKey, AutoIncrement, Column("TranscriptionID"), NotNull, Unique]
        public int TranscriptionID { get; set; }

        [NotNull, Unique]
        public string TranscriptionItem { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public IEnumerable<Meaning> meanings { get; set; }
    }

    [Table("Meanings")]
    public class Meaning
    {
        [PrimaryKey, AutoIncrement, Column("MeaningID"), NotNull, Unique]
        public int MeaningID { get; set; }

        [ForeignKey(typeof(DictionaryWord)), NotNull]
        public int WordFK { get; set; }

        [ForeignKey(typeof(DictionaryWord)), NotNull]
        public int MeaningFK { get; set; }

        [ForeignKey(typeof(Transcription))]
        public int TransctriptionFK { get; set; }
        
        public bool IsFavorite { get; set; }
        public DateTime DateViewed { get; set; }


        [ManyToOne(nameof(WordFK), CascadeOperations = CascadeOperation.All)]
        public DictionaryWord word { get; set; }

        [ManyToOne(nameof(MeaningFK), CascadeOperations = CascadeOperation.All)]
        public DictionaryWord meaningWord { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public Transcription transcription { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<MeaningInLecture> lectures { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<MeaningInTask> tasks { get; set; }
    }

    [Table("Sections")]
    public class Section
    {
        [PrimaryKey, AutoIncrement, Column("SectionID")]
        public int SectionID { get; set; }

        public string SectionTitle { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public IEnumerable<Chapter> chapters { get; set; }

    }

    [Table("Chapters")]
    public class Chapter
    {
        [PrimaryKey, AutoIncrement, Column("ChapterID")]
        public int ChapterID { get; set; }
        public string ChapterTitle { get; set;}

        [ForeignKey(typeof(Section))]
        public int SectionFK { get; set; }


        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public Section section { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public IEnumerable<Topic> topics { get; set; }

    }

    [Table("Topics")]
    public class Topic
    {
        [PrimaryKey, AutoIncrement, Column("TopicID")]
        public int TopicID { get; set; }
        public string TopicTitle { get; set; }

        [ForeignKey(typeof(Chapter))]
        public int ChapterFK { get; set; }


        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public Chapter chapter { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public IEnumerable<TaskByTopic> tasks { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public IEnumerable<LectureByTopic> lectures { get; set; }

    }

    [Table("Lectures")]
    public class Lecture
    {
        [PrimaryKey, AutoIncrement, Column("LectureID")]
        public int LectureID { get; set; }
        public string LectureTopic { get; set; }
        public string ChineseTopic { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public IEnumerable<LectureByTopic> lectures { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public IEnumerable<MeaningInLecture> meanings { get; set; }
    }

    [Table("PracticeTasks")]
    public class PracticeTask
    {
        [PrimaryKey, AutoIncrement, Column("TaskID")]
        public int TaskID { get; set; }
        public string TaskTopic { get; set; }
        public string TaskDescription { get; set; }
        public string SampleInput { get; set; }
        public string TaskKey { get; set; }
        public bool IsTest { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public IEnumerable<TaskByTopic> tasks { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public IEnumerable<MeaningInTask> meanings { get; set; }
    }

    [Table("TaskTest")]
    public class TaskTest
    {
        [PrimaryKey, AutoIncrement, Column("TestID")]
        public int TestID { get; set; }
        public string TestValue{ get; set; }

        [ForeignKey(typeof(PracticeTask))]
        public int TaskFK { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public PracticeTask task { get; set; }
    }

    [Table("TaskByTopics")]
    public class TaskByTopic
    {
        [PrimaryKey, AutoIncrement, Column("TaskByTopicID")]
        public int TaskByTopicID { get; set; }

        [ForeignKey(typeof(Topic))]
        public int TopicFK { get; set; }

        [ForeignKey(typeof(PracticeTask))]
        public int TaskFK { get; set; }

        public string Solution { get; set; }
        public bool IsDone { get; set; }
        public bool Result { get; set; }
        public DateTime DateCopmplition { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public Topic topic { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public PracticeTask task { get; set; }
    }

    [Table("LectureByTopics")]
    public class LectureByTopic
    {
        [PrimaryKey, AutoIncrement, Column("LectureByTopicID")]
        public int LectureByTopicID { get; set; }

        [ForeignKey(typeof(Topic))]
        public int TopicFK { get; set; }

        [ForeignKey(typeof(Lecture))]
        public int LectureFK { get; set; }
        public DateTime DateViewed { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public Topic topic { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public Lecture lecture { get; set; }
    }

    [Table("MeaningInLectures")]
    public class MeaningInLecture
    {
        [PrimaryKey, AutoIncrement, Column("MeaningInLectureID")]
        public int MeaningInLectureID { get; set; }

        [ForeignKey(typeof(Meaning))]
        public int MeaningFK { get; set; }

        [ForeignKey(typeof(Lecture))]
        public int LectureFK { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public Meaning meaning { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public Lecture lecture { get; set; }
    }

    [Table("MeaningInTasks")]
    public class MeaningInTask
    {
        [PrimaryKey, AutoIncrement, Column("MeaningInTaskID")]
        public int MeaningInTaskID { get; set; }

        [ForeignKey(typeof(Meaning))]
        public int MeaningFK { get; set; }

        [ForeignKey(typeof(PracticeTask))]
        public int TaskFK { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public Meaning meaning { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public PracticeTask practiceTask { get; set; }
    }
}
