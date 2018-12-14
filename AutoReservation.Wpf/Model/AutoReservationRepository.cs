using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Extensions;
using AutoReservation.Common.Interfaces;
using System.ServiceModel;


namespace AutoReservation.Wpf.Model {    
    public class AutoReservationRepository : INotifyPropertyChanged {
        private readonly ObservableCollection<AutoDto> _autos;

        private IAutoReservationService autoReservationService;

        public AutoReservationRepository(string serverUrl = null) {

            if (autoReservationService == null)
            {
                ChannelFactory<IAutoReservationService> channelFactory = new ChannelFactory<IAutoReservationService>("AutoReservationService");
                autoReservationService = channelFactory.CreateChannel();
            }
            
            // TODO: Real connection
            _autos = new ObservableCollection<AutoDto>(autoReservationService.GetAllCars());
        }

        public ObservableCollection<AutoDto> Autos => _autos;


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

