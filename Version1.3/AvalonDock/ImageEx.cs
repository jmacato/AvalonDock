﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Avalonia.Controls;
using Avalonia;

namespace AvalonDock
{
    public class ImageEx : Image
    {
        static ImageEx()
        { 
#if NET4
            UseLayoutRoundingProperty.OverrideMetadata(typeof(ImageEx), new FrameworkPropertyMetadata(true));
#endif
        }
    }
}
