﻿/* 
* Copyright (c) Marc Clifton
* The Code Project Open License (CPOL) 1.02
* http://www.codeproject.com/info/cpol10.aspx
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using Newtonsoft.Json;

using Clifton.Core.ExtensionMethods;

namespace FlowSharpLib
{
    public static class GraphicElementHelpers
    {
        public static void Erase(this Bitmap background, Canvas canvas, Rectangle r)
        {
            canvas.DrawImage(background, r);
            background.Dispose();
        }
    }

    public class PropertiesChangedEventArgs : EventArgs
    {
        public GraphicElement GraphicElement { get; protected set; }

        public PropertiesChangedEventArgs(GraphicElement el)
        {
            GraphicElement = el;
        }
    }

    public class GraphicElement : IDisposable
    {
        // public EventHandler<PropertiesChangedEventArgs> PropertiesChanged;

        public Guid Id { get; set; }
        public virtual bool Selected { get; protected set; }
        public bool Tagged { get; protected set; }
        public bool Visible { get; set; }                   // Used at the moment to hide collapsed groupbox elements.
        public bool ShowConnectionPoints { get; set; }
        // public bool HideConnectionPoints { get; set; }
        public bool ShowAnchors { get; set; }
        public Canvas Canvas { get { return canvas; } }

        // This is probably a ridiculous optimization -- should just grow pen width + connection point size / 2
        // public virtual Rectangle UpdateRectangle { get { return DisplayRectangle.Grow(BorderPen.Width + ((ShowConnectionPoints || HideConnectionPoints) ? 3 : 0)); } }
        public virtual Rectangle UpdateRectangle { get { return ZoomRectangle.Grow(BorderPen.Width + BaseController.CONNECTION_POINT_SIZE); } }
        public virtual bool IsConnector { get { return false; } }
        public List<Connection> Connections = new List<Connection>();
        public List<GraphicElement> GroupChildren = new List<GraphicElement>();
        public GraphicElement Parent { get; set; }

        /// <summary>
        /// Extra data that the application can associate with an element.
        /// </summary>
        public Dictionary<string, string> Json { get; set; }

        protected Rectangle displayRectangle;
        public Rectangle DisplayRectangle
        {
            get { return displayRectangle; }
            set
            {
                // Ew, side effect! :)
                displayRectangle = value;
                UpdateZoomRectangle();
            }
        }

        public Rectangle ZoomRectangle { get; protected set; }

        public virtual Rectangle ToolboxDisplayRectangle { get { return new Rectangle(0, 0, 25, 25); } }
        public Pen BorderPen { get; set; }
        public SolidBrush FillBrush { get; set; }

        // Helpers for reflection undo/redo update
        public Color BorderPenColor { get { return BorderPen.Color; } set { BorderPen.Color = value; } }
        public int BorderPenWidth { get { return (int)BorderPen.Width; } set { BorderPen.Width = value; } }
        public Color FillColor { get { return FillBrush.Color; } set { FillBrush.Color = value; } }

        public bool IsBookmarked { get; protected set; }
        public string NavigateName { get { return String.IsNullOrEmpty(Name) ? GetType().Name + " " + Text : Name; } }

        public string Name { get; set; }
        public string Text { get; set; }
        public Font TextFont { get; set; }
        public Color TextColor { get; set; }
        public ContentAlignment TextAlign { get; set; }
        public bool Multiline { get; set; }
        // TODO: Text location - left, top, right, middle, bottom

        protected bool HasCornerAnchors { get; set; }
        protected bool HasCenterAnchors { get; set; }
        protected bool HasLeftRightAnchors { get; set; }
        protected bool HasTopBottomAnchors { get; set; }
        protected bool HasCenterAnchor { get; set; }

        protected bool HasCornerConnections { get; set; }
        protected bool HasCenterConnections { get; set; }
        protected bool HasLeftRightConnections { get; set; }
        protected bool HasTopBottomConnections { get; set; }
        protected bool HasCenterConnection { get; set; }

        protected Bitmap background;
        protected Rectangle backgroundRectangle;
        protected Pen selectionPen;
        protected Pen altSelectionPen;
        protected Pen tagPen;
        protected Pen altTagPen;
        protected Pen anchorPen = new Pen(Color.Black);
        protected Pen connectionPointPen = new Pen(Color.Blue);
        protected SolidBrush anchorBrush = new SolidBrush(Color.White);
        protected int anchorWidthHeight = 6;        // TODO: Make const?
        protected Canvas canvas;

        protected bool disposed;
        protected bool removed;

        public GraphicElement(Canvas canvas)
        {
            this.canvas = canvas;
            Visible = true;
            Id = Guid.NewGuid();
            Json = new Dictionary<string, string>();
            selectionPen = new Pen(Color.Red);
            altSelectionPen = new Pen(Color.Blue);
            tagPen = new Pen(Color.Blue, 3);
            altTagPen = new Pen(Color.LightGreen, 3);
            HasCenterAnchors = true;
            HasCornerAnchors = true;
            HasLeftRightAnchors = false;
            HasTopBottomAnchors = false;
            HasCenterAnchor = false;
            HasCenterConnections = true;
            HasCornerConnections = true;
            HasLeftRightConnections = false;
            HasTopBottomConnections = false;
            HasCenterConnection = false;
            FillBrush = new SolidBrush(Color.White);
            BorderPen = new Pen(Color.Black);
            BorderPen.Width = 1;
            TextFont = new Font(FontFamily.GenericSansSerif, 10);
            TextColor = Color.Black;
            TextAlign = ContentAlignment.MiddleCenter;
        }

        public virtual void RightClick() { }

        public virtual void Select()
        {
            Selected = true;
        }

        public virtual void ElementSelected() { }

        public virtual void Deselect()
        {
            Selected = false;
        }

        public virtual void Removed(bool dispose)
        {
            removed = true;
        }

        public virtual void Restored()
        {
            removed = false;
        }

        public void ClearTag()
        {
            Tagged = false;
        }

        public void SetTag()
        {
            Tagged = true;
        }

        // For convenience when we know the shape and we just want to redraw it.
        public virtual void Redraw()
        {
            Canvas.Controller.Redraw(this);
        }

        public override string ToString()
        {
            return GetType().Name + " (" + Id + ") : " + Text;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                disposed = true;

                if (disposing)
                {
                    BorderPen.Dispose();
                    FillBrush.Dispose();
                    background?.Dispose();
                    selectionPen.Dispose();
                    altSelectionPen.Dispose();
                    anchorPen.Dispose();
                    anchorBrush.Dispose();
                    TextFont.Dispose();
                    connectionPointPen.Dispose();
                    tagPen.Dispose();
                    altTagPen.Dispose();
                }
            }
        }

        // TODO: Unify these into the second form at the call site.
        public virtual void MoveAnchor(ConnectionPoint cpShape, ConnectionPoint tocp) { }
        public virtual void MoveAnchor(GripType type, Point delta) { }

        public virtual ElementProperties CreateProperties()
        {
            return new ShapeProperties(this);
        }

        public virtual Rectangle DefaultRectangle()
        {
            return new Rectangle(20, 20, 60, 60);
        }

        public virtual bool IsSelectable(Point p)
        {
            return UpdateRectangle.Contains(p);
        }

        public virtual GraphicElement CloneDefault(Canvas canvas)
        {
            return CloneDefault(canvas, Point.Empty);
        }

        /// <summary>
        /// Clone onto the specified canvas the default shape.
        /// </summary>
        public virtual GraphicElement CloneDefault(Canvas canvas, Point offset)
        {
            GraphicElement el = (GraphicElement)Activator.CreateInstance(GetType(), new object[] { canvas });
            el.DisplayRectangle = el.DefaultRectangle();
            el.Move(offset);
            el.UpdateProperties();
            el.UpdatePath();

            return el;
        }

        public virtual TextBox CreateTextBox(Point mousePosition)
        {
            TextBox tb = new TextBox();
            tb.Multiline = Multiline;
            tb.WordWrap = false;

            if (Multiline)
            {
                tb.Location = DisplayRectangle.Location;
                tb.Size = new Size(DisplayRectangle.Width, DisplayRectangle.Height);
            }
            else
            {
                tb.Location = DisplayRectangle.LeftMiddle().Move(0, -10);
                tb.Size = new Size(DisplayRectangle.Width, 20);
            }

            tb.Text = Text;

            return tb;
        }

        // Certain shapes, like the GridBox, require custom handling.
        public virtual void EndEdit(string newVal, string oldVal)
        {
            canvas.Controller.UndoStack.UndoRedo("Inline edit",
                () =>
                {
                    canvas.Controller.Redraw(this, (el) => el.Text = newVal);
                    // Updates the property grid with the text change.
                    canvas.Controller.ElementSelected.Fire(canvas.Controller, new ElementEventArgs() { Element = this });
                },
                () =>
                {
                    canvas.Controller.Redraw(this, (el) => el.Text = oldVal);
                    // Updates the property grid with the text change.
                    canvas.Controller.ElementSelected.Fire(canvas.Controller, new ElementEventArgs() { Element = this });
                });
        }

        public virtual void Serialize(ElementPropertyBag epb, IEnumerable<GraphicElement> elementsBeingSerialized)
        {
            epb.Name = Name;
            epb.ElementName = GetType().AssemblyQualifiedName;
            epb.Id = Id;
            epb.Visible = Visible;
            epb.Json = JsonConvert.SerializeObject(Json);
            epb.DisplayRectangle = DisplayRectangle;
            epb.BorderPenColor = BorderPen.Color;
            epb.BorderPenWidth = (int)BorderPen.Width;
            epb.FillBrushColor = FillBrush.Color;
            epb.IsBookmarked = IsBookmarked;
            epb.Text = Text;
            epb.TextColor = TextColor;
            epb.TextAlign = TextAlign;
            epb.Multiline = Multiline;
            epb.TextFontFamily = TextFont.FontFamily.Name;
            epb.TextFontSize = TextFont.Size;
            epb.TextFontUnderline = TextFont.Underline;
            epb.TextFontStrikeout = TextFont.Strikeout;
            epb.TextFontItalic = TextFont.Italic;
            epb.TextFontBold = TextFont.Bold;

            epb.HasCornerAnchors = HasCornerAnchors;
            epb.HasCenterAnchors = HasCenterAnchors;
            epb.HasLeftRightAnchors = HasLeftRightAnchors;
            epb.HasTopBottomAnchors = HasTopBottomAnchors;

            epb.HasCornerConnections = HasCornerConnections;
            epb.HasCenterConnections = HasCenterConnections;
            epb.HasLeftRightConnections = HasLeftRightConnections;
            epb.HasTopBottomConnections = HasTopBottomConnections;

            epb.HasCenterAnchor = HasCenterAnchor;
            epb.HasCenterConnection = HasCenterConnection;

            Connections.ForEach(c => c.Serialize(epb, elementsBeingSerialized));
            GroupChildren.ForEach(c => epb.Children.Add(new ChildPropertyBag() { ChildId = c.Id }));
        }

        public virtual void Deserialize(ElementPropertyBag epb)
        {
            Id = epb.Id;
            Visible = epb.Visible;

            if (!String.IsNullOrEmpty(epb.Json))
            {
                Json = JsonConvert.DeserializeObject<Dictionary<string, string>>(epb.Json);
            }

            Name = epb.Name;
            DisplayRectangle = epb.DisplayRectangle;
            BorderPen.Dispose();
            BorderPen = new Pen(epb.BorderPenColor, epb.BorderPenWidth);
            FillBrush.Dispose();
            FillBrush = new SolidBrush(epb.FillBrushColor);
            Text = epb.Text;
            TextColor = epb.TextColor;
            IsBookmarked = epb.IsBookmarked;
            // If missing (backwards compatibility) middle-center align.
            TextAlign = epb.TextAlign == 0 ? ContentAlignment.MiddleCenter : epb.TextAlign;
            Multiline = epb.Multiline;
            TextFont.Dispose();
            FontStyle fontStyle = (epb.TextFontUnderline ? FontStyle.Underline : FontStyle.Regular) | (epb.TextFontItalic ? FontStyle.Italic : FontStyle.Regular) | (epb.TextFontStrikeout ? FontStyle.Strikeout : FontStyle.Regular) | (epb.TextFontBold ? FontStyle.Bold : FontStyle.Regular); ;
            TextFont = new Font(epb.TextFontFamily, epb.TextFontSize, fontStyle);

            HasCornerAnchors = epb.HasCornerAnchors;
            HasCenterAnchors = epb.HasCenterAnchors;
            HasLeftRightAnchors = epb.HasLeftRightAnchors;
            HasTopBottomAnchors = epb.HasTopBottomAnchors;

            HasCornerConnections = epb.HasCornerConnections;
            HasCenterConnections = epb.HasCenterConnections;
            HasLeftRightConnections = epb.HasLeftRightConnections;
            HasTopBottomConnections = epb.HasTopBottomConnections;
            HasCenterConnection = epb.HasCenterConnection;

            HasCenterAnchor = epb.HasCenterAnchor;
        }

        public virtual void FinalFixup(List<GraphicElement> elements, ElementPropertyBag epb, Dictionary<Guid, Guid> oldNewGuidMap)
        {
            elements.ForEach(el => el.UpdateProperties());
        }

        public void ToggleBookmark()
        {
            IsBookmarked ^= true;
        }

        public void ClearBookmark()
        {
            IsBookmarked = false;
        }

        public bool OnScreen(Rectangle r)
        {
            return canvas.OnScreen(r);
        }

        public bool OnScreen()
        {
            return canvas.OnScreen(UpdateRectangle);
        }

        public bool OnScreen(int dx, int dy)
        {
            return canvas.OnScreen(UpdateRectangle.Grow(dx, dy));
        }

        public virtual void UpdatePath() { }

        public virtual void Move(Point delta)
        {
            DisplayRectangle = DisplayRectangle.Move(delta);
        }

        public virtual void UpdateSize(ShapeAnchor anchor, Point delta)
        {
            canvas.Controller.UpdateSize(this, anchor, delta);
        }

        // Placeholders:
        public virtual void SetConnection(GripType gt, GraphicElement shape) { }
        public virtual void DisconnectShapeFromConnector(GripType gt) { }
        public virtual void DetachAll() { }
        public virtual void UpdateProperties() { }

        public virtual void RemoveConnection(GripType gt)
        {
            // Connections.SingleOrDefault(c=>c.ElementConnectionPoint.Type == gr)
        }

        public virtual void SetCanvas(Canvas canvas)
        {
            this.canvas = canvas;
        }

        public virtual void Erase()
        {
            InternalErase();
        }

        public virtual void Draw()
        {
            Graphics gr = canvas.AntiAliasGraphics;

            if (Visible)
            {
                if (canvas.OnScreen(UpdateRectangle))
                {
                    Trace.WriteLine("Shape:Draw " + ToString());
                    Draw(gr);
                }

                // Draw text before anchors and CP's, otherwise centered text will overlay
                // on top of any center anchor/CP.
                if (!String.IsNullOrEmpty(Text))
                {
                    DrawText(gr);
                }

                if (ShowAnchors)
                {
                    DrawAnchors(gr);
                }

                if (ShowConnectionPoints)
                {
                    DrawConnectionPoints(gr);
                }

                if (IsBookmarked)
                {
                    DrawBookmark(gr);
                }
            }
            else
            {
                Hide();
            }
        }

        public virtual void UpdateScreen(int ix = 0, int iy = 0)
        {
            InternalUpdateScreen(ix, iy);
        }

        public virtual void GetBackground()
        {
            InternalGetBackground();
        }

        public virtual void CancelBackground()
        {
            InternalCancelBackground();
        }

        public virtual ShapeAnchor GetBottomRightAnchor()
        {
            Size anchorSize = new Size(anchorWidthHeight, anchorWidthHeight);
            Rectangle r = new Rectangle(ZoomRectangle.BottomRightCorner().Move(-anchorWidthHeight, -anchorWidthHeight), anchorSize);
            ShapeAnchor anchor = new ShapeAnchor(GripType.BottomRight, r, Cursors.SizeNWSE);

            return anchor;
        }

        public virtual List<ShapeAnchor> GetAnchors()
        {
            List<ShapeAnchor> anchors = new List<ShapeAnchor>();
            Rectangle r;
            Size anchorSize = new Size(anchorWidthHeight, anchorWidthHeight);

            if (HasCornerAnchors)
            {
                r = new Rectangle(ZoomRectangle.TopLeftCorner(), anchorSize);
                anchors.Add(new ShapeAnchor(GripType.TopLeft, r, Cursors.SizeNWSE));
                r = new Rectangle(ZoomRectangle.TopRightCorner().Move(-anchorWidthHeight, 0), anchorSize);
                anchors.Add(new ShapeAnchor(GripType.TopRight, r, Cursors.SizeNESW));
                r = new Rectangle(ZoomRectangle.BottomLeftCorner().Move(0, -anchorWidthHeight), anchorSize);
                anchors.Add(new ShapeAnchor(GripType.BottomLeft, r, Cursors.SizeNESW));
                r = new Rectangle(ZoomRectangle.BottomRightCorner().Move(-anchorWidthHeight, -anchorWidthHeight), anchorSize);
                anchors.Add(new ShapeAnchor(GripType.BottomRight, r, Cursors.SizeNWSE));
            }

            if (HasCenterAnchors || HasLeftRightAnchors)
            {
                r = new Rectangle(ZoomRectangle.LeftMiddle().Move(0, -anchorWidthHeight / 2), anchorSize);
                anchors.Add(new ShapeAnchor(GripType.LeftMiddle, r, Cursors.SizeWE));
                r = new Rectangle(ZoomRectangle.RightMiddle().Move(-anchorWidthHeight, -anchorWidthHeight / 2), anchorSize);
                anchors.Add(new ShapeAnchor(GripType.RightMiddle, r, Cursors.SizeWE));
            }

            if (HasCenterAnchors || HasTopBottomAnchors)
            {
                r = new Rectangle(ZoomRectangle.TopMiddle().Move(-anchorWidthHeight / 2, 0), anchorSize);
                anchors.Add(new ShapeAnchor(GripType.TopMiddle, r, Cursors.SizeNS));
                r = new Rectangle(ZoomRectangle.BottomMiddle().Move(-anchorWidthHeight / 2, -anchorWidthHeight), anchorSize);
                anchors.Add(new ShapeAnchor(GripType.BottomMiddle, r, Cursors.SizeNS));
            }

            if (HasCenterAnchor)
            {
                r = new Rectangle(ZoomRectangle.Center().Move(-anchorWidthHeight / 2, -anchorWidthHeight / 2), anchorSize);
                anchors.Add(new ShapeAnchor(GripType.Center, r, Cursors.Cross));
            }

            return anchors;
        }

        public virtual List<ConnectionPoint> GetConnectionPoints()
        {
            List<ConnectionPoint> connectionPoints = new List<ConnectionPoint>();

            if (HasCornerConnections)
            {
                connectionPoints.Add(new ConnectionPoint(GripType.TopLeft, DisplayRectangle.TopLeftCorner()));
                connectionPoints.Add(new ConnectionPoint(GripType.TopRight, DisplayRectangle.TopRightCorner()));
                connectionPoints.Add(new ConnectionPoint(GripType.BottomLeft, DisplayRectangle.BottomLeftCorner()));
                connectionPoints.Add(new ConnectionPoint(GripType.BottomRight, DisplayRectangle.BottomRightCorner()));
            }

            if (HasCenterConnections)
            {
                connectionPoints.Add(new ConnectionPoint(GripType.LeftMiddle, DisplayRectangle.LeftMiddle()));
                connectionPoints.Add(new ConnectionPoint(GripType.RightMiddle, DisplayRectangle.RightMiddle()));
                connectionPoints.Add(new ConnectionPoint(GripType.TopMiddle, DisplayRectangle.TopMiddle()));
                connectionPoints.Add(new ConnectionPoint(GripType.BottomMiddle, DisplayRectangle.BottomMiddle()));
            }

            if (HasLeftRightConnections)
            {
                connectionPoints.Add(new ConnectionPoint(GripType.Start, DisplayRectangle.LeftMiddle()));
                connectionPoints.Add(new ConnectionPoint(GripType.End, DisplayRectangle.RightMiddle()));
            }

            if (HasTopBottomConnections)
            {
                connectionPoints.Add(new ConnectionPoint(GripType.Start, DisplayRectangle.TopMiddle()));
                connectionPoints.Add(new ConnectionPoint(GripType.End, DisplayRectangle.BottomMiddle()));
            }

            if (HasCenterConnection)
            {
                connectionPoints.Add(new ConnectionPoint(GripType.Center, DisplayRectangle.Center()));
            }

            return connectionPoints;
        }

        /// <summary>
        /// Stub, so that certain shapes (like shapes that embed controls) can hide their sub-components.
        /// </summary>
        public virtual void Hide() { }

        public virtual void Draw(Graphics gr, bool showSelection = true)
        {
            if (Selected && showSelection)      // show selection is overriden when we save the image as a PNG.
            {
                DrawSelection(gr);
            }

            if (Tagged && showSelection)
            {
                DrawTag(gr);
            }

            // For illustration / debugging of what's being updated.
            // DrawUpdateRectangle(gr);
        }

        public void UpdateZoomRectangle()
        {
            // TODO: canvas controller is null when saving as PNG!
            if (canvas.Controller != null)
            {
                double dz = canvas.Controller.Zoom / 100.0;

                ZoomRectangle = new Rectangle(
                    (int)(DisplayRectangle.X * dz),
                    (int)(DisplayRectangle.Y * dz),
                    (int)(DisplayRectangle.Width * dz),
                    (int)(DisplayRectangle.Height * dz));
            }
        }

        protected void InternalUpdateScreen(int ix, int iy)
        {
            Rectangle r = canvas.Clip(UpdateRectangle.Grow(ix, iy));

            if (canvas.OnScreen(r))
            {
                canvas.CopyToScreen(r);
            }
        }

        protected void InternalErase()
        {
            if (canvas.OnScreen(backgroundRectangle))
            {
                Trace.WriteLine("Shape:Erase " + ToString());
                background?.Erase(canvas, backgroundRectangle);
                // canvas.Graphics.DrawRectangle(selectionPen, backgroundRectangle);
                background?.Dispose();
                background = null;
            }
        }


        protected void InternalGetBackground()
        {
            background?.Dispose();
            background = null;
            backgroundRectangle = canvas.Clip(UpdateRectangle);

            if (canvas.OnScreen(backgroundRectangle))
            {
                background = canvas.GetImage(backgroundRectangle);
            }
        }

        protected void InternalCancelBackground()
        {
            background?.Dispose();
            background = null;
        }

        protected virtual void DrawSelection(Graphics gr)
        {
            if (BorderPen.Color.ToArgb() == selectionPen.Color.ToArgb())
            {
                Rectangle r = ZoomRectangle;
                gr.DrawRectangle(altSelectionPen, r);
            }
            else
            {
                Rectangle r = ZoomRectangle;
                gr.DrawRectangle(selectionPen, r);
            }
        }

        protected virtual void DrawTag(Graphics gr)
        {
            if (FillBrush.Color.ToArgb() == tagPen.Color.ToArgb())
            {
                Rectangle r = DisplayRectangle.Grow(-2);
                gr.DrawRectangle(altTagPen, r);
            }
            else
            {
                Rectangle r = DisplayRectangle.Grow(-2);
                gr.DrawRectangle(tagPen, r);
            }
        }

        // For illustration / debugging of what's being updated.
        protected virtual void DrawUpdateRectangle(Graphics gr)
        {
            Pen pen = new Pen(Color.Gray);
            Rectangle r = UpdateRectangle.Grow(-1);
            gr.DrawRectangle(pen, r);
            pen.Dispose();
        }

        protected virtual void DrawAnchors(Graphics gr)
        {
            GetAnchors().ForEach((a =>
            {
                gr.DrawRectangle(anchorPen, a.Rectangle);
                gr.FillRectangle(anchorBrush, a.Rectangle.Grow(-1));
            }));
        }

        protected virtual void DrawConnectionPoints(Graphics gr)
        {
            GetConnectionPoints().ForEach(cp =>
            {
                // We change where the connection point renders, not the unzoomed connection point.
                var cpz = AdjustForZoom(cp);
                gr.FillRectangle(anchorBrush, new Rectangle(cpz.Point.X - BaseController.CONNECTION_POINT_SIZE, cpz.Point.Y - BaseController.CONNECTION_POINT_SIZE, BaseController.CONNECTION_POINT_SIZE * 2, BaseController.CONNECTION_POINT_SIZE * 2));
                gr.DrawLine(connectionPointPen, cpz.Point.X - BaseController.CONNECTION_POINT_SIZE, cpz.Point.Y - BaseController.CONNECTION_POINT_SIZE, cpz.Point.X + BaseController.CONNECTION_POINT_SIZE, cpz.Point.Y + BaseController.CONNECTION_POINT_SIZE);
                gr.DrawLine(connectionPointPen, cpz.Point.X + BaseController.CONNECTION_POINT_SIZE, cpz.Point.Y - BaseController.CONNECTION_POINT_SIZE, cpz.Point.X - BaseController.CONNECTION_POINT_SIZE, cpz.Point.Y + BaseController.CONNECTION_POINT_SIZE);
            });
        }

        protected ConnectionPoint AdjustForZoom(ConnectionPoint cp)
        {
            ConnectionPoint ret = cp;

            // TODO: controller is null when saving as PNG!
            if (canvas.Controller != null && canvas.Controller.Zoom != 100)
            {
                ret = new ConnectionPoint(cp.Type, AdjustForZoom(cp.Point));
            }

            return ret;
        }

        protected Point AdjustForZoom(Point p)
        {
            Point ret = p;

            // TODO: canvas.Controller is null when saving image as PNG!
            if (canvas.Controller != null && canvas.Controller.Zoom != 100)
            {
                ret = new Point(p.X * canvas.Controller.Zoom / 100, p.Y * canvas.Controller.Zoom / 100);
            }

            return ret;
        }

        protected virtual void DrawBookmark(Graphics gr)
        {
            Brush bookmarkBrush = new SolidBrush(Color.Green);
            Rectangle r = UpdateRectangle;
            r.Width = 5;
            r.Height = 5;
            gr.FillRectangle(bookmarkBrush, r);
            bookmarkBrush.Dispose();
        }

        public virtual void DrawText(Graphics gr)
        {
            DrawText(gr, Text, TextFont, TextColor, TextAlign);
        }

        protected virtual void DrawText(Graphics gr, string text, Font textFont, Color textColor, ContentAlignment textAlign)
        {
            SizeF size = gr.MeasureString(text, textFont);
            Brush brush = new SolidBrush(textColor);
            Point textpos;
            Font font = textFont;
            bool disposeFont = false;

            // TODO: Should we just create the font and dispose it every time we draw?
            // TODO: canvas.Controller is null when we save the drawing as a PNG!
            if (canvas.Controller != null && canvas.Controller.Zoom != 100)
            {
                font = new Font(textFont.FontFamily, textFont.Size * canvas.Controller.Zoom / 100, textFont.Style);
                disposeFont = true;
            }

            // TextRenderer is terrible when font is bolded.  Not sure why.
            // Would be great to use this, but the rendering is so bad, I won't.

            // Some info here:
            // http://stackoverflow.com/questions/9038125/asp-net-textrenderer-drawtext-awful-text-images
            //gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //gr.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit; // <-- important!
            //gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            //gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            //gr.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            //gr.TextContrast = 0;
            //TextFormatFlags flags = TextFormatFlags.Right | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak;
            //TextRenderer.DrawText(gr, Text, TextFont, DisplayRectangle, TextColor, flags);

            switch (textAlign)
            {
                case ContentAlignment.TopLeft:
                    textpos = ZoomRectangle.TopLeftCorner().Move(5, 5);
                    break;

                case ContentAlignment.TopCenter:
                    textpos = ZoomRectangle.TopMiddle().Move((int)(-size.Width / 2), 5);
                    break;

                case ContentAlignment.TopRight:
                    textpos = ZoomRectangle.TopRightCorner().Move((int)(-(size.Width + 5)), 5);
                    break;

                case ContentAlignment.MiddleLeft:
                    textpos = ZoomRectangle.LeftMiddle().Move(5, (int)(-size.Height / 2));
                    break;

                case ContentAlignment.MiddleCenter:
                    textpos = ZoomRectangle.Center().Move((int)(-size.Width / 2), (int)(-size.Height / 2));
                    break;

                case ContentAlignment.MiddleRight:
                    textpos = ZoomRectangle.RightMiddle().Move((int)(-(size.Width + 5)), (int)(-size.Height / 2));
                    break;

                case ContentAlignment.BottomLeft:
                    textpos = ZoomRectangle.BottomLeftCorner().Move(5, (int)-(size.Height + 5));
                    break;

                case ContentAlignment.BottomCenter:
                    textpos = ZoomRectangle.BottomMiddle().Move((int)(-size.Width / 2), (int)-(size.Height + 5));
                    break;

                case ContentAlignment.BottomRight:
                    textpos = ZoomRectangle.BottomRightCorner().Move((int)(-(size.Width + 5)), (int)-(size.Height + 5));
                    break;

                default:        // middle center
                    textpos = ZoomRectangle.Center().Move((int)(-size.Width / 2), (int)(-size.Height / 2));
                    break;
            }

            TextFormatFlags tff = TextFormatFlags.Default;

            switch (textAlign)
            {
                case ContentAlignment.TopLeft:
                    tff |= TextFormatFlags.Left;
                    break;

                case ContentAlignment.TopCenter:
                    tff |= TextFormatFlags.Top | TextFormatFlags.HorizontalCenter;
                    break;

                case ContentAlignment.TopRight:
                    tff |= TextFormatFlags.Top | TextFormatFlags.Right;
                    break;

                case ContentAlignment.MiddleLeft:
                    tff |= TextFormatFlags.VerticalCenter | TextFormatFlags.Left;
                    break;

                case ContentAlignment.MiddleCenter:
                    tff |= TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter;
                    break;

                case ContentAlignment.MiddleRight:
                    tff |= TextFormatFlags.VerticalCenter | TextFormatFlags.Right;
                    break;

                case ContentAlignment.BottomLeft:
                    tff |= TextFormatFlags.Bottom | TextFormatFlags.Left;
                    break;

                case ContentAlignment.BottomCenter:
                    tff |= TextFormatFlags.Bottom | TextFormatFlags.HorizontalCenter;
                    break;

                case ContentAlignment.BottomRight:
                    tff |= TextFormatFlags.Bottom | TextFormatFlags.Right;
                    break;

                default:        // middle center
                    tff |= TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter;
                    break;
            }

            TextRenderer.DrawText(gr, text, font, ZoomRectangle.Grow(-3), textColor, FillColor, tff);
            // gr.DrawString(text, textFont, brush, textpos);
            brush.Dispose();

            if (disposeFont)
            {
                font.Dispose();
            }
        }
    }
}
