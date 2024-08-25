using CommunityGamesTable.Properties;

namespace CommunityGamesTable {
	internal static class Program {

		const bool BypassVisualization = false;
        public const string logsDir = "logs";
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
				CrashLogger.RunCrashableAction(() => Application.Run(new Form1(settings)));
			} else {
				var cb = new ChatBot(settings, settings.GetRegions()[0], (x) => true, (x, y) => true, () => { }, () => { });
				if(cb.Start()) {
					for(int i = 0; i < 20; i++) {
						Thread.Sleep(1000);
					}
					cb.Dispose();
				}
			}
		}

	}
}