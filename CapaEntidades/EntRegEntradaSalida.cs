using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class EntRegEntradaSalida
    {
        public int IdTabla { get; set; }
        public DateTime FechaHora { get; set; }
        public string TipoRegistro { get; set; }
        public int IdUsuario { get; set; }
    }
}
