using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AutoReservation.Wpf.View.Window.ViewModel {
    public class MainViewModel : INotifyPropertyChanged {
        public MainViewModel() {
            GadgetsTabModel = new AutoTabModel(this);

            /*
            Repository = GadgeothekRepository.GetInstance();
            Repository.PropertyChanged += (o, e) => { this.OnPropertyChanged("Repository");};
            Repository.Open();*/
        }
        // public GadgeothekRepository Repository { get; private set; }

        public AutoTabModel GadgetsTabModel { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}