using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AutoReservation.Common.DataTransferObjects.Faults
{
    [DataContract]
    public class DbFault
    {
        [DataMember]
        public string Operation { get; set; }

        [DataMember]
        public string ProblemType { get; set; }
    }
}
