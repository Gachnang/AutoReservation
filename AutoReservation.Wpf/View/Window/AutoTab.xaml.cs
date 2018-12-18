using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Wpf.Model;
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

        private void BtnAdd_OnClick(object sender, RoutedEventArgs e) {
            ChangeTracker<AutoDto> auto = new ChangeTracker<AutoDto>(new AutoDto() {
                Id = 0,
                Marke = "NEW"
            })
            { 
                IsDirty = true,
                IsNew = true
            };

            Model.Autos.Add(auto);
            Model.SelectedAuto = auto;
        }

        private void BtnDel_OnClick(object sender, RoutedEventArgs e) {
            Model.SelectedAuto.IsDeleted = !Model.SelectedAuto.IsDeleted;
        }
    }
}