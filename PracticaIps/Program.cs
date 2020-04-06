using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using Entity;

namespace PracticaIps
{
    class Program
    {
        static Liquidacion liquidacion;
        static RegimenSubsidiado liquidacionSubsidiado = new RegimenSubsidiado();
        static LiquidacionService liquidacionService = new LiquidacionService();

        static void Main(string[] args)
        {
            int opcion;
            do
            {
                Console.Clear();
                Console.WriteLine("\n ----------- Menu -----------");
                Console.WriteLine(" 1. Registrar Paciente");
                Console.WriteLine(" 2. Consultar");
                Console.WriteLine(" 3  Eliminar");
                Console.WriteLine(" 4. Modificar");
                Console.WriteLine(" 5. Salir");
                Console.WriteLine(" --------------------------");
                Console.Write(" Digite una opcion: "); opcion = Convert.ToInt32(Console.ReadLine());
                switch (opcion)
                {
                    case 1: RegistrarPaciente(); break;
                    case 2: Consultar(); break;
                    case 3: Eliminar(); break;
                    case 4: Modificar(); break;
                    default: break;
                }
            } while (opcion != 5);
        }

        static public void RegistrarPaciente()
        {
            Console.Clear();
            int opcion;
            Console.WriteLine("\n 1. Registrar Paciente\n");
            Console.WriteLine(" Informacion del paciente");
            Console.WriteLine(" 1. Subsidiado \t\t 2. Contributivo");
            Console.Write(" Digite una opcion: "); opcion = Convert.ToInt32(Console.ReadLine());

            if (opcion == 1)
            {
                liquidacion = new RegimenSubsidiado();
                LlenarInformacion(liquidacion, "Subsidiado");
                Console.WriteLine(liquidacionService.Guardar(liquidacion));
            }
            else if (opcion == 2)
            {
                liquidacion = new RegimenContributivo();
                LlenarInformacion(liquidacion, "Contributivo");
                Console.WriteLine(liquidacionService.Guardar(liquidacion));
            }
            Console.ReadKey();
        }

        static public void LlenarInformacion(Liquidacion liquidacion, string tipo)
        {
            Console.Write(" Digite numero de liquidacion: "); liquidacion.NumeroLiquidacion = Console.ReadLine();
            Console.Write(" Digite identificacion: "); liquidacion.Identificacion = Console.ReadLine();
            Console.Write(" Digite nombre: "); liquidacion.Nombre = Console.ReadLine();
            Console.Write(" Digite valor del servicio: "); liquidacion.ValorServicio = Convert.ToDecimal(Console.ReadLine());
            Console.Write(" Digite salario: "); liquidacion.Salario = Convert.ToDecimal(Console.ReadLine());
            liquidacion.TipoAfiliacion = tipo;
            Console.WriteLine(liquidacionSubsidiado.CalcularLiquidacion(liquidacion.Salario, liquidacion.ValorServicio));
            liquidacion.Tarifa = liquidacionSubsidiado.Tarifa;
            liquidacion.ValorCuota = liquidacionSubsidiado.ValorCuota;
        }
    
        static public void Consultar()
        {
            Console.Clear();
            Console.WriteLine("\n 2. Consultar\n");
            foreach (var item in liquidacionService.Consultar())
            {
                Console.WriteLine(" -----------------------");
                Console.WriteLine($" Numero de liquidacion: {item.NumeroLiquidacion}");
                Console.WriteLine($" Identificacion: {item.Identificacion}");
                Console.Write($" Nombre: {item.Nombre}");
                Console.WriteLine($" Valor del servicio: {item.ValorServicio}");
                Console.WriteLine($" Salario: {item.Salario}");
                Console.WriteLine($" Tipo de Afiliacion: {item.TipoAfiliacion}");
                Console.WriteLine($" Tarifa aplicada: {item.Tarifa}");
                Console.WriteLine($" Cuota moderada: {item.ValorCuota}");
                Console.WriteLine(" -----------------------\n");
            }
            Console.Write("\n Presione enter para continuar..."); Console.ReadKey();
        }

        static public void Eliminar()
        {
            string numeroLiquidacion;
            Console.Write("\n Digite numero de liquidacion a eliminar: "); numeroLiquidacion = Console.ReadLine();
            Console.WriteLine(liquidacionService.Eliminar(numeroLiquidacion));
            Console.ReadKey();
        }

       
            static public void Buscar()
            {
                string NumeroLiquidacion;
                Console.Write("\n Digite NumeroLiquidacion: "); NumeroLiquidacion = Console.ReadLine();
                liquidacion = liquidacionService.Buscar(NumeroLiquidacion);
                if (liquidacion == null)
                {
                    Console.WriteLine($"\n El numero de liquidacion [{NumeroLiquidacion}] no existe");
                }
                else
                {

                    Console.WriteLine("\n Informacion de persona");
                    Console.WriteLine($" Numero de liquidacion: {liquidacion.NumeroLiquidacion}");
                    Console.WriteLine($" Identificacion: {liquidacion.Identificacion}");
                    Console.WriteLine($" Nombre: {liquidacion.Nombre}");
                    Console.WriteLine($" Valor del servicio: {liquidacion.ValorServicio}");
                    Console.WriteLine($" Salario: {liquidacion.Salario}");
                    Console.WriteLine($" Tipo de Afiliacion: {liquidacion.TipoAfiliacion}");
                    Console.WriteLine($" Tarifa aplicada: {liquidacion.Tarifa}");
                    Console.WriteLine($" Cuota moderada: {liquidacion.ValorCuota}");

                }
                Console.Write("\n Pulse enter para continuar..."); Console.ReadKey();

            }
        static public void Modificar()
        {
            string NumeroLiquidacion;
            Console.Write("\n Digite numero de liquidacion a Modificar: "); NumeroLiquidacion = Console.ReadLine();
            if(liquidacionService.Buscar(NumeroLiquidacion) == null)
            {
                Console.WriteLine("\n El numero de liquidacion no existe...");
            }
            else
            {
                liquidacion = liquidacionService.Buscar(NumeroLiquidacion);
                if (liquidacion.TipoAfiliacion.Equals("Subsidiado"))
                {
                    Console.Write(" Digite numero valor del serivicio: "); liquidacion.ValorServicio = Convert.ToDecimal(Console.ReadLine());
                    liquidacionSubsidiado.CalcularLiquidacion(liquidacion.Salario, liquidacion.ValorServicio);
                    liquidacion.ValorCuota = liquidacionSubsidiado.ValorCuota;
                    Console.WriteLine($" Su nueva cuota moderada es: {liquidacion.ValorCuota}");
                    Console.WriteLine(liquidacionService.Modificar(liquidacion));
                    Console.ReadKey();
                }
            }

        }
    }
    }
   

