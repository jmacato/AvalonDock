//Copyright (c) 2007-2010, Adolfo Marinucci
//All rights reserved. See LICENSE.MD for licensing information.

using System;
using System.Collections.Generic;
using System.Text;

namespace AvalonDock
{
    /// <summary>
    /// Defines a list of context menu resources
    /// </summary>
    public enum ContextMenuElement
    {
        /// <summary>
        /// Context menu related to a <see cref="DockablePane"/>
        /// </summary>
        DockablePane,

        /// <summary>
        /// Context menu related to a <see cref="DocumentPane"/>
        /// </summary>
        DocumentPane,

        /// <summary>
        /// Context menu related to a <see cref="DockableFloatingWindow"/>
        /// </summary>
        DockableFloatingWindow,

        /// <summary>
        /// Context menu related to a <see cref="DocumentFloatingWindow"/>
        /// </summary>
        DocumentFloatingWindow

    }
}
