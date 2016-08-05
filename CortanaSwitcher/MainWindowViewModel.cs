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
			_isRedstoneOneOrNewer = OsVersion.IsRedstoneOneOrNewer;

			CheckCortana(true);
		}

		private bool _isRedstoneOneOrNewer;

		private bool? _cortanaIsEnabled;
		private bool? _cortanaIsEnabledInitial;

		public string CortanaStatus
		{
			get { return _cortanaStatus; }
			set
			{
				_cortanaStatus = value;
				RaisePropertyChanged();
				RaisePropertyChanged(nameof(CanEnable));
				RaisePropertyChanged(nameof(CanDisable));
			}
		}
		private string _cortanaStatus;

		public bool CanEnable => (_isRedstoneOneOrNewer && (!_cortanaIsEnabled.HasValue || !_cortanaIsEnabled.Value));
		public bool CanDisable => (_isRedstoneOneOrNewer && (!_cortanaIsEnabled.HasValue || _cortanaIsEnabled.Value));

		public bool ShowMessage => (_cortanaIsEnabled != _cortanaIsEnabledInitial);

		private void CheckCortana(bool isInitial = false)
		{
			int data = RegAccessor.GetAllowCortana();
			_cortanaIsEnabled = ConvertFromInt(data);

			if (isInitial)
				_cortanaIsEnabledInitial = _cortanaIsEnabled;

			switch (_cortanaIsEnabled)
			{
				case true: CortanaStatus = Properties.Resources.StatusEnabled; break;
				case false: CortanaStatus = Properties.Resources.StatusDisabled; break;
				default: CortanaStatus = Properties.Resources.StatusUnknown; break;
			}
		}

		public void EnableCortana() => EnableCortanaBase(true);
		public void DisableCortana() => EnableCortanaBase(false);

		private void EnableCortanaBase(bool enable)
		{
			int data = ConvertToInt(enable);
			RegAccessor.SetAllowCortana(data);

			CheckCortana();
			RaisePropertyChanged(nameof(ShowMessage));
		}

		#region Helper

		private static bool? ConvertFromInt(int value)
		{
			switch (value)
			{
				case 1: return true;
				case 0: return false;
				default: return null;
			}
		}

		private static int ConvertToInt(bool? value)
		{
			switch (value)
			{
				case true: return 1;
				case false: return 0;
				default: return -1;
			}
		}

		#endregion
	}
}