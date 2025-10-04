using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RevitAddin.CommandBindingTester.Views
{
    public partial class BindingView : Window
    {
        public BindingView()
        {
            InitializeComponent();
            InitializeWindow();
            UpdateDataGridWithStyle(dataGrid);
            Title = "Binding Viewer";
        }

        private void UpdateDataGridWithStyle(DataGrid dataGrid)
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
            dataGrid.CanUserSortColumns = false;
            dataGrid.CanUserResizeRows = false;

            dataGrid.AutoGenerateColumns = true;

            dataGrid.HeadersVisibility = DataGridHeadersVisibility.Column;
        }

        #region InitializeWindow
        private void InitializeWindow()
        {
            this.SizeToContent = SizeToContent.WidthAndHeight;
            this.ShowInTaskbar = false;
            this.ResizeMode = ResizeMode.NoResize;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        #endregion
    }
}