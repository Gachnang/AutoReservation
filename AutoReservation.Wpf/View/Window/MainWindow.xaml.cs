using System.Windows.Controls;
using AutoReservation.Wpf.View.Window.ViewModel;

namespace AutoReservation.Wpf.View.Window {
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window {
        public MainViewModel Model;

        public MainWindow() {
            InitializeComponent();

            Model = new MainViewModel();
            DataContext = Model;
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (((TabControl)sender).SelectedItem == null)
            {
                ((TabControl)sender).SelectedItem = e.RemovedItems[0];
            }
        }
    }
}