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
    [ContentProperty("Columns")]
    public partial class GridViewSorter : ListViewSorter.ListViewSorterBase
    {
        
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public GridViewColumnCollection Columns {
            get => this.GridView.Columns;
        }
        /*
        /// <summary>
        /// This is the dependency property registered for the GridView' ColumnCollection attached property.
        /// </summary>
        public static readonly DependencyProperty ColumnCollectionProperty
            = DependencyProperty.RegisterAttached(
                "ColumnCollection",
                typeof(GridViewColumnCollection),
                typeof(GridViewSorter));

        /// <summary>
        /// Reads the attached property GridViewColumnCollection from the given element.
        /// </summary>
        /// <param name="element">The element from which to read the GridViewColumnCollection attached property.</param>
        /// <returns>The property's value.</returns>
        public static GridViewColumnCollection GetColumnCollection(DependencyObject element)
        {
            if (element == null) { throw new ArgumentNullException(nameof(element)); }
            return (GridViewColumnCollection)element.GetValue(GridViewSorter.ColumnCollectionProperty);
        }

        /// <summary>
        /// Writes the attached property GridViewColumnCollection to the given element.
        /// </summary>
        /// <param name="element">The element to which to write the GridViewColumnCollection attached property.</param>
        /// <param name="collection">The collection to set</param>
        public static void SetColumnCollection(DependencyObject element, GridViewColumnCollection collection)
        {
            if (element == null) { throw new ArgumentNullException(nameof(element)); }
            element.SetValue(ColumnCollectionProperty, collection);
        }*/

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
