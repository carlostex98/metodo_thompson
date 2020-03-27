using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyeco1_ocl
{
    /*
     * aclaraciones
     * tenemos un arbol ternario para el manejo de los nodos
     * no es arbol B, dado que el programa decide en donde poner los nosdos
     * 
     * genera el grafo, luego la lista estados para el afd
     * el afd mo existe de manera de grafo como una edd, se guarda en una tabla de trasn
     */
    class nodo
    {
        public int id;
        public nodo der;//enlace derecho
        public nodo cen;//enlace centro, retorno
        public nodo izq;//enlace izquierdo
        public string a;//trans izq
        public string b;//trans centro
        public string c;//trans derecho
    }

    public class grafo
    {
        LinkedList<string[]> aux = new LinkedList<string[]>();//aca guardaremos nuestra lista de grupos
        string principal = "";

        nodo raiz;
        public void retear()
        {
            raiz = null;
        }



        public void lista_aux()
        {
            for (int i = 0; i < forx.grupos.grupo.Count; i++)
            {
                aux.AddLast(forx.grupos.grupo.ElementAt(i));
            }
            principal = forx.grupos.regex.ElementAt(0)[0];
        }
        public void armar_grafo()
        {
            int x = 0; //contador de estados
            nodo nuevo_n = new nodo();
            nodo nuevo_x = new nodo();
            raiz = nuevo_n;
            nuevo_n.id = x;
            nuevo_n.b = "ε";
            nuevo_n.cen = nuevo_x;
            x++;
            nuevo_x.id = x;
            //--------------------------------------------
            nodo a = null;        //nodos a reemplazar
            nodo b = null;
            nodo au = null;
            //--------------------------------------------
            for (int i = 0; i < aux.Count; i++)
            {
                //varios casos |,*,.
                string[] e = aux.ElementAt(i);
                if (e[5].Equals("*"))
                {

                }
                else if (e[5].Equals("|"))
                {
                    nodo n1 = new nodo();
                    nodo n2 = new nodo();
                    nodo n3 = new nodo();
                    nodo n4 = new nodo();
                    nodo n5 = new nodo();
                    nodo n6 = new nodo();
                }
                else if (e[5].Equals("."))
                {

                }
            }

        }

    }


}
