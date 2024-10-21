using System;
using System.Collections.Generic;
using System.Linq;

/*
 Crear un programa que simule la gestión de un inventario en una tienda. Utilizar un menú para
agregar, eliminar, modificar y consultar productos en el inventario. Cada producto tendrá un
código, nombre, cantidad y precio.
Menú de opciones:
1. Agregar producto
2. Eliminar producto
3. Modificar producto
4. Consultar producto
5. Mostrar todos los productos 
6. Salir 
 */

namespace GestionInventario
{
    //struct para representar un producto
    public struct Producto
    {
        public int Codigo;//almacena el código único del producto
        public string Nombre;//almacena el nombre del producto
        public int Cantidad;//almacena la cantidad del producto
        public double Precio;//almacena el precio del producto

        //constructor para inicializar un producto con datos
        public Producto(int codigo, string nombre, int cantidad, double precio)
        {
            Codigo = codigo;
            Nombre = nombre;
            Cantidad = cantidad;
            Precio = precio;
        }

        //muestra los detalles del producto en la consola
        public void MostrarDetalles()
        {
            Console.WriteLine($"Código: {Codigo}, Nombre: {Nombre}, Cantidad: {Cantidad}, Precio: {Precio:C}");
        }
    }

    //clase para manejar el inventario de productos
    public class ProductoService
    {
        private List<Producto> productos; //lista para almacenar los productos

        //constructor que inicializa la lista de productos
        public ProductoService()
        {
            productos = new List<Producto>();
        }

        //verifica si un producto con el código dado ya existe en el inventario
        private bool ProductoExiste(int codigo)
        {
            return productos.Any(p => p.Codigo == codigo);
            /*
             metodo para verificar si algun producto que esta en la lista tiene un codigo que coincida con el que se pasa como argumento

            Any(), metodo LinQ que recorre la coleccion (lista Producto) y devuelve true si almenos un elemento cumple con la condicion

            p => p.Codigo == codigo, expresion lambda, "para cada producto p en la lista, comprueba si p.Codigo es igual a codigo. Si
            algun producto cumple con la condicion Any() devuelve true, de lo contrario, false. Es decir este metodo revisa si ya hay algun producto
            en el inventario con el mismo codigo.
             */
        }

        //agrega un producto al inventario, validando que no haya duplicados
        public void AgregarProducto(Producto producto)
        {
            if (ProductoExiste(producto.Codigo)) //si ya existe, muestra un error
            {
                Console.WriteLine("Error: Ya existe un producto con el mismo código.");
            }
            else
            {
                productos.Add(producto); //si no existe, lo agrega
                Console.WriteLine("Producto agregado correctamente.");
            }
        }

        //elimina un producto del inventario por su código
        public void EliminarProducto(int codigo)
        {
            Producto? productoAEliminar = BuscarProducto(codigo);  //busca el producto por código

            if (productoAEliminar.HasValue) //si lo encuentra
            {
                productos.Remove(productoAEliminar.Value); //lo elimina de la lista
                Console.WriteLine("Producto eliminado correctamente.");
            }
            else //si no lo encuentra
            {
                Console.WriteLine("Error: Producto no encontrado.");
            }
        }

        //modifica un producto existente por su código
        public void ModificarProducto(int codigo, string nuevoNombre, int nuevaCantidad, double nuevoPrecio)
        {
            Producto? productoAModificar = BuscarProducto(codigo); //busca el producto
            if (productoAModificar.HasValue) //si lo encuentra
            {
                Producto modificado = new Producto(codigo, nuevoNombre, nuevaCantidad, nuevoPrecio); //crea uno nuevo con los cambios
                int index = productos.IndexOf(productoAModificar.Value); //obtiene el índice en la lista
                productos[index] = modificado; //reemplaza el viejo producto con el nuevo
                Console.WriteLine("Producto modificado correctamente.");
            }
            else //si no lo encuentra
            {
                Console.WriteLine("Error: Producto no encontrado.");
            }
        }

        //busca un producto en la lista por su código
        public Producto? BuscarProducto(int codigo)
        {
            foreach (Producto producto in productos)
            {
                if (producto.Codigo == codigo) return producto;
            }

            return null;
        }

        //consulta y muestra los detalles de un producto por código
        public void ConsultarProducto(int codigo)
        {
            Producto? producto = BuscarProducto(codigo); //busca el producto
            if (producto.HasValue)
            {
                producto.Value.MostrarDetalles(); //si lo encuentra, muestra sus detalles
            }
            else
            {
                Console.WriteLine("Error: Producto no encontrado.");
            }
        }

