using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CortanaSwitcher
{
	internal static class OsVersion
	{
		/// <summary>
		/// Whether OS is Windows 10 Redstone 1 or newer
		/// </summary>
		/// <remarks>Redstone 1 = Version 1607 = Build 14393</remarks>
		public static bool IsRedstoneOneOrNewer => (new Version("10.0.14393") <= Environment.OSVersion.Version);
	}
}