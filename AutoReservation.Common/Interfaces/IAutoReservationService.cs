using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.DataTransferObjects.Faults;
using System.Collections.Generic;
using System.ServiceModel;

namespace AutoReservation.Common.Interfaces
{
    [ServiceContract(
        SessionMode = SessionMode.Allowed)]
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
        bool DeleteCar(AutoDto car);

        [OperationContract]
        [FaultContract(typeof(DbFault))]
        [FaultContract(typeof(ConverterFault))]
        bool UpdateCar(AutoDto car);

        [OperationContract]
        [FaultContract(typeof(DbFault))]
        [FaultContract(typeof(ConverterFault))]
        int AddCar(AutoDto car);
        #endregion

        #region CRUD Operations Customer
        [OperationContract]
        List<KundeDto> GetAllCustomers();

        [OperationContract]
        KundeDto GetCustomer(int id);

        [OperationContract]
        [FaultContract(typeof(DbFault))]
        bool DeleteCustomer(KundeDto customer);

        [OperationContract]
        [FaultContract(typeof(DbFault))]
        bool UpdateCustomer(KundeDto customer);

        [OperationContract]
        [FaultContract(typeof(DbFault))]
        int AddCustomer(KundeDto customer);

        #endregion

        #region CRUD Operations Reservation
        [OperationContract]
        List<ReservationDto> GetAllReservations();

        [OperationContract]
        ReservationDto GetReservation(int id);

        [OperationContract]
        [FaultContract(typeof(DbFault))]
        bool DeleteReservation(ReservationDto reservation);

        [OperationContract]
        [FaultContract(typeof(DbFault))]
        bool UpdateReservation(ReservationDto reservation);

        [OperationContract]
        [FaultContract(typeof(DbFault))]
        int AddReservation(ReservationDto reservation);

        //Check if Car is available
        [OperationContract]
        bool CarAvailable();
        #endregion
    }
}
