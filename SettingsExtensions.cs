using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CommunityGamesTable {
	internal static class SettingsExtensions {
		public static IReadOnlyList<string> GetRegions(this Properties.Settings settings) {
			return settings.regions.Split(';');
		}
	}
}
