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

            //var models = commands
            //    .Select(x => Models.CreateBindingModel.Create(x))
            //    .OrderBy(x => x.Id)
            //    .ToList();

            var viewModel = new CreateBindingViewModel();
            var result = viewModel.ShowDialog();

            return Result.Succeeded;
        }
    }
}
