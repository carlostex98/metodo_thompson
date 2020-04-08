using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proyeco1_ocl
{
    public partial class img : Form
    {
        public int ix = 0;

        public img()
        {
            InitializeComponent();
            //panel1.AutoScroll = true;


        }

        private void button2_Click(object sender, EventArgs e)
        {
            //anterior
            if (forx.ins.expresiones.Count != 0)
            {
                if (ix < 0 || ix > forx.ins.expresiones.Count - 1)
                {
                    ix = forx.ins.expresiones.Count - 1;

                }
                Console.WriteLine(ix);
                pictureBox1.Image = Image.FromFile("grafo_" + forx.ins.expresiones.ElementAt(ix)[0] + ".png");
                label1.Text = forx.ins.expresiones.ElementAt(ix)[0];
                ix--; 

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (forx.ins.expresiones.Count != 0)
            {
                if (ix > forx.ins.expresiones.Count - 1 || ix < 0)
                {
                    ix = 0;

                }

                pictureBox1.Image = Image.FromFile("grafo_" + forx.ins.expresiones.ElementAt(ix)[0] + ".png");
                label1.Text = forx.ins.expresiones.ElementAt(ix)[0];
                ix++;

            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void img_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Hide();
        }

        private void img_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
