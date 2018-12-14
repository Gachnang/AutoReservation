using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Extensions;
using AutoReservation.Common.Interfaces;
using AutoReservation.Wpf.Logic;
using AutoReservation.Service.Wcf;

using System.ServiceModel;


namespace AutoReservation.Wpf.Model {    
    public class AutoReservationRepository : INotifyPropertyChanged {
        private readonly ObservableCollection<AutoDto> _autos;
        public ObservableCollection<AutoDto> Autos => _autos;

        private IAutoReservationService target;
        private IAutoReservationServiceCallback callback;

        public AutoReservationRepository(string serverUrl = null) {

            if (target == null)
            {
                callback = new AutoReservationServiceCallback(this);
                ChannelFactory<IAutoReservationService> channelFactory = new DuplexChannelFactory<IAutoReservationService>(callback, "AutoReservationService");
                target = channelFactory.CreateChannel();
            }
            
            // TODO: Real connection
            _autos = new ObservableCollection<AutoDto>(target.GetAllCars());
        }

        public void AddCar(AutoDto car) => target.AddCar(car);
        public void UpdateCar(AutoDto car) => target.UpdateCar(car);

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

