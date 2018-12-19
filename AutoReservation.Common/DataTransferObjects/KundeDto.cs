using System;
using AutoReservation.Common.Interfaces;

namespace AutoReservation.Common.DataTransferObjects
{
    // TODO: Double check if implemented right
	// Why lambda setter/getter? (M)
    public class KundeDto : AbstractDto
    {
        private int _id;
        private string _nachname;
        private string _vorname;
        private DateTime _geburtsdatum;
        private byte[] _rowVersion;

        public int Id {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public string Nachname {
            get => _nachname;
            set => SetProperty(ref _nachname, value);
        }

        public string Vorname {
            get => _vorname;
            set => SetProperty(ref _vorname, value);
        }

        public DateTime Geburtsdatum {
            get => _geburtsdatum;
            set => SetProperty(ref _geburtsdatum, value);
        }

        public byte[] RowVersion {
            get => _rowVersion;
            set => SetProperty(ref _rowVersion, value);
        }

        //public override string ToString()
        //    => $"{Id}; {Nachname}; {Vorname}; {Geburtsdatum}; {RowVersion}";

		public bool CompareTo(KundeDto other){
			return this.Geburtsdatum.Equals(other.Geburtsdatum)
				&& this.Id.Equals(other.Id)
				&& this.Nachname.Equals(other.Nachname)
				&& this.Vorname.Equals(other.Vorname);
		}
    }
}
