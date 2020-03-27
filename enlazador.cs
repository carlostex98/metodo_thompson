using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyeco1_ocl
{
    public class enlazador
    {
        public void iniciar_thompson()
        {
            for (int i = 0; i < forx.ins.expresiones.Count; i++)
            {
                forx.grupos.iniciar(forx.ins.expresiones.ElementAt(i)[0], forx.ins.expresiones.ElementAt(i)[1]);
                forx._grafo.retear();
            }
        }

    }
}
