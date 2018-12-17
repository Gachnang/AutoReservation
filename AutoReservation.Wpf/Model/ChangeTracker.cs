using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Serialization;
using System.Text;
using AutoReservation.Wpf.Logic;

namespace AutoReservation.Wpf.Model {
    public class ChangeTracker<T> where T : INotifyPropertyChanged {
        public readonly T Original;
        public readonly T Current;

        public bool IsDirty { get; set; }

        public ChangeTracker(T original) {
            if (original == null) {
                throw new ArgumentNullException(nameof(original));
            }

            Original = original;
            Current = original.Clone();

            Current.PropertyChanged += (sender, args) => IsDirty = true;
        }
    }
}