using LearnCpp.Helpers;
using LearnCpp.Models;
using LearnCpp.Views;
using System;
using System.IO;
using System.Reflection;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LearnCpp
{

    public partial class App : Application
    {
        public const string DATABASE_NAME = "LearnCppDB.db";

        private static DatabaseRepository database;
        public static DatabaseRepository Database
        {
            get
            {
                if (database == null)
                {
                    string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME);
                    if (!File.Exists(dbPath))
                    {
                        var assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
                        using (Stream stream = assembly.GetManifestResourceStream($"LearnCpp.{DATABASE_NAME}"))
                        {
                            using (FileStream fs = new FileStream(dbPath, FileMode.OpenOrCreate))
                            {
                                stream.CopyTo(fs);
                                fs.Flush();
                            }
                        }
                    }
                    database = new DatabaseRepository(dbPath);
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            TheTheme.SetTheme();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            TheTheme.SetTheme();
            RequestedThemeChanged -= App_RequestedThemeChanged;
        }

        protected override void OnResume()
        {
            TheTheme.SetTheme();
            RequestedThemeChanged += App_RequestedThemeChanged;
        }

        private void App_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                TheTheme.SetTheme();
            });
        }
    }
}
