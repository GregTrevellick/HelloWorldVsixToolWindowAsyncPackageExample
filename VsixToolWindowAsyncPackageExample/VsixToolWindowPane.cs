﻿using Microsoft.VisualStudio.Shell;
using System;
using System.Runtime.InteropServices;

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

    [Guid("3905CB28-68F6-4DB4-967A-91E5684979CB")]
    public class VsixToolWindowPane : ToolWindowPane
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

        public VsixToolWindowPane(string message) : this()
        {
            //just before this breakpoint the IDE kicks into life 
            //when debugging 'message' is 'foo'
        }

        public VsixToolWindowPane() : base(null)
        {
            //SetLeagueModeCaption();

            // This is the user control hosted by the tool window; Note that, even if this class implements IDisposable, we are not calling Dispose on this object. 
            // This is because ToolWindowPane calls Dispose on the object returned by the Content property.
            Content = new ToolWindow1Control();// GetOptionsFromStoreAndMapToInternalFormatMethod, UpdateLastUpdatedDate, GetLastUpdatedDate);

            var toolWindow1ControlContent = (ToolWindow1Control) Content;
            //toolWindow1ControlContent.BossModeEventHandler += SetBossModeCaption;
            //toolWindow1ControlContent.LeagueModeEventHandler += SetLeagueModeCaption;
        }
    }
}
