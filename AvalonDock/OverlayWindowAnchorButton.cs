//Copyright (c) 2007-2010, Adolfo Marinucci
//All rights reserved. See LICENSE.MD for licensing information.

using System;
using System.Collections.Generic;
using System.Text;
using Avalonia;

namespace AvalonDock
{
    class OverlayWindowDockingButton : IDropSurface
    {
        OverlayWindow _owner;
        FrameworkElement _btnDock;

        public OverlayWindowDockingButton(FrameworkElement btnDock, OverlayWindow owner)
            : this(btnDock, owner, true)
        {

        }
        public OverlayWindowDockingButton(FrameworkElement btnDock, OverlayWindow owner, bool enabled)
        {
            if (btnDock == null)
                throw new ArgumentNullException("btnDock");
            if (owner == null)
                throw new ArgumentNullException("owner");

            _btnDock = btnDock;
            _owner = owner;
            Enabled = enabled;
        }

        bool _enabled = true;

        public bool Enabled
        {
            get { return _enabled; }
            set 
            {
                _enabled = value;

                if (_enabled)
                    _btnDock.Visibility = Visibility.Visible;
                else
                    _btnDock.Visibility = Visibility.Hidden;
            }
        }



        #region IDropSurface Membri di



        Rect IDropSurface.SurfaceRectangle
        {
            get
            {
                if (!(this as IDropSurface).IsSurfaceVisible)
                    return Rect.Empty;

                if (PresentationSource.FromVisual(_btnDock) == null)
                    return Rect.Empty;

                return new Rect(HelperFunc.PointToScreenWithoutFlowDirection(_btnDock, new Point()), new Size(_btnDock.ActualWidth, _btnDock.ActualHeight));
            }
        }

        void IDropSurface.OnDragEnter(Point point)
        {
            if (!Enabled)
                return;

            _owner.OnDragEnter(this, point);
        }

        void IDropSurface.OnDragOver(Point point)
        {
            if (!Enabled)
                return;

            _owner.OnDragOver(this, point);
        }

        void IDropSurface.OnDragLeave(Point point)
        {
            if (!Enabled)
                return;

            _owner.OnDragLeave(this, point);
        }

        bool IDropSurface.OnDrop(Point point)
        {
            if (!Enabled)
                return false;

            return _owner.OnDrop(this, point);
        }

        bool IDropSurface.IsSurfaceVisible
        {
            get { return (_owner.IsLoaded && _btnDock != null); }
        }

        #endregion
    }
}
