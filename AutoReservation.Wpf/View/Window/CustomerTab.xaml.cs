using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Wpf.Model;
using AutoReservation.Wpf.View.Window.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AutoReservation.Wpf.View.Window
{
	/// <summary>
	///     Interaction logic for CustomerTab.xaml
	/// </summary>
	public partial class CustomerTab : UserControl
	{
		public CustomerTab()
		{
			InitializeComponent();
		}

		public CustomerTabModel Model => DataContext as CustomerTabModel;

		private void BtnSave_OnClick(object sender, RoutedEventArgs e)
		{
			Model.Save();
		}

		private void Wtb_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			e.Handled = !int.TryParse(((TextBox)sender).Text + e.Text, out int i);
		}

		private void BtnAdd_OnClick(object sender, RoutedEventArgs e)
		{
			ChangeTracker<KundeDto> kunde = new ChangeTracker<KundeDto>(new KundeDto()
			{
				Id = -1,
				Nachname = "Bob"
			})
			{
				IsDirty = true,
				IsNew = true
			};

			Model.Kunden.Add(kunde);
			Model.SelectedKunde = kunde;
		}

		private void BtnDel_OnClick(object sender, RoutedEventArgs e)
		{
			Model.SelectedKunde.IsDeleted = !Model.SelectedKunde.IsDeleted;
		}
	}
}
