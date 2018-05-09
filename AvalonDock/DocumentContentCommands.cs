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
    /// Contains a list of commands that can be applied to a <see cref="DocumentContent"/>
    /// </summary>
    public sealed class DocumentContentCommands
    {
        static object syncRoot = new object();

        private static RoutedUICommand _floatingDocumentCommand = null;

        /// <summary>
        /// Shows the <see cref="DocumentContent"/> as a floating window document 
        /// </summary>
        public static RoutedUICommand FloatingDocument
        {
            get
            {
                lock (syncRoot)
                {
                    if (null == _floatingDocumentCommand)
                    {
                        _floatingDocumentCommand = new RoutedUICommand(AvalonDock.Properties.Resources.DocumentContentCommands_FloatingDocument, "FloatingDocument", typeof(DocumentContentCommands));
                    }
                }
                return _floatingDocumentCommand;
            }
        }


        private static RoutedUICommand _tabbedDocumentCommand = null;

        /// <summary>
        /// Shows the <see cref="DocumentContent"/> as a tabbed document 
        /// </summary>
        public static RoutedUICommand TabbedDocument
        {
            get
            {
                lock (syncRoot)
                {
                    if (null == _tabbedDocumentCommand)
                    {
                        _tabbedDocumentCommand = new RoutedUICommand(AvalonDock.Properties.Resources.DocumentContentCommands_TabbedDocument, "TabbedDocument", typeof(DocumentContentCommands));
                    }
                }
                return _tabbedDocumentCommand;
            }
        }        

    }
}
