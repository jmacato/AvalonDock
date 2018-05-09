//Copyright (c) 2007-2010, Adolfo Marinucci
//All rights reserved. See LICENSE.MD for licensing information.

using System;
using System.Collections.Generic;
using System.Text;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Documents;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Shapes;
using System.Linq;
using Avalonia.Markup;

namespace AvalonDock
{
    public abstract class PaneTabPanel : Panel
    {
        protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved)
        {
            base.OnVisualChildrenChanged(visualAdded, visualRemoved);

            ManagedContent mc = visualAdded as ManagedContent;
            if (mc != null)
            {
                mc.Style = TabItemStyle;
                mc.ApplyTemplate();
            }

        }

 
        internal PaneTabPanel()
        { 
            
        }

        #region TabItemStyle

        /// <summary>
        /// TabItemStyle Dependency Property
        /// </summary>
        public static readonly DependencyProperty TabItemStyleProperty =
            DependencyProperty.Register("TabItemStyle", typeof(Style), typeof(PaneTabPanel),
                new FrameworkPropertyMetadata((Style)null,
                    new PropertyChangedCallback(OnTabItemStyleChanged)));

        /// <summary>
        /// Gets or sets the TabItemStyle property.  This dependency property 
        /// indicates style to use for tabs.
        /// </summary>
        public Style TabItemStyle
        {
            get { return (Style)GetValue(TabItemStyleProperty); }
            set { SetValue(TabItemStyleProperty, value); }
        }

        /// <summary>
        /// Handles changes to the TabItemStyle property.
        /// </summary>
        private static void OnTabItemStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((PaneTabPanel)d).OnTabItemStyleChanged(e);
        }

        /// <summary>
        /// Provides derived classes an opportunity to handle changes to the TabItemStyle property.
        /// </summary>
        protected virtual void OnTabItemStyleChanged(DependencyPropertyChangedEventArgs e)
        {
            //Children.Cast<ManagedContent>().ForEach(c =>
            //    {
            //        Binding bnd = new Binding("TabItemStyle");
            //        bnd.Source = this;
            //        bnd.Mode = BindingMode.OneWay;

            //        c.SetBinding(StyleProperty, bnd);

            //        //c.Style = TabItemStyle;
            //    });
        }


        #endregion
    }
}
