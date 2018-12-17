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
        private IAutoReservationServiceCallback currentClient =>
            OperationContext.Current.GetCallbackChannel<IAutoReservationServiceCallback>();

        private List<IAutoReservationServiceCallback> clients = new List<IAutoReservationServiceCallback>();

        private List<IAutoReservationServiceCallback> EnsureClient() {
            IAutoReservationServiceCallback client = currentClient;
            
            if (!clients.Contains(client)) {
                clients.Add(client);
            }
            
            return clients;
        }

        /// <summary>
        ///  https://stackoverflow.com/questions/2332531/prevent-exceptions-from-wcf-callbacks
        /// </summary>
        /// <param name="contract"></param>
        /// <param name="op"></param>
        private void InvokeWcf(IAutoReservationServiceCallback contract, Action<IAutoReservationServiceCallback> op)
        {
            if (((ICommunicationObject)contract).State != CommunicationState.Opened)
            {
                lock (clients)
                    clients.Remove(contract);
                //myLogger.LogError("That contract isn't open! Disconnected.");
                return;
            }

            try {
                op(contract);
            } catch (TimeoutException ex) {
                lock (clients)
                    clients.Remove(contract);
                //myLogger.LogError("That contract timed out! Disconnected.", ex);
                return;
            } catch (CommunicationException ex)
            {
                lock (clients)
                    clients.Remove(contract);
            } catch (ObjectDisposedException ex)
            {
                lock (clients)
                    clients.Remove(contract);
            } catch (Exception ex)
            {
                // Unexpected case.
                lock (clients)
                    clients.Remove(contract);
                // myLogger.FatalError("Something really bad happened!.", ex);
                throw;
            }
        }

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

            lock (clients) {
                EnsureClient().ForEach(client => this.InvokeWcf(client, c => c.AddCar(car)));
            }
        }

        public void AddCustomer(KundeDto customer)
        {
            WriteActualMethod();
            InvokeDb(() => new KundeManager().InsertKunde(DtoConverter.ConvertToEntity(customer)));

            lock (clients)
            {
                EnsureClient().Where(client => !client.Equals(currentClient)).ToList().ForEach(client => client.AddCustomer(customer));
            }
        }

        public void AddReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            InvokeDb(() => new ReservationManager().InsertReservation(DtoConverter.ConvertToEntity(reservation)));

            lock (clients)
            {
                EnsureClient().Where(client => !client.Equals(currentClient)).ToList().ForEach(client => client.AddReservation(reservation));
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
            InvokeDb(() => new AutoManager().DeleteAuto(DtoConverter.ConvertToEntity(car))); ;

            lock (clients) {
                EnsureClient().Where(client => !client.Equals(currentClient)).ToList().ForEach(client => client.DeleteCar(car));
            }
        }

        public void DeleteCustomer(KundeDto customer)
        {
            WriteActualMethod();
            InvokeDb(() => new KundeManager().DeleteKunde(DtoConverter.ConvertToEntity(customer)));

            lock (clients)
            {
                EnsureClient().Where(client => !client.Equals(currentClient)).ToList().ForEach(client => client.DeleteCustomer(customer));
            }
        }

        public void DeleteReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            InvokeDb(() => new ReservationManager().DeleteReservation(DtoConverter.ConvertToEntity(reservation)));

            lock (clients)
            {
                EnsureClient().Where(client => !client.Equals(currentClient)).ToList().ForEach(client => client.DeleteReservation(reservation));
            }
        }

        public List<AutoDto> GetAllCars() {
            EnsureClient();

            WriteActualMethod();
            AutoManager manager = new AutoManager();
            return DtoConverter.ConvertToDtos(manager.ListOfAutos);
        }

        public List<KundeDto> GetAllCustomers() {
            EnsureClient();

            WriteActualMethod();
            KundeManager manager = new KundeManager();
            return DtoConverter.ConvertToDtos(manager.ListOfKunden);
        }

        public List<ReservationDto> GetAllReservations() {
            EnsureClient();

            WriteActualMethod();
            ReservationManager manager = new ReservationManager();
            return DtoConverter.ConvertToDtos(manager.ListOfReservationen);
        }

        public AutoDto GetCar(int id) {
            EnsureClient();

            WriteActualMethod();
            AutoManager manager = new AutoManager();
            return DtoConverter.ConvertToDto(manager.GetAuto(id));
        }

        public KundeDto GetCustomer(int id) {
            EnsureClient();

            WriteActualMethod();
            KundeManager manager = new KundeManager();
            return DtoConverter.ConvertToDto(manager.GetKunde(id));
        }

        public ReservationDto GetReservation(int id) {
            EnsureClient();

            WriteActualMethod();
            ReservationManager manager = new ReservationManager();
            return DtoConverter.ConvertToDto(manager.GetReservation(id));
        }

        public void UpdateCar(AutoDto car)
        {
            WriteActualMethod();
            InvokeDb(() => new AutoManager().UpdateAuto(DtoConverter.ConvertToEntity(car)));

            lock (clients) {
                EnsureClient().Where(client => !client.Equals(currentClient)).ToList().ForEach(client => client.UpdateCar(car));
            }
        }

        public void UpdateCustomer(KundeDto customer)
        {
            WriteActualMethod();
            InvokeDb(() => new KundeManager().UpdateKunde(DtoConverter.ConvertToEntity(customer)));

            lock (clients)
            {
                EnsureClient().Where(client => !client.Equals(currentClient)).ToList().ForEach(client => client.UpdateCustomer(customer));
            }
        }

        public void UpdateReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            InvokeDb(() => new ReservationManager().UpdateReservation(DtoConverter.ConvertToEntity(reservation)));

            lock (clients)
            {
                EnsureClient().Where(client => !client.Equals(currentClient)).ToList().ForEach(client => client.UpdateReservation(reservation));
            }
        }
    }
}