using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitAddin.CommandBindingTester.ViewModels;
using System;
using System.Linq;

namespace RevitAddin.CommandBindingTester.Revit.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class CreateCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
        {
            UIApplication uiapp = commandData.Application;

            // get all postable commands
            var commands = Enum.GetValues(typeof(PostableCommand))
                .OfType<PostableCommand>();

            // Force to Remove all Binding in this AddIn
            foreach (var command in commands)
            {
                try
                {
                    var revitCommandId = RevitCommandId.LookupPostableCommandId(command);
                    if (revitCommandId.HasBinding)
                        uiapp.RemoveAddInCommandBinding(revitCommandId);
                }
                catch { }
            }

            var viewModel = new CreateBindingViewModel();
            var result = viewModel.ShowDialog();

            return Result.Succeeded;
        }
    }
}
