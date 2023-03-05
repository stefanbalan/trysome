using System.Collections.Generic;
using Microsoft.VisualStudio.Imaging;

using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace KeySome
{
    public class KeyEditorWindow : BaseToolWindow<KeyEditorWindow>
    {
        private IEnumerable<KeyItem> commandList;
        public override string GetTitle(int toolWindowId) => "Key map editor";

        public override Type PaneType => typeof(Pane);

        public override async Task<FrameworkElement> CreateAsync(int toolWindowId, CancellationToken cancellationToken)
        {
            commandList = await Commands.GetCommandsAsync();
            var toolWindow1Control = new ToolWindow1Control();
            toolWindow1Control.SetItems(commandList);

            
            return toolWindow1Control;
        }

        [Guid("dd182121-31ca-4f13-a9ff-3e27e1510b01")]
        internal class Pane : ToolkitToolWindowPane
        {
            public Pane()
            {
                BitmapImageMoniker = KnownMonikers.Keyboard;
            }
        }
        
    }
}
