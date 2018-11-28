using System;
using System.Diagnostics;
using AutoReservation.Common.Interfaces;

namespace AutoReservation.Common.DataTransferObjects
{
    public class ReservationDto : AbstractDto, IReservation
    {
        private int _reservationNr;
        private int _autoId;
        private IAuto _auto;
        private int _kundeId;
        private IKunde _kunde;
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

        public IAuto Auto {
            get => _auto;
            set {
                Debug.Assert(value is AutoDto);
                SetProperty(ref _auto, value);
            }
        }

        public int KundeId {
            get => _kundeId;
            set => SetProperty(ref _kundeId, value);
        }

        public IKunde Kunde {
            get => _kunde;
            set {
                Debug.Assert(value is KundeDto);
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
    }
}
