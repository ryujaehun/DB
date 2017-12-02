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
        public diagramForm()
        {
            InitializeComponent();

        }

        private void attribute_Click(object sender, EventArgs e)
        {
            actions.Add(new DrawAction('R', new Rectangle(11, 22, 66, 88), Color.DarkGoldenrod));
            mainPanel.Invalidate();  // this triggers the Paint event!
        }

 

        private void multi_attribute_Click(object sender, EventArgs e)
        {

        }

        private void entity_Click(object sender, EventArgs e)
        {

        }

        private void weak_entity_Click(object sender, EventArgs e)
        {

        }

        private void relation_Click(object sender, EventArgs e)
        {

        }

        private void weak_relation_Click(object sender, EventArgs e)
        {

        }

        private void er_table_Click(object sender, EventArgs e)
        {

        }



        private void mainPanel_Paint(object sender, PaintEventArgs e)
        {

            foreach (DrawAction da in actions)
            {
                if (da.type == 'R') e.Graphics.DrawRectangle(new Pen(da.color), da.rect);
                else if (da.type == 'E') e.Graphics.DrawEllipse(new Pen(da.color), da.rect);
                //..
            }
        }
    }
}
