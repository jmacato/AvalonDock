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
    public class DockableTabPanel : PaneTabPanel
    {
        protected override Size MeasureOverride(Size availableSize)
        {
            double totWidth = 0;

            if (base.VisualChildrenCount == 0)
                return base.MeasureOverride(availableSize);


            var childsOrderedByWidth = new List<FrameworkElement>();

            foreach (FrameworkElement child in Children)
            {
                child.Measure(new Size(double.PositiveInfinity, availableSize.Height));
                totWidth += child.DesiredSize.Width - child.Margin.Left - child.Margin.Right;
                childsOrderedByWidth.Add(child);
            }

            if (totWidth > availableSize.Width)
            {
                foreach (FrameworkElement child in Children)
                {
                    child.Measure(new Size(availableSize.Width / Children.Count, availableSize.Height));
                }
            }

            return base.MeasureOverride(availableSize);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            double offsetX = 0;

            foreach (FrameworkElement child in Children)
            {
                double childFinalWidth = child.DesiredSize.Width;
                child.Arrange(new Rect(offsetX, 0, childFinalWidth, finalSize.Height));

                offsetX += child.DesiredSize.Width;
            }

            return base.ArrangeOverride(finalSize);
        }

    }
}
