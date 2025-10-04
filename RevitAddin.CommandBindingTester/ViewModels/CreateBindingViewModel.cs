using RevitAddin.CommandBindingTester.Models;
using RevitAddin.CommandBindingTester.Views;
using ricaun.Revit.Mvvm;
using ricaun.Revit.UI;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace RevitAddin.CommandBindingTester.ViewModels
{
    public class CreateBindingViewModel : ObservableObject
    {
        #region Public Properties
        public ObservableCollection<CreateBindingModel> Models { get; } = new();
        public IRelayCommand Command => new RelayCommand(AddModel);
        #endregion

        #region Constructor
        public CreateBindingViewModel()
        {

        }
        #endregion

        #region View / Window
        public string Title { get; set; } = "CreateBindingViewModel";
        public CreateBindingView Window { get; private set; }
        public void Show()
        {
            if (Window is null)
            {
                Window = new CreateBindingView();
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
                Window = new CreateBindingView();
                Window.DataContext = this;
                Window.SetAutodeskOwner();
                Window.Closed += (s, e) => { Window = null; };
            }
            return Window?.ShowDialog();
        }
        #endregion

        #region Private Methods
        private void AddModel()
        {
            Models.Add(new CreateBindingModel() { Name = $"{Guid.NewGuid()}" });
        }
        #endregion
    }
}