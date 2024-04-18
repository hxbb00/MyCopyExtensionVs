using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.ComponentModel.Design;
using System.IO;
using System.Windows.Forms;
using Task = System.Threading.Tasks.Task;

namespace MyCopyExtensionVs.Commands
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class CommandFolderNodeCopyLinuxPath
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0302;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("97e2ae24-8468-4209-b875-5349da664640");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly AsyncPackage package;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandFolderNodeCopyName"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        private CommandFolderNodeCopyLinuxPath(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(this.Execute, menuCommandID);
            commandService.AddCommand(menuItem);
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static CommandFolderNodeCopyLinuxPath Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private Microsoft.VisualStudio.Shell.IAsyncServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }
        private IServiceProvider ServiceProviderSys => this.package;
        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static async Task InitializeAsync(AsyncPackage package)
        {
            // Switch to the main thread - the call to AddCommand in Command1's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new CommandFolderNodeCopyLinuxPath(package, commandService);
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void Execute(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            var selectedItems = (ServiceProviderSys.GetService(typeof(SDTE)) as DTE2)?.SelectedItems;
            if (selectedItems == null || selectedItems.Count == 0)
            {
                return;
            }

            FileInfo path = null;
            try
            {
                foreach (SelectedItem selectedItem in selectedItems)
                {
                    path = ProjectSettings.GetSelectedItemPath(selectedItem);
                }
            }
            catch (FileNotFoundException)
            {
                path = ProjectSettings.LookingForSelectedItem(this.ServiceProviderSys);
            }
            Clipboard.SetText(path.FullName.Replace("\\", "/"));
        }
    }
}
