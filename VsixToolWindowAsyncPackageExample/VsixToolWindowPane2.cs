using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;

namespace VsixToolWindowAsyncPackageExample
{
    // This class implements the tool window exposed by this package and hosts a user control.
    // In Visual Studio tool windows are composed of a frame (implemented by the shell) and a pane, usually implemented by the package implementer.
    // This class derives from the ToolWindowPane class provided from the MPF in order to use its implementation of the IVsUIElementPane interface.

    //////////////////////////public ToolWindow1() : base(null)
    //////////////////////////{
    //////////////////////////    this.Caption = "ToolWindow1";
    //////////////////////////    this.Content = new ToolWindow1Control();
    //////////////////////////}

    [Guid("a7e33b03-7489-478d-bb22-57130d19ca29")]
    public class VsixToolWindowPane2 : ToolWindowPane
    {
        //public static Func<string, DateTime> GetLastUpdatedDate { get; set; }
        //public static Action<string> GetOptionsFromStoreAndMapToInternalFormatMethod { get; set; }
        //public static Action<string> UpdateLastUpdatedDate { get; set; }

        //private void SetBossModeCaption()
        //{
        //    Caption = "F-crash_.Data";
        //}

        //private void SetLeagueModeCaption()
        //{
        //    Caption = Vsix.Name;
        //}

        public VsixToolWindowPane2(string message) : this()
        {
            //just before this breakpoint the IDE kicks into life 
            //when debugging 'message' is 'foo'
        }

        public VsixToolWindowPane2() : base(null)
        {
            this.Caption = "VsixToolWindowPane2 VsixToolWindowAsyncPackageExample gregt1";

            // This is the user control hosted by the tool window; Note that, even if this class implements IDisposable,
            // we are not calling Dispose on this object. This is because ToolWindowPane calls Dispose on
            // the object returned by the Content property.
            this.Content = new ToolWindow1Control();
        }
    }
}
