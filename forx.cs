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
        public static grafo _grafo = new grafo();
        public img imgs = new img();
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
            ins.limpiar();
            lsx.limpia_todo();
            lx.scanner_x(entrada.Text + "   ");
            lsx.reportar_xml();
            if (lsx.erx() == 0)
            {
                enlace.iniciar_thompson();
                imgs.Show();
            }


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
                    Encoding ascii = Encoding.ASCII;
                    StreamWriter bw;
                    try
                    {
                        bw = new StreamWriter(new FileStream(saveFileDialog1.FileName, FileMode.Create), ascii);
                    }
                    catch (IOException e2)
                    {
                        Console.WriteLine(e2.Message + "\n error.");
                        return;
                    }

                    try
                    {
                        bw.Write(entrada.Text + " ");


                    }
                    catch (IOException e2)
                    {
                        Console.WriteLine(e2.Message + "\n Cannot write to file.");
                        return;
                    }
                    bw.Close();

                    //fs.Close();
                }
                else
                {



                }

            }
            else
            {
                Encoding ascii = Encoding.ASCII;
                StreamWriter bw;
                try
                {
                    bw = new StreamWriter(new FileStream(ruta, FileMode.Create), ascii);
                    bw.WriteLine(entrada.Text);
                }
                catch (IOException e2)
                {
                    Console.WriteLine(e2.Message + "\n error.");
                    return;
                }
                bw.Close();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "ER Archivo|*.er";
            saveFileDialog1.Title = "Guardar un ER";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.
                Encoding ascii = Encoding.ASCII;
                StreamWriter bw;
                try
                {
                    ruta = saveFileDialog1.FileName;
                    bw = new StreamWriter(new FileStream(saveFileDialog1.FileName, FileMode.Create), ascii);
                }
                catch (IOException e2)
                {
                    Console.WriteLine(e2.Message + "\n error.");
                    return;
                }

                try
                {
                    bw.Write(entrada.Text + " ");


                }
                catch (IOException e2)
                {
                    Console.WriteLine(e2.Message + "\n Cannot write to file.");
                    return;
                }
                bw.Close();

            }
        }
    }
}
