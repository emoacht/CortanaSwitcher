using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace CortanaSwitcher
{
	internal static class RegManager
	{
		private const string KeyName = @"SOFTWARE\Policies\Microsoft\Windows\Windows Search";
		private const string ValueName = "AllowCortana";

		/// <summary>
		/// Get AllowCortana value data.
		/// </summary>
		/// <returns>
		/// 1: Enabled
		/// 0: Disabled
		/// -1: No value
		/// </returns>
		public static int GetAllowCortana()
		{
			using (var key = Registry.LocalMachine.OpenSubKey(KeyName))
			{
				return (int)(key?.GetValue(ValueName) ?? -1);
			}
		}

		/// <summary>
		/// Set AllowCortana value data.
		/// </summary>
		/// <param name="valueData">Value data to be set</param>
		/// <remarks>This function requires Administrator privileges.</remarks>
		public static void SetAllowCortana(int valueData)
		{
			using (var key = Registry.LocalMachine.CreateSubKey(KeyName, true))
			{
				key.SetValue(ValueName, valueData);
			}
		}
	}
}