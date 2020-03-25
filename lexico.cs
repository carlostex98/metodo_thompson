using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyeco1_ocl
{
    public class lexico
    {
        //enviamos los tokens a otra clase
        public void scanner_x(string cadena)
        {
            //hacemos el mapeo de nustros caracteres
            //for por toda la cadena
            int cl = 0;//cl actual
            int ln = 0;//ln actual
            int e = 0;//estado
            int n = 0;//var de porcentaje
            string a1 = "";//var aux 1
            string a2 = "";//var aux 2
            string a3 = "";//var aux 3
            char c = ' ';
            char v = ' ';
            for (int i = 0; i < cadena.Length; i++)
            {
                c = cadena[i];

                if (i != cadena.Length - 1)
                {
                    v = cadena[i + 1];//predictivo
                }

                switch (e)
                {
                    case 0://inicial->comentario,cadena, esp en blanco
                        if (c == '/' && v == '/')
                        {
                            //comentario e 1
                            e = 1; i++; cl++;
                        }
                        else if (c == '<' && v == '!')//comentario multilinea
                        {
                            e = 2; i++; cl++;
                        }
                        else if (Char.IsLetter(c))//c es letra
                        {
                            a1 = "";
                            a1 += c;
                            e = 3;
                        }
                        else if (c == '%' && v == '%')
                        {
                            //estado de chunche
                            e = 14; i++; cl++;

                        }
                        else if (c != ' ' && c != '\n' && c != '\t')//error
                        {
                            forx.lsx.in_error(Char.ToString(c), ln, cl);
                            e = 0;
                        }
                        break;
                    case 1://fin comenatr
                        if (c == '\n')
                        {
                            e = 0;
                            //fin de comentario
                        }
                        break;
                    case 2://fin comentario multi
                        if (c == '!' && v == '>')
                        {
                            //hasta aqui
                            e = 0; i++; cl++;
                        }
                        break;
                    case 3://palabra reservada, hasta :,-> o espacio, tab o \n
                        if (Char.IsLetterOrDigit(c) || c == '_')
                        {
                            //sumamos
                            a1 += c;
                        }
                        else if (c == ':')//conj
                        {
                            if (a1.ToUpper().Trim().Equals("CONJ"))
                            {
                                //si la cadena es conj
                                //reportamos el token
                                forx.lsx.in_token("reservada", a1, ln, cl - a1.Length);
                                forx.lsx.in_token("Simbolo", ":", ln, cl);
                                a1 = "";
                                e = 5;
                            }
                            else
                            {
                                //error
                                forx.lsx.in_error(Char.ToString(c), ln, cl);
                                e = 0;
                            }
                        }
                        else if (c == ' ' || c == '\n' || c == '\t')
                        {
                            //salto estado
                            e = 4;
                            if (n == 0)
                            {
                                //def regular
                                forx.lsx.in_token("ID asignacion exp reg", a1, ln, cl - a1.Length);
                                //a1 = "";
                            }
                            else
                            {
                                //var a evaluar
                                forx.lsx.in_token("Cadena a evaluar", a1, ln, cl - a1.Length);
                                //a1 = "";
                            }
                            //para determinar que vino jaja
                        }
                        else if (c == '-' && v == '>')//exp reg o exp a evaluar
                        {
                            if (n == 0)
                            {
                                //def regular
                                forx.lsx.in_token("ID asignacion exp reg", a1, ln, cl - a1.Length);
                                forx.lsx.in_token("Simbolo", "->", ln, cl);
                                a2 = a1;
                                a1 = "";
                                e = 9;
                            }
                            else
                            {
                                //var a evaluar
                                forx.lsx.in_token("Cadena a evaluar", a1, ln, cl - a1.Length);
                                forx.lsx.in_token("Simbolo", "->", ln, cl);
                                a2 = a1;
                                a1 = "";
                                e = 10;
                            }

                            //a2 = a1;
                            //forx.lsx.in_token("Flecha asignacion", a1, ln, cl - a1.Length);
                        }
                        else
                        {
                            //error
                            forx.lsx.in_error(Char.ToString(c), ln, cl);
                            e = 0;
                        }

                        break;
                    case 4://estado indet de asignacion
                        //esp, tab, salto, ->, :
                        //no hay suma de char dado que viene de una finalizacion
                        if (c == ':')
                        {

                            if (a1.ToUpper().Trim().Equals("CONJ"))
                            {
                                //si la cadena es conj
                                //reportamos el token
                                forx.lsx.in_token("reservada", a1, ln, cl - a1.Length);
                                forx.lsx.in_token("Simbolo", ":", ln, cl);
                                a1 = "";
                                e = 5;
                            }
                            else
                            {
                                //error
                            }

                        }
                        else if (c == '-' && v == '>')
                        {
                            a2 = a1;
                            a1 = "";
                            if (n == 0)
                            {
                                e = 9;
                            }
                            else
                            {
                                e = 10;
                            }


                            forx.lsx.in_token("Simbolo", "->", ln, cl);
                            i++; cl++;
                        }
                        else if (c != ' ' && c != '\n' && c != '\t')
                        {
                            //error
                            forx.lsx.in_error(Char.ToString(c), ln, cl);
                            e = 0;
                        }

                        break;
                    case 5://de dos puntos
                        //venimos de conj
                        //name->value
                        //ante la primera letra cambiamos de estado
                        if (Char.IsLetter(c) || c == '_')
                        {
                            a1 = "";
                            a1 += c;
                            e = 7;
                        }
                        else if (c != ' ' && c != '\n' && c != '\t')
                        {
                            //error
                            forx.lsx.in_error(Char.ToString(c), ln, cl);
                            e = 0;
                        }


                        break;
                    case 6://de flecha
                           //clausurado

                        break;
                    case 7://despues de la definicion de conj
                        if (Char.IsLetterOrDigit(c) || c == '_')
                        {
                            a1 += c;
                        }
                        else if (c == '-' && v == '>')
                        {
                            //indica fin de nombre conjunto
                            a2 = a1;
                            forx.lsx.in_token("Identificador", a1, ln, cl - a1.Length);
                            forx.lsx.in_token("Simbolo", "->", ln, cl);
                            i++; cl++;
                            e = 8;
                            a1 = "";

                        }
                        else if (c != ' ' && c != '\n' && c != '\t')
                        {
                            //error
                            forx.lsx.in_error(Char.ToString(c), ln, cl);
                            e = 0;

                        }
                        break;
                    case 8:
                        //contenido de conjunto
                        /*simbolos permitidos
                             letra digito, coma, ~,!,&

                         */
                        if (c == ';')
                        {
                            //fin de declaracion
                            a3 = a1;
                            //dec de token 2
                            e = 0;
                            forx.lsx.in_token("Identificador", a1, ln, cl - a1.Length);
                            forx.lsx.in_token("Simbolo", ";", ln, cl);
                            forx.ins.in_conj(a2, a3);
                            a1 = a2 = a3 = "";
                            //Console.WriteLine("si conj");
                        }
                        else if (Char.IsLetterOrDigit(c) || c == ',' || c == '~' || c == '!' || c == '&')
                        {
                            a1 += c;
                        }
                        else if (c != ' ' && c != '\n' && c != '\t')
                        {
                            //error
                            forx.lsx.in_error(Char.ToString(c), ln, cl);
                            e = 0;
                        }

                        break;
                    case 9:
                        //fin definicion
                        //se permite cualquier simbolo
                        //
                        if (c == ';')
                        {
                            //reporte token y var
                            //ex reg
                            forx.lsx.in_token("Cadena regex", a1, ln, cl - a1.Length);
                            forx.lsx.in_token("Simbolo", ";", ln, cl);
                            a3 = a1;
                            forx.ins.in_xpr(a2.Trim(), a3.Trim());
                            a1 = a2 = a3 = "";
                            e = 0;
                        }
                        else
                        {
                            a1 += c;
                        }

                        break;

                    case 10:
                        //cadena a evaluar
                        //Console.WriteLine("f");
                        if (c == '"')
                        {
                            e = 11;
                        }
                        else if (c != ' ' && c != '\n' && c != '\t')
                        {
                            forx.lsx.in_error(Char.ToString(c), ln, cl);
                            e = 0;
                        }

                        break;

                    case 11:
                        if (c == '"')
                        {
                            a3 = a1;
                            e = 12;
                        }
                        else
                        {
                            a1 += c;
                        }

                        break;

                    case 12:
                        if (c == ';')
                        {
                            forx.ins.in_cadena(a2, a3);
                            forx.lsx.in_token("Cadena", a1, ln, cl - a1.Length);
                            forx.lsx.in_token("Simbolo", ";", ln, cl);
                            a1 = a2 = a3 = "";
                            e = 0;

                        }
                        else if (c != ' ' && c != '\n' && c != '\t')
                        {
                            forx.lsx.in_error(Char.ToString(c), ln, cl);
                            e = 0;
                        }

                        break;


                    case 14://estado chunche
                        if (c == '%' && v == '%')
                        {
                            e = 0; n = 1; i++; cl++;
                        }
                        else if (c != ' ' && c != '\n' && c != '\t')
                        {
                            //error
                            forx.lsx.in_error(Char.ToString(c), ln, cl);
                            e = 0;
                        }
                        break;



                    default:
                        break;
                }
                cl++;//sig caracter de la linea
                if (c == '\n')//contador
                {
                    ln++;
                    cl = 0;
                }
            }

        }


    }
}
