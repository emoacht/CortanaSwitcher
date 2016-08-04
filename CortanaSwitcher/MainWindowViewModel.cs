using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CortanaSwitcher
{
	public class MainWindowViewModel : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged

		public event PropertyChangedEventHandler PropertyChanged;

		private void RaisePropertyChanged([CallerMemberName] string propertyName = null) =>
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

		#endregion

		public MainWindowViewModel()
		{
			_isOsRedStoneOneOrNewer = OsVersion.IsRedstoneOneOrNewer;

			CheckCortana();
		}

		private bool _isOsRedStoneOneOrNewer;
		private bool? _cortanaIsEnabled;

		public string CortanaState
		{
			get { return _cortanaState; }
			set
			{
				_cortanaState = value;
				RaisePropertyChanged();
				RaisePropertyChanged(nameof(CanEnable));
				RaisePropertyChanged(nameof(CanDisable));
			}
		}
		private string _cortanaState;

		public bool CanEnable => (_isOsRedStoneOneOrNewer && (!_cortanaIsEnabled.HasValue || !_cortanaIsEnabled.Value));
		public bool CanDisable => (_isOsRedStoneOneOrNewer && (!_cortanaIsEnabled.HasValue || _cortanaIsEnabled.Value));

		private void CheckCortana()
		{
			var valueData = RegManager.GetAllowCortana();
			_cortanaIsEnabled = ConvertToNullableBoolean(valueData);

			switch (_cortanaIsEnabled)
			{
				case true: CortanaState = "Cortana Enabled"; break;
				case false: CortanaState = "Cortana Disabled"; break;
				default: CortanaState = "Unknown"; break;
			}
		}

		public void EnableCortana()
		{
			RegManager.SetAllowCortana(1);

			CheckCortana();
		}

		public void DisableCortana()
		{
			RegManager.SetAllowCortana(0);

			CheckCortana();
		}

		private static int ConvertFromNullableBoolean(bool? value)
		{
			switch (value)
			{
				case true: return 1;
				case false: return 0;
				default: return -1;
			}
		}

		private static bool? ConvertToNullableBoolean(int value)
		{
			switch (value)
			{
				case 1: return true;
				case 0: return false;
				default: return null;
			}
		}
	}
}