using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using LapsRemote.Models;
using System.IO;

namespace LapsRemote.Utilities
{
	static class Settings
	{
		public static SettingsModel settingsModel;
		private static string AppDataFolderPath;
		private static string SettingsPath;

		public static void Initialize()
		{
			AppDataFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "LapsRemoteV2");
			SettingsPath = Path.Combine(AppDataFolderPath, "Settings.json");
			
			if (!File.Exists(SettingsPath))
			{
				using (StreamWriter streamWriter = new StreamWriter(SettingsPath))
				{
					string SettingJson = JsonConvert.SerializeObject(new SettingsModel() { });
					streamWriter.Write(SettingJson);
				}
			}

			settingsModel = JsonConvert.DeserializeObject<SettingsModel>(File.ReadAllText(SettingsPath));
		}

		public static void Save()
		{
			using (StreamWriter streamWriter = new StreamWriter(SettingsPath))
			{
				string JsonData = JsonConvert.SerializeObject(settingsModel);
				streamWriter.Write(JsonData);
			}
		}
	}
}
