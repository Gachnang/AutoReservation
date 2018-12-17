using AutoReservation.BusinessLayer;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.DataTransferObjects.Faults;
using AutoReservation.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace AutoReservation.Service.Wcf
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class AutoReservationService : IAutoReservationService
    {
        private bool InvokeDb(Action op) {

            try {
                op();
                return true;
            } catch (ArgumentException) {
                ConverterFault fault = new ConverterFault() {
                    Operation = ActualMethod,
                    ProblemType = "Entry has non existing class type"
                };
                throw new FaultException<ConverterFault>(fault);
            } catch (DbUpdateConcurrencyException) {
                DbFault fault = new DbFault() {
                    Operation = ActualMethod,
                    ProblemType = "Onother user updated the DB reload datas."
                };
                throw new FaultException<DbFault>(fault);
            } catch (DbUpdateException) {
                DbFault fault = new DbFault() {
                    Operation = ActualMethod,
                    ProblemType = "Error while creating new entry."
                };
                throw new FaultException<DbFault>(fault);
            }
        }

        /*
         * TODO: Exception handling => catch known exceptions and pack it into faults
         */

        private static string ActualMethod => new StackTrace().GetFrame(2).GetMethod().Name;

        private static void WriteActualMethod()
        => Console.WriteLine($"Calling: {ActualMethod}");

        public void AddCar(AutoDto car)
        {
            WriteActualMethod();
            InvokeDb(() => new AutoManager().InsertAuto(DtoConverter.ConvertToEntity(car)));
        }

        public void AddCustomer(KundeDto customer)
        {
            WriteActualMethod();
            InvokeDb(() => new KundeManager().InsertKunde(DtoConverter.ConvertToEntity(customer)));
        }

        public void AddReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            InvokeDb(() => new ReservationManager().InsertReservation(DtoConverter.ConvertToEntity(reservation)));
        }

        public bool CarAvailable()
        {
            WriteActualMethod();
            // TODO: Check if car is available
			// Do we even need that? (M)
            throw new NotImplementedException();
        }

        public void DeleteCar(AutoDto car)
        {
            WriteActualMethod();
            InvokeDb(() => new AutoManager().DeleteAuto(DtoConverter.ConvertToEntity(car))); ;
        }

        public void DeleteCustomer(KundeDto customer)
        {
            WriteActualMethod();
            InvokeDb(() => new KundeManager().DeleteKunde(DtoConverter.ConvertToEntity(customer)));
        }

        public void DeleteReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            InvokeDb(() => new ReservationManager().DeleteReservation(DtoConverter.ConvertToEntity(reservation)));
        }

        public List<AutoDto> GetAllCars() {
            WriteActualMethod();
            AutoManager manager = new AutoManager();
            return DtoConverter.ConvertToDtos(manager.ListOfAutos);
        }

        public List<KundeDto> GetAllCustomers() {
            WriteActualMethod();
            KundeManager manager = new KundeManager();
            return DtoConverter.ConvertToDtos(manager.ListOfKunden);
        }

        public List<ReservationDto> GetAllReservations() {
            WriteActualMethod();
            ReservationManager manager = new ReservationManager();
            return DtoConverter.ConvertToDtos(manager.ListOfReservationen);
        }

        public AutoDto GetCar(int id) {
            WriteActualMethod();
            AutoManager manager = new AutoManager();
            return DtoConverter.ConvertToDto(manager.GetAuto(id));
        }

        public KundeDto GetCustomer(int id) {
            WriteActualMethod();
            KundeManager manager = new KundeManager();
            return DtoConverter.ConvertToDto(manager.GetKunde(id));
        }

        public ReservationDto GetReservation(int id) {
            WriteActualMethod();
            ReservationManager manager = new ReservationManager();
            return DtoConverter.ConvertToDto(manager.GetReservation(id));
        }

        public void UpdateCar(AutoDto car)
        {
            WriteActualMethod();
            InvokeDb(() => new AutoManager().UpdateAuto(DtoConverter.ConvertToEntity(car)));
        }

        public void UpdateCustomer(KundeDto customer)
        {
            WriteActualMethod();
            InvokeDb(() => new KundeManager().UpdateKunde(DtoConverter.ConvertToEntity(customer)));
        }

        public void UpdateReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            InvokeDb(() => new ReservationManager().UpdateReservation(DtoConverter.ConvertToEntity(reservation)));
        }
    }
}