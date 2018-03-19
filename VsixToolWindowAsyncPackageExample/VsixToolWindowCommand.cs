using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.ComponentModel.Design;
using Task = System.Threading.Tasks.Task;

namespace VsixToolWindowAsyncPackageExample
{
    internal sealed class VsixToolWindowCommand
    {
        public const int CommandId = 0x0100;
        public static readonly Guid CommandSet = new Guid("919cd8f4-e812-4b71-9734-0149032c7c8c");
        private static AsyncPackage _asyncPackage;

        public static VsixToolWindowCommand Instance { get; private set; }

        public static async Task InitializeGregt(AsyncPackage asyncPackage)
        {
            if (asyncPackage == null)
            {
                throw new ArgumentNullException(nameof(asyncPackage));
            }
            _asyncPackage = asyncPackage;

            OleMenuCommandService commandService = await asyncPackage.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new VsixToolWindowCommand(commandService);
        }

        private VsixToolWindowCommand(OleMenuCommandService commandService)
        {
            var commandId = new CommandID(CommandSet, CommandId);
            var menuItem = new OleMenuCommand(FindShowToolWindowAsync, commandId);
            commandService.AddCommand(menuItem);
        }

        /// <summary>
        /// Is hit when user selects Tools > Windows > VS Sports Desk
        /// </summary>
        private void FindShowToolWindowAsync(object sender, EventArgs e)
        {
            // Get the instance number 0 of this tool window. This window is single instance so this instance is actually the only one.
            // The last flag is set to true so that if the tool window does not exists it will be created.
            _asyncPackage.JoinableTaskFactory.RunAsync(async delegate
            {
                //var window = await _asyncPackage.ShowToolWindowAsync(typeof(VsixToolWindowPane), 0, true, _asyncPackage.DisposalToken);
                var toolWindowPane = await _asyncPackage.FindToolWindowAsync(typeof(VsixToolWindowPane), 0, true, _asyncPackage.DisposalToken);
                if (toolWindowPane?.Frame == null)
                {
                    throw new NotSupportedException("Cannot create tool window");
                }
                await _asyncPackage.JoinableTaskFactory.SwitchToMainThreadAsync();
                var windowFrame = (IVsWindowFrame)toolWindowPane.Frame;
                Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(windowFrame.Show());
            });
        }
    }
}
