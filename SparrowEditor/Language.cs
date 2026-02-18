using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;

namespace SparrowEditor
{
	public static class Language
	{
		public static ResourceDictionary langs = null;

		public static void Load()
		{
			Application.Current.Resources.MergedDictionaries.Clear();

			try
			{
				// Load language files by Current UI Culture language file name.
				System.Diagnostics.Debug.WriteLine($"Loaded resources! [{CultureInfo.CurrentUICulture}]");
				var dict = new ResourceDictionary
				{
					Source = new Uri($"Lang/{CultureInfo.CurrentUICulture}.xaml", UriKind.Relative)
				};
				Application.Current.Resources.MergedDictionaries.Add(dict);
				langs = dict;
			}
			catch
			{
				// Fallback: Use one language selected file.
				System.Diagnostics.Debug.WriteLine($"Failed to load resources in current UI Culture! [{CultureInfo.CurrentUICulture}]");
				var dict = new ResourceDictionary
				{
					Source = new Uri("Lang/pt-BR.xaml", UriKind.Relative) // pt-BR by now, cause its my main language lol
				};
				Application.Current.Resources.MergedDictionaries.Add(dict);
				langs = dict;
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
			key = key?.Trim();

			if (String.IsNullOrEmpty(key))
			{
				return "[!]";
			}

			// If langs was never set, try to find the language dictionary in merged dictionaries
			if (langs == null)
			{
				var merged = Application.Current?.Resources?.MergedDictionaries;
				if (merged != null)
				{
					foreach (ResourceDictionary rd in merged)
					{
						if (rd?.Source != null && rd.Source.OriginalString.StartsWith("Lang/", StringComparison.OrdinalIgnoreCase))
						{
							langs = rd;
							break;
						}
					}

					if (langs == null && merged.Count > 0)
					{
						langs = merged[0];
					}
				}
			}

			if (langs == null || !langs.Contains(key))
			{
				System.Diagnostics.Debug.WriteLine($"ERROR: The current language doesn't have the expected key ({key}) or dictionary is null.");
				return $"[! {key} !]";
			}

			var obj = langs[key];
			var str = obj as string ?? obj?.ToString() ?? $"[! {key} !]";

			if (value != null && value.Length > 0)
			{
				return String.Format(str, value);
			}

			return str;
		}
	}
}
