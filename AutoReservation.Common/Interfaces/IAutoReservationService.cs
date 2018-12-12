using AutoReservation.Common.DataTransferObjects;
using System.Collections.Generic;
using System.ServiceModel;

namespace AutoReservation.Common.Interfaces
{
    [ServiceContract]
    public interface IAutoReservationService
    {
        #region CRUD Operations Car

        [OperationContract]
        List<AutoDto> GetAllCars();

        [OperationContract]
        AutoDto GetCar(int id);

        [OperationContract]
        void DeleteCar(AutoDto car);

        [OperationContract]
        void UpdateCar(AutoDto car);

        [OperationContract]
        void AddCar(AutoDto car);
        #endregion

        #region CRUD Operations Customer
        [OperationContract]
        List<KundeDto> GetAllCustomers();

        [OperationContract]
        KundeDto GetCustomer(int id);

        [OperationContract]
        void DeleteCustomer(KundeDto customer);

        [OperationContract]
        void UpdateCustomer(KundeDto customer);

        [OperationContract]
        void AddCustomer(KundeDto customer);

        #endregion

        #region CRUD Operations Reservation
        [OperationContract]
        List<ReservationDto> GetAllReservations();

        [OperationContract]
        ReservationDto GetReservation(int id);

        [OperationContract]
        void DeleteReservation(ReservationDto reservation);

        [OperationContract]
        void UpdateReservation(ReservationDto reservation);

        [OperationContract]
        void AddReservation(ReservationDto reservation);

        //Check if Car is available
        [OperationContract]
        bool CarAvailable();
        #endregion
    }
}
