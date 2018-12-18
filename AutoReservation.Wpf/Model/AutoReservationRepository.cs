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
        public ObservableCollection<ChangeTracker<AutoDto>> Autos => _autos; //.Select(auto => auto.Current).ToList();

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

        private void AddCar(AutoDto car) {
            try {
                target.AddCar(car);
            } catch (Exception e) {
                throw new RepositoryException("Auto konnte nicht hinzugefügt werden.", e);
            }
        }

        private void UpdateCar(AutoDto car)
        {
            try
            {
                target.UpdateCar(car);
            }
            catch (Exception e)
            {
                throw new RepositoryException("Auto konnte nicht geändert werden.", e);
            }
        }

        private void DeleteCar(AutoDto car)
        {
            try
            {
                target.DeleteCar(car);
            }
            catch (Exception e)
            {
                throw new RepositoryException("Auto konnte nicht gelöscht werden.", e);
            }
        }

        public void SaveCarChanges()
        {
            _autos.Where(auto => auto.IsDirty).ToList().ForEach(auto => {
                if (auto.IsNew && auto.IsDeleted) {
                    _autos.Remove(auto);
                } else if (auto.IsNew) {
                    AddCar(auto.Current);
                    auto.IsNew = false;
                } else if (auto.IsDeleted)
                {
                    DeleteCar(auto.Current);
                    _autos.Remove(auto);
                } else {
                    UpdateCar(auto.Current);
                }
                auto.IsDirty = false;
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

