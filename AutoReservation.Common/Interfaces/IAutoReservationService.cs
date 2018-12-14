using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.DataTransferObjects.Faults;
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
        [FaultContract(typeof(DbFault))]
        [FaultContract(typeof(ConverterFault))]
        void DeleteCar(AutoDto car);

        [OperationContract]
        [FaultContract(typeof(DbFault))]
        [FaultContract(typeof(ConverterFault))]
        void UpdateCar(AutoDto car);

        [OperationContract]
        [FaultContract(typeof(DbFault))]
        [FaultContract(typeof(ConverterFault))]
        void AddCar(AutoDto car);
        #endregion

        #region CRUD Operations Customer
        [OperationContract]
        List<KundeDto> GetAllCustomers();

        [OperationContract]
        KundeDto GetCustomer(int id);

        [OperationContract]
        [FaultContract(typeof(DbFault))]
        void DeleteCustomer(KundeDto customer);

        [OperationContract]
        [FaultContract(typeof(DbFault))]
        void UpdateCustomer(KundeDto customer);

        [OperationContract]
        [FaultContract(typeof(DbFault))]
        void AddCustomer(KundeDto customer);

        #endregion

        #region CRUD Operations Reservation
        [OperationContract]
        List<ReservationDto> GetAllReservations();

        [OperationContract]
        ReservationDto GetReservation(int id);

        [OperationContract]
        [FaultContract(typeof(DbFault))]
        void DeleteReservation(ReservationDto reservation);

        [OperationContract]
        [FaultContract(typeof(DbFault))]
        void UpdateReservation(ReservationDto reservation);

        [OperationContract]
        [FaultContract(typeof(DbFault))]
        void AddReservation(ReservationDto reservation);

        //Check if Car is available
        [OperationContract]
        bool CarAvailable();
        #endregion
    }
}
