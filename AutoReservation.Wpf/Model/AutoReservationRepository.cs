using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Extensions;
using AutoReservation.Common.Interfaces;
using AutoReservation.Wpf.Logic;

namespace AutoReservation.Wpf.Model {
    public class AutoReservationRepository : INotifyPropertyChanged {
        private static string DefaultServerUrl = "http://localhost:8080";

        private readonly ObservableRangeCollection<IAuto> _autos;

        public AutoReservationRepository(string serverUrl = null) {
            if (string.IsNullOrEmpty(serverUrl)) {
                serverUrl = DefaultServerUrl;
            }

            // TODO: Real connection
            _autos = new ObservableRangeCollection<IAuto>(new List<IAuto> {
                new AutoDto {
                    Id = 0,
                    AutoKlasse = AutoKlasse.Standard,
                    Basistarif = 1,
                    Marke = "VW",
                    Tagestarif = 2
                },
                new AutoDto {
                    Id = 1,
                    AutoKlasse = AutoKlasse.Mittelklasse,
                    Basistarif = 2,
                    Marke = "Mercedes",
                    Tagestarif = 3
                },
                new AutoDto {
                    Id = 2,
                    AutoKlasse = AutoKlasse.Luxusklasse,
                    Basistarif = 3,
                    Marke = "Smart",
                    Tagestarif = 4
                }
            });
        }

        public ObservableCollection<IAuto> Autos => _autos;


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}