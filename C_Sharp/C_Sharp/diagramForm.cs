using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C_Sharp
{
    public partial class diagramForm : Form
    {
        List<DrawAction> actions = new List<DrawAction>();
        List<DrawImage> di = new List<DrawImage>();

        private bool []flag=new bool[10] { false, false, false, false, false, false, false, false, false, false };
        public diagramForm()
        {
            InitializeComponent();
        

        }



        private void attribute_Click(object sender, EventArgs e)
        {
            flag[0] = true;
            
        }

 

        private void multi_attribute_Click(object sender, EventArgs e)
        {
            flag[1] = true;
        }

        private void entity_Click(object sender, EventArgs e)
        {
            flag[2] = true;
            
        }

        private void weak_entity_Click(object sender, EventArgs e)
        {
            flag[3] = true;
         
        }

        private void relationship_Click(object sender, EventArgs e)
        {
            flag[4] = true;
        }

        private void weak_relationship_Click(object sender, EventArgs e)
        {
            flag[5] = true;
        }

        private void er_table_Click(object sender, EventArgs e)
        {
            //er_table 은 flag 6과 7을 같이쓴다!!!
            //flag[6] = true;
           
        }



        private void mainPanel_Paint(object sender, PaintEventArgs e)
        {

            foreach (DrawAction da in actions)
            {

                if (da.type == 'R') e.Graphics.DrawRectangle(new Pen(da.color), da.rect); //entity
                else if (da.type == 'W') e.Graphics.DrawRectangle(new Pen(da.color), da.rect); //weak entity

                else if (da.type == 'E') e.Graphics.DrawEllipse(new Pen(da.color), da.rect); //attibute
                else if (da.type == 'O') e.Graphics.DrawEllipse(new Pen(da.color), da.rect);  //multi attibute


                else if (da.type == 'B') e.Graphics.DrawEllipse(new Pen(da.color), da.rect);  //multi attibute

                else if (da.type == 'S') {
                    Point[] PointArray = { new Point((da.rect.Left+da.rect.Right)/2, da.rect.Top), new Point(da.rect.Right, (da.rect.Top+da.rect.Bottom)/2), new Point((da.rect.Left+da.rect.Right)/2, da.rect.Bottom), new Point(da.rect.Left, (da.rect.Top + da.rect.Bottom)/2)};
          
                    e.Graphics.DrawPolygon(new Pen(da.color), PointArray);
                }

                else if (da.type == 'X')
                {
                    Point[] PointArray = { new Point((da.rect.Left + da.rect.Right) / 2, da.rect.Top), new Point(da.rect.Right, (da.rect.Top + da.rect.Bottom) / 2), new Point((da.rect.Left + da.rect.Right) / 2, da.rect.Bottom), new Point(da.rect.Left, (da.rect.Top + da.rect.Bottom) / 2) };

                    e.Graphics.DrawPolygon(new Pen(da.color), PointArray);
                }


            }


            foreach(DrawImage a in di)
            {
                 if (a.type == 'Z')
                {
                    Point p = mainPanel.PointToClient(Cursor.Position);
                    e.Graphics.DrawImage(a.image, p.X, p.Y);  //multi attibute
                }
            }

          
        }

      

        private void mainPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (flag[0])
            {
                Point p = mainPanel.PointToClient(Cursor.Position);
                p.X = p.X - 44;
                p.Y = p.Y - 44;
                actions.Add(new DrawAction('E', new Rectangle(p.X, p.Y, 88, 88), Color.Black));
                mainPanel.Invalidate();  // this triggers the Paint event!
                flag[0] = false;
            }
            else if(flag[1])
            {
                Point p = mainPanel.PointToClient(Cursor.Position);
                p.X = p.X - 44;
                p.Y = p.Y - 44;
                actions.Add(new DrawAction('O', new Rectangle(p.X, p.Y, 88, 88), Color.Black));
                actions.Add(new DrawAction('O', new Rectangle(p.X+5, p.Y+5, 78, 78), Color.Black));
                mainPanel.Invalidate();  // this triggers the Paint event!
                flag[1] = false;
            }
            else if (flag[2])
            {
                Point p = mainPanel.PointToClient(Cursor.Position);
                p.X = p.X - 44;
                p.Y = p.Y - 44;
                actions.Add(new DrawAction('R', new Rectangle(p.X, p.Y, 88, 88), Color.Black));
                mainPanel.Invalidate();  // this triggers the Paint event!
                flag[2] = false;
            }
            else if (flag[3])
            {
                Point p = mainPanel.PointToClient(Cursor.Position);
                p.X = p.X - 44;
                p.Y = p.Y - 44;
                actions.Add(new DrawAction('W', new Rectangle(p.X, p.Y, 88, 88), Color.Black));
                actions.Add(new DrawAction('W', new Rectangle(p.X+5, p.Y+5, 78, 78), Color.Black));
                mainPanel.Invalidate();  // this triggers the Paint event!
                flag[3] = false;
            }
            else if (flag[4])
            {
                Point p = mainPanel.PointToClient(Cursor.Position);
                p.X = p.X - 44;
                p.Y = p.Y - 44;
                actions.Add(new DrawAction('S', new Rectangle(p.X, p.Y, 124, 124), Color.Black));
                mainPanel.Invalidate();  // this triggers the Paint event!
                flag[4] = false;
            }
            else if (flag[5])
            {
                Point p = mainPanel.PointToClient(Cursor.Position);
                p.X = p.X - 44;
                p.Y = p.Y - 44;
                actions.Add(new DrawAction('X', new Rectangle(p.X, p.Y, 124, 124), Color.Black));
                actions.Add(new DrawAction('X', new Rectangle(p.X + 5, p.Y + 5, 114, 114), Color.Black));
                mainPanel.Invalidate();  // this triggers the Paint event!
                flag[5] = false;
            }
            //else if (flag[6])
            //{

            //    di.Add(new DrawImage('Z',er_table.Image));
            //    flag[7] = true;
            //}
           

        }
    }
}
