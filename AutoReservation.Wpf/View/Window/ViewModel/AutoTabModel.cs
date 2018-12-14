using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Extensions;
using AutoReservation.Common.Interfaces;
using AutoReservation.Wpf.Model;

namespace AutoReservation.Wpf.View.Window.ViewModel {
    public class AutoTabModel : INotifyPropertyChanged {
        private readonly MainViewModel _mainViewModel;
        private AutoReservationRepository Repository => _mainViewModel.Repository;

        private AutoDto _selectedAuto;

        public AutoTabModel() {
            if ((bool) DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue) {
                _mainViewModel = new MainViewModel();
            } else {
                throw new ArgumentException(
                    "MainViewModel is needed to inject repository.\n" +
                    "(It is only allowed to be null to show data in designTime)");
            }
        }

        public AutoTabModel(MainViewModel mainViewModel) {
            _mainViewModel = mainViewModel;
        }

        public Collection<AutoDto> Autos => _mainViewModel.Repository?.Autos;

        public AutoDto SelectedAuto {
            get => _selectedAuto;
            set {
                _selectedAuto = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;


        public void Save() {
            Repository.UpdateCar(SelectedAuto);
            // Repository.SetAuto(SelectedAuto);
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}