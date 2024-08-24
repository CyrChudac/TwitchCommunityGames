namespace CommunityGamesTable {
	internal static class Program {

		const bool BypassVisualization = false;
        public const string logsDir = "logs";
		static string CrashLogDirectory => Path.Combine(logsDir, "crashLogs");
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() {
			// To customize application configuration such as set high DPI settings or default font,
			// see https://aka.ms/applicationconfiguration.
			Properties.Settings settings = Properties.Settings.Default;
			if(!BypassVisualization) {
				ApplicationConfiguration.Initialize();
				try {
					Application.Run(new Form1(settings));
				}catch(Exception e) {
					File.WriteAllText(CrashLogFile(), e.Message + '\n' + e.StackTrace);
					throw;
				}
			} else {
				var cb = new ChatBot(settings, "NA", (x) => true, (x, y) => true, () => { }, () => { });
				cb.Start();
				for(int i = 0; i < 20; i++) {
					Thread.Sleep(1000);
				}
				cb.Dispose();
			}
		}

		static string CrashLogFile() {
			if(!Directory.Exists(CrashLogDirectory)) {
				Directory.CreateDirectory(CrashLogDirectory);
			}
			int i = 1;
			string FilePath() => Path.Combine(CrashLogDirectory, $"CommunityGamesExceptionLog_{i}.txt");
			while(File.Exists(FilePath()))
				i++;
			return FilePath();
		}
	}
}