using RevitAddin.CommandBindingTester.Models;
using RevitAddin.CommandBindingTester.Views;
using ricaun.Revit.Mvvm;
using ricaun.Revit.UI;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace RevitAddin.CommandBindingTester.ViewModels
{
    public class BindingViewModel : ObservableObject
    {
        #region Public Properties
        public ObservableCollection<BindingModel> Models { get; internal set; } = new();
        #endregion

        #region Constructor
        public BindingViewModel()
        {
            
        }
        #endregion

        #region View / Window
        public string Title { get; set; } = "RevitCommand Binding Viewer";
        public BindingView Window { get; private set; }
        public void Show()
        {
            if (Window is null)
            {
                Window = new BindingView();
                Window.DataContext = this;
                Window.SetAutodeskOwner();
                Window.Closed += (s, e) => { Window = null; };
            }
            Window?.Show();
            Window?.Activate();
        }

        public bool? ShowDialog()
        {
            if (Window is null)
            {
                Window = new BindingView();
                Window.DataContext = this;
                Window.SetAutodeskOwner();
                Window.Closed += (s, e) => { Window = null; };
            }
            return Window?.ShowDialog();
        }
        #endregion
    }
}