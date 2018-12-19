using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Extensions;
using AutoReservation.Wpf.Model;

namespace AutoReservation.Wpf.View.Window.ViewModel {
    public class ReservationTabModel : INotifyPropertyChanged {
        private readonly MainViewModel _mainViewModel;
        private AutoReservationRepository Repository => _mainViewModel.Repository;

        private ChangeTracker<ReservationDto> _selectedReservation;
        private ChangeTracker<AutoDto> _selectedAuto;
        private ChangeTracker<KundeDto> _selectedKunde;

        public ReservationTabModel() {
            if ((bool) DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue) {
                _mainViewModel = new MainViewModel();
            } else {
                throw new ArgumentException(
                    "MainViewModel is needed to inject repository.\n" +
                    "(It is only allowed to be null to show data in designTime)");
            }
        }

        public ReservationTabModel(MainViewModel mainViewModel) {
            _mainViewModel = mainViewModel;
            SelectedReservation = Reservationen.FirstOrDefault();
            SelectedAuto = AvailableAutos.FirstOrDefault();
            SelectedKunde = Kunden.FirstOrDefault();
        }

        public ObservableCollection<ChangeTracker<ReservationDto>> Reservationen => _mainViewModel.Repository?.Reservationen;
        public ObservableCollection<ChangeTracker<AutoDto>> AvailableAutos => _mainViewModel.Repository?.Autos;
        public ObservableCollection<ChangeTracker<KundeDto>> Kunden => _mainViewModel.Repository?.Kunden;

        public ChangeTracker<ReservationDto> SelectedReservation {
            get => _selectedReservation;
            set {
                _selectedReservation = value;
                OnPropertyChanged();
            }
        }

        public ChangeTracker<AutoDto> SelectedAuto
        {
            get => _selectedAuto;
            set
            {
                _selectedAuto = value;
                OnPropertyChanged();
            }
        }

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

        public void Save() {
            //Repository.UpdateReservation(SelectedReservation);
            try {
                SelectedReservation.Current.Auto = SelectedAuto.Current;
                SelectedReservation.Current.Kunde = SelectedKunde.Current;
                Repository.SaveReservationChanges();
            } catch (Exception e) {
                StringBuilder sb = new StringBuilder("Message:");
                do {
                    sb.Append(Environment.NewLine + e.Message);
                    e = e.InnerException;
                } while(e != null);

                MessageBox.Show(sb.ToString(), "Fehler beim Speichern");
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}