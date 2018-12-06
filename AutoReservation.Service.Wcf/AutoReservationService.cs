using AutoReservation.BusinessLayer;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AutoReservation.Service.Wcf
{
    public class AutoReservationService : IAutoReservationService
    {
        /*
         * TODO: Converting of DTO Objects to DAL Objects and the other way around
         */

        /*
         * TODO: Exception handling => catch known exceptions and pack it into faults
         */

        private static void WriteActualMethod()
            => Console.WriteLine($"Calling: {new StackTrace().GetFrame(1).GetMethod().Name}");

        public void AddCar(AutoDto car)
        {
            WriteActualMethod();
            AutoManager manager = new AutoManager();
            manager.InsertAuto(DtoConverter.ConvertToEntity(car));
        }

        public void AddCustomer(KundeDto customer)
        {
            WriteActualMethod();
            KundeManager manager = new KundeManager();
            manager.Insert(DtoConverter.ConvertToEntity(customer));
        }

        public void AddReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            ReservationManager manager = new ReservationManager();
            manager.Insert(DtoConverter.ConvertToEntity(reservation));
        }

        public bool CarAvaileable()
        {
            WriteActualMethod();
            // TODO: Check if car is availeable
            throw new NotImplementedException();
        }

        public void DeleteCar(AutoDto car)
        {
            WriteActualMethod();
            AutoManager manager = new AutoManager();
            manager.DeleteAuto(DtoConverter.ConvertToEntity(car));
        }

        public void DeleteCustomer(KundeDto customer)
        {
            WriteActualMethod();
            KundeManager manager = new KundeManager();
            manager.DeleteKunde(DtoConverter.ConvertToEntity(customer));
        }

        public void DeleteReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            ReservationManager manager = new ReservationManager();
            manager.Delete(DtoConverter.ConvertToEntity(reservation));
        }

        public List<AutoDto> GetAllCars()
        {
            WriteActualMethod();
            AutoManager manager = new AutoManager();
            return DtoConverter.ConvertToDtos(manager.List);
        }

        public List<KundeDto> GetAllCustomers()
        {
            WriteActualMethod();
            KundeManager manager = new KundeManager();
            return DtoConverter.ConvertToDtos(manager.GetKunden());
        }

        public List<ReservationDto> GetAllReservations()
        {
            WriteActualMethod();
            ReservationManager manager = new ReservationManager();
            return DtoConverter.ConvertToDtos(manager.GetAllReservations());
        }

        public AutoDto GetCar(int id)
        {
            WriteActualMethod();
            AutoManager manager = new AutoManager();
            return DtoConverter.ConvertToDto(manager.GetAuto(id));
        }

        public KundeDto GetCustomer(int id)
        {
            WriteActualMethod();
            KundeManager manager = new KundeManager();
            return DtoConverter.ConvertToDto(manager.GetKunde(id));
        }

        public ReservationDto GetReservation(int id)
        {
            WriteActualMethod();
            ReservationManager manager = new ReservationManager();
            return DtoConverter.ConvertToDto(manager.GetReservation(id));
        }

        public void UpdateCar(AutoDto car)
        {
            WriteActualMethod();
            AutoManager manager = new AutoManager();
            manager.UpdateAuto(DtoConverter.ConvertToEntity(car));
        }

        public void UpdateCustomer(KundeDto customer)
        {
            WriteActualMethod();
            KundeManager manager = new KundeManager();
            manager.Update(DtoConverter.ConvertToEntity(customer));
        }

        public void UpdateReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            ReservationManager manager = new ReservationManager();
            manager.Update(DtoConverter.ConvertToEntity(reservation));
        }
    }
}