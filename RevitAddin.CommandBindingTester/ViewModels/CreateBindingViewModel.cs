using Autodesk.Revit.UI;
using RevitAddin.CommandBindingTester.Models;
using RevitAddin.CommandBindingTester.Views;
using ricaun.Revit.Mvvm;
using ricaun.Revit.UI;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RevitAddin.CommandBindingTester.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class CreateBindingViewModel : ObservableObject
    {
        public static CreateBindingViewModel Instance { get; } = new CreateBindingViewModel();

        #region Public Properties
        public ObservableCollection<CreateBindingModel> Models { get; } = new();
        public CreateBindingModel SelectedItem { get; set; }
        public IRelayCommand Command => new RelayCommand(AddModel);
        public IRelayCommand CommandDelete => new RelayCommand(DeleteSelected, CanDeleteSelected);

        private bool CanDeleteSelected()
        {
            return SelectedItem is not null;
        }

        private void DeleteSelected()
        {
            Models.Remove(SelectedItem);
            SelectedItem = Models.FirstOrDefault();
        }

        public IRelayCommand CommandResult => new RelayCommand(WindowResult);
        #endregion

        #region Constructor
        public CreateBindingViewModel()
        {
            Models.Add(new CreateBindingModel()
            {
                PostableCommand = PostableCommand.ThinLines,
                CanExecute = CanExecuteCommand.None,
                BeforeExecuted = BeforeExecutedCommand.None,
                Executed = ExecutedCommand.ShowMessage,
            });
            Models.Add(new CreateBindingModel()
            {
                PostableCommand = PostableCommand.ThinLines,
                CanExecute = CanExecuteCommand.WhenFamily,
                BeforeExecuted = BeforeExecutedCommand.None,
                Executed = ExecutedCommand.TransactionAuthor,
            });
            Models.Add(new CreateBindingModel()
            {
                PostableCommand = PostableCommand.ThinLines,
                CanExecute = CanExecuteCommand.WhenDocument,
                BeforeExecuted = BeforeExecutedCommand.None,
                Executed = ExecutedCommand.TransactionAuthor,
            });
        }
        #endregion

        #region View / Window
        public string Title { get; set; } = "RevitCommand Binding Editor";
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
        private void WindowResult()
        {
            if (Window is not null)
            {
                Window.DialogResult = true;
                Window.Close();
            }
        }
        private void AddModel()
        {
            Models.Add(new CreateBindingModel());
        }
        #endregion
    }
}