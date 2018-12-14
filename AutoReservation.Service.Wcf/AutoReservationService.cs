using AutoReservation.BusinessLayer;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.DataTransferObjects.Faults;
using AutoReservation.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel;

namespace AutoReservation.Service.Wcf
{
    public class AutoReservationService : IAutoReservationService
    {

        /*
         * TODO: Exception handling => catch known exceptions and pack it into faults
         */

        private static void WriteActualMethod()
            => Console.WriteLine($"Calling: {new StackTrace().GetFrame(1).GetMethod().Name}");

        public void AddCar(AutoDto car)
        {
            WriteActualMethod();
            AutoManager manager = new AutoManager();
            try
            {
                manager.InsertAuto(DtoConverter.ConvertToEntity(car));
            }
            catch(ArgumentException)
            {
                ConverterFault fault = new ConverterFault()
                {
                    Operation = "AddCar",
                    ProblemType = "Car has non existing class type"
                };
                throw new FaultException<ConverterFault>(fault);
            }
            catch (DbUpdateConcurrencyException)
            {
                DbFault fault = new DbFault()
                {
                    Operation = "AddCar",
                    ProblemType = "Onother user updated the DB reload datas."
                };
                throw new FaultException<DbFault>(fault);
            }
            catch (DbUpdateException)
            {
                DbFault fault = new DbFault()
                {
                    Operation = "AddCar",
                    ProblemType = "Error while creating new car entry."
                };
                throw new FaultException<DbFault>(fault);
            }
        }

        public void AddCustomer(KundeDto customer)
        {
            WriteActualMethod();
            KundeManager manager = new KundeManager();
            try
            {
                manager.Insert(DtoConverter.ConvertToEntity(customer));
            }
            catch (DbUpdateConcurrencyException)
            {
                DbFault fault = new DbFault()
                {
                    Operation = "AddCustomer",
                    ProblemType = "Onother user updated the DB reload datas."
                };
                throw new FaultException<DbFault>(fault);
            }
            catch (DbUpdateException)
            {
                DbFault fault = new DbFault()
                {
                    Operation = "AddCustomer",
                    ProblemType = "Error while creating new customer entry."
                };
                throw new FaultException<DbFault>(fault);
            }
        }

        public void AddReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            ReservationManager manager = new ReservationManager();
            try
            {
                manager.InsertReservation(DtoConverter.ConvertToEntity(reservation));
            }
            catch (DbUpdateConcurrencyException)
            {
                DbFault fault = new DbFault()
                {
                    Operation = "AddReservation",
                    ProblemType = "Onother user updated the DB reload datas."
                };
                throw new FaultException<DbFault>(fault);
            }
            catch (DbUpdateException)
            {
                DbFault fault = new DbFault()
                {
                    Operation = "AddReservation",
                    ProblemType = "Error while creating new reservation entry."
                };
                throw new FaultException<DbFault>(fault);
            }
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
            AutoManager manager = new AutoManager();
            try
            {
                manager.DeleteAuto(DtoConverter.ConvertToEntity(car));
            }
            catch (DbUpdateConcurrencyException)
            {
                DbFault fault = new DbFault()
                {
                    Operation = "DeleteCar",
                    ProblemType = "Onother user updated the DB reload datas."
                };
                throw new FaultException<DbFault>(fault);
            }
            catch (DbUpdateException)
            {
                DbFault fault = new DbFault()
                {
                    Operation = "DeleteCar",
                    ProblemType = "Error while deleting car entry."
                };
                throw new FaultException<DbFault>(fault);
            }
        }

        public void DeleteCustomer(KundeDto customer)
        {
            WriteActualMethod();
            KundeManager manager = new KundeManager();
            try
            {
                manager.DeleteKunde(DtoConverter.ConvertToEntity(customer));
            }
            catch (DbUpdateConcurrencyException)
            {
                DbFault fault = new DbFault()
                {
                    Operation = "DeleteCustomer",
                    ProblemType = "Onother user updated the DB reload datas."
                };
                throw new FaultException<DbFault>(fault);
            }
            catch (DbUpdateException)
            {
                DbFault fault = new DbFault()
                {
                    Operation = "DeleteCustomer",
                    ProblemType = "Error while deleting customer entry."
                };
                throw new FaultException<DbFault>(fault);
            }
        }

        public void DeleteReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            ReservationManager manager = new ReservationManager();
            try
            {
                manager.DeleteReservation(DtoConverter.ConvertToEntity(reservation));
            }
            catch (DbUpdateConcurrencyException)
            {
                DbFault fault = new DbFault()
                {
                    Operation = "DeleteReservation",
                    ProblemType = "Onother user updated the DB reload datas."
                };
                throw new FaultException<DbFault>(fault);
            }
            catch (DbUpdateException)
            {
                DbFault fault = new DbFault()
                {
                    Operation = "DeleteReservation",
                    ProblemType = "Error while deleting reservation entry."
                };
                throw new FaultException<DbFault>(fault);
            }
            
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
            return DtoConverter.ConvertToDtos(manager.List);
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
            try
            {
                manager.UpdateAuto(DtoConverter.ConvertToEntity(car));
            }
            catch (DbUpdateConcurrencyException)
            {
                DbFault fault = new DbFault()
                {
                    Operation = "UpdateCar",
                    ProblemType = "Onother user updated the DB reload datas."
                };
                throw new FaultException<DbFault>(fault);
            }
            catch (DbUpdateException)
            {
                DbFault fault = new DbFault()
                {
                    Operation = "UpdateCar",
                    ProblemType = "Error while updating car entry."
                };
                throw new FaultException<DbFault>(fault);
            }
        }

        public void UpdateCustomer(KundeDto customer)
        {
            WriteActualMethod();
            KundeManager manager = new KundeManager();
            try
            {
                manager.Update(DtoConverter.ConvertToEntity(customer));
            }
            catch (DbUpdateConcurrencyException)
            {
                DbFault fault = new DbFault()
                {
                    Operation = "UpdateCustomer",
                    ProblemType = "Onother user updated the DB reload datas."
                };
                throw new FaultException<DbFault>(fault);
            }
            catch (DbUpdateException)
            {
                DbFault fault = new DbFault()
                {
                    Operation = "UpdteCustomer",
                    ProblemType = "Error while updating customer entry."
                };
                throw new FaultException<DbFault>(fault);
            }
        }

        public void UpdateReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            ReservationManager manager = new ReservationManager();
            try
            {
                manager.UpdateReservation(DtoConverter.ConvertToEntity(reservation));
            }
            catch (DbUpdateConcurrencyException)
            {
                DbFault fault = new DbFault()
                {
                    Operation = "UpdateReservation",
                    ProblemType = "Onother user updated the DB reload datas."
                };
                throw new FaultException<DbFault>(fault);
            }
            catch (DbUpdateException)
            {
                DbFault fault = new DbFault()
                {
                    Operation = "UpdateReservation",
                    ProblemType = "Error while updating reservation entry."
                };
                throw new FaultException<DbFault>(fault);
            }
        }
    }
}