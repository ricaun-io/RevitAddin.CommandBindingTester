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
            dataGrid.UpdateDataGridStyle();
            Title = "RevitCommand Binding Viewer";
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