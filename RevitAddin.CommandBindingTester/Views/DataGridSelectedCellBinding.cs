using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RevitAddin.CommandBindingTester.Views
{
    /// <summary>
    /// DataGrid SelectedCell Binding Helper, used when SelectionUnit is Cell
    /// </summary>
    public static class DataGridSelectedCellBinding
    {
        #region SelectedItem
        private static object defaultValue = new object(); // This is used to force the PropertyChangedCallback event to fire.
        private const string SelectedItemPropertyName = "SelectedItem";
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.RegisterAttached(SelectedItemPropertyName, typeof(object), typeof(DataGridSelectedCellBinding),
                new FrameworkPropertyMetadata(defaultValue, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectedItemChanged));
        public static object GetSelectedItem(DependencyObject obj) => obj.GetValue(SelectedItemProperty);
        public static void SetSelectedItem(DependencyObject obj, object value) => obj.SetValue(SelectedItemProperty, value);
        private static void OnSelectedItemChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (obj is not DataGrid grid)
                return;

            if (GetSelectedItemIsUpdating(grid))
                return;

            // Remove previous handler
            grid.SelectedCellsChanged -= Grid_SelectedCellsChanged;

            if (e.NewValue is not null)
            {
                grid.SelectedCells.Clear();
                foreach (var column in grid.Columns)
                {
                    if (column.Visibility == Visibility.Visible)
                        grid.SelectedCells.Add(new DataGridCellInfo(e.NewValue, column));
                }
                grid.Focus();
            }
            else
            {
                grid.SelectedCells.Clear();
            }

            // Add new handler if bound
            grid.SelectedCellsChanged += Grid_SelectedCellsChanged;
        }

        private const string SelectedItemIsUpdatingPropertyName = "SelectedItemIsUpdating";
        private static readonly DependencyProperty SelectedItemIsUpdatingProperty =
            DependencyProperty.RegisterAttached(SelectedItemIsUpdatingPropertyName, typeof(bool), typeof(DataGridSelectedCellBinding));
        private static bool GetSelectedItemIsUpdating(DependencyObject obj) => (bool)obj.GetValue(SelectedItemIsUpdatingProperty);
        private static void SetSelectedItemIsUpdating(DependencyObject obj, bool value) => obj.SetValue(SelectedItemIsUpdatingProperty, value);
        private static void Grid_SelectedCellsChanged(object? sender, SelectedCellsChangedEventArgs e)
        {
            if (sender is not DataGrid grid)
                return;

            if (grid.SelectedCells.Count > 0)
            {
                var first = grid.SelectedCells[0];
                SetSelectedItemIsUpdating(grid, true);
                try
                {
                    grid.SetCurrentValue(SelectedItemProperty, first.Item);
                }
                catch { }
                SetSelectedItemIsUpdating(grid, false);
            }
        }
        #endregion
    }
}