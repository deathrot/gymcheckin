using System;
using System.Collections.Generic;
using System.Text;

namespace GymCheckin.Utility
{
    /// <summary>
    /// Preferences Utility
    /// </summary>
    public static class PreferencesUtility
    {

        /// <summary>
        /// Get preference as bool
        /// </summary>
        /// <param name="prefName"></param>
        /// <returns></returns>
        public static bool GetSavedPreferenceAsBool(string prefName)
        {
            var d = Xamarin.Essentials.Preferences.Get(prefName, false);

            return d;
        }

        /// <summary>
        /// Get Preferences as int
        /// </summary>
        /// <param name="prefName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int GetSavedPreferenceAsInt(string prefName, int defaultValue)
        {
            var d = Xamarin.Essentials.Preferences.Get(prefName, defaultValue);

            return d;
        }

        /// <summary>
        /// Get Preferences as int
        /// </summary>
        /// <param name="prefName"></param>
        /// <param name="defaultValue"></param>
        public static int GetSavedPreferenceAsInt(string prefName)
        {
            var d = Xamarin.Essentials.Preferences.Get(prefName, default(int));

            return d;
        }

        /// <summary>
        /// Get Preferences as string
        /// </summary>
        /// <param name="prefName"></param>
        /// <param name="defaultValue"></param>
        public static string GetSavedPreferenceAsString(string prefName)
        {
            var d = Xamarin.Essentials.Preferences.Get(prefName, default(string));

            return d;
        }

        /// <summary>
        /// Get Preferences as string
        /// </summary>
        /// <param name="prefName"></param>
        /// <param name="defaultValue"></param>
        public static string GetSavedPreferenceAsString(string prefName, string defaultValue)
        {
            var d = Xamarin.Essentials.Preferences.Get(prefName, defaultValue);

            return d;
        }

        /// <summary>
        /// Set bool preference
        /// </summary>
        /// <param name="prefName"></param>
        /// <returns></returns>
        public static void SavePreference(string prefName, bool value)
        {
            Xamarin.Essentials.Preferences.Set(prefName, value);
        }

        /// <summary>
        /// Set int preferences
        /// </summary>
        /// <param name="prefName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static void SavePreference(string prefName, int preference)
        {
            Xamarin.Essentials.Preferences.Set(prefName, preference);
        }

        /// <summary>
        /// Set string preferences
        /// </summary>
        /// <param name="prefName"></param>
        /// <param name="defaultValue"></param>
        public static void SavePreference(string prefName, string value)
        {
            Xamarin.Essentials.Preferences.Set(prefName, value);
        }

        public static void RemovePreference(string prefName)
        {
            Xamarin.Essentials.Preferences.Remove(prefName);
        }

    }
}
