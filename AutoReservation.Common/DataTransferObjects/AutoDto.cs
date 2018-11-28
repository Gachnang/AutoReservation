using System;
using System.Runtime.Serialization;
using AutoReservation.Common.Interfaces;

namespace AutoReservation.Common.DataTransferObjects
{
    [DataContract]
    public class AutoDto : AbstractDto, IAuto
    {
        private int _id;
        [DataMember]
        public int Id { get => _id; set => SetProperty(ref _id, value); }

        private string _marke;
        [DataMember]
        public string Marke { get => _marke; set => SetProperty(ref _marke, value); }

        private int _tagestarif;
        [DataMember]
        public int Tagestarif { get => _tagestarif; set => SetProperty(ref _tagestarif, value); }

        private int _autoKlasseId;
        [DataMember]
        public int AutoKlasseId { get => _autoKlasseId; set => SetProperty(ref _autoKlasseId, value); }

        public AutoKlasse AutoKlasse
        {
            get => AutoKlasseId.ToAutoKlasse();
            set => AutoKlasseId = value.ToInt();
        }

        private int _basisearif;
        [DataMember]
        public int Basistarif { get => _basisearif; set => SetProperty(ref _basisearif, value); }

        private byte[] _rowVersion;
        [DataMember]
        public byte[] RowVersion { get => _rowVersion; set => SetProperty(ref _rowVersion, value); }

        //public override string ToString()
        //    => $"{Id}; {Marke}; {Tagestarif}; {Basistarif}; {AutoKlasse}; {RowVersion}";
    }
}
