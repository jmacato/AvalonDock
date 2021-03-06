﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Documents;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Navigation;
using Avalonia.Shapes;
using AvalonDock;

namespace Sample5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Tools_Click(object sender, RoutedEventArgs e)
        {
            if (dcTools.State != DockableContentState.Docked)
                dcTools.Show(dockManager, AnchorStyle.Top);
            dcTools.Activate();
        }

        private void ProjectExplorer_Click(object sender, RoutedEventArgs e)
        {
            if (dcProjectExplore.State != DockableContentState.Docked)
                dcProjectExplore.Show(dockManager, AnchorStyle.Top);
            dcProjectExplore.Activate();

        }

        private void PropertiesWindow_Click(object sender, RoutedEventArgs e)
        {
            if (dcPropertiesWindow.State != DockableContentState.Docked)
                dcPropertiesWindow.Show(dockManager, AnchorStyle.Bottom);
            dcPropertiesWindow.Activate();

        }
    }
}
