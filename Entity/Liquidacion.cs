using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public abstract class Liquidacion
    {
        
            public string NumeroLiquidacion { get; set; }
            public string Identificacion { get; set; }
            public string Nombre { get; set; }
            public string TipoAfiliacion { get; set; }
            public decimal Tarifa { get; set; }
            public decimal Salario { get; set; }
            public decimal ValorServicio { get; set; }
            public decimal ValorCuota { get; set; }

            public abstract string CalcularLiquidacion(decimal salario, decimal valorServicio);
        





    }
}
