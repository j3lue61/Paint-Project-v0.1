using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint_Project_v0._1
{
    public partial class GraphicPanel : UserControl
    {
        public bool isbusy = false;
        bool resize = false;
        bool draw = false;
        public int posx = -1;
        public int posy = -1;
        public Pen brsh;
        Graphics grp;
        bool RBEdge = false;
        bool REdge = false;
        bool BEdge = false;
        public GraphicPanel()
        {
            InitializeComponent();
            brsh = new Pen(Color.Black, 4);
            grp = CreateGraphics();

        }
        public int getWidth()
        {
            return Width;
        }
        public int GetHeight()
        {
            return Height;
        }
        private void MDown(object sender, MouseEventArgs e)
        {
            if((RBEdge||REdge||BEdge)&& e.Button == MouseButtons.Left)
            {
                resize = true;
                draw = false;
            }
            if(e.Button == MouseButtons.Left)
                draw = true;
        }

        private void MMove(object sender, MouseEventArgs e)
        {
////////////////////////////////////////////////////////////////////////RESIZING
            if (Math.Abs(e.X-Width) <= 10 && Math.Abs(e.Y-Height) <=10)
            {
                RBEdge = true;
                Cursor = Cursors.SizeNWSE;
            }
            else if (Math.Abs(e.X - Width) <= 10 && Math.Abs(e.Y - Height) > 10)
            {
                REdge = true;
                Cursor = Cursors.SizeWE;
            }

            else if (Math.Abs(e.X - Width) > 10 && Math.Abs(e.Y - Height) <= 10)
            {
                BEdge = true;
                Cursor = Cursors.SizeNS;
            }
            else 
            {
                if(!resize)
                {
                RBEdge = false;
                REdge = false;
                BEdge = false;
                Cursor = Cursors.Cross;
                }
            }
            if (REdge&&resize)
            {
                Width = e.X;
                grp = CreateGraphics();
            }
            if (BEdge&&resize)
            {
                Height = e.Y;
                grp = CreateGraphics();
            }
            if (RBEdge&&resize)
            {
                Size = new Size(e.X, e.Y);
                grp = CreateGraphics();
            }


                

////////////////////////////////////////////////////////////////////////DRAWING
            if (draw&&!resize&&!isbusy)
                grp.DrawLine(brsh, new Point(posx, posy), e.Location);
            posx = e.X;
            posy = e.Y;
        }

        private void MUp(object sender, MouseEventArgs e)
        {
            resize = false;
            draw = false;
        }
        public Bitmap DrawBitmap(Control control)
        {
            Bitmap bmp = new Bitmap(control.Width, control.Height);
            Graphics grp = Graphics.FromImage(bmp);
            Rectangle rect = control.RectangleToScreen(control.ClientRectangle);
            grp.CopyFromScreen(rect.Location, Point.Empty, control.Size);
            return bmp;
        }

    }
}
