using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Paint_Project_v0._1
{
    public partial class mainform : Form
    {
        bool sliderchange = false;
        float sliderdef = 4.0f, slidermin = 0.0f, slidermax = 7.0f;
        public mainform()
        {
            InitializeComponent();
            postimer.Interval = 1;
            postimer.Start();
        }

        private void mainform_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }
        private void bckgrndpaint(object sender, PaintEventArgs e)
        {
            Graphics bckgrnd = e.Graphics;
            Rectangle bckgrndrec = new Rectangle(0, 0, Width, Height);
            Brush b = new LinearGradientBrush(bckgrndrec, Color.FromArgb(153, 100, 255), Color.FromArgb(204, 229, 255), 65f);
            bckgrnd.FillRectangle(b, bckgrndrec);
        }
        public float brushsliderbar(float value)
        {
            return (brushslider.Width - 24) * (value - slidermin) / (slidermax - slidermin);
        }
        public float brushsliderwidth(int x)
        {
            return slidermin + (slidermax - slidermin) * x / (float)brushslider.Width;
        }
        private void brushslidervisual(object sender, PaintEventArgs e)
        {
            float bar_size = 0.45f;
            float x = brushsliderbar(sliderdef);
            int y = (int)(brushslider.Height * bar_size);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.FillRectangle(Brushes.DimGray, 0, y, brushslider.Width, y / 2);
            e.Graphics.FillRectangle(Brushes.SkyBlue, 0, y, x, brushslider.Height - y * 2);
            using (Pen pen = new Pen(Color.Black, 8))
            {
                e.Graphics.DrawEllipse(pen, x + 4, y - 6, brushslider.Height / 2, brushslider.Height / 2);
            }
        }
        public void brushsliderchange(float value)
        {
            if (value < slidermin) value = slidermin;
            if (value > slidermax) value = slidermax;
            sliderdef = value;
            brushslider.Refresh();
            graphicPanel1.brsh.Width = sliderdef;
        }
        private void brushslidermdown(object sender, MouseEventArgs e)
        {
            sliderchange = true;
            brushsliderchange(brushsliderwidth(e.X));
        }
        private void brushslidermup(object sender, MouseEventArgs e)
        {
            sliderchange = false;
        }
        private void brushslidermmove(object sender, MouseEventArgs e)
        {
            if(sliderchange) brushsliderchange(brushsliderwidth(e.X));
        }
        private void postimertick(object sender, EventArgs e)
        {
            poslabel.Text = graphicPanel1.posx + "," + graphicPanel1.posy;
            sizelabel.Text = "Size:" + graphicPanel1.getWidth() + "x" + graphicPanel1.GetHeight();
        }

        private void SaveAsButton(object sender, EventArgs e)
        {
            Bitmap bmp = graphicPanel1.DrawBitmap(graphicPanel1);
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = @"C:\";
            sfd.Title = "Kaydedilecek Yeri Seçiniz!";
            sfd.Filter = "Jpeg Dosyası (.jpg) |*.jpg | Png Dosyası (.png) |*.png | Bitmap Dosyası (bmp)|*.bmp";
            sfd.FilterIndex = 1;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                bmp.Save(sfd.FileName);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            graphicPanel1.brsh.Color = Color.Black;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            graphicPanel1.brsh.Color = Color.White;
        }

        private void ToolStripIsOpen(object sender, EventArgs e)
        {
            graphicPanel1.isbusy = true;
        }

        private void ToolStripIsClosed(object sender, EventArgs e)
        {
            graphicPanel1.isbusy = false;
        }

        private void stripmenuexitbutton(object sender, EventArgs e)
        {
            askexit();
        }
        private void askexit()
        {
            this.Close();
        }
    }
}
