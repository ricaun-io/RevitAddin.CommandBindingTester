using Autodesk.Revit.UI;
using PropertyChanged;
using System;

namespace RevitAddin.CommandBindingTester.Models
{
    public enum CanExecuteCommand
    {
        None,
        Always,
        Never,
        WhenDocument,
        WhenFamily,
    }

    public enum BeforeExecutedCommand
    {
        None,
        ShowMessage,
        ThrowException,
        Cancel,
    }

    public enum ExecutedCommand
    {
        None,
        ShowMessage,
        ThrowException,
    }

    [AddINotifyPropertyChangedInterface]
    public class CreateBindingModel
    {
        [AlsoNotifyFor(nameof(Name), nameof(Id))]
        public PostableCommand PostableCommand { get; set; } = PostableCommand.ThinLines;
        public string Name => GetName();
        public uint Id => GetId();

        public CanExecuteCommand CanExecute { get; set; }
        public BeforeExecutedCommand BeforeExecuted { get; set; }
        public ExecutedCommand Executed { get; set; }
        public bool IsEnabled { get; set; } = false;

        private string GetName()
        {
            try
            {
                var revitCommandId = RevitCommandId.LookupPostableCommandId(PostableCommand);
                return revitCommandId.Name;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        private uint GetId()
        {
            try
            {
                return RevitCommandId.LookupPostableCommandId(PostableCommand).Id;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}