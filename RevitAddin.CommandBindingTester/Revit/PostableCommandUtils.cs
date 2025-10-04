using Autodesk.Revit.UI;
using System;
using System.Linq;

namespace RevitAddin.CommandBindingTester.Revit
{
    public static class PostableCommandUtils
    {
        public static PostableCommand[] GetPostableCommands()
        {
            return Enum.GetValues(typeof(PostableCommand))
                .OfType<PostableCommand>()
                .ToArray();
        }

        public static void RemoveAllAddInCommandBinding(this UIApplication uiapp)
        {
            var commands = GetPostableCommands();
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
        }

        public static void RemoveAllAddInCommandBinding(this UIControlledApplication application)
        {
            var commands = GetPostableCommands();
            foreach (var command in commands)
            {
                try
                {
                    var revitCommandId = RevitCommandId.LookupPostableCommandId(command);
                    if (revitCommandId.HasBinding)
                        application.RemoveAddInCommandBinding(revitCommandId);
                }
                catch { }
            }
        }
    }

}