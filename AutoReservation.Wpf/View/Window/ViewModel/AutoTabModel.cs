﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using AutoReservation.Common.Interfaces;

namespace AutoReservation.Wpf.View.Window.ViewModel {
    public class AutoTabModel : INotifyPropertyChanged {
        private readonly MainViewModel _mainViewModel;
        public Collection<IAuto> Autos => _mainViewModel.Repository.Autos;

        private IAuto _selectedAuto;
        public IAuto SelectedAuto
        {
            get => _selectedAuto;
            set
            {
                _selectedAuto = value;
                OnPropertyChanged();
            }
        }

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

        public event PropertyChangedEventHandler PropertyChanged;

        public void Save() {
            // Repository.SetAuto(SelectedAuto);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}