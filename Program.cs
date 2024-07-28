namespace CommunityGamesTable {
	internal static class Program {

		const bool BypassVisualization = false;
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
					int i = 1;
					while(File.Exists($"CommunityGamesExceptionLog_{i}.txt"))
						i++;
					File.WriteAllText($"CommunityGamesExceptionLog_{i}.txt", e.Message + '\n' + e.StackTrace);
					throw;
				}
			} else {
				var cb = new ChatBot(settings, "NA", (x) => true, (x, y) => true);
				cb.Start();
				for(int i = 0; i < 20; i++) {
					Thread.Sleep(1000);
				}
				cb.Dispose();
			}
		}
	}
}