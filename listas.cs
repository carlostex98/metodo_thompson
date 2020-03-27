using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyeco1_ocl
{
    public class listas
    {
        //dos listas, tokens y errores
        private LinkedList<string[]> tokens = new LinkedList<string[]>();//struct: nombre,valor,fila,columna
        private LinkedList<string[]> errores = new LinkedList<string[]>();//struct: valor,fila,columna
        /*
         para:
            linea=ln
            columna=cl
        */


        public void in_token(string nombre, string valor, int ln, int cl)
        {
            ln++; cl++;
            string[] temp = { nombre, valor, ln.ToString(), cl.ToString() };
            tokens.AddLast(temp);
        }

        public void in_error(string valor, int ln, int cl)
        {
            ln++; cl++;
            string[] temp = { valor, ln.ToString(), cl.ToString() };
            errores.AddLast(temp);
        }

        public void reportar_xml()
        {
            Encoding ascii = Encoding.ASCII;
            StreamWriter bw;
            try
            {
                bw = new StreamWriter(new FileStream("tokens.xml", FileMode.Create), ascii);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + "\n error.");
                return;
            }
            

            try
            {
                bw.Write("<ListaTokens>\n");
                for (int i = 0; i < tokens.Count; i++)
                {
                    bw.Write("  <Token>\n");

                    bw.Write("      <Nombre>" + tokens.ElementAt(i)[0] + "</Nombre>\n");
                    bw.Write("      <Valor>" + tokens.ElementAt(i)[1] + "</Valor>\n");
                    bw.Write("      <Fila>" + tokens.ElementAt(i)[2] + "</Fila>\n");
                    bw.Write("      <Columna>" + tokens.ElementAt(i)[3] + "</Columna>\n");

                    bw.Write("  </Token>\n");
                }

                bw.Write("</ListaTokens>");

            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + "\n Cannot write to file.");
                return;
            }
            bw.Close();


            try
            {
                bw = new StreamWriter(new FileStream("errores.xml", FileMode.Create));
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + "\n error.");
                return;
            }

            try
            {
                bw.Write("<ListaErrores>\n");
                for (int i = 0; i < errores.Count; i++)
                {
                    bw.Write("  <Error>\n");

                    bw.Write("      <Valor>" + errores.ElementAt(i)[0] + "</Valor>\n");
                    bw.Write("      <Fila>" + errores.ElementAt(i)[1] + "</Fila>\n");
                    bw.Write("      <Columna>" + errores.ElementAt(i)[2] + "</Columna>\n");

                    bw.Write("  </Error>\n");
                }

                bw.Write("</ListaErrores>");

            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + "\n Cannot write to file.");
                return;
            }
            bw.Close();

        }

        public void limpia_todo(){
            tokens.Clear();
            errores.Clear();
        
        }



    }
}
