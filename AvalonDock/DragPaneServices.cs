//Copyright (c) 2007-2010, Adolfo Marinucci
//All rights reserved. See LICENSE.MD for licensing information.

using System;
using System.Collections.Generic;
using System.Text;
using Avalonia;
using System.Diagnostics;
using System.ComponentModel;

namespace AvalonDock
{
    /// <summary>
    /// Provides drag-drop functionalities for dockable panes
    /// </summary>
    internal sealed class DragPaneServices
    {
        List<IDropSurface> Surfaces = new List<IDropSurface>();
        List<IDropSurface> SurfacesWithDragOver = new List<IDropSurface>();

        DockingManager _owner;

        public DockingManager DockManager
        {
            get { return _owner; }
        }

        public DragPaneServices(DockingManager owner)
        {
            // if (DesignerProperties.GetIsInDesignMode(owner))
            //     throw new NotSupportedException("DragPaneServices not valid in design mode");

            if (owner == null)
                throw new ArgumentNullException("owner");

            _owner = owner;
        }

        public void Register(IDropSurface surface)
        {
            if (!Surfaces.Contains(surface))
                Surfaces.Add(surface);
        }

        public void Unregister(IDropSurface surface)
        {
            Surfaces.Remove(surface);
        }

        Point Offset;

        public bool IsDragging { get; private set; }

        public void StartDrag(FloatingWindow wnd, Point point, Point offset)
        {
            Debug.Assert(!IsDragging);

            IsDragging = true;

            Offset = offset;

            _wnd = wnd;

            if (Offset.X >= _wnd.Width)
            {
                var ox = _wnd.Width / 2;
                Offset = new Point(ox, Offset.Y);
            }

            _wnd.Position = new Point(point.X - Offset.X,
                  point.Y - Offset.Y);
            _wnd.Show();

            int surfaceCount = 0;
        restart:
            surfaceCount = Surfaces.Count;
            foreach (IDropSurface surface in Surfaces)
            {
                if (surface.SurfaceRectangle.Contains(point))
                {
                    SurfacesWithDragOver.Add(surface);
                    surface.OnDragEnter(point);
                    Debug.WriteLine("Enter " + surface.ToString());
                    if (surfaceCount != Surfaces.Count)
                    {
                        //Surfaces list has been changed restart cycle
                        SurfacesWithDragOver.Clear();
                        goto restart;
                    }
                }
            }

        }

        public void MoveDrag(Point point)
        {
            if (_wnd == null)
                return;

            _wnd.Position = new
            Point(point.X - Offset.X,
                  point.Y - Offset.Y);

            List<IDropSurface> enteringSurfaces = new List<IDropSurface>();
            foreach (IDropSurface surface in Surfaces)
            {
                if (surface.SurfaceRectangle.Contains(point))
                {
                    if (!SurfacesWithDragOver.Contains(surface))
                        enteringSurfaces.Add(surface);
                    else
                        surface.OnDragOver(point);
                }
                else if (SurfacesWithDragOver.Contains(surface))
                {
                    SurfacesWithDragOver.Remove(surface);
                    surface.OnDragLeave(point);
                }
            }

            foreach (IDropSurface surface in enteringSurfaces)
            {
                SurfacesWithDragOver.Add(surface);
                surface.OnDragEnter(point);
            }
        }

        public void EndDrag(Point point)
        {
            IDropSurface dropSufrace = null;
            foreach (IDropSurface surface in Surfaces)
            {
                if (surface.SurfaceRectangle.Contains(point))
                {
                    if (surface.OnDrop(point))
                    {
                        dropSufrace = surface;
                        break;
                    }
                }
            }

            foreach (IDropSurface surface in SurfacesWithDragOver)
            {
                if (surface != dropSufrace)
                {
                    surface.OnDragLeave(point);
                }
            }

            SurfacesWithDragOver.Clear();

            _wnd.OnEndDrag();//notify floating window that drag operation is coming to end

            if (dropSufrace != null)
                _wnd.Close();
            else
            {
                _wnd.IsVisible = true;
                _wnd.Activate();
            }

            _wnd = null;

            IsDragging = false;

        }

        FloatingWindow _wnd;
        public FloatingWindow FloatingWindow
        {
            get { return _wnd; }
        }

    }
}
