//Copyright (c) 2007-2010, Adolfo Marinucci
//All rights reserved. See LICENSE.MD for licensing information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace AvalonDock
{
    /// <summary>
    /// Defines commands that can be executed against a DockableContent
    /// </summary>
    public sealed class DockableContentCommands
    {
        static object syncRoot = new object();
        
        private static RoutedUICommand documentCommand = null;

        /// <summary>
        /// Shows the DockableContent as a tabbed document
        /// </summary>
        public static RoutedUICommand ShowAsDocument
        {
            get
            {
                lock (syncRoot)
                {
                    if (null == documentCommand)
                    {
                        documentCommand = new RoutedUICommand(AvalonDock.Properties.Resources.DockableContentCommands_ShowAsDocument, "Document", typeof(DockableContentCommands));
                    }
                }
                return documentCommand;
            }
        }


        private static RoutedUICommand floatingWindowCommand = null;
        
        /// <summary>
        /// Shows the <see cref="DockableContent"/> as a <see cref="FloatingWindow"/> which overlays the <see cref="DockingManager"/>
        /// </summary>
        /// <remarks>A floating window can't be redocked to the docking manager.</remarks>
        public static RoutedUICommand FloatingWindow
        {
            get
            {
                lock (syncRoot)
                {
                    if (null == floatingWindowCommand)
                    {
                        floatingWindowCommand = new RoutedUICommand(AvalonDock.Properties.Resources.DockableContentCommands_FloatingWindow, "FloatingWindow", typeof(DockableContentCommands));
                    }
                }
                return floatingWindowCommand;
            }
        }

        private static RoutedUICommand dockableWindowCommand = null;

        /// <summary>
        /// Shows the <see cref="DockableContent"/> as a <see cref="DockableFloatingWindow"/> which overlays the <see cref="DockingManager"/>
        /// </summary>
        /// <remarks>A floating window can't be redocked to the docking manager.</remarks>
        public static RoutedUICommand DockableFloatingWindow
        {
            get
            {
                lock (syncRoot)
                {
                    if (null == dockableWindowCommand)
                    {
                        dockableWindowCommand = new RoutedUICommand(AvalonDock.Properties.Resources.DockableContentCommands_DockableFloatingWindow, "DockableFloatingWindow", typeof(DockableContentCommands));
                    }
                }
                return dockableWindowCommand;
            }
        }

        private static RoutedUICommand autoHideCommand = null;

        /// <summary>
        /// Switch the state of a <see cref="DockableContent"/> from <see cref="DockableContentState.AutoHidden"/> to <see cref="DockableContentState.Docked"/> and viceversa
        /// </summary>
        /// <remarks>This command has the same effect as applying command <see cref="DockablePaneCommands.ToggleAutoHide"/> to container pane
        public static RoutedUICommand ToggleAutoHide
        {
            get
            {
                lock (syncRoot)
                {
                    if (null == autoHideCommand)
                    {
                        autoHideCommand = new RoutedUICommand(AvalonDock.Properties.Resources.DockableContentCommands_ToggleAutoHide, "AutoHide", typeof(DockableContentCommands));
                    }
                }
                return autoHideCommand;
            }
        }



    }
}
