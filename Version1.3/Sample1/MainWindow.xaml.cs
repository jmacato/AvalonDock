using System;
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
using Microsoft.Win32;
using System.IO;

namespace Sample1
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


        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Text Files (*.txt)|*.txt";

            if (dlg.ShowDialog().GetValueOrDefault())
            {
                using (StreamReader sr = new StreamReader(dlg.FileName))
                {
                    var doc = new Document() { Title = System.IO.Path.GetFileName(dlg.FileName) };
                    doc.Show(dockingManager);

                    doc.TextContent = sr.ReadToEnd();
                    doc.Activate();
                }
            }
        }

        private void New_click(object sender, RoutedEventArgs e)
        {
            string title = "newDoc";
            int i = 0;
            while (dockingManager.Documents.Any(d => d.Title == title))
            {
                title = "newDoc" + i.ToString();
                i++;
            }

            var doc = new Document() { Title = title };
            doc.Show(dockingManager);
            doc.Activate();
        } 
    }
}
