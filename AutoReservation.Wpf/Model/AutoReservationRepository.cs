using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Extensions;
using AutoReservation.Common.Interfaces;
using AutoReservation.Wpf.Logic;
using AutoReservation.Service.Wcf;

using System.ServiceModel;


namespace AutoReservation.Wpf.Model {    
    public class AutoReservationRepository : INotifyPropertyChanged {
        private readonly ObservableCollection<ChangeTracker<AutoDto>> _autos;
        public List<AutoDto> Autos => _autos.Select(auto => auto.Current).ToList();

        private IAutoReservationService target;

        public AutoReservationRepository(string serverUrl = null) {

            if (target == null)
            {
                ChannelFactory<IAutoReservationService> channelFactory = new ChannelFactory<IAutoReservationService>("AutoReservationService");
                target = channelFactory.CreateChannel();
            }
            
            // TODO: Repair Real connection
            _autos = new ObservableCollection<ChangeTracker<AutoDto>>(target.GetAllCars().Select(auto => new ChangeTracker<AutoDto>(auto)));
        }

        public void AddCar(AutoDto car) {
            try {
                target.AddCar(car);
            } catch (Exception e) {
                throw new RepositoryException("Auto konnte nicht hinzugefügt werden.", e);
            }
        }

        public void UpdateCar(AutoDto car) {
            try
            {
                target.UpdateCar(car);
            }
            catch (Exception e)
            {
                throw new RepositoryException("Auto konnte nicht geändert werden.", e);
            }
        }

        public void SaveCarChanges()
        {
            _autos.Where(auto => auto.IsDirty).ToList().ForEach(auto => {
                UpdateCar(auto.Original);
            });
        }

        public void SaveAllChanges()
        {
            SaveCarChanges();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

