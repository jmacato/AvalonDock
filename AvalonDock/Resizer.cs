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

namespace AvalonDock
{
    public class Resizer : Thumb
    {
        static Resizer()
        {
            //This OverrideMetadata call tells the system that this element wants to provide a style that is different than its base class.
            //This style is defined in themes\generic.xaml
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Resizer), new FrameworkPropertyMetadata(typeof(Resizer)));
            MinWidthProperty.OverrideMetadata(typeof(Resizer), new FrameworkPropertyMetadata(6.0, FrameworkPropertyMetadataOptions.AffectsParentMeasure));
            MinHeightProperty.OverrideMetadata(typeof(Resizer), new FrameworkPropertyMetadata(6.0, FrameworkPropertyMetadataOptions.AffectsParentMeasure));
            HorizontalAlignmentProperty.OverrideMetadata(typeof(Resizer), new FrameworkPropertyMetadata(HorizontalAlignment.Stretch, FrameworkPropertyMetadataOptions.AffectsParentMeasure));
            VerticalAlignmentProperty.OverrideMetadata(typeof(Resizer), new FrameworkPropertyMetadata(VerticalAlignment.Stretch, FrameworkPropertyMetadataOptions.AffectsParentMeasure));
        }

    }
}
