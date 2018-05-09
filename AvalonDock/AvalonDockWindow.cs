using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Avalonia;
using Avalonia.Controls;

namespace AvalonDock
{
    public class AvalonDockWindow : Window
    {
        static AvalonDockWindow()
        {
            ShowInTaskbarProperty.OverrideMetadata(typeof(AvalonDockWindow), new StyledPropertyMetadata<bool>(false));
        }

        internal AvalonDockWindow()
        { 
            
        }
    }
}
