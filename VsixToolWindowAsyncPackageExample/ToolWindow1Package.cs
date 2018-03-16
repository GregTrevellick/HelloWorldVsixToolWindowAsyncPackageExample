using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace VsixToolWindowAsyncPackageExample
{
    #region attributes
    [Guid(ToolWindow1Package.PackageGuidString)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
    [PackageRegistration(UseManagedResourcesOnly = true)]//, AllowsBackgroundLoading = true)]
    [ProvideAutoLoad(UIContextGuids.NoSolution, PackageAutoLoadFlags.BackgroundLoad)]//UIContextGuids.NoSolution vs VSConstants.UICONTEXT.NoSolution_string
    [ProvideMenuResource("Menus.ctmenu", 1)]//[ProvideMenuResource(1000, 1)]
    [ProvideToolWindow(typeof(VsixToolWindowPane), Style = VsDockStyle.Tabbed, Window = "3ae79031-e1bc-11d0-8f78-00a0c9110057")]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    #endregion
    public sealed partial class ToolWindow1Package : AsyncPackage
    {
        public const string PackageGuidString = "44f7531c-e8ac-4cc8-af51-8580f2bac0ef";

        public ToolWindow1Package()
        {
        }

        public override IVsAsyncToolWindowFactory GetAsyncToolWindowFactory(Guid toolWindowType)
        {
            if (toolWindowType == typeof(VsixToolWindowPane).GUID)
            {
                return this;
            }
            //we always return above so next line superfluous, and for now can ignore the non-await light bulb suggestion
            return base.GetAsyncToolWindowFactory(toolWindowType);
        }

        protected override async Task<object> InitializeToolWindowAsync(Type toolWindowType, int id, CancellationToken cancellationToken)
        {
            //potentially expensive work, preferably done on a background thread where possible.

            //await Task.Delay(5000, cancellationToken);
            await VsixToolWindowCommand.InitializeGregt(this);

            return "foo"; // this is passed to the tool window constructor
        }

        protected override string GetToolWindowTitle(Type toolWindowType, int id)//gregt - is this ever hit ? it ought to be !  
        {
            if (toolWindowType == typeof(VsixToolWindowPane))
            {
                return "VsixToolWindowPane loading";
            }
            return base.GetToolWindowTitle(toolWindowType, id);
        }
    }
}
