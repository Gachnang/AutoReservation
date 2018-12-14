using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using AutoReservation.Wpf.Logic;

namespace AutoReservation.Wpf.Model
{
    class AutoReservationServiceCallback : IAutoReservationServiceCallback {
        private readonly AutoReservationRepository repository;

        public AutoReservationServiceCallback(AutoReservationRepository repository) {
            this.repository = repository;
        }

        public void DeleteCar(AutoDto car) {
            repository.Autos.Remove(car);
        }

        public void UpdateCar(AutoDto car) {
            repository.Autos.First(auto => auto.Id.Equals(car.Id)).Replace(car);
        }

        public void AddCar(AutoDto car) {
           repository.Autos.Add(car);
        }

        public void DeleteCustomer(KundeDto customer) {
            throw new NotImplementedException();
        }

        public void UpdateCustomer(KundeDto customer) {
            throw new NotImplementedException();
        }

        public void AddCustomer(KundeDto customer) {
            throw new NotImplementedException();
        }

        public void DeleteReservation(ReservationDto reservation) {
            throw new NotImplementedException();
        }

        public void UpdateReservation(ReservationDto reservation) {
            throw new NotImplementedException();
        }

        public void AddReservation(ReservationDto reservation) {
            throw new NotImplementedException();
        }
    }
}
