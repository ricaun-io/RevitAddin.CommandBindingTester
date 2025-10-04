using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ricaun.Revit.UI;
using System;
using System.Linq;

namespace RevitAddin.CommandBindingTester.Revit
{
    [AppLoader]
    public class App : IExternalApplication
    {
        private RibbonPanel ribbonPanel;
        public Result OnStartup(UIControlledApplication application)
        {
            ribbonPanel = application.CreatePanel("CommandBinding");
            ribbonPanel.RowStackedItems(
                ribbonPanel.CreatePushButton<Commands.ViewerCommand>("RevitCommand Viewer")
                    .SetLargeImage("Resources/Revit.ico"),
                ribbonPanel.CreatePushButton<Commands.CreateCommand>("RevitCommand Binding")
                    .SetLargeImage("Resources/Revit.ico")
                );

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            ribbonPanel?.Remove();

            application.RemoveAllAddInCommandBinding();

            return Result.Succeeded;
        }
    }

}