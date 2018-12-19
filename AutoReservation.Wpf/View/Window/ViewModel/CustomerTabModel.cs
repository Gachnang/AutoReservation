using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Extensions;
using AutoReservation.Wpf.Model;

namespace AutoReservation.Wpf.View.Window.ViewModel
{
	public class CustomerTabModel : INotifyPropertyChanged
	{
		private readonly MainViewModel _mainViewModel;
		private AutoReservationRepository Repository => _mainViewModel.Repository;

		private ChangeTracker<KundeDto> _selectedKunde;

		public CustomerTabModel()
		{
			if ((bool)DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue)
			{
				_mainViewModel = new MainViewModel();
			}
			else
			{
				throw new ArgumentException(
					"MainViewModel is needed to inject repository.\n" +
					"(It is only allowed to be null to show data in designTime)");
			}
		}

		public CustomerTabModel(MainViewModel mainViewModel)
		{
			_mainViewModel = mainViewModel;
		    SelectedKunde = Repository.Kunden.FirstOrDefault();
        }

		public ObservableCollection<ChangeTracker<KundeDto>> Kunden => _mainViewModel.Repository?.Kunden;

		public ChangeTracker<KundeDto> SelectedKunde
		{
			get => _selectedKunde;
			set
			{
				_selectedKunde = value;
				OnPropertyChanged();
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;


		public void Save()
		{
			//Repository.UpdateCustomer(SelectedKunde);
			try
			{
				Repository.SaveCustomerChanges();
			}
			catch (Exception e)
			{
				StringBuilder sb = new StringBuilder("Message:");
				do
				{
					sb.Append(Environment.NewLine + e.Message);
					e = e.InnerException;
				} while (e != null);

				MessageBox.Show(sb.ToString(), "Fehler beim Speichern");
			}
		}

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}