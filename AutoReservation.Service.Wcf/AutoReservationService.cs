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
        private T InvokeDb<T>(Func<T> op) {

            try {
                return op();
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

        public int AddCar(AutoDto car)
        {
            WriteActualMethod();
            return InvokeDb(() => new AutoManager().InsertAuto(DtoConverter.ConvertToEntity(car)));
        }

        public int AddCustomer(KundeDto customer)
        {
            WriteActualMethod();
            return InvokeDb(() => new KundeManager().InsertKunde(DtoConverter.ConvertToEntity(customer)));
        }

        public int AddReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            return InvokeDb(() => new ReservationManager().InsertReservation(DtoConverter.ConvertToEntity(reservation)));
        }

        public bool CarAvailable()
        {
            WriteActualMethod();
            // TODO: Check if car is available
			// Do we even need that? (M)
            throw new NotImplementedException();
        }

        public bool DeleteCar(AutoDto car)
        {
            WriteActualMethod();
            return InvokeDb(() => new AutoManager().DeleteAuto(DtoConverter.ConvertToEntity(car))); ;
        }

        public bool DeleteCustomer(KundeDto customer)
        {
            WriteActualMethod();
            return InvokeDb(() => new KundeManager().DeleteKunde(DtoConverter.ConvertToEntity(customer)));
        }

        public bool DeleteReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            return InvokeDb(() => new ReservationManager().DeleteReservation(DtoConverter.ConvertToEntity(reservation)));
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

        public bool UpdateCar(AutoDto car)
        {
            WriteActualMethod();
            return InvokeDb(() => new AutoManager().UpdateAuto(DtoConverter.ConvertToEntity(car)));
        }

        public bool UpdateCustomer(KundeDto customer)
        {
            WriteActualMethod();
            return InvokeDb(() => new KundeManager().UpdateKunde(DtoConverter.ConvertToEntity(customer)));
        }

        public bool UpdateReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            return InvokeDb(() => new ReservationManager().UpdateReservation(DtoConverter.ConvertToEntity(reservation)));
        }
    }
}