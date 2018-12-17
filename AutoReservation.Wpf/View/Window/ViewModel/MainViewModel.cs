using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using AutoReservation.Common.Extensions;
using AutoReservation.Wpf.Model;

namespace AutoReservation.Wpf.View.Window.ViewModel {
    public class MainViewModel : INotifyPropertyChanged {
        public MainViewModel() {
            try {
                Repository = new AutoReservationRepository();
            } catch (Exception e) {
                StringBuilder sb = new StringBuilder("Message:");
                do
                {
                    sb.Append(Environment.NewLine + e.Message);
                    e = e.InnerException;
                } while (e != null);

                MessageBox.Show(sb.ToString(), "Fehler beim Öffnen");
                Application.Current.Shutdown(2);
            }

            Repository.PropertyChanged += (o, e) => { OnPropertyChanged("Repository"); };

            AutoTabModel = new AutoTabModel(this);
        }

        public AutoReservationRepository Repository { get; }
        public AutoTabModel AutoTabModel { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}