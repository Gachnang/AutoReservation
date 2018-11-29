using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AutoReservation.Wpf.View.Window.ViewModel;

namespace AutoReservation.Wpf.View.Window {
    /// <summary>
    ///     Interaction logic for AutoTab.xaml
    /// </summary>
    public partial class AutoTab : UserControl {
        public AutoTab() {
            InitializeComponent();
        }

        public AutoTabModel Model => DataContext as AutoTabModel;

        private void BtnSave_OnClick(object sender, RoutedEventArgs e) {
            Model.Save();
        }

        private void Wtb_OnPreviewTextInput(object sender, TextCompositionEventArgs e) {
            e.Handled = !int.TryParse(((TextBox) sender).Text + e.Text, out int i);
        }
    }
}