using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;

namespace VsixToolWindowAsyncPackageExample
{
    [Guid("a7e33b03-7489-478d-bb22-57130d19ca29")]
    public class VsixToolWindowPane : ToolWindowPane
    {
        public VsixToolWindowPane(string message) : this()
        {
            //just before this breakpoint the IDE kicks into life 
            //when debugging 'message' is 'foo'
        }

        public VsixToolWindowPane() : base(null)
        {
            this.Caption = "VsixToolWindowAsyncPackageExample_Caption";
            this.Content = new ToolWindow1Control();
        }
    }
}
