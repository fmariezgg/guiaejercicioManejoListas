using System;
using System.Collections.Generic;//permite el uso de las colecciones genericas <List>, entre otras
using System.Linq;//permite realizar consultas a colecciones de datos(lista, arrays) de manera concisa
using System.Text;//proporciona clases para manipulacion de textos, como String Builder para construir o modificar cadenas de texto
using System.Threading.Tasks;//usa el patron Task, util para operaciones como la lectura de archivos o llamadas de bases de datos sin detener el hilo principal
/*
 Desarrollar un programa que se comporte como un diccionario Inglés-Español; esto es, solicitará
una palabra en inglés y escribirá la correspondiente palabra en español. Para hacer más sencillo
el ejercicio, el número de parejas de palabras estará limitado a 5. Por ejemplo, suponer que
introducimos las siguientes parejas de palabras:
book libro
green verde
mouse ratón

Una vez finalizada la introducción de las listas de palabras, pasamos al modo traducción, de
forma que si introducimos green, la respuesta ha de ser verde. Si la palabra no se encuentra, se
emitirá un mensaje que lo indique.
El programa constará de dos métodos, aparte de Main():
1. crearDiccionario(). Este método creará el diccionario.
2. traducir(). Este método realizará la labor de traducción.
*/

namespace Diccionario
{
    internal class DiccionarioT
    {
        public static void Main(string[] args)
        {
            int op;
            List<Tuple<string, string>> diccionario = new List<Tuple<string, string>> { };//declaro e inicializo la lista
            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(""""



                 /$$$$$$$$                       /$$                       /$$                        
                |__  $$__/                      | $$                      | $$                        
                   | $$  /$$$$$$  /$$$$$$   /$$$$$$$ /$$   /$$  /$$$$$$$ /$$$$$$    /$$$$$$   /$$$$$$ 
                   | $$ /$$__  $$|____  $$ /$$__  $$| $$  | $$ /$$_____/|_  $$_/   /$$__  $$ /$$__  $$
                   | $$| $$  \__/ /$$$$$$$| $$  | $$| $$  | $$| $$        | $$    | $$  \ $$| $$  \__/
                   | $$| $$      /$$__  $$| $$  | $$| $$  | $$| $$        | $$ /$$| $$  | $$| $$      
                   | $$| $$     |  $$$$$$$|  $$$$$$$|  $$$$$$/|  $$$$$$$  |  $$$$/|  $$$$$$/| $$      
                   |__/|__/      \_______/ \_______/ \______/  \_______/   \___/   \______/ |__/
                   -------------------------------------------------------
                   | 1.Ingrese las palabras a traducir con su traduccion |
                   |2.Traductor                                          |
                   | 3.Salir                                             |
                   -------------------------------------------------------
                """");//""""multilinea
                Console.Write("Elija una opcion: ");
                //?? para manejar posibles null cuando uses Console.ReadLine(), "" cadena vacia
                string input = Console.ReadLine() ?? "0";//si la entrada es null, por defecto ponemos "0".
                op = int.Parse(input);//ahora es seguro llamar a Parse, ya que nunca será null

                //se hace el llamado a los metodos
                switch (op)
                {
                    case 1:
                        diccionario = crearDiccionario(); //asignamos  diccionario
                        break;
                    case 2:
                        if (diccionario.Count != 0) //verificamos que el diccionario tenga datos para la traduccion
                        {
                            traducir(diccionario);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nPrimero debes crear el diccionario con palabras (opción 1).");
                        }
                        break;
                    case 3:
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Opción no válida.");
                        break;

                }


            } while (op != 3);

        }

        public static List<Tuple<string, string>> crearDiccionario()//ponemos el tipodo, metodo
                                                                    //crear estructura de tipo string, string
        {
            List<Tuple<string, string>> diccionario = new List<Tuple<string, string>>();//()hacemos llamado al contructor, inicializa la estructura
            //pedimos las pares de palabras
            for (int i = 0; i < 5; i++)
            {
                //variables independientes
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"Introduzca la palabra {i + 1} en ingles: ");//interpolar strings
                string palabra1 = Console.ReadLine() ?? "";//si Console.ReadLine() es nulo, asigna una cadena vacía
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Introduzca la palabra {i + 1} en español: ");
                string palabra2 = Console.ReadLine() ?? "";
                //por cada par de palabra la ingresamos al objeto y anadimos a la estructura
                //agregamos a la lista
                diccionario.Add(new Tuple<string, string>(palabra1 ?? "", palabra2 ?? ""));//recuperacion de las palabras e incorporacion al diccionario

            }
            return diccionario;//se lo retornamos a diccionario
        }

        //seria de tipo void por solo vamos a pasar no a retornar
        public static void traducir(List<Tuple<string, string>> diccionario)
        {
            Console.Write("Introduzca la palabra a traducir: ");
            string palcomp = Console.ReadLine() ?? "";//palabra a traducir
            bool encontrado = false;//bandera

            foreach (var duo in diccionario)//todos los elemntos estan en duo
            {
                if (duo.Item1.Equals(palcomp, StringComparison.OrdinalIgnoreCase))
                //de la tupla el primer elemento y se compara con el primer elemento de la lista
                //StringComparison.OrdinalIgnoreCase, hace que la comparacion no distinga entre mayusculas y minusculas
                {
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.Write($"La traduccion de la palabra {palcomp} es: {duo.Item2}. ");
                    encontrado = true;
                    break;
                }
                else if (duo.Item2.Equals(palcomp, StringComparison.OrdinalIgnoreCase))
                {
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.Write($"---La traduccion de la palabra {palcomp} es: {duo.Item1}. ---");
                    encontrado = true;
                }
            }

            if (!encontrado)//si no lo encontro
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"---La palabra {palcomp} no fue encontrada.---");
            }

        }

    }


}

