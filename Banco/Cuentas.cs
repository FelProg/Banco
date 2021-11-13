using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco
{
    class Cuentas
    {
        public int ID { get; set; }
        public int NumeroDeCuenta { get; set; }
        public DateTime FechaDeAlta { get; set; }
        public Decimal LimiteDeCredito { get; set; }
        public Decimal Saldo { get; set; }
        public int ClienteId { get; set; }
    }
}
