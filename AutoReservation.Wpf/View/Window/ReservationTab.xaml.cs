using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Wpf.Model;
using AutoReservation.Wpf.View.Window.ViewModel;

namespace AutoReservation.Wpf.View.Window {
    /// <summary>
    ///     Interaction logic for ReservationTab.xaml
    /// </summary>
    public partial class ReservationTab : UserControl {
        public ReservationTab() {
            InitializeComponent();
        }

        public ReservationTabModel Model => DataContext as ReservationTabModel;

        private void BtnSave_OnClick(object sender, RoutedEventArgs e) {
            Model.Save();
        }

        private void Wtb_OnPreviewTextInput(object sender, TextCompositionEventArgs e) {
            e.Handled = !int.TryParse(((TextBox) sender).Text + e.Text, out int i);
        }

        private void BtnAdd_OnClick(object sender, RoutedEventArgs e) {
            ChangeTracker<ReservationDto> reservation = new ChangeTracker<ReservationDto>(new ReservationDto() {
                ReservationsNr = 0,
                Auto = null,
                Kunde = null,
                AutoId = 0,
                KundeId = 0,
                Von = DateTime.Now,
                Bis = DateTime.Now.AddHours(24)
            })
            { 
                IsDirty = true,
                IsNew = true
            };

            Model.Reservationen.Add(reservation);
            Model.SelectedReservation = reservation;
        }

        private void BtnDel_OnClick(object sender, RoutedEventArgs e) {
            Model.SelectedReservation.IsDeleted = !Model.SelectedReservation.IsDeleted;
        }
    }
}