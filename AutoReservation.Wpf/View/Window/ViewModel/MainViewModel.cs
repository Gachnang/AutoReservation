using System.ComponentModel;
using System.Runtime.CompilerServices;
using AutoReservation.Common.Extensions;
using AutoReservation.Wpf.Model;

namespace AutoReservation.Wpf.View.Window.ViewModel {
    public class MainViewModel : INotifyPropertyChanged {
        public MainViewModel() {
            Repository = new AutoReservationRepository();
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