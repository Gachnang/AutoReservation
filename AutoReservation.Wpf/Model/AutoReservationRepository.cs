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
    public class ServiceTestRemoteFixture
        : IDisposable
    {
        public ServiceTestRemoteFixture()
        {
            ServiceHost = new ServiceHost(typeof(AutoReservationService));
            ServiceHost.Open();
        }

        public void Dispose()
        {
            if (ServiceHost.State != CommunicationState.Closed)
            {
                ServiceHost.Close();
            }
        }

        public ServiceHost ServiceHost { get; }
    }
    
    public class AutoReservationRepository : INotifyPropertyChanged {
        private static string DefaultServerUrl = "http://localhost:8080";

        private readonly ObservableCollection<AutoDto> _autos;

        private IAutoReservationService target;

        public AutoReservationRepository(string serverUrl = null) {

            if (target == null)
            {
                ChannelFactory<IAutoReservationService> channelFactory = new ChannelFactory<IAutoReservationService>("AutoReservationService");
                target = channelFactory.CreateChannel();
            }
            
            // TODO: Real connection
            _autos = new ObservableCollection<AutoDto>(target.GetAllCars());
        }

        public ObservableCollection<AutoDto> Autos => _autos;


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

