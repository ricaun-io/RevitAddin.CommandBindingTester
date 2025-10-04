using Autodesk.Revit.UI;
using System;

namespace RevitAddin.CommandBindingTester.Models
{
    public class BindingModel
    {
        public string Name { get; internal set; }
        public uint Id { get; internal set; }
        public PostableCommand PostableCommand { get; internal set; }
        public bool CanHaveBinding { get; internal set; }
        public bool HasBinding { get; internal set; }

        override public string ToString()
        {
            return $"{Name} ({Id}) - CanHaveBinding: {CanHaveBinding}, HasBinding: {HasBinding}";
        }

        public static BindingModel Create(PostableCommand postableCommand)
        {
            var revitCommandId = RevitCommandId.LookupPostableCommandId(postableCommand);
            return new BindingModel()
            {
                Name = revitCommandId.Name,
                Id = revitCommandId.Id,
                PostableCommand = postableCommand,
                CanHaveBinding = revitCommandId.CanHaveBinding,
                HasBinding = revitCommandId.HasBinding
            };
        }
    }
}