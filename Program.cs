namespace CommunityGamesTable {
	internal static class Program {
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() {
			// To customize application configuration such as set high DPI settings or default font,
			// see https://aka.ms/applicationconfiguration.
			try {
				ApplicationConfiguration.Initialize();
				Properties.Settings settings = Properties.Settings.Default;
				Application.Run(new Form1(settings));
			}catch(Exception e) {
				int i = 1;
				while(File.Exists($"CommunityGamesExceptionLog_{i}.txt"))
					i++;
				File.WriteAllText($"CommunityGamesExceptionLog_{i}.txt", e.Message + '\n' + e.StackTrace);
				throw;
			}
		}
	}
}