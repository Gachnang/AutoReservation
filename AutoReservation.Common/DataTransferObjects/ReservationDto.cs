﻿using System;
using System.Diagnostics;
using AutoReservation.Common.Interfaces;

namespace AutoReservation.Common.DataTransferObjects
{
    public class ReservationDto : AbstractDto
    {
        private int _reservationNr;
        private int _autoId;
        private AutoDto _auto;
        private int _kundeId;
        private KundeDto _kunde;
        private DateTime _von;
        private DateTime _bis;
        private byte[] _rowVersion;

        //public override string ToString()
        //    => $"{ReservationsNr}; {Von}; {Bis}; {Auto}; {Kunde}";
        
        public int ReservationsNr {
            get => _reservationNr;
            set => SetProperty(ref _reservationNr, value);
        }

        public int AutoId {
            get => _autoId;
            set => SetProperty(ref _autoId, value);
        }

        public AutoDto Auto {
            get => _auto;
            set {
                SetProperty(ref _auto, value);
            }
        }

        public int KundeId {
            get => _kundeId;
            set => SetProperty(ref _kundeId, value);
        }

        public KundeDto Kunde {
            get => _kunde;
            set {
                SetProperty(ref _kunde, value);
            }
        }

        public DateTime Von {
            get => _von;
            set => SetProperty(ref _von, value);
        }

        public DateTime Bis {
            get => _bis;
            set => SetProperty(ref _bis, value);
        }

        public byte[] RowVersion {
            get => _rowVersion;
            set => SetProperty(ref _rowVersion, value);
        }

		public bool CompareTo(ReservationDto other){
			return this.Auto.CompareTo(other.Auto) && this.Kunde.CompareTo(other.Kunde);
		}
    }
}
