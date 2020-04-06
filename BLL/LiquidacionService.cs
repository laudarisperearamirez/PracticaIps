using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entity;

namespace BLL
{
   public class LiquidacionService

    {
        LiquidacionRepository liquidacionRepository;

        public LiquidacionService()
        {
            liquidacionRepository = new LiquidacionRepository();
        }

        public string Guardar(Liquidacion liquidacion)
        {
            liquidacionRepository.Guardar(liquidacion);
            return $"Liquidacion: {liquidacion.NumeroLiquidacion} guardada";
        }

        public List<Liquidacion> Consultar()
        {
            return liquidacionRepository.Consultar();
        }

       
        public Liquidacion Buscar(string NumeroLiquidacion)
        {
            Liquidacion liquidacion = liquidacionRepository.Buscar(NumeroLiquidacion);
            return liquidacion;
        }

        public string Eliminar(string NumeroLiquidacion)
        {

            Liquidacion liquidacion = liquidacionRepository.Buscar(NumeroLiquidacion);
            if (liquidacion != null)
            {
                liquidacionRepository.Eliminar(liquidacion);
                return $"Los datos de {liquidacion.NumeroLiquidacion} se elimino correctamente";
            }
            else
            {
                return $"Error";
            }


        }



        public string Modificar(Liquidacion liquidacion)
        {
            try
            {
                liquidacionRepository.Modificar(liquidacion);
                return $"Los Datos de la liquidacion{liquidacion.NumeroLiquidacion}se  modifico correctamente";


            }
            catch (Exception e)
            {

                return "Error de la lectura" + e.Message;
            }
        }

    }




}

