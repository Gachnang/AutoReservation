using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace AutoReservation.Wpf.View.Control.ListViewSorter {
    public class ListViewSorterBase : ListView {
        protected CustomSorter _customSorter = new CustomSorter();
        protected GridViewColumnHeader _sortColumn;
        protected ListSortDirection _sortDirection = ListSortDirection.Ascending;


        public void GridViewColumnHeaderClickedHandler(object sender, RoutedEventArgs e) {
            GridViewColumnHeader columnHeader = e.OriginalSource as GridViewColumnHeader;
            if (columnHeader == null) {
                return;
            }

            Sort(columnHeader);
        }

        private void Sort(GridViewColumnHeader columnHeader) {
            if (_sortColumn == columnHeader) {
                // Toggle sorting direction 
                _sortDirection = _sortDirection == ListSortDirection.Ascending
                    ? ListSortDirection.Descending
                    : ListSortDirection.Ascending;
            } else {
                // Remove arrow from previously sorted header 
                if (_sortColumn != null) {
                    _sortColumn.Column.HeaderTemplate = null;
                    // TODO find better way than always increase width..
                    _sortColumn.Column.Width = _sortColumn.ActualWidth - 20;
                }

                _sortColumn = columnHeader;
                _sortDirection = ListSortDirection.Ascending;
                columnHeader.Column.Width = columnHeader.ActualWidth + 20;
            }

            if (_sortDirection == ListSortDirection.Ascending) {
                columnHeader.Column.HeaderTemplate = Resources["ArrowUp"] as DataTemplate;
            } else {
                columnHeader.Column.HeaderTemplate = Resources["ArrowDown"] as DataTemplate;
            }

            Binding binding = columnHeader.Column.DisplayMemberBinding as Binding;
            if (binding != null) {
                ICollectionView dataView = CollectionViewSource.GetDefaultView(ItemsSource);
                ListCollectionView view = (ListCollectionView) dataView;
                _customSorter.SortPropertyName = binding.Path.Path;
                ;
                view.CustomSort = _customSorter;
            }
        }

        protected class CustomSorter : IComparer {
            private Dictionary<string, ListSortDirection> _dictOfSortDirections =
                new Dictionary<string, ListSortDirection>();

            private string _sortPropertyName;

            public string SortPropertyName {
                get => _sortPropertyName;
                set {
                    _sortPropertyName = value;
                    if (!_dictOfSortDirections.ContainsKey(_sortPropertyName)) {
                        _dictOfSortDirections.Add(_sortPropertyName, ListSortDirection.Ascending);
                    }

                    // Alternate sort directions inside the dictionary
                    _dictOfSortDirections[_sortPropertyName] =
                        _dictOfSortDirections[_sortPropertyName] == ListSortDirection.Ascending
                            ? ListSortDirection.Descending
                            : ListSortDirection.Ascending;
                }
            }

            public int Compare(object x, object y) {
                PropertyInfo pi = x.GetType().GetProperty(_sortPropertyName);
                if (pi != null) {
                    object value1 = pi.GetValue(x);
                    object value2 = pi.GetValue(y);

                    bool valuesAreNotSortable = !(value1 is IComparable) || !(value2 is IComparable);
                    if (valuesAreNotSortable) {
                        return 0;
                    }

                    ListSortDirection dir = _dictOfSortDirections[_sortPropertyName];

                    if (dir == ListSortDirection.Ascending) {
                        return ((IComparable) value1).CompareTo(value2);
                    }

                    return ((IComparable) value2).CompareTo(value1);
                }

                return 0;
            }
        }
    }
}