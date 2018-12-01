using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutoReservation.Wpf.View.Control.GridViewSorter
{
    /// <summary>
    /// Interaction logic for GridViewSorter.xaml
    /// </summary>
    public partial class GridViewSorter : ListViewSorter.ListViewSorterBase {
        public GridViewColumnCollection Columns {
            get => this.GridView.Columns;
        }

        static GridViewSorter() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GridViewSorter),
                new FrameworkPropertyMetadata(typeof(GridViewSorter)));
        }

        public GridViewSorter() : base()
        {
            InitializeComponent();
        }
    }
}
