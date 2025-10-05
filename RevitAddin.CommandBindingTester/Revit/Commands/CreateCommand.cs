using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitAddin.CommandBindingTester.ViewModels;
using System;
using System.Linq;

namespace RevitAddin.CommandBindingTester.Revit.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class CreateCommand : IExternalCommand, IExternalCommandAvailability
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
        {
            UIApplication uiapp = commandData.Application;

            uiapp.RemoveAllAddInCommandBinding();

            var viewModel = CreateBindingViewModel.Instance;
            var result = viewModel.ShowDialog();

            foreach (var model in viewModel.Models)
            {
                CreateAddInCommandBindingForModel(uiapp, model);
            }

            return Result.Succeeded;
        }

        private static void CreateAddInCommandBindingForModel(UIApplication uiapp, Models.CreateBindingModel model)
        {
            if (model.IsEnabled == false)
                return;

            var command = model.PostableCommand;
            var revitCommandId = RevitCommandId.LookupPostableCommandId(command);

            try
            {
                if (revitCommandId.HasBinding)
                    uiapp.RemoveAddInCommandBinding(revitCommandId);
            }
            catch { }

            if (revitCommandId.CanHaveBinding == false)
            {
                model.IsEnabled = false;
                return;
            }

            var addInCommandBinding = uiapp.CreateAddInCommandBinding(revitCommandId);

            switch (model.CanExecute)
            {
                case Models.CanExecuteCommand.Always:
                    addInCommandBinding.CanExecute += (s, e) =>
                    {
                        e.CanExecute = true;
                    };
                    break;
                case Models.CanExecuteCommand.Never:
                    addInCommandBinding.CanExecute += (s, e) =>
                    {
                        e.CanExecute = false;
                    };
                    break;
                case Models.CanExecuteCommand.WhenDocument:
                    addInCommandBinding.CanExecute += (s, e) =>
                    {
                        e.CanExecute = uiapp.ActiveUIDocument?.Document is not null;
                    };
                    break;
                case Models.CanExecuteCommand.WhenFamily:
                    addInCommandBinding.CanExecute += (s, e) =>
                    {
                        var doc = uiapp.ActiveUIDocument?.Document;
                        e.CanExecute = doc?.IsFamilyDocument == true;
                    };
                    break;
                case Models.CanExecuteCommand.WhenNotFamily:
                    addInCommandBinding.CanExecute += (s, e) =>
                    {
                        var doc = uiapp.ActiveUIDocument?.Document;
                        e.CanExecute = doc?.IsFamilyDocument == false;
                    };
                    break;
            }

            switch (model.BeforeExecuted)
            {
                case Models.BeforeExecutedCommand.ShowMessage:
                    addInCommandBinding.BeforeExecuted += (s, e) =>
                    {
                        System.Windows.MessageBox.Show($"Before Executed: {model.Name} ({model.Id})");
                    };
                    break;
                case Models.BeforeExecutedCommand.ThrowException:
                    addInCommandBinding.BeforeExecuted += (s, e) =>
                    {
                        throw new Exception($"Before Executed Exception: {model.Name} ({model.Id})");
                    };
                    break;
                case Models.BeforeExecutedCommand.Cancel:
                    addInCommandBinding.BeforeExecuted += (s, e) =>
                    {
                        e.Cancel = true;
                    };
                    break;
            }

            switch (model.Executed)
            {
                case Models.ExecutedCommand.ShowMessage:
                    addInCommandBinding.Executed += (s, e) =>
                    {
                        System.Windows.MessageBox.Show($"Executed: {model.Name} ({model.Id})");
                    };
                    break;
                case Models.ExecutedCommand.ThrowException:
                    addInCommandBinding.Executed += (s, e) =>
                    {
                        throw new Exception($"Executed Exception: {model.Name} ({model.Id})");
                    };
                    break;
                case Models.ExecutedCommand.TransactionAuthor:
                    addInCommandBinding.Executed += (s, e) =>
                    {
                        var uiapp = s as UIApplication;
                        if (uiapp?.ActiveUIDocument is null)
                            return;
                        var document = uiapp.ActiveUIDocument.Document;
                        using (var transaction = new Transaction(document, "Change Author"))
                        {
                            transaction.Start();
                            document.ProjectInformation.Author = uiapp.ActiveAddInId.GetAddInName();
                            transaction.Commit();
                        }
                    };
                    break;
                case Models.ExecutedCommand.TransactionView:
                    addInCommandBinding.Executed += (s, e) =>
                    {
                        var uiapp = s as UIApplication;
                        if (uiapp?.ActiveUIDocument is null)
                            return;
                        var document = uiapp.ActiveUIDocument.Document;
                        View3D view3d = null;
                        using (var transaction = new Transaction(document, "Create View 3D"))
                        {
                            transaction.Start();
                            view3d = View3D.CreateIsometric(document, document.GetDefaultElementTypeId(ElementTypeGroup.ViewType3D));
                            transaction.Commit();
                        }

                        if (view3d is not null)
                            uiapp.ActiveUIDocument.ActiveView = view3d;
                    };
                    break;
            }
        }

        public bool IsCommandAvailable(UIApplication applicationData, CategorySet selectedCategories)
        {
            return true;
        }
    }
}
