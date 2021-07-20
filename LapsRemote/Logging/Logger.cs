using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Windows;
using LapsRemote.Utilities;

namespace LapsRemote.Logging
{
	static class Logger
	{
		public static Queue<Message> LogQueue;
		public static StreamWriter LogWriter;
		private static volatile bool _logging;
		

		public static void Initialize()
		{
			string AppDataFolderPath = Settings.settingsModel.AppLicationLogPath;
			string LogFilePath = Path.Combine(AppDataFolderPath, "LapsRemoteV2.log");
			_logging = true;
			MessageBox.Show(LogFilePath);
			if (!Directory.Exists(AppDataFolderPath))
				Directory.CreateDirectory(AppDataFolderPath);

			if (!File.Exists(LogFilePath))
				File.Create(LogFilePath);
			
			LogQueue = new Queue<Message>();
			LogWriter = File.AppendText(LogFilePath);

			new Thread(() => StartLogging()).Start();
		}
		
		private static void StartLogging()
		{
			while (_logging)
				if (LogQueue.Count != 0)
						DiskWrite(LogQueue.Peek());
		}

		public static void Log(string LogMessage, Level Level, DateTime Time)
		{
			Message MsgToEnq = new Message
			{
				LogMessage = LogMessage,
				Level = Level,
				Time = Time
			};
			LogQueue.Enqueue(MsgToEnq);
		}
		
		public static void MessageBoxLog(string LogMessage, Level Level, DateTime Time)
		{
			Message MsgToEnq = new Message
			{
				LogMessage = LogMessage,
				Level = Level,
				Time = Time
			};
			LogQueue.Enqueue(MsgToEnq);

			switch (Level)
			{
				case Level.Fatal:
					MessageBox.Show(LogMessage, Time.ToString(),MessageBoxButton.OK,MessageBoxImage.Error);
					break;

				case Level.Error:
					MessageBox.Show(LogMessage, Time.ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
					break;

				case Level.Warning:
					MessageBox.Show(LogMessage, Time.ToString(), MessageBoxButton.OK, MessageBoxImage.Warning);
					break;

				case Level.Debug:
					MessageBox.Show(LogMessage, Time.ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
					break;
			}
		}

		private static void DiskWrite(Message LogMessage)
		{
			string LogToWrite = $"{LogMessage.Time} {LogMessage.Level} {LogMessage.LogMessage}";
			LogWriter.WriteLine(LogToWrite);
			LogWriter.Flush();
			LogQueue.Dequeue();
		}

		public static void KillAll()
		{
			_logging = false;
			LogQueue.Clear();
			LogWriter.Close();
		}
	}
}
