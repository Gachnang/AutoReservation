using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Serialization;
using System.Text;
using AutoReservation.Wpf.Logic;

namespace AutoReservation.Wpf.Model {
    public class ChangeTracker<T> : INotifyPropertyChanged where T : INotifyPropertyChanged {
        public T Current { get; set; }

        public string Label => IsNew ? "NEW" : IsDeleted ? "DEL" : IsDirty ? "*" : "";

        private bool _isDirty = false;
        public bool IsDirty {
            get => _isDirty;
            set {
                SetProperty(ref _isDirty, value);
                OnPropertyChanged(nameof(Label));
            }
        }

        private bool _isNew = false;
        public bool IsNew {
            get => _isNew;
            set {
                SetProperty(ref _isNew, value);
                OnPropertyChanged(nameof(Label));
            }
        }

        private bool _isDeleted = false;
        public bool IsDeleted {
            get=>_isDeleted ;
            set {
                SetProperty(ref _isDeleted, value);
                OnPropertyChanged(nameof(Label));
            }
        }

        public ChangeTracker(T current) {
            if (current == null) {
                throw new ArgumentNullException(nameof(current));
            }

            Current = current;
            Current.PropertyChanged += (sender, args) => {
                IsDirty = true;
                OnPropertyChanged(nameof(Label));
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// HelperFunction to set a property.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="storage">The property to set.</param>
        /// <param name="value">The new value fot the property.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>True when property changed.</returns>
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return false;
            }

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}