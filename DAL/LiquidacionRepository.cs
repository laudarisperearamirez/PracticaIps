using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;

namespace DAL
{
     public  class LiquidacionRepository
    {
        string ruta = @"Liquidacion.txt";
        static List<Liquidacion> liquidacions;

        public LiquidacionRepository()
        {
            liquidacions = new List<Liquidacion>();
        }

        public void Guardar(Liquidacion liquidacion)
        {
            FileStream file = new FileStream(ruta, FileMode.Append);
            StreamWriter writer = new StreamWriter(file);
            writer.WriteLine(liquidacion.NumeroLiquidacion + ";" + liquidacion.TipoAfiliacion + ";"
                + liquidacion.Identificacion + ";" + liquidacion.Nombre + ";" + liquidacion.Salario + ";"
                + liquidacion.Tarifa + ";" + liquidacion.ValorServicio + ";" + liquidacion.ValorCuota);
            writer.Close();
            file.Close();
        }

        public List<Liquidacion> Consultar()
        {
            liquidacions.Clear();
            FileStream file = new FileStream(ruta, FileMode.OpenOrCreate);
            StreamReader reader = new StreamReader(file);
            string linea = string.Empty;
            while ((linea = reader.ReadLine()) != null)
            {
                Liquidacion liquidacion = Mapear(linea);
                liquidacions.Add(liquidacion);
            }
            file.Close();
            reader.Close();
            return liquidacions;
        }

        public Liquidacion Mapear(string linea)
        {
            Liquidacion liquidacion;

            string[] datos = linea.Split(';');
            if (datos[1].Equals("Subsidiado"))
            {
                liquidacion = new RegimenSubsidiado();
            }
            else
            {
                liquidacion = new RegimenContributivo();
            }

            liquidacion.NumeroLiquidacion = datos[0];
            liquidacion.TipoAfiliacion = datos[1];
            liquidacion.Identificacion = datos[2];
            liquidacion.Nombre = datos[3];
            liquidacion.Salario = decimal.Parse(datos[4]);
            liquidacion.Tarifa = decimal.Parse(datos[5]);
            liquidacion.ValorServicio = decimal.Parse(datos[6]);
            liquidacion.ValorCuota = decimal.Parse(datos[7]);

            return liquidacion;
        }

        public void Eliminar(Liquidacion liquidacionCuotaModeradora)
        {
            liquidacions.Clear();
            liquidacions = Consultar();
            liquidacions.Remove(liquidacionCuotaModeradora);
            FileStream file = new FileStream(ruta, FileMode.Create);
            file.Close();
            foreach (var item in liquidacions)
            {
                if (item.NumeroLiquidacion != liquidacionCuotaModeradora.NumeroLiquidacion)
                {
                    Guardar(item);
                }
            }

        }
        public Liquidacion Buscar(string NumeroLiquidacion)
        {
            liquidacions.Clear();
            liquidacions = Consultar();
            foreach (var item in liquidacions)
            {
                if (item.NumeroLiquidacion.Equals(NumeroLiquidacion))
                {
                    return item;

                }

            }
            return null;
        }
        public void Modificar(Liquidacion liquidacion)
        {
            liquidacions.Clear();
            liquidacions = Consultar();
            FileStream file = new FileStream(ruta, FileMode.Create);
            file.Close();
            foreach (var item in liquidacions)
            {
                if (item.NumeroLiquidacion != liquidacion.NumeroLiquidacion)
                {
                    Guardar(item);
                }
                else
                {
                    Guardar(liquidacion);
                }

            }

        }


    }
   
}
