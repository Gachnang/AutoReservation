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
    }
}