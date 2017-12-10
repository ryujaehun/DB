using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
namespace C_Sharp
{
    enum DIAGRAMID { RECT, DOUBLERECT, ELLIPSE, DOUBLEELLIPSE, DIAMOND, DOUBLEDIAMOND };
    public abstract class Diagram
    {
        public Diagram()
        {

        }
        public int tl_x , tl_y, width, height;
        public Rectangle sizeRect;
        public string name;
        public void DisplayRect()
        {

        }
        public DrawAction da;

        public void setSizeRect()
        {
            sizeRect = new Rectangle(tl_x, tl_y, width, height);
        }

        public void setLocation(int m_x, int m_y) {
            tl_x = m_x - 44;
            tl_y = m_y - 44;
        }

        public abstract void Draw(PaintEventArgs e);
    }

  
}
