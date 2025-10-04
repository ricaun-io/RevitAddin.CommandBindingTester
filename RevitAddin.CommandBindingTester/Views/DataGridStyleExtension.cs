using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RevitAddin.CommandBindingTester.Views
{
    public static class DataGridStyleExtension
    {
        public static void UpdateDataGridStyle(this DataGrid dataGrid)
        {
            var brush = new SolidColorBrush();
            brush.Color = Color.FromRgb(211, 211, 211); //#FFD3D3D3
            brush.Opacity = 0.3;
            dataGrid.HorizontalGridLinesBrush = brush;
            dataGrid.VerticalGridLinesBrush = brush;

            dataGrid.SelectionMode = DataGridSelectionMode.Extended;
            dataGrid.SelectionUnit = DataGridSelectionUnit.Cell;

            var alternativeBrush = new SolidColorBrush();
            alternativeBrush.Color = Color.FromRgb(211, 211, 211); //#FFD3D3D3
            alternativeBrush.Opacity = 0.1;
            dataGrid.AlternatingRowBackground = alternativeBrush;

            dataGrid.CanUserAddRows = false;

            dataGrid.CanUserReorderColumns = true;
            dataGrid.CanUserResizeColumns = true;
            dataGrid.CanUserSortColumns = true;
            dataGrid.CanUserResizeRows = false;

            dataGrid.AutoGenerateColumns = true;

            dataGrid.HeadersVisibility = DataGridHeadersVisibility.Column;

            dataGrid.RowHeight = 24;

            // Force auto generated columns to fill the available space, except for CheckBox columns
            dataGrid.AutoGeneratingColumn += (s, e) =>
            {
                var column = e.Column;

                if (column is not DataGridCheckBoxColumn)
                    column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);

                if (column is DataGridTextColumn dataGridTextColumn)
                {
                    var style = new Style(typeof(TextBlock), dataGridTextColumn.ElementStyle);
                    style.Setters.Add(new Setter(FrameworkElement.VerticalAlignmentProperty, VerticalAlignment.Center));
                    dataGridTextColumn.ElementStyle = style;
                }
                else if (column is DataGridCheckBoxColumn dataGridCheckBoxColumn)
                {
                    var style = new Style(typeof(CheckBox), dataGridCheckBoxColumn.ElementStyle);
                    style.Setters.Add(new Setter(FrameworkElement.VerticalAlignmentProperty, VerticalAlignment.Center));
                    dataGridCheckBoxColumn.ElementStyle = style;

                    var styleEditing = new Style(typeof(CheckBox), dataGridCheckBoxColumn.EditingElementStyle);
                    styleEditing.Setters.Add(new Setter(FrameworkElement.VerticalAlignmentProperty, VerticalAlignment.Center));
                    dataGridCheckBoxColumn.EditingElementStyle = styleEditing;
                }
                else if (column is DataGridComboBoxColumn dataGridComboBoxColumn)
                {
                    var style = new Style(typeof(ComboBox), dataGridComboBoxColumn.ElementStyle);
                    style.Setters.Add(new Setter(FrameworkElement.VerticalAlignmentProperty, VerticalAlignment.Center));
                    dataGridComboBoxColumn.ElementStyle = style;
                }
            };
        }
    }
}