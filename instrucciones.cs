using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyeco1_ocl
{
    public class instrucciones
    {
        //nombre, valor
        public LinkedList<string[]> conjuntos = new LinkedList<string[]>();
        public LinkedList<string[]> expresiones = new LinkedList<string[]>();
        public LinkedList<string[]> cadena = new LinkedList<string[]>();

        public void in_conj(string nombre, string valor)
        {
            string[] g = { nombre, valor };
            conjuntos.AddLast(g);
        }
        public void crea_vacio()
        {
            string[] g = { "vacio_vacio", "vacio" };
            conjuntos.AddLast(g);
        }

        public void in_xpr(string nombre, string valor)
        {
            string[] g = { nombre, valor };
            expresiones.AddLast(g);
        }
        public void in_cadena(string nombre, string valor)
        {
            string[] g = { nombre, valor };
            cadena.AddLast(g);
        }

        public void imprimir()
        {
            for (int i = 0; i < conjuntos.Count; i++)
            {
                Console.WriteLine(conjuntos.ElementAt(i)[0]+","+ conjuntos.ElementAt(i)[1]);
            }
            Console.WriteLine("--------------");
            for (int i = 0; i < expresiones.Count; i++)
            {
                Console.WriteLine(expresiones.ElementAt(i)[0] + "," + expresiones.ElementAt(i)[1]);
            }
            Console.WriteLine("--------------");
            for (int i = 0; i < cadena.Count; i++)
            {
                Console.WriteLine(cadena.ElementAt(i)[0] + "," + cadena.ElementAt(i)[1]);
            }
        }


    }
}