        //muestra todos los productos del inventario
        public void MostrarTodos()
        {
            try
            {
                if (productos.Count > 0) //verifica si hay productos
                {
                    Producto[] productosArray = productos.ToArray(); //convierte la lista en un arreglo
                    foreach (var producto in productosArray) //recorre y muestra cada producto
                    {
                        producto.MostrarDetalles();
                    }
                }
                else
                {
                    Console.WriteLine("No hay productos en el inventario."); //si no hay productos
                }
            }
            catch (Exception excep) //manejo de errores
            {
                Console.WriteLine($"Error al mostrar productos: {excep.Message}");
            }
        }
    }

    public class Program
    {
        //método para obtener el código del producto del usuario
        private static int ObtenerCodigoProducto()
        {
            int codigo;
            Console.Write("Ingrese el código del producto: ");
            while (!int.TryParse(Console.ReadLine(), out codigo) || codigo <= 0) //validación de entrada
            {
                Console.Write("Código inválido. Intente de nuevo (número positivo): ");
            }
            return codigo;
        }

        //método para obtener el precio del producto del usuario
        private static double ObtenerPrecioProducto()
        {
            double precio;
            Console.Write("Ingrese el precio del producto: ");
            while (!double.TryParse(Console.ReadLine(), out precio) || precio < 0) //validación de entrada
            {
                Console.Write("Precio inválido. Intente de nuevo (número positivo): ");
            }
            return precio;
        }

        //método para obtener la cantidad del producto del usuario
        private static int ObtenerCantidadProducto()
        {
            int cantidad;
            Console.Write("Ingrese la cantidad del producto: ");
            while (!int.TryParse(Console.ReadLine(), out cantidad) || cantidad < 0) //validación de entrada
            {
                Console.Write("Cantidad inválida. Intente de nuevo (número positivo): ");
            }
            return cantidad;
        }

        public static void Main()
        {
            ProductoService inventario = new ProductoService(); //crea el servicio de inventario
            bool continuar = true; //controla el bucle del menú

            while (continuar)
            {
                Console.WriteLine("\n---- Menú de Inventario ----");
                Console.WriteLine("1. Agregar producto");
                Console.WriteLine("2. Eliminar producto");
                Console.WriteLine("3. Modificar producto");
                Console.WriteLine("4. Consultar producto");
                Console.WriteLine("5. Mostrar todos los productos");
                Console.WriteLine("6. Salir");
                Console.Write("Seleccione una opción: ");

                string opcion = Console.ReadLine() ?? ""; //lee la opción del usuario
                try
                {
                    switch (opcion)
                    {
                        case "1":
                            int codigo = ObtenerCodigoProducto(); //obtiene código
                            Console.Write("Ingrese el nombre del producto: ");
                            string nombre = Console.ReadLine() ?? ""; //lee nombre
                            int cantidad = ObtenerCantidadProducto(); //obtiene cantidad
                            double precio = ObtenerPrecioProducto(); //obtiene precio

                            Producto nuevoProducto = new Producto(codigo, nombre, cantidad, precio); //crea el producto
                            inventario.AgregarProducto(nuevoProducto); //lo agrega
                            break;

                        case "2":
                            codigo = ObtenerCodigoProducto(); //obtiene el código
                            inventario.EliminarProducto(codigo); //elimina el producto
                            break;

                        case "3":
                            codigo = ObtenerCodigoProducto(); //obtiene el código
                            Console.Write("Ingrese el nuevo nombre del producto: ");
                            nombre = Console.ReadLine() ?? ""; //lee nuevo nombre
                            cantidad = ObtenerCantidadProducto(); //obtiene nueva cantidad
                            precio = ObtenerPrecioProducto(); //obtiene nuevo precio

                            inventario.ModificarProducto(codigo, nombre, cantidad, precio); //modifica el producto
                            break;

                        case "4":
                            codigo = ObtenerCodigoProducto(); //obtiene el código
                            inventario.ConsultarProducto(codigo); //consulta y muestra el producto
                            break;

                        case "5":
                            inventario.MostrarTodos(); //muestra todos los productos
                            break;

                        case "6":
                            continuar = false; //salir del bucle
                            break;

                        default:
                            Console.WriteLine("Opción no válida. Intente de nuevo."); //si la opción es inválida
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}"); //maneja cualquier error
                }
            }
        }
    }
}

