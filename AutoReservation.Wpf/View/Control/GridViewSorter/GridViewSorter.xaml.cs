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
    public partial class GridViewSorter : ListViewSorter.ListViewSorterBase, IAddChild
    {
        public GridViewColumnCollection Columns {
            get => this.GridView.Columns;
        }

        /// <summary>
        ///  Add an object child to this control
        /// </summary>
        void IAddChild.AddChild(object column)
        {
            AddChild(column);
        }

        /// <summary>
        ///  Add an object child to this control
        /// </summary>
        protected override void AddChild(object column)
        {
            GridViewColumn c = column as GridViewColumn;

            if (c != null)
            {
                Columns.Add(c);
            }
            else
            {
                throw new InvalidOperationException("Invalid type of child.");
            }
        }

        /// <summary>
        ///  Add a text string to this control
        /// </summary>
        void IAddChild.AddText(string text)
        {
            AddText(text);
        }

        /// <summary>
        ///  Add a text string to this control
        /// </summary>
        protected override void AddText(string text)
        {
            AddChild(text);
        }
        
        /// <summary>
        /// This is the dependency property registered for the GridView' ColumnCollection attached property.
        /// </summary>
        public static readonly DependencyProperty ColumnCollectionProperty
            = System.Windows.Controls.GridView.ColumnCollectionProperty;

        /// <summary>
        /// Reads the attached property GridViewColumnCollection from the given element.
        /// </summary>
        /// <param name="element">The element from which to read the GridViewColumnCollection attached property.</param>
        /// <returns>The property's value.</returns>
        public static GridViewColumnCollection GetColumnCollection(DependencyObject element) {
            GridViewSorter sorter = element as GridViewSorter;
            if (sorter != null) {
                return System.Windows.Controls.GridView.GetColumnCollection(sorter.GridView);
            }
            else
            {
                throw new InvalidOperationException("Invalid type of element.");
            }
        }

        /// <summary>
        /// Writes the attached property GridViewColumnCollection to the given element.
        /// </summary>
        /// <param name="element">The element to which to write the GridViewColumnCollection attached property.</param>
        /// <param name="collection">The collection to set</param>
        public static void SetColumnCollection(DependencyObject element, GridViewColumnCollection collection)
        {
            GridViewSorter sorter = element as GridViewSorter;
            if (sorter != null)
            {
                System.Windows.Controls.GridView.SetColumnCollection(sorter.GridView, collection);
            }
            else
            {
                throw new InvalidOperationException("Invalid type of element.");
            }
        }

        /// <summary>
        /// Whether should serialize ColumnCollection attach DP
        /// </summary>
        /// <param name="obj">Object on which this DP is set</param>
        public static bool ShouldSerializeColumnCollection(DependencyObject obj) {
            return System.Windows.Controls.GridView.ShouldSerializeColumnCollection(obj);
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
