using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityGamesTable {
	internal class CrashLogger {
		static string CrashLogDirectory => Path.Combine(Program.logsDir, "crashes");
		
		public static void RunCrashableAction(Action a, bool rethrow = true) {
			try {
				a.Invoke();
			}catch(Exception e) {
				File.WriteAllText(CrashLogFile(), e.Message + '\n' + e.StackTrace);
				if(rethrow)
					throw;
			}
		}

		public static T RunCrashableAction<T>(Func<T> f) {
			try {
				return f.Invoke();
			}catch(Exception e) {
				File.WriteAllText(CrashLogFile(), e.Message + '\n' + e.StackTrace);
				throw;
			}
		}

		static string CrashLogFile() {
			if(!Directory.Exists(CrashLogDirectory)) {
				Directory.CreateDirectory(CrashLogDirectory);
			}
			int i = 1;
			var date = DateTime.Now;
			string dateStr = $"{date.Year % 1000}_{date.Month}_{date.Day}_{date.Hour}_{date.Minute}";
			string FilePath() => Path.Combine(CrashLogDirectory, $"{dateStr}_{i}.txt");
			string path;
			while(File.Exists(path = FilePath()))
				i++;
			return path;
		}
	}
}
