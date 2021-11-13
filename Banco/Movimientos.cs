using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco
{
    class Movimientos
    {
        public int ID { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public decimal Cantidad { get; set; }
        public string TipoMovimiento { get; set; }
        public string Descripcion { get; set; }
        public int TarjetaID { get; set; }
        public int ClienteID { get; set; }
    }
}

