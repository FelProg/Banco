using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco
{
    class Tarjeta
    {
        public int ID { get; set; }
        public int NumTarjeta { get; set; }
        public DateTime FechaExp { get; set; }
        public int CVV { get; set; }
        public int Cuenta { get; set; }
    }
}
