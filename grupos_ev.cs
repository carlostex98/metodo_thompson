using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyeco1_ocl
{
    public class grupos_ev
    {
        LinkedList<string[]> regex = new LinkedList<string[]>();
        LinkedList<string[]> grupo = new LinkedList<string[]>();
        LinkedList<string[]> aux = new LinkedList<string[]>();
        //al final de nustro analisis es necesario transformar a+ -> a.a*
        /* aclaraciones:
         * los conjuntos estan declarados por su nombre, sin llaves
         * 
         * se plica transformacion de grupos y leyes de idempotencia
         * regex es SOLO una lista, mas no la libreria regex
         * 
         * sin importar la forma del epresion, en regex solo quedará un
         * solo grupo que es el unicial
         * de no ser asi no se compilara
         * 
         * las expresiones ? y + son convertidas a su equivalente con *
         * 
         */

        string in_cadena = "";
        string id = "";
        int g = 0;

        public void iniciar(string id_var, string rgx)
        {
            //seteo de variables y limpieza del mismo
            in_cadena = rgx;
            id = id_var;
            g = 0;
            regex.Clear();
            grupo.Clear();
            aux.Clear();

            cadena(rgx + " ");
            //nd();
            build_groups();
            prints();
            nd();
        }

        //primer paso es pasar la expresion a una lista


        public void cadena(string cadena)
        {
            int e = 0;
            string a = "";
            int s = 0;
            for (int i = 0; i < cadena.Length; i++)
            {
                switch (e)
                {
                    case 0:
                        if (Char.IsLetter(cadena[i]))
                        {
                            e = 1;
                            a += cadena[i];
                        }
                        else if (cadena[i] == '.' || cadena[i] == '|' || cadena[i] == '*' || cadena[i] == '+' || cadena[i] == '?')//vermos si existe (a)?
                        {

                            if (cadena[i] == '.' || cadena[i] == '|')
                            {
                                string[] ax = { cadena[i].ToString(), "op", s.ToString() };
                                regex.AddLast(ax);
                                s++;
                            }
                            else
                            {
                                string[] ax = { cadena[i].ToString(), "sum", s.ToString() };
                                regex.AddLast(ax);
                                s++;
                            }

                        }
                        else if (cadena[i] == '"')
                        {
                            e = 2;
                        }
                        break;
                    case 1:
                        if (cadena[i] == '.' || cadena[i] == '|' || cadena[i] == '*' || cadena[i] == '+' || cadena[i] == '?')//vermos si existe (a)?
                        {
                            e = 0;
                            if (cadena[i] == '.' || cadena[i] == '|')
                            {
                                string[] ax = { cadena[i].ToString(), "op", s.ToString() };
                                regex.AddLast(ax);
                                s++;
                            }
                            else
                            {
                                string[] ax = { cadena[i].ToString(), "sum", s.ToString() };
                                regex.AddLast(ax);
                                s++;
                            }
                        }
                        else if (cadena[i] == '\n' || cadena[i] == ' ' || cadena[i] == '\t')
                        {
                            //solo rompe coso
                            e = 0;
                            string[] ax = { a, "conj", s.ToString() };
                            a = "";
                            s++;
                            regex.AddLast(ax);
                        }
                        else
                        {
                            //suma
                            a += cadena[i];
                        }
                        break;

                    case 2:
                        if (cadena[i] == '"')
                        {
                            e = 0;
                            string[] ax = { a, "lit", s.ToString() };
                            a = "";
                            s++;
                            regex.AddLast(ax);
                        }
                        else
                        {

                            a += cadena[i];
                        }
                        break;

                    default:
                        break;
                }
            }
        }

        public void build_groups()
        {
            int x = 0;
            int a = 0;
            //regex.Remove(regex.ElementAt(0));
            for (int i = 0; i < regex.Count; i++)
            {
                if (regex.ElementAt(i)[1].Equals("op"))
                {
                    if ((i + 2) < regex.Count)
                    {
                        if (regex.ElementAt(i + 1)[1].Equals("lit") || regex.ElementAt(i + 1)[1].Equals("conj") || regex.ElementAt(i + 1)[1].Equals("grup"))
                        {
                            if (regex.ElementAt(i + 2)[1].Equals("lit") || regex.ElementAt(i + 2)[1].Equals("conj") || regex.ElementAt(i + 2)[1].Equals("grup"))
                            {

                                //tenemos grupo
                                string[] g = { "g" + x.ToString(), regex.ElementAt(i + 1)[0], regex.ElementAt(i + 1)[1], regex.ElementAt(i + 2)[0], regex.ElementAt(i + 2)[1], regex.ElementAt(i)[0] };
                                grupo.AddLast(g);
                                string[] s = { "g" + x.ToString(), "grup", regex.ElementAt(i)[2] };
                                for (int j = 0; j < regex.Count; j++)
                                {
                                    if (j == i)
                                    {
                                        aux.AddLast(s);
                                        j = j + 2;
                                    }
                                    else
                                    {
                                        aux.AddLast(regex.ElementAt(j));
                                    }
                                }
                                regex.Clear();
                                for (int j = 0; j < aux.Count; j++)
                                {
                                    regex.AddLast(aux.ElementAt(j));
                                }
                                aux.Clear();

                                i = 0;
                                a = 1;
                                x++;
                            }
                        }
                    }
                }
                if (regex.ElementAt(i)[1].Equals("sum") && a == 0)
                {
                    if ((i + 1) < regex.Count)
                    {
                        if (regex.ElementAt(i + 1)[1].Equals("lit") || regex.ElementAt(i + 1)[1].Equals("conj") || regex.ElementAt(i + 1)[1].Equals("grup"))
                        {

                            if (regex.ElementAt(i)[0].Equals("+"))
                            {
                                //en este punto se crean dos grupos
                                //uno del asterisco
                                //otro de la concatenacion
                                string[] s = { "g" + ((x + 1).ToString()), "grup", regex.ElementAt(i)[2] };//este es el del final
                                string[] g = { "g" + x.ToString(), regex.ElementAt(i + 1)[0], regex.ElementAt(i + 1)[1], "_", "_", "*" };
                                grupo.AddLast(g);
                                string[] gx = { "g" + (x + 1).ToString(), "g" + x.ToString(), "grup", "g" + x.ToString(), "grup", "." };
                                grupo.AddLast(gx);
                                for (int j = 0; j < regex.Count; j++)
                                {
                                    if (j == i)
                                    {
                                        aux.AddLast(s);
                                        j = j + 1;
                                    }
                                    else
                                    {
                                        aux.AddLast(regex.ElementAt(j));
                                    }
                                }
                                regex.Clear();
                                for (int j = 0; j < aux.Count; j++)
                                {
                                    regex.AddLast(aux.ElementAt(j));
                                }
                                aux.Clear();
                                i = 0;
                                a = 1;
                                x++;
                                x++;

                            }
                            else if (regex.ElementAt(i)[0].Equals("?"))
                            {
                                //solo se crea un grupo
                                //lalala
                                string[] s = { "g" + x.ToString(), "grup", regex.ElementAt(i)[2] };//grupo nuevo
                                string[] g = { "g" + x.ToString(), regex.ElementAt(i + 1)[0], regex.ElementAt(i + 1)[1], "vacio_vacio", "grup", "|" };
                                grupo.AddLast(g);
                                for (int j = 0; j < regex.Count; j++)
                                {
                                    if (j == i)
                                    {
                                        aux.AddLast(s);
                                        j = j + 1;
                                    }
                                    else
                                    {
                                        aux.AddLast(regex.ElementAt(j));
                                    }
                                }
                                regex.Clear();
                                for (int j = 0; j < aux.Count; j++)
                                {
                                    regex.AddLast(aux.ElementAt(j));
                                }
                                aux.Clear();
                                i = 0;
                                a = 1;
                                x++;

                            }
                            else
                            {
                                string[] s = { "g" + x.ToString(), "grup", regex.ElementAt(i)[2] };
                                string[] g = { "g" + x.ToString(), regex.ElementAt(i + 1)[0], regex.ElementAt(i + 1)[1], "_", "_", regex.ElementAt(i)[0] };
                                grupo.AddLast(g);
                                for (int j = 0; j < regex.Count; j++)
                                {
                                    if (j == i)
                                    {
                                        aux.AddLast(s);
                                        j = j + 1;
                                    }
                                    else
                                    {
                                        aux.AddLast(regex.ElementAt(j));
                                    }
                                }
                                regex.Clear();
                                for (int j = 0; j < aux.Count; j++)
                                {
                                    regex.AddLast(aux.ElementAt(j));
                                }
                                aux.Clear();
                                i = 0;
                                a = 1;
                                x++;
                            }




                            //nd();
                        }
                    }
                }
                if (a == 1)
                {
                    i = -1;
                    a = 0;
                }

            }
        }


        public void prints()
        {
            Console.WriteLine("---------------");
            for (int i = 0; i < grupo.Count; i++)
            {
                Console.WriteLine(grupo.ElementAt(i)[0] + " - " + grupo.ElementAt(i)[1] + " - " + grupo.ElementAt(i)[2] + " - " + grupo.ElementAt(i)[3] + " - " + grupo.ElementAt(i)[4] + " - " + grupo.ElementAt(i)[5]);
            }
            Console.WriteLine("-------e-------\n");
        }

        public void nd()
        {
            Console.WriteLine("-----------------------------------");
            for (int i = 0; i < regex.Count; i++)
            {
                Console.WriteLine(regex.ElementAt(i)[0] + " - " + regex.ElementAt(i)[1] + " - " + regex.ElementAt(i)[2]);
            }
            Console.WriteLine("------ttt-------");
        }

    }
}
