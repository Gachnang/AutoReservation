using System.ComponentModel;
using System.Runtime.CompilerServices;
using AutoReservation.Common.Extensions;
using AutoReservation.Wpf.Model;

namespace AutoReservation.Wpf.View.Window.ViewModel {
    public class MainViewModel : INotifyPropertyChanged {
        public AutoReservationRepository Repository { get; private set; }
        public AutoTabModel AutoTabModel { get; private set; }

        public MainViewModel() {
            Repository = new AutoReservationRepository();
            Repository.PropertyChanged += (o, e) => { this.OnPropertyChanged($"Repository");};

            AutoTabModel = new AutoTabModel(this);
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}