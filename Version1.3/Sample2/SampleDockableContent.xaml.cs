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

using AvalonDock;

namespace Sample2
{
    /// <summary>
    /// Interaction logic for SampleDockableContent.xaml
    /// </summary>
    public partial class SampleDockableContent : DockableContent
    {
        public SampleDockableContent()
        {
            InitializeComponent();

            this.DataContext = this;
        }

        public override void SaveLayout(System.Xml.XmlWriter storeWriter)
        {
            storeWriter.WriteAttributeString("TextSaved", txtTestFocus.Text);
            
            base.SaveLayout(storeWriter);
        }

        public override void RestoreLayout(System.Xml.XmlElement contentElement)
        {
            txtTestFocus.Text = contentElement.GetAttribute("TextSaved");
            
            base.RestoreLayout(contentElement);
        }
    }
}
