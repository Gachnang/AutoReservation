using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoReservation.Wpf.Model
{
    class RepositoryException : Exception {
        public RepositoryException(string msg, Exception inner) : base(msg, inner) {}
    }
}
