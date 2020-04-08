using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        public string b;//trans der
        public string c;//trans centro
        public bool v;
    }

    public class grafo
    {
        LinkedList<string[]> aux = new LinkedList<string[]>();//aca guardaremos nuestra lista de grupos
        LinkedList<string> rel = new LinkedList<string>();
        string principal = "";
        public string nomb_grafo = "";
        string gv = "";
        int y = 0;

        nodo raiz;
        public void retear()
        {
            raiz = null;
            aux.Clear();
            rel.Clear();
        }

        public void start_x(string ee)
        {
            retear();
            lista_aux();
            armar_grafo();
            nomb_grafo = ee; 
            generar_visual();
            y++;
            
            //Console.WriteLine(nomb_grafo);

        }

        public void lista_aux()
        {
            for (int i = 0; i < forx.grupos.grupo.Count; i++)
            {
                aux.AddLast(forx.grupos.grupo.ElementAt(i));
            }
            principal = forx.grupos.regex.ElementAt(0)[0];
        }
        public bool rel_x(string n)
        {
            bool x = false;
            for (int i = 0; i < rel.Count; i++)
            {
                if (rel.ElementAt(i).Equals(n))
                {
                    x = true;
                    break;

                }
            }
            if (!x) { rel.AddLast(n); }
            return x;
        }

        public void armar_grafo()
        {
            int x = 0; //contador de estados
            nodo nuevo_n = new nodo();
            raiz = nuevo_n;
            nuevo_n.a = "Ɛ";
            nuevo_n.id = x;
            x++;

            nodo nuevo_x = new nodo();
            nuevo_n.izq = nuevo_x;
            nuevo_x.a = principal;
            nuevo_x.id = x;
            x++;

            nodo nuevo_y = new nodo();
            nuevo_x.izq = nuevo_y;
            nuevo_y.id = x;
            x++;



            //--------------------------------------------
            nodo a1 = null;
            nodo a2 = null;//grupo g0
            nodo b1 = null;
            nodo b2 = null;//grupo g1
            nodo c1 = null;
            nodo c2 = null;//no se jaja

            //tarea inicial
            a1 = nuevo_n;
            a2 = nuevo_y;
            //--------------------------------------------
            for (int i = aux.Count - 1; i >= 0; i--)
            {
                //varios casos |,*,.
                string[] e = aux.ElementAt(i);
                if (e[5].Equals("*"))
                {
                    //tucum
                    nodo n1 = new nodo();
                    n1.id = x; x++;
                    nodo n2 = new nodo();
                    n2.id = x; x++;
                    nodo n3 = new nodo();
                    n3.id = x; x++;
                    nodo n4 = new nodo();
                    n4.id = x; x++;

                    n1.a = "Ɛ";
                    n2.a = e[1];
                    n3.a = "Ɛ";
                    n1.c = "Ɛ";
                    n3.c = "Ɛ";
                    //rels
                    n1.izq = n2;
                    n2.izq = n3;
                    n3.izq = n4;
                    n1.cen = n4;
                    n3.cen = n2;
                    if (a1 != null)
                    {
                        if (a1.izq.a.Equals(e[0]))
                        {
                            if (a1 != null)
                            {
                                if (a1.izq == b2)
                                {
                                    b2 = n1;
                                }
                                if (a1.izq == c2)
                                {
                                    c2 = n1;
                                }
                            }

                            a1.izq = n1;

                            if (a2.izq != null) { n4.izq = a2.izq; n4.a = a2.a; }
                            if (a2.der != null) { n4.der = a2.der; n4.b = a2.b; }
                            if (a2.cen != null)
                            {
                                if (a2.id < a2.cen.id)
                                {
                                    n4.cen = a2.cen; n4.c = a2.c;
                                }
                                else
                                {
                                    n4.cen = n1; n4.c = "Ɛ";
                                }


                            }



                            a1 = a2 = null;
                            if (e[2].Equals("grup"))
                            {
                                a1 = n1;
                                a2 = n3;
                            }






                        }
                        else if (a1.der != null)
                        {
                            if (a1.der.a.Equals(e[0]))
                            {
                                a1.der = n1;

                                //si venimos de uno que no es *
                                if (a2.izq != null) { n4.izq = a2.izq; n4.a = a2.a; }
                                if (a2.der != null) { n4.der = a2.der; n4.b = a2.b; }
                                if (a2.cen != null)
                                {
                                    if (a2.id < a2.cen.id)
                                    {
                                        n4.cen = a2.cen; n4.c = a2.c;
                                    }
                                    else
                                    {
                                        n4.cen = n1; n4.c = "Ɛ";
                                    }


                                }




                                a1 = a2 = null;
                                if (e[2].Equals("grup"))
                                {
                                    a1 = n1;
                                    a2 = n3;
                                }


                            }


                        }


                    }

                    if (b1 != null)
                    {
                        if (b1.izq.a.Equals(e[0]))
                        {
                            if (b1 != null)
                            {

                                if (b1.izq == a2)
                                {
                                    a2 = n1;
                                }
                                if (b1.izq == c2)
                                {
                                    c2 = n1;
                                }
                            }

                            b1.izq = n1;
                            if (b2.izq != null) { n4.izq = b2.izq; n4.a = b2.a; }
                            if (b2.der != null) { n4.der = b2.der; n4.b = b2.b; }
                            if (b2.cen != null)
                            {
                                if (b2.id < b2.cen.id)
                                {
                                    n4.cen = b2.cen; n4.c = b2.c;
                                }
                                else
                                {
                                    n4.cen = n1; n4.c = "Ɛ";
                                }


                            }



                            b1 = b2 = null;
                            if (e[2].Equals("grup"))
                            {
                                b1 = n1;
                                b2 = n3;
                            }


                        }

                        else if (b1.der != null)
                        {
                            if (b1.der.a.Equals(e[0]))
                            {
                                b1.der = n1;

                                if (c1 != null)
                                {
                                    if (c1.izq == a2)
                                    {
                                        a2 = n1;
                                    }
                                    if (c1.izq == b2)
                                    {
                                        b2 = n1;
                                    }

                                }

                                if (b2.izq != null) { n4.izq = b2.izq; n4.a = b2.a; }
                                if (b2.der != null) { n4.der = b2.der; n4.b = b2.b; }
                                if (b2.cen != null)
                                {
                                    if (b2.id < b2.cen.id)
                                    {
                                        n4.cen = b2.cen; n4.c = b2.c;
                                    }
                                    else
                                    {
                                        n4.cen = n1; n4.c = "Ɛ";
                                    }


                                }
                                b1 = b2 = null;
                                if (e[2].Equals("grup"))
                                {
                                    b1 = n1;
                                    b2 = n3;
                                }


                            }

                        }


                    }


                    if (c1 != null)
                    {
                        if (c1.izq.a.Equals(e[0]))
                        {

                            if (c1 != null)
                            {
                                if (c1.izq == a2)
                                {
                                    a2 = n1;
                                }
                                if (c1.izq == b2)
                                {
                                    b2 = n1;
                                }
                            }

                            c1.izq = n1;
                            if (c2.izq != null) { n4.izq = c2.izq; n4.a = c2.a; }
                            if (c2.der != null) { n4.der = c2.der; n4.b = c2.b; }
                            if (c2.cen != null)
                            {
                                if (c2.id < c2.cen.id)
                                {
                                    n4.cen = c2.cen; n4.c = c2.c;
                                }
                                else
                                {
                                    n4.cen = n1; n4.c = "Ɛ";
                                }


                            }
                            c1 = c2 = null;
                            if (e[2].Equals("grup"))
                            {
                                c1 = n1;
                                c2 = n3;
                            }




                        }
                        else if (c1.der != null)
                        {
                            if (c1.der.a.Equals(e[0]))
                            {
                                c1.der = n1;
                                if (c2.izq != null) { n4.izq = c2.izq; n4.a = c2.a; }
                                if (c2.der != null) { n4.der = c2.der; n4.b = c2.b; }
                                if (c2.cen != null)
                                {
                                    if (c2.id < c2.cen.id)
                                    {
                                        n4.cen = c2.cen; n4.c = c2.c;
                                    }
                                    else
                                    {
                                        n4.cen = n1; n4.c = "Ɛ";
                                    }


                                }
                                c1 = c2 = null;
                                if (e[2].Equals("grup"))
                                {
                                    c1 = n1;
                                    c2 = n3;
                                }

                            }

                        }

                    }

                }
                else if (e[5].Equals("|"))
                {
                    //Console.WriteLine("eeeee");
                    nodo n1 = new nodo();
                    n1.a = "Ɛ";
                    n1.b = "Ɛ";
                    n1.id = x; x++;

                    nodo n2 = new nodo();
                    if (e[1].Equals("vacio_vacio"))
                    {
                        n2.a = "Ɛ";
                    }
                    else
                    {
                        n2.a = e[1];
                    }

                    n2.id = x; x++;

                    nodo n3 = new nodo();
                    if (e[3].Equals("vacio_vacio"))
                    {
                        n3.a = "Ɛ";
                    }
                    else
                    {
                        n3.a = e[3];
                    }
                    n3.id = x; x++;

                    nodo n4 = new nodo();
                    n4.a = "Ɛ";
                    n4.id = x; x++;

                    nodo n5 = new nodo();
                    n5.a = "Ɛ";
                    n5.id = x; x++;


                    nodo n6 = new nodo();//nodo final
                    n6.id = x; x++;


                    n1.izq = n3;//es reemplazado
                    n3.izq = n4;
                    n4.izq = n6;
                    n1.der = n2;
                    n2.izq = n5;
                    n5.izq = n6;//este hereda

                    //lo ponemos
                    if (a1 != null)
                    {
                        //Console.WriteLine(a1.izq.a);
                        if (a1.izq.a.Equals(e[0]))
                        {
                            if (a1 != null)
                            {
                                if (a1.izq == b2)
                                {
                                    b2 = n1;
                                }
                                if (a1.izq == c2)
                                {
                                    c2 = n1;
                                }
                            }

                            a1.izq = n1;

                            //si venimos de uno que no es *
                            if (a2.izq != null) { n6.izq = a2.izq; n6.a = a2.a; }
                            if (a2.der != null) { n6.der = a2.der; n6.b = a2.b; }
                            if (a2.cen != null)
                            {
                                if (a2.id < a2.cen.id)
                                {
                                    n6.cen = a2.cen; n6.c = a2.c;
                                }
                                else
                                {
                                    n6.cen = n1; n6.c = "Ɛ";
                                }


                            }



                            a1 = a2 = null;
                            if (e[4].Equals("grup"))
                            {
                                a1 = n1;
                                a2 = n4;
                            }
                            if (e[2].Equals("grup"))
                            {
                                //place
                                if (b1 == null)
                                {
                                    b1 = n1;
                                    b2 = n5;
                                }
                                else
                                {
                                    c1 = n1;
                                    c2 = n5;
                                }
                            }




                        }
                        else if (a1.der != null)
                        {
                            if (a1.der.a.Equals(e[0]))
                            {
                                a1.der = n1;

                                //si venimos de uno que no es *
                                if (a2.izq != null) { n6.izq = a2.izq; n6.a = a2.a; }
                                if (a2.der != null) { n6.der = a2.der; n6.b = a2.b; }
                                if (a2.cen != null)
                                {
                                    if (a2.id < a2.cen.id)
                                    {
                                        n6.cen = a2.cen; n6.c = a2.c;
                                    }
                                    else
                                    {
                                        n6.cen = n1; n6.c = "Ɛ";
                                    }


                                }


                                a1 = a2 = null;
                                if (e[4].Equals("grup"))
                                {
                                    a1 = n1;
                                    a2 = n4;
                                }
                                if (e[2].Equals("grup"))
                                {
                                    //place
                                    if (b1 == null)
                                    {
                                        b1 = n1;
                                        b2 = n5;
                                    }
                                    else
                                    {
                                        c1 = n1;
                                        c2 = n5;
                                    }
                                }


                            }


                        }
                    }

                    if (b1 != null)
                    {
                        if (b1.izq.a.Equals(e[0]))
                        {
                            if (b1 != null)
                            {
                                if (b1.izq == a2)
                                {
                                    a2 = n1;
                                }
                                if (b1.izq == c2)
                                {
                                    c2 = n1;
                                }
                            }

                            b1.izq = n1;

                            //si venimos de uno que no es *
                            if (b2.izq != null) { n6.izq = b2.izq; n6.a = b2.a; }
                            if (b2.der != null) { n6.der = b2.der; n6.b = b2.b; }
                            if (b2.cen != null)
                            {
                                if (b2.id < b2.cen.id)
                                {
                                    n6.cen = b2.cen; n6.c = b2.c;
                                }
                                else
                                {
                                    n6.cen = n1; n6.c = "Ɛ";
                                }


                            }



                            b1 = b2 = null;
                            if (e[4].Equals("grup"))
                            {
                                b1 = n1;
                                b2 = n4;
                            }
                            if (e[2].Equals("grup"))
                            {
                                //place
                                if (a1 == null)
                                {
                                    a1 = n1;
                                    a2 = n5;
                                }
                                else
                                {
                                    c1 = n1;
                                    c2 = n5;
                                }
                            }


                        }
                        else if (b1.der != null)
                        {
                            if (b1.der.a.Equals(e[0]))
                            {
                                b1.der = n1;

                                //si venimos de uno que no es *
                                if (b2.izq != null) { n6.izq = b2.izq; n6.a = b2.a; }
                                if (b2.der != null) { n6.der = b2.der; n6.b = b2.b; }
                                if (b2.cen != null)
                                {
                                    if (b2.id < b2.cen.id)
                                    {
                                        n6.cen = b2.cen; n6.c = b2.c;
                                    }
                                    else
                                    {
                                        n6.cen = n1; n6.c = "Ɛ";
                                    }


                                }


                                b1 = b2 = null;
                                if (e[4].Equals("grup"))
                                {
                                    b1 = n1;
                                    b2 = n4;
                                }
                                if (e[2].Equals("grup"))
                                {
                                    //place
                                    if (a1 == null)
                                    {
                                        a1 = n1;
                                        a2 = n5;
                                    }
                                    else
                                    {
                                        c1 = n1;
                                        c2 = n5;
                                    }
                                }


                            }


                        }
                    }


                    if (c1 != null)
                    {
                        if (c1.izq.a.Equals(e[0]))
                        {
                            if (c1 != null)
                            {
                                if (c1.izq == a2)
                                {
                                    a2 = n1;
                                }
                                if (c1.izq == b2)
                                {
                                    b2 = n1;
                                }
                            }

                            c1.izq = n1;

                            //si venimos de uno que no es *
                            if (c2.izq != null) { n6.izq = c2.izq; n6.a = c2.a; }
                            if (c2.der != null) { n6.der = c2.der; n6.b = c2.b; }
                            if (c2.cen != null)
                            {
                                if (c2.id < c2.cen.id)
                                {
                                    n6.cen = c2.cen; n6.c = c2.c;
                                }
                                else
                                {
                                    n6.cen = n1; n6.c = "Ɛ";
                                }


                            }



                            c1 = c2 = null;
                            if (e[4].Equals("grup"))
                            {
                                c1 = n1;
                                c2 = n4;
                            }
                            if (e[2].Equals("grup"))
                            {
                                //place
                                if (a1 == null)
                                {
                                    a1 = n1;
                                    a2 = n5;
                                }
                                else
                                {
                                    b1 = n1;
                                    b2 = n5;
                                }
                            }




                        }
                        else if (c1.der != null)
                        {
                            if (c1.der.a.Equals(e[0]))
                            {
                                c1.der = n1;

                                //si venimos de uno que no es *
                                if (c2.izq != null) { n6.izq = c2.izq; n6.a = c2.a; }
                                if (c2.der != null) { n6.der = c2.der; n6.b = c2.b; }
                                if (c2.cen != null)
                                {
                                    if (c2.id < c2.cen.id)
                                    {
                                        n6.cen = c2.cen; n6.c = c2.c;
                                    }
                                    else
                                    {
                                        n6.cen = n1; n6.c = "Ɛ";
                                    }


                                }


                                c1 = c2 = null;
                                if (e[4].Equals("grup"))
                                {
                                    c1 = n1;
                                    c2 = n4;
                                }
                                if (e[2].Equals("grup"))
                                {
                                    //place
                                    if (a1 == null)
                                    {
                                        a1 = n1;
                                        a2 = n5;
                                    }
                                    else
                                    {
                                        b1 = n1;
                                        b2 = n5;
                                    }
                                }

                            }



                        }

                    }


                }
                else if (e[5].Equals("."))
                {
                    nodo n1 = new nodo();
                    n1.a = e[1];
                    n1.id = x;
                    x++;
                    nodo n2 = new nodo();
                    n2.a = e[3];
                    n2.id = x;
                    x++;
                    nodo n3 = new nodo();
                    n3.id = x;
                    x++;
                    n1.izq = n2;
                    n2.izq = n3;
                    if (a1 != null)
                    {
                        if (a1.izq.a.Equals(e[0]))
                        {
                            if (a1 != null)
                            {
                                if (a1.izq == b2)
                                {
                                    b2 = n1;
                                }
                                if (a1.izq == c2)
                                {
                                    c2 = n1;
                                }
                            }

                            a1.izq = n1;

                            //si venimos de uno que no es *
                            if (a2.izq != null) { n3.izq = a2.izq; n3.a = a2.a; }
                            if (a2.der != null) { n3.der = a2.der; n3.b = a2.b; }
                            if (a2.cen != null)
                            {
                                if (a2.id < a2.cen.id)
                                {
                                    n3.cen = a2.cen; n3.c = a2.c;
                                }
                                else
                                {
                                    n3.cen = n1; n3.c = "Ɛ";
                                }


                            }



                            //a1 = a2 = null;
                            if (e[2].Equals("grup"))
                            {
                                a2 = n2;
                            }
                            if (e[4].Equals("grup"))
                            {
                                //place
                                if (b1 == null)
                                {
                                    b1 = n1;
                                    b2 = n3;

                                }
                                else
                                {
                                    c1 = n1;
                                    c2 = n3;
                                }
                            }



                        }
                        else if (a1.der != null)
                        {
                            if (a1.der.a.Equals(e[0]))
                            {
                                a1.der = n1;

                                //si venimos de uno que no es *
                                if (a2.izq != null) { n3.izq = a2.izq; n3.a = a2.a; }
                                if (a2.der != null) { n3.der = a2.der; n3.b = a2.b; }
                                if (a2.cen != null)
                                {
                                    if (a2.id < a2.cen.id)
                                    {
                                        n3.cen = a2.cen; n3.c = a2.c;
                                    }
                                    else
                                    {
                                        n3.cen = n1; n3.c = "Ɛ";
                                    }


                                }



                                //a1 = a2 = null;
                                if (e[2].Equals("grup"))
                                {
                                    //a1 = n1;
                                    a2 = n2;
                                }
                                if (e[4].Equals("grup"))
                                {
                                    //place
                                    if (b1 == null)
                                    {
                                        b1 = n1;
                                        b2 = n3;
                                    }
                                    else
                                    {
                                        c1 = n1;
                                        c2 = n3;
                                    }
                                }


                            }

                        }
                    }

                    if (b1 != null)
                    {
                        if (b1.izq.a.Equals(e[0]))
                        {
                            if (b1 != null)
                            {
                                if (b1.izq == a2)
                                {
                                    a2 = n1;
                                }
                                if (b1.izq == c2)
                                {
                                    c2 = n1;
                                }

                            }

                            b1.izq = n1;

                            //si venimos de uno que no es *
                            if (b2.izq != null) { n3.izq = b2.izq; n3.a = b2.a; }
                            if (b2.der != null) { n3.der = b2.der; n3.b = b2.b; }
                            if (b2.cen != null)
                            {
                                if (b2.id < b2.cen.id)
                                {
                                    n3.cen = b2.cen; n3.c = b2.c;
                                }
                                else
                                {
                                    n3.cen = n1; n3.c = "Ɛ";
                                }


                            }



                            //b1 = b2 = null;
                            if (e[2].Equals("grup"))
                            {
                                //b1 = n1;
                                b2 = n2;
                            }
                            if (e[4].Equals("grup"))
                            {
                                //place
                                if (a1 == null)
                                {
                                    a1 = n1;
                                    a2 = n3;
                                }
                                else
                                {
                                    c1 = n1;
                                    c2 = n3;
                                }
                            }




                        }
                        else if (b1.der != null)
                        {
                            if (b1.der.a.Equals(e[0]))
                            {
                                b1.der = n1;

                                //si venimos de uno que no es *
                                if (b2.izq != null) { n3.izq = b2.izq; n3.a = b2.a; }
                                if (b2.der != null) { n3.der = b2.der; n3.b = b2.b; }
                                if (b2.cen != null)
                                {
                                    if (b2.id < b2.cen.id)
                                    {
                                        n3.cen = b2.cen; n3.c = b2.c;
                                    }
                                    else
                                    {
                                        n3.cen = n1; n3.c = "Ɛ";
                                    }


                                }

                                //b1 = b2 = null;
                                if (e[2].Equals("grup"))
                                {
                                    //b1 = n1;
                                    b2 = n2;
                                }
                                if (e[4].Equals("grup"))
                                {
                                    //place
                                    if (a1 == null)
                                    {
                                        a1 = n1;
                                        a2 = n3;
                                    }
                                    else
                                    {
                                        c1 = n1;
                                        c2 = n3;
                                    }
                                }


                            }

                        }
                    }
                    if (c1 != null)
                    {
                        if (c1.izq.a.Equals(e[0]))
                        {
                            if (c1 != null)
                            {
                                if (c1.izq == a2)
                                {
                                    a2 = n1;
                                }
                                if (c1.izq == b2)
                                {
                                    b2 = n1;
                                }
                            }

                            c1.izq = n1;

                            //si venimos de uno que no es *
                            if (c2.izq != null) { n3.izq = c2.izq; n3.a = c2.a; }
                            if (c2.der != null) { n3.der = c2.der; n3.b = c2.b; }
                            if (c2.cen != null)
                            {
                                if (c2.id < c2.cen.id)
                                {
                                    n3.cen = c2.cen; n3.c = c2.c;
                                }
                                else
                                {
                                    n3.cen = n1; n3.c = "Ɛ";
                                }


                            }



                            //c1 = c2 = null;
                            if (e[2].Equals("grup"))
                            {
                                //c1 = n1;
                                c2 = n2;
                            }
                            if (e[4].Equals("grup"))
                            {
                                //place
                                if (a1 == null)
                                {
                                    a1 = n1;
                                    a2 = n3;
                                }
                                else
                                {
                                    b1 = n1;
                                    b2 = n3;
                                }
                            }





                        }
                        else if (c1.der != null)
                        {
                            if (c1.der.a.Equals(e[0]))
                            {
                                c1.der = n1;

                                //si venimos de uno que no es *
                                if (c2.izq != null) { n3.izq = c2.izq; n3.a = c2.a; }
                                if (c2.der != null) { n3.der = c2.der; n3.b = c2.b; }
                                if (c2.cen != null)
                                {
                                    if (c2.id < c2.cen.id)
                                    {
                                        n3.cen = c2.cen; n3.c = c2.c;
                                    }
                                    else
                                    {
                                        n3.cen = n1; n3.c = "Ɛ";
                                    }


                                }


                                //c1 = c2 = null;
                                if (e[2].Equals("grup"))
                                {
                                    //c1 = n1;
                                    c2 = n2;
                                }
                                if (e[4].Equals("grup"))
                                {
                                    //place
                                    if (a1 == null)
                                    {
                                        a1 = n1;
                                        a2 = n3;
                                    }
                                    else
                                    {
                                        b1 = n1;
                                        b2 = n3;
                                    }
                                }


                            }



                        }

                    }




                }
            }

        }

        void preOrder(nodo nodex)
        {
            if (nodex != null)
            {
                gv += nodex.id.ToString() + "[label=\" " + nodex.id.ToString() + " \"]; \n";
                if (nodex.izq != null)
                {
                    if (!rel_x(nodex.id.ToString() + "->" + nodex.izq.id.ToString() + "[label=\" " + nodex.a + " \"]; \n"))
                    {
                        gv += nodex.id.ToString() + "->" + nodex.izq.id.ToString() + "[label=\" " + nodex.a + " \"]; \n";
                    }

                }
                if (nodex.der != null)
                {
                    if (!rel_x(nodex.id.ToString() + "->" + nodex.der.id.ToString() + "[label=\" " + nodex.b + " \"]; \n"))
                    {
                        gv += nodex.id.ToString() + "->" + nodex.der.id.ToString() + "[label=\" " + nodex.b + " \"]; \n";
                    }

                }
                if (nodex.cen != null)
                {
                    if (!rel_x(nodex.id.ToString() + "->" + nodex.cen.id.ToString() + "[label=\" " + nodex.c + " \"]; \n"))
                    {
                        gv += nodex.id.ToString() + "->" + nodex.cen.id.ToString() + "[label=\" " + nodex.c + " \"]; \n";
                    }

                }
                preOrder(nodex.izq);
                preOrder(nodex.der);
            }
        }

        public void generar_visual()
        {
            // Saves the Image via a FileStream created by the OpenFile method.
            string nom= "grafo_" + nomb_grafo + ".dot";
            Encoding ascii = Encoding.ASCII;
            StreamWriter bw;
            try
            {
                bw = new StreamWriter(new FileStream(nom, FileMode.Create), ascii);
                gv = "";
                preOrder(raiz);
                bw.WriteLine("digraph X {");
                bw.WriteLine("rankdir=LR;");
                bw.WriteLine(gv);
                bw.WriteLine("}");
                //Console.WriteLine(nom);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + "\n error.");
                return;
            }

            bw.Close();

            string xx = "dot" + " grafo_" + nomb_grafo + ".dot" + " -Tpng -o" + " grafo_" + nomb_grafo + ".png";

            /*System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "CMD.exe";
            startInfo.Arguments = xx;
            process.StartInfo = startInfo;
            process.Start();*/

            //System.Diagnostics.Process.Start("CMD.exe", xx);

            

            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine(xx);
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            Console.WriteLine(cmd.StandardOutput.ReadToEnd());

        }

    }


}
