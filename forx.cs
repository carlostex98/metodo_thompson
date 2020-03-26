using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace proyeco1_ocl
{
    public partial class forx : Form
    {
        //some_s nx = new some_s();
        public static lexico lx = new lexico();
        public static listas lsx = new listas();
        public static instrucciones ins = new instrucciones();
        public static grupos_ev grupos = new grupos_ev();
        public static enlazador enlace = new enlazador();
        string ruta = "";
        public forx()
        {
            InitializeComponent();
            //inicializaciones necesarias
            ins.crea_vacio();

        }

        private void abrir_archivo_Click(object sender, EventArgs e)
        {
            //nx.txt();
            string path;
            OpenFileDialog file = new OpenFileDialog();
            if (file.ShowDialog() == DialogResult.OK)
            {
                path = file.FileName;
                //Console.WriteLine(path);
                using (StreamReader sr = File.OpenText(path))
                {
                    ruta = path;
                    string s = "";
                    string c = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        c += s + "\n";
                    }
                    entrada.Text = c;

                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lsx.limpia_todo();
            lx.scanner_x(entrada.Text + "   ");
            lsx.reportar_xml();
            enlace.iniciar_thompson();
            //ins.imprimir();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ruta == "")
            {
                //debemos preguntar en que ruta se guarda
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "ER Archivo|*.er";
                saveFileDialog1.Title = "Guardar un ER";
                saveFileDialog1.ShowDialog();

                if (saveFileDialog1.FileName != "")
                {
                    // Saves the Image via a FileStream created by the OpenFile method.
                    System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog1.OpenFile();

                    fs.Close();
                }
                else
                {

                }

            }

        }


    }
}
