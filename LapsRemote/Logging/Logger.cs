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
	public static class Logger
	{
		static Queue<Message> LogQueue;
		public static StreamWriter MainWriter;
		private static volatile bool _logging;
		
		public static void Initialize()
		{
			string AppDataFolderPath = Settings.settingsModel.AppLicationLogPath;
			string LogFilePath = Path.Combine(AppDataFolderPath, "LapsRemoteV2.log");
			_logging = true;

			if (!Directory.Exists(AppDataFolderPath))
				Directory.CreateDirectory(AppDataFolderPath);

			if (!File.Exists(LogFilePath))
				File.Create(LogFilePath);
			
			LogQueue = new Queue<Message>();
			MainWriter = File.AppendText(LogFilePath);

			new Thread(() => StartLogging()).Start();
		}
		
		private static void StartLogging()
		{
			while (_logging)
				if (LogQueue.Count != 0)
						DiskWrite(LogQueue.Peek());
		}

		public static void Log(string LogMessage, LogFrom LoggingFrom, Level LogLevel, DateTime LogTime)
		{
			Message MsgToEnq = new Message
			{
				LogMessage = LogMessage,
				LoggingFrom = LoggingFrom,
				LogLevel = LogLevel,
				LogTime = LogTime
			};
			LogQueue.Enqueue(MsgToEnq);
		}
		
		public static void MessageBoxLog(string LogMessage, LogFrom LoggingFrom, Level LogLevel, DateTime LogTime)
		{
			Message MsgToEnq = new Message
			{
				LogMessage = LogMessage,
				LoggingFrom = LoggingFrom,
				LogLevel = LogLevel,
				LogTime = LogTime
			};
			LogQueue.Enqueue(MsgToEnq);

			switch (LogLevel)
			{
				case Level.Fatal:
					MessageBox.Show(LogMessage, LogTime.ToString(), MessageBoxButton.OK,MessageBoxImage.Error);
					break;

				case Level.Error:
					MessageBox.Show(LogMessage, LogTime.ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
					break;

				case Level.Warning:
					MessageBox.Show(LogMessage, LogTime.ToString(), MessageBoxButton.OK, MessageBoxImage.Warning);
					break;

				case Level.Debug:
					MessageBox.Show(LogMessage, LogTime.ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
					break;
			}
		}

		private static void DiskWrite(Message LogMessage)
		{
			string LogToWrite = $"{LogMessage.LogTime} [{LogMessage.LoggingFrom}] {LogMessage.LogLevel} {LogMessage.LogMessage}";
			MainWriter.WriteLine(LogToWrite);
			MainWriter.Flush();
			LogQueue.Dequeue();
		}

		public static void KillAll()
		{
			while(LogQueue.Count != 0)
			{
				_logging = false;
				LogQueue.Clear();
				MainWriter.Close();
			}
		}
	}
}
