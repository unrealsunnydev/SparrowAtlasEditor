using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;

namespace SparrowEditor.Lang
{
	public static class Language
	{
		public static ResourceDictionary langs = null;

		public static void Load()
		{
			langs = new ResourceDictionary();

			Application.Current.Resources.MergedDictionaries.Clear();

			try
			{
				// Load language files by Current UI Culture language file name.
				System.Diagnostics.Debug.WriteLine($"Loaded resources! [{CultureInfo.CurrentUICulture}]");
				Application.Current.Resources.MergedDictionaries.Add(
					new ResourceDictionary
					{
						Source = new Uri($"Lang/{CultureInfo.CurrentUICulture}.xaml", UriKind.Relative)
					});
			}
			catch
			{
				// Fallback: Use one language selected file.
				System.Diagnostics.Debug.WriteLine($"Failed to load resources in current UI Culture! [{CultureInfo.CurrentUICulture}]");
				Application.Current.Resources.MergedDictionaries.Add(
					new ResourceDictionary
					{
						Source = new Uri("Lang/pt-BR.xaml", UriKind.Relative) // pt-BR by now, cause its my main language lol
					});
			}
			
		}

		/// <summary>
		/// Custom function to load language strings in dictionary resources
		/// </summary>
		/// <param name="key">Resource Key to use</param>
		/// <param name="value">(OPTIONAL) Format replacements in the string,
		/// e.g. "{0}" in twill be replaced with "Sunny" if "['Sunny']" is passed here.</param>
		/// <returns></returns>
		public static string GetString(string key, params object[] value)
		{
			key = key.Trim();

			if (String.IsNullOrEmpty(key))
			{
				return "[!]";
			}
			else if (!langs.Contains(key) || langs == null)
			{
				System.Diagnostics.Debug.WriteLine($"ERROR: The current language doesn't have the spected key ({key}) or dictionary is null.");
				return $"[! {key} !]";
			}

			if (value != null || value?.Length > 0)
			{
				return String.Format((string)langs[key], value);
			}

			return key;
		}
	}
}
