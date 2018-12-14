using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.DataTransferObjects.Faults;
using System.Collections.Generic;
using System.ServiceModel;

namespace AutoReservation.Common.Interfaces
{
    [ServiceContract]
    public interface IAutoReservationServiceCallback
    {
        #region CUD Operations Car
        [OperationContract(IsOneWay = true)]
        void DeleteCar(AutoDto car);

        [OperationContract(IsOneWay = true)]
        void UpdateCar(AutoDto car);

        [OperationContract(IsOneWay = true)]
        void AddCar(AutoDto car);
        #endregion

        #region CUD Operations Customer
        [OperationContract(IsOneWay = true)]
        void DeleteCustomer(KundeDto customer);

        [OperationContract(IsOneWay = true)]
        void UpdateCustomer(KundeDto customer);

        [OperationContract(IsOneWay = true)]
        void AddCustomer(KundeDto customer);

        #endregion

        #region CUD Operations Reservation
        [OperationContract(IsOneWay = true)]
        void DeleteReservation(ReservationDto reservation);

        [OperationContract(IsOneWay = true)]
        void UpdateReservation(ReservationDto reservation);

        [OperationContract(IsOneWay = true)]
        void AddReservation(ReservationDto reservation);
        #endregion
    }
}
