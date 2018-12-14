using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoReservation.Common.DataTransferObjects;

namespace AutoReservation.Common.Interfaces
{
    public interface IAuto
    {
		// Tarife in Rappen
        int Id { get; set; }
        string Marke { get; set; }
        int Tagestarif { get; set; }
        int AutoKlasseId { get; set; }
        AutoKlasse AutoKlasse { get; set; }
        int Basistarif { get; set; }
        byte[] RowVersion { get; set; }
    }
}
