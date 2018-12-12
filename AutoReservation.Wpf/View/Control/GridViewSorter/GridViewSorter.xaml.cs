using System.Windows;
using System.Windows.Controls;
using AutoReservation.Wpf.View.Control.ListViewSorter;

namespace AutoReservation.Wpf.View.Control.GridViewSorter {
    /// <summary>
    ///     Interaction logic for GridViewSorter.xaml
    /// </summary>
    public partial class GridViewSorter : ListViewSorterBase {
        static GridViewSorter() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GridViewSorter),
                new FrameworkPropertyMetadata(typeof(GridViewSorter)));
        }

        public GridViewSorter() {
            InitializeComponent();
        }

        public GridViewColumnCollection Columns => GridView.Columns;
    }
}