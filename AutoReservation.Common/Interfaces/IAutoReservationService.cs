using AutoReservation.Common.DataTransferObjects;
using System.Collections.Generic;

namespace AutoReservation.Common.Interfaces
{
    public interface IAutoReservationService
    {
        //CRUD Operations Car
        List<AutoDto> GetAllCars();

        AutoDto GetCar(int id);

        void DeleteCar(AutoDto car);

        void UpdateCar(AutoDto car);

        void AddCar(AutoDto car);

        // CRUD Operations Customer
        List<KundeDto> GetAllCustomers();

        KundeDto GetCustomer(int id);

        void DeleteCustomer(KundeDto customer);

        void UpdateCustomer(KundeDto customer);

        void AddCustomer(KundeDto customer);

        // CRUD Operations Reservation
        List<ReservationDto> GetAllReservations();

        ReservationDto GetReservation(int id);

        void DeleteReservation(ReservationDto reservation);

        void UpdateReservation(ReservationDto reservation);

        void AddReservation(ReservationDto reservation);

        //Check if Car is available
        bool CarAvailable();
    }
}
