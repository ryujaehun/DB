using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace C_Sharp
{
    public class Rectan: Diagram
    {
        public Rectan()
        {

        }

        public Rectan(int _tl_x, int _tl_y, int _width, int _height)
        {
            tl_x = _tl_x;
            tl_y = _tl_y;
            width = _width;
            height = _height;
            setSizeRect();
        }

        public override void Draw(PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.Black), sizeRect);
            e.Graphics.DrawString(name, new Font("TimesNewRoman", 10), new SolidBrush(Color.Black), new PointF(tl_x + 22, tl_y + 22));
            //throw new NotImplementedException();
        }
    }
}
