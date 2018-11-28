using System.ComponentModel;
using System.Runtime.CompilerServices;
using AutoReservation.Wpf.Model;

namespace AutoReservation.Wpf.View.Window.ViewModel {
    public class MainViewModel : INotifyPropertyChanged {
        public AutoReservationRepository Repository { get; private set; }

        public MainViewModel() {
            Repository = new AutoReservationRepository();
            Repository.PropertyChanged += (o, e) => { this.OnPropertyChanged("Repository");};

            // todo Why is this not working?
            AutoTabModel = new AutoTabModel(this);
        }
        // public GadgeothekRepository Repository { get; private set; }

        public AutoTabModel AutoTabModel { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}