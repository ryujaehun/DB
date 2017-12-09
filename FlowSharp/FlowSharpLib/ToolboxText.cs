﻿/* 
* Copyright (c) Marc Clifton
* The Code Project Open License (CPOL) 1.02
* http://www.codeproject.com/info/cpol10.aspx
*/

using System.Drawing;

namespace FlowSharpLib
{
	/// <summary>
	/// Special rendering for this element in the toolbox only.
	/// </summary>
	public class ToolboxText : GraphicElement
	{
		public const string TOOLBOX_TEXT = "A";

		protected Brush brush = new SolidBrush(Color.Black);

		public ToolboxText(Canvas canvas) : base(canvas)
		{
			TextFont.Dispose();
			TextFont = new Font(FontFamily.GenericSansSerif, 20);
		}

		public override GraphicElement CloneDefault(Canvas canvas)
		{
            return CloneDefault(canvas, Point.Empty);
		}

        public override GraphicElement CloneDefault(Canvas canvas, Point offset)
        {
            TextShape shape = new TextShape(canvas);
            shape.DisplayRectangle = shape.DefaultRectangle().Move(offset);
            shape.UpdateProperties();
            shape.UpdatePath();

            return shape;
        }

        public override void Draw(Graphics gr)
		{
			SizeF size = gr.MeasureString(TOOLBOX_TEXT, TextFont);
			Point textpos = DisplayRectangle.Center().Move((int)(-size.Width / 2), (int)(-size.Height / 2));
			gr.DrawString(TOOLBOX_TEXT, TextFont, brush, textpos);
			base.Draw(gr);
		}
	}
}
