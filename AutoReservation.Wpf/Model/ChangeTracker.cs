using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Serialization;
using System.Text;
using AutoReservation.Wpf.Logic;

namespace AutoReservation.Wpf.Model {
    public class ChangeTracker<T> {
        public readonly T Original;
        public readonly T Current;

        public bool IsDirty {
            get {
                PropertyInfo[] properties = Original.GetType().GetProperties();
                foreach (PropertyInfo propertyInfo in properties)
                {
                    if (!propertyInfo.GetValue(Original).Equals(propertyInfo.GetValue(Current))) {
                        return true;
                    }
                }

                return false;
            }
        }

        public ChangeTracker(T original) {
            if (original == null) {
                throw new ArgumentNullException(nameof(original));
            }

            Original = original;
            Current = original.Clone();
        }
    }
}