//Copyright (c) 2007-2010, Adolfo Marinucci
//All rights reserved. See LICENSE.MD for licensing information.

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AvalonDock
{
    public class DockablePaneAnchorTabGroup : System.Windows.Controls.StackPanel
    {
        static DockablePaneAnchorTabGroup()
        {
            //This OverrideMetadata call tells the system that this element wants to provide a style that is different than its base class.
            //This style is defined in themes\generic.xaml
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DockablePaneAnchorTabGroup), new FrameworkPropertyMetadata(typeof(DockablePaneAnchorTabGroup)));
        }

        DockablePane _pane = null;

        internal DockablePane ReferencedPane
        {
            get
            { return _pane; }
            set { _pane = value; }
        }

        protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved)
        {
            base.OnVisualChildrenChanged(visualAdded, visualRemoved);
        }
    }
}
