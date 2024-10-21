using System;
using System.Collections.Generic;

/*
 Desarrollar un programa que permita al usuario gestionar una cuenta bancaria. El programa
deberá utilizar un menú que permita realizar diferentes operaciones sobre el saldo de la cuenta.
Menú de opciones:
1. Consultar saldo
2. Depositar dinero
3. Retirar dinero
4. Salir
El programa debe permitir al usuario ingresar la cantidad para depositar o retirar, actualizar el
saldo y mostrar los resultados. Si se elige la opción de retiro, debe verificar que el saldo sea
suficiente
*/

namespace GestionCuentaBancaria
{
    public class CuentaBancaria
    {
        private double saldo; //variable que almacena el saldo actual de la cuenta
        private const double SALDO_MINIMO = 1000; //saldo mínimo recomendado en la cuenta

        //constructor que inicializa el saldo con un valor inicial
        public CuentaBancaria(double saldoInicial)
        {
            saldo = saldoInicial;
        }

        //método para consultar el saldo y mostrar advertencia si el saldo es menor al mínimo
        public void ConsultarSaldo()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Su saldo actual es: C${saldo:N2}");//N: pone una coma para separar los miles
                                                                   //redondea a dos decimalesma 
            if (saldo < SALDO_MINIMO) //si el saldo es menor que el mínimo
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Advertencia: Su saldo es inferior a C$1000. Considere hacer un depósito.");
            }
        }

        //método para depositar dinero en la cuenta
        public void Depositar(double cantidad)
        {
            if (cantidad > 0) //verifica que la cantidad a depositar sea positiva
            {
                saldo += cantidad; //incrementa el saldo con el monto depositado
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Depósito exitoso. Su nuevo saldo es: C${saldo:N2}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: La cantidad a depositar debe ser positiva.");
            }
        }

        //método para retirar dinero de la cuenta, verificando si hay fondos suficientes
        public void Retirar(double cantidad)
        {
            if (cantidad > saldo) //si el monto a retirar es mayor que el saldo
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: Fondos insuficientes para completar esta transacción.");
            }
            else if (cantidad <= 0) //verifica que el monto de retiro sea positivo
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: La cantidad a retirar debe ser mayor que cero.");
            }
            else
            {
                saldo -= cantidad; //decrementa el saldo con el monto retirado
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Retiro exitoso. Su nuevo saldo es: C${saldo:N2}");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(""""
            --------------------------------------------------------
            |Bienvenido al sistema de gestión de su cuenta bancaria.|
            --------------------------------------------------------
            """");

            //inicia cuenta con un saldo inicial proporcionado por el usuario
            Console.Write("Ingrese su primer deposito: C$");
            double saldoInicial = ObtenerCantidadValida(); //llama al método para validar la cantidad

            CuentaBancaria cuenta = new CuentaBancaria(saldoInicial); //crea una nueva cuenta con el saldo inicial

            bool salir = false; //controla el bucle del menú

            //menú principal del sistema
            while (!salir)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine(""""
                     __       __  ________  __    __  __    __ 
                    /  \     /  |/        |/  \  /  |/  |  /  |
                    $$  \   /$$ |$$$$$$$$/ $$  \ $$ |$$ |  $$ |
                    $$$  \ /$$$ |$$ |__    $$$  \$$ |$$ |  $$ |
                    $$$$  /$$$$ |$$    |   $$$$  $$ |$$ |  $$ |
                    $$ $$ $$/$$ |$$$$$/    $$ $$ $$ |$$ |  $$ |
                    $$ |$$$/ $$ |$$ |_____ $$ |$$$$ |$$ \__$$ |
                    $$ | $/  $$ |$$       |$$ | $$$ |$$    $$/ 
                    $$/      $$/ $$$$$$$$/ $$/   $$/  $$$$$$/  
                """");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("1. Consultar saldo");
                Console.WriteLine("2. Depositar dinero");
                Console.WriteLine("3. Retirar dinero");
                Console.WriteLine("4. Salir");
                Console.Write("Seleccione una opción: ");

                string opcion = Console.ReadLine() ?? "";//captura la opción elegida por el usuario

                Console.WriteLine();
                Console.Clear();
                //ejecuta la opción seleccionada por el usuario
                switch (opcion)
                {
                    case "1":
                        cuenta.ConsultarSaldo(); //consulta el saldo
                        break;
                    case "2":
                        Console.Write("Ingrese la cantidad a depositar: C$");
                        double deposito = ObtenerCantidadValida(); //captura el monto a depositar
                        cuenta.Depositar(deposito); //realiza el depósito
                        break;
                    case "3":
                        Console.Write("Ingrese la cantidad a retirar: C$");
                        double retiro = ObtenerCantidadValida(); //captura el monto a retirar
                        cuenta.Retirar(retiro); //realiza el retiro
                        break;
                    case "4":
                        salir = true; //sale del bucle y termina el programa
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("Gracias por utilizar nuestro sistema. ¡Hasta luego!:)");
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Opción no válida. Por favor, seleccione una opción del 1 al 4.");
                        break;
                }
            }
        }

        //método para asegurar que el usuario ingrese una cantidad válida (número positivo)
        private static double ObtenerCantidadValida()
        {
            double cantidad;
            //bucle que valida que la entrada sea un número positivo
            while (!double.TryParse(Console.ReadLine(), out cantidad) || cantidad < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Entrada inválida. Ingrese una cantidad válida (mayor o igual a C$0): ");
            }
            return cantidad;//devuelve la cantidad validada
        }
    }
}

