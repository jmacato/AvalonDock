//Copyright (c) 2007-2010, Adolfo Marinucci
//All rights reserved. See LICENSE.MD for licensing information.s

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
using System.Diagnostics;
using System.Linq;
using Avalonia.Markup.Primitives;

namespace AvalonDock
{
    public class DocumentPane : Pane
    {
        static DocumentPane()
        {
            //This OverrideMetadata call tells the system that this element wants to provide a style that is different than its base class.
            //This style is defined in themes\generic.xaml
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DocumentPane), new FrameworkPropertyMetadata(typeof(DocumentPane)));

        }

        public DocumentPane()
        {
            this.Loaded += new RoutedEventHandler(DocumentPane_Loaded);
        }

        void DocumentPane_Loaded(object sender, RoutedEventArgs e)
        {
            if (Parent == null)
                return;

            if (GetManager() == null)
                return;
                //throw new InvalidOperationException("DocumentPane must be put under a DockingManager!");

            //try to set this as main document pane
            if (GetManager().MainDocumentPane == null)
            {
                GetManager().MainDocumentPane = this;
                NotifyPropertyChanged("IsMainDocumentPane");
            }
            else
            {
                //or ensure that this document pane is under or at the same level of the MainDocumentPane
                GetManager().EnsurePanePositionIsValid(this);
            }
        }

        /// <summary>
        /// Returns a value indicating if this pane is the main document pane
        /// </summary>
        /// <remarks>The main document pane is the default pane that remains always visible.</remarks>
        public bool? IsMainDocumentPane
        {
            get 
            {
                if (GetManager() == null)
                    return null;

                return GetManager().MainDocumentPane == this;
            }
        }


        #region ContainsActiveDocument

        internal void RefreshContainsActiveDocumentProperty()
        { 
            SetContainsActiveDocument(
                Items.Cast<ManagedContent>().FirstOrDefault(d => d.IsActiveDocument) != null);
            
            if (Items.Count > 0)
                Debug.WriteLine(string.Format("{0} ContainsActiveDocument ={1}", (Items[0] as ManagedContent).Title, ContainsActiveDocument));
        }

        
        /// <summary>
        /// ContainsActiveDocument Read-Only Dependency Property
        /// </summary>
        /// <remarks>This property is especially intended for use in restyling.</remarks>
        private static readonly DependencyPropertyKey ContainsActiveDocumentPropertyKey
            = DependencyProperty.RegisterReadOnly("ContainsActiveDocument", typeof(bool), typeof(DocumentPane),
                new FrameworkPropertyMetadata((bool)false,
                    new PropertyChangedCallback(OnContainsActiveDocumentChanged)));

        public static readonly DependencyProperty ContainsActiveDocumentProperty
            = ContainsActiveDocumentPropertyKey.DependencyProperty;

        /// <summary>
        /// Gets the ContainsActiveDocument property.  This dependency property 
        /// indicates if this pane contains a <see cref="DocumentPane"/> that has <see cref="ManagedContent.IsActiveDocument"/> property set to true.   
        /// </summary>
        public bool ContainsActiveDocument
        {
            get { return (bool)GetValue(ContainsActiveDocumentProperty); }
        }

        /// <summary>
        /// Provides a secure method for setting the ContainsActiveDocument property.  
        /// This dependency property indicates if this pane contains a <see cref="DocumentPane"/> that has <see cref="ManagedContent.IsActiveDocument"/> property set to true.
        /// </summary>
        /// <param name="value">The new value for the property.</param>
        protected void SetContainsActiveDocument(bool value)
        {
            SetValue(ContainsActiveDocumentPropertyKey, value);
        }

        /// <summary>
        /// Handles changes to the ContainsActiveDocument property.
        /// </summary>
        private static void OnContainsActiveDocumentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DocumentPane)d).OnContainsActiveDocumentChanged(e);
        }

        /// <summary>
        /// Provides derived classes an opportunity to handle changes to the ContainsActiveDocument property.
        /// </summary>
        protected virtual void OnContainsActiveDocumentChanged(DependencyPropertyChangedEventArgs e)
        {
            NotifyPropertyChanged("ContainsActiveDocument");
        }

        #endregion
     
        protected override void OnInitialized(EventArgs e)
        {
            this.CommandBindings.Add(
                new CommandBinding(DocumentPaneCommands.CloseThis, OnExecuteCommand, OnCanExecuteCommand));
            this.CommandBindings.Add(
                new CommandBinding(DocumentPaneCommands.CloseAllButThis, this.OnExecuteCommand, this.OnCanExecuteCommand));
            this.CommandBindings.Add(
                new CommandBinding(DocumentPaneCommands.NewHorizontalTabGroup, this.OnExecuteCommand, this.OnCanExecuteCommand));
            this.CommandBindings.Add(
                new CommandBinding(DocumentPaneCommands.NewVerticalTabGroup, this.OnExecuteCommand, this.OnCanExecuteCommand));


            base.OnInitialized(e);
        }

        #region DocumentPane Commands

        //#region Show Document Window List Command

        //public static RoutedCommand ShowDocumentsListMenuCommand = new RoutedCommand();

        //public void ExecutedShowDocumentsListMenuCommand(object sender,
        //    ExecutedRoutedEventArgs e)
        //{
        //    //MessageBox.Show("ShowOptionsMenu");
        //    ShowDocumentsListMenu();
        //}


        //public void CanExecuteShowDocumentsListMenuCommand(object sender,
        //    CanExecuteRoutedEventArgs e)
        //{
        //    e.CanExecute = true;
        //}

        //#endregion

        //#region Close Command

        ////ApplicationCommands.Close command....

        //public void ExecutedCloseCommand(object sender,
        //    ExecutedRoutedEventArgs e)
        //{
        //}


        //public void CanExecuteCloseCommand(object sender,
        //    CanExecuteRoutedEventArgs e)
        //{
        //    e.CanExecute = true;
        //}

        //#endregion

        #region Commands

        protected override void OnExecuteCommand(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == DocumentPaneCommands.CloseThis)
            {
                CloseThis(e.Parameter as ManagedContent);
                e.Handled = true;
            }
            else if (e.Command == DocumentPaneCommands.CloseAllButThis)
            {
                CloseAllButThis();
                e.Handled = true;
            }
            else if (e.Command == DocumentPaneCommands.NewHorizontalTabGroup)
            {
                CreateNewHorizontalTabGroup();
                e.Handled = true;
            }
            else if (e.Command == DocumentPaneCommands.NewVerticalTabGroup)
            {
                CreateNewVerticalTabGroup();
                e.Handled = true;
            }
            //else if (e.Command == DocumentPaneCommands.ActivateDocument)
            //{
            //    ManagedContent doc = e.Parameter as ManagedContent;
            //    if (doc != null)
            //    {
            //        doc.Activate();
            //    }
            //}

        }

        protected override void OnCanExecuteCommand(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.GetManager() != null;

            if (e.CanExecute)
            {
                if (e.Command == DocumentPaneCommands.NewHorizontalTabGroup ||
                    e.Command == DocumentPaneCommands.NewVerticalTabGroup ||
                    e.Command == DocumentPaneCommands.CloseAllButThis)
                {
                    if (this.Items.Count <= 1)
                        e.CanExecute = false;
                }
            }
        }

        public void Close()
        {
            CloseThis(null);
        }

        void CloseThis(ManagedContent contentToClose)
        {
            if (GetManager() == null)
                return;

            if (contentToClose == null)
                contentToClose = SelectedItem as ManagedContent;

            DockableContent dockableContent = contentToClose as DockableContent;

            if (dockableContent != null)
                dockableContent.Close();
            else
            {
                DocumentContent documentContent = contentToClose as DocumentContent;
                documentContent.Close();
            }

        }

        void CloseAllButThis()
        {
            DocumentContent activeContent = GetManager().ActiveDocument as DocumentContent;
            foreach (DocumentContent cnt in this.GetManager().Documents.ToArray())
            {
                if (cnt != activeContent)
                    cnt.Close();
            }
        }

        public DocumentPane CreateNewHorizontalTabGroup()
        {
            var activeContent = SelectedItem as ManagedContent;
            var oldContainerPane = activeContent.ContainerPane as DocumentPane;
            var newContainerPane = new DocumentPane();
            
            oldContainerPane.RemoveContent(activeContent);
            newContainerPane.Items.Add(activeContent);

            GetManager().Anchor(newContainerPane, this, AnchorStyle.Bottom);

            activeContent.Activate();
            newContainerPane.RefreshContainsActiveContentProperty();
            newContainerPane.RefreshContainsActiveDocumentProperty();
            oldContainerPane.RefreshContainsActiveContentProperty();
            oldContainerPane.RefreshContainsActiveDocumentProperty();

            return newContainerPane;
        }

        public DocumentPane CreateNewVerticalTabGroup()
        {
            var activeContent = SelectedItem as ManagedContent;
            var oldContainerPane = activeContent.ContainerPane as DocumentPane;
            var newContainerPane = new DocumentPane();

            oldContainerPane.RemoveContent(activeContent);
            newContainerPane.Items.Add(activeContent);

            GetManager().Anchor(newContainerPane, this, AnchorStyle.Right);
            
            activeContent.Activate();
            newContainerPane.RefreshContainsActiveContentProperty();
            newContainerPane.RefreshContainsActiveDocumentProperty();
            oldContainerPane.RefreshContainsActiveContentProperty();
            oldContainerPane.RefreshContainsActiveDocumentProperty();


            return newContainerPane;
        }

        #endregion    

        #endregion

        Button _optionsContextMenuPlacementTarget;

        public override void OnApplyTemplate()
        {
            _optionsContextMenuPlacementTarget = GetTemplateChild("PART_ShowContextMenuButton") as Button;

            if (_optionsContextMenuPlacementTarget != null)
            {
                _optionsContextMenuPlacementTarget.Click += (s, e) => { ShowDocumentsListMenu(); };
            }

            base.OnApplyTemplate();
        }

        void ShowDocumentsListMenu()
        {
            if (Items.Count == 0)
                return; //nothings to show

            ContextMenu cxMenuDocuments = (ContextMenu)TryFindResource("DocumentsListMenu");
            if (cxMenuDocuments != null)
            {
                //cxMenuDocuments.ItemsSource = Items.OfType<ManagedContent>().OrderBy(c => c.Title);
                cxMenuDocuments.Items.Clear();
                Items.OfType<ManagedContent>().OrderBy(c => c.Title).ForEach(
                    c =>
                    {
                        cxMenuDocuments.Items.Add(new MenuItem()
                        {
                            Header = c.Title,
                            Command = ManagedContentCommands.Activate,
                            CommandTarget = c,
                            Icon = new Image()
                            {
                                Source = c.Icon,
                                Width = 16,
#if NET4
                                UseLayoutRounding = true
#endif
                            }
                        });
                    });
                //cxMenuDocuments.CommandBindings.Add(new CommandBinding(ActivateDocumentCommand, new ExecutedRoutedEventHandler(this.ExecutedActivateDocumentCommand), new CanExecuteRoutedEventHandler(CanExecuteActivateDocumentCommand)));

                if (_optionsContextMenuPlacementTarget != null)
                {
                    cxMenuDocuments.Placement = PlacementMode.Bottom;
                    cxMenuDocuments.PlacementTarget = _optionsContextMenuPlacementTarget;
                }
                else
                {
                    cxMenuDocuments.Placement = PlacementMode.MousePoint;
                    cxMenuDocuments.PlacementTarget = this;
                }
                
                cxMenuDocuments.IsOpen = true;
                cxMenuDocuments.Closed += new RoutedEventHandler(cxMenuDocuments_Closed);
            }
        }

        /// <summary>
        /// Clear context menu that shows documents list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cxMenuDocuments_Closed(object sender, RoutedEventArgs e)
        {
            ContextMenu cxMenuDocuments = (ContextMenu)sender;
            cxMenuDocuments.Items.Clear();
            cxMenuDocuments.Closed -= new RoutedEventHandler(cxMenuDocuments_Closed);
        }


        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            if (!e.Handled)
            {
                if (_partHeader != null &&
                    !_partHeader.IsMouseOver)
                {
                    //prevent document content to start dragging when it is clicked outside of the header area
                    e.Handled = true;
                }
            }
        }

        internal void CheckContentsEmpty()
        { 
            if (Items.Count == 0)
            {
                bool isMainDocPaneToBeClosed = IsMainDocumentPane.HasValue &&
                    IsMainDocumentPane.Value;

                if (isMainDocPaneToBeClosed)
                {
                    DockingManager manager = GetManager();
                    DocumentPane candidateNewMainDocPane = manager.FindAnotherLogicalChildContained<DocumentPane>(this);
                    if (candidateNewMainDocPane != null && 
                        candidateNewMainDocPane.GetManager() == this.GetManager())
                    {
                        ResizingPanel containerPanel = Parent as ResizingPanel;
                        if (containerPanel != null)
                            containerPanel.RemoveChild(this);

                        manager.MainDocumentPane = candidateNewMainDocPane;
                        candidateNewMainDocPane.NotifyPropertyChanged("IsMainDocumentPane");
                    }
                }
                else
                {
                    ResizingPanel containerPanel = Parent as ResizingPanel;
                    if (containerPanel != null)
                        containerPanel.RemoveChild(this);
                }
            }

        }


        internal override ResizingPanel GetContainerPanel()
        {
            return GetParentDocumentPaneResizingPanel();
        }

        internal DocumentPaneResizingPanel GetParentDocumentPaneResizingPanel()
        {
            ResizingPanel parentPanel = LogicalTreeHelper.GetParent(this) as ResizingPanel;
            
            if (parentPanel == null)
                return null;

            while (!(parentPanel is DocumentPaneResizingPanel))
            {
                parentPanel = LogicalTreeHelper.GetParent(parentPanel) as ResizingPanel;

                if (parentPanel == null)
                    return null;
            }

            return parentPanel as DocumentPaneResizingPanel;
        }

        protected override bool IsSurfaceVisible
        {
            get 
            {
                if (IsMainDocumentPane.HasValue &&
                    !IsMainDocumentPane.Value &&
                    Items.Count == 0)
                    return false;

                return true; 
            }
        }

        protected override Rect SurfaceRectangle
        {
            get
            {
                return base.SurfaceRectangle;
                //it is dragging a document let drop in this document pane
                //if (GetManager().DragPaneServices.FloatingWindow is DocumentFloatingWindow)
                //    return base.SurfaceRectangle;

                ////otherwise we should provide a drop surface for all the DocumentPaneResizingPanel
                //DocumentPaneResizingPanel parentPanel = GetParentDocumentPaneResizingPanel();

                //if (parentPanel == null)
                //    return base.SurfaceRectangle;

                //return new Rect(HelperFunc.PointToScreenWithoutFlowDirection(parentPanel, new Point(0, 0)), new Size(parentPanel.ActualWidth, parentPanel.ActualHeight));
                //return new Rect(parentPanel.PointToScreen(new Point(0, 0)), new Size(parentPanel.ActualWidth, parentPanel.ActualHeight));
            }
        }

        protected override void OnItemsChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (object newItem in e.NewItems)
                {
                    if (!(newItem is DockableContent) &&
                        !(newItem is DocumentContent))
                        throw new InvalidOperationException("DocumentPane can contain only DockableContents or DocumentContents!");

                    if (newItem is DockableContent &&
                        (((DockableContent)newItem).DockableStyle & DockableStyle.Document) == 0)
                    {
                        ((DockableContent)newItem).DockableStyle |= DockableStyle.Document;
                    }

                    var dockContent = newItem as DockableContent;
                    if (dockContent != null)
                        dockContent.SetStateToDocument();
                }
            }

            RefreshContainsActiveDocumentProperty();

            //if (Items.Count > 0 &&
            //    SelectedItem == null && 
            //    GetManager() != null)
            //{
            //    //get previously activated content
            //    var documentToActivate = Items.OrderBy(d => d.LastActivation).LastOrDefault(d => d != cntDeactivated);

            //    Debug.WriteLine(string.Format("Activated Document '{0}'", documentToActivate != null ? documentToActivate.Title : "<none>"));

            //    if (documentToActivate != null)
            //        documentToActivate.Activate();
            //    //ActiveDocument = MainDocumentPane.SelectedItem as ManagedContent;
            //}

            //foreach (var pi in GetType().GetFields(System.Reflection.BindingFlags.FlattenHierarchy | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static))
            //{ 
            //    var dpi = pi.GetValue(this) as DependencyProperty;
            //    if (dpi != null)
            //        Debug.WriteLine(string.Format("{0} - {1}", dpi.Name, dpi.GlobalIndex));
            //    var dkpi = pi.GetValue(this) as DependencyPropertyKey;
            //    if (dkpi != null)
            //        Debug.WriteLine(string.Format("{0} - {1}", dkpi.DependencyProperty.Name, dkpi.DependencyProperty.GlobalIndex));
            //}


            foreach (var dp in GetDependencyProperties(this))
            {
                Debug.WriteLine(string.Format("{0} - {1}", dp.Name, dp.GlobalIndex));
            }
            foreach (var dp in GetAttachedProperties(this))
            {
                Debug.WriteLine(string.Format("{0} - {1}", dp.Name, dp.GlobalIndex));
            }
            base.OnItemsChanged(e);
        }


        public static List<DependencyProperty> GetDependencyProperties(Object element)
        { 
            List<DependencyProperty> properties = new List<DependencyProperty>(); 
            MarkupObject markupObject = MarkupWriter.GetMarkupObjectFor(element); 
            if (markupObject != null) 
            {
                foreach (MarkupProperty mp in markupObject.Properties)
                { 
                    if (mp.DependencyProperty != null)
                    { properties.Add(mp.DependencyProperty); } 
                } 
            } 
            return properties; 
        }         

        public static List<DependencyProperty> GetAttachedProperties(Object element) 
        { 
            List<DependencyProperty> attachedProperties = new List<DependencyProperty>(); 
            MarkupObject markupObject = MarkupWriter.GetMarkupObjectFor(element);
            if (markupObject != null)
            {
                foreach (MarkupProperty mp in markupObject.Properties) 
                {
                    if (mp.IsAttached) 
                    { attachedProperties.Add(mp.DependencyProperty); }
                }
            } 
            return attachedProperties;
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            ManagedContent selectedContent = this.SelectedItem as ManagedContent;
            if (selectedContent != null && GetManager() != null)
                GetManager().ActiveDocument = selectedContent;

            base.OnSelectionChanged(e);
        }


        public override bool OpenOptionsMenu(UIElement menuTarget)
        {
            if (cxOptions == null)
            {
                cxOptions = TryFindResource(new ComponentResourceKey(typeof(DockingManager), ContextMenuElement.DocumentPane)) as ContextMenu;
            }
           
            return base.OpenOptionsMenu(menuTarget);
        }
    }
}
