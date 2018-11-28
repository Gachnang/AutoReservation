using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace AutoReservation.Wpf.View.Window.ViewModel {
    public class AutoTabModel : INotifyPropertyChanged {
        private readonly MainViewModel _mainWindowModel;
        /*public GadgeothekRepository Repository => _mainWindowModel.Repository;
        public Collection<Gadget> Gadgets => Repository.Gadgets;

        private Gadget _selectedGadget;
        public Gadget SelectedGadget
        {
            get => _selectedGadget;
            set
            {
                _selectedGadget = value;
                OnPropertyChanged();
            }
        }*/

        public AutoTabModel() {
            if ((bool) DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue) {
                _mainWindowModel = new MainViewModel();
            } else {
                throw new ArgumentException(
                    "MainWindowModel is needed to inject repository.\n" +
                    "(It is allowed to be null to show data in designTime)");
            }
        }

        public AutoTabModel(MainViewModel mainWindowModel) {
            _mainWindowModel = mainWindowModel;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Save() {
            // Repository.SetGadget(SelectedGadget);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}