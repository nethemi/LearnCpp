﻿using Xamarin.Forms;

namespace LearnCpp.Helpers
{
    public static class TheTheme
    {
        public static void SetTheme()
        {
            switch (Settings.Theme)
            {
                //default
                case 0:
                    App.Current.UserAppTheme = OSAppTheme.Unspecified;
                    break;
                //light
                case 1:
                    App.Current.UserAppTheme = OSAppTheme.Light;
                    break;
                //dark
                case 2:
                    App.Current.UserAppTheme = OSAppTheme.Dark;
                    break;
            }
        }
    }
}
