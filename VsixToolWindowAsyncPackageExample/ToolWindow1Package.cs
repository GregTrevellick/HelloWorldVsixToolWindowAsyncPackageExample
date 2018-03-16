﻿using Microsoft.VisualStudio.Shell;
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
    //[ProvideOptionPage(typeof(GeneralOptions), Vsix.Name, CommonConstants.CategorySubLevelFootball, 0, 0, true)]
    [ProvideToolWindow(typeof(VsixToolWindowPane2), Style = VsDockStyle.Tabbed, Window = "3ae79031-e1bc-11d0-8f78-00a0c9110057")]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    #endregion
    public sealed partial class ToolWindow1Package : AsyncPackage
    {
        public const string PackageGuidString = "44f7531c-e8ac-4cc8-af51-8580f2bac0ef";

        public ToolWindow1Package()
        {
            //if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
            //{
            //    ChaseRating();
            //}
        }

        //this block moved to InitializeToolWindowAsync() where potentially expensive work, preferably done on a background thread where possible.
        //protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        //{
        //    await ToolWindow1Command.InitializeGregt(this);
        //    VsixToolWindowPane.GetOptionsFromStoreAndMapToInternalFormatMethod =
        //        any
        //            =>
        //        {
        //            var generalOptions = (GeneralOptions)GetDialogPage(typeof(GeneralOptions));
        //            ToolWindow1Control.LeagueGeneralOptions = GetLeagueGeneralOptions(generalOptions);
        //        };
        //    VsixToolWindowPane.UpdateLastUpdatedDate =
        //        any
        //            =>
        //        {
        //            var hiddenOptions = (HiddenOptions)GetDialogPage(typeof(HiddenOptions));
        //            hiddenOptions.LastUpdated = DateTime.Now;
        //            hiddenOptions.SaveSettingsToStorage();
        //        };
        //    VsixToolWindowPane.GetLastUpdatedDate =
        //        any
        //            =>
        //        {
        //            var hiddenOptions = (HiddenOptions)GetDialogPage(typeof(HiddenOptions));
        //            return hiddenOptions.LastUpdated;
        //        };
        //}


        //https://github.com/Microsoft/VSSDK-Analyzers/blob/master/doc/VSSDK003.md

        public override IVsAsyncToolWindowFactory GetAsyncToolWindowFactory(Guid toolWindowType)
        {
            if (toolWindowType == typeof(VsixToolWindowPane2).GUID)
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

            #region define actions/funcs for later on
            //VsixToolWindowPane.GetOptionsFromStoreAndMapToInternalFormatMethod =
            //    any
            //        =>
            //    {
            //        var generalOptions = (GeneralOptions)GetDialogPage(typeof(GeneralOptions));
            //        ToolWindow1Control.LeagueGeneralOptions = GetLeagueGeneralOptions(generalOptions);
            //    };
            //VsixToolWindowPane.UpdateLastUpdatedDate =
            //    any
            //        =>
            //    {
            //        var hiddenOptions = (HiddenOptions)GetDialogPage(typeof(HiddenOptions));
            //        hiddenOptions.LastUpdated = DateTime.Now;
            //        hiddenOptions.SaveSettingsToStorage();
            //    };
            //VsixToolWindowPane.GetLastUpdatedDate =
            //    any
            //        =>
            //    {
            //        var hiddenOptions = (HiddenOptions)GetDialogPage(typeof(HiddenOptions));
            //        return hiddenOptions.LastUpdated;
            //    };
            #endregion

            return "foo"; // this is passed to the tool window constructor
        }

        protected override string GetToolWindowTitle(Type toolWindowType, int id)//gregt - is this ever hit ? it ought to be !  
        {
            if (toolWindowType == typeof(VsixToolWindowPane2))
            {
                return "VsixToolWindowPane loading";
            }
            return base.GetToolWindowTitle(toolWindowType, id);
        }

        //private LeagueGeneralOptions GetLeagueGeneralOptions(GeneralOptions generalOptions)
        //{
        //    return new LeagueGeneralOptions
        //    {
        //        LeagueOptions = new List<LeagueOption>
        //        {
        //            //This is the sequence leagues appear in the UI
        //            WpfHelper.GetLeagueOption(generalOptions.InterestedInUk1, InternalLeagueCode.UK1),
        //            WpfHelper.GetLeagueOption(generalOptions.InterestedInUk2, InternalLeagueCode.UK2),
        //            WpfHelper.GetLeagueOption(generalOptions.InterestedInUk3, InternalLeagueCode.UK3),
        //            WpfHelper.GetLeagueOption(generalOptions.InterestedInUk4, InternalLeagueCode.UK4),
        //            WpfHelper.GetLeagueOption(generalOptions.InterestedInEs1, InternalLeagueCode.ES1),
        //            WpfHelper.GetLeagueOption(generalOptions.InterestedInNl1, InternalLeagueCode.NL1),
        //            WpfHelper.GetLeagueOption(generalOptions.InterestedInDe1, InternalLeagueCode.DE1),
        //            WpfHelper.GetLeagueOption(generalOptions.InterestedInDe2, InternalLeagueCode.DE2),
        //            WpfHelper.GetLeagueOption(generalOptions.InterestedInFr1, InternalLeagueCode.FR1),
        //            WpfHelper.GetLeagueOption(generalOptions.InterestedInFr2, InternalLeagueCode.FR2),
        //            WpfHelper.GetLeagueOption(generalOptions.InterestedInIt1, InternalLeagueCode.IT1),
        //            WpfHelper.GetLeagueOption(generalOptions.InterestedInIt2, InternalLeagueCode.IT2),
        //            WpfHelper.GetLeagueOption(generalOptions.InterestedInPt1, InternalLeagueCode.PT1),
        //            WpfHelper.GetLeagueOption(generalOptions.InterestedInUefa1, InternalLeagueCode.UEFA1),
        //            WpfHelper.GetLeagueOption(generalOptions.InterestedInBr1, InternalLeagueCode.BR1),
        //        }
        //    };
        //}

        //private void ChaseRating()
        //{
        //    var hiddenChaserOptions = (IRatingDetailsDto)GetDialogPage(typeof(HiddenRatingDetailsDto));
        //    var packageRatingChaser = new PackageRatingChaser();
        //    packageRatingChaser.Hunt(hiddenChaserOptions);
        //}
    }
}
