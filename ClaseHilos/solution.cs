using System.Threading;

namespace ClaseHilos
{
    internal class Producto
    {
        public string Nombre { get; set; }
        public decimal PrecioUnitarioDolares { get; set; }
        public int CantidadEnStock { get; set; }

        public Producto(string nombre, decimal precioUnitario, int cantidadEnStock)
        {
            Nombre = nombre;
            PrecioUnitarioDolares = precioUnitario;
            CantidadEnStock = cantidadEnStock;
        }
    }
    internal class Solution //reference: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/lock
    {

        static List<Producto> productos = new List<Producto>
        {
            new Producto("Camisa", 10, 50),
            new Producto("Pantalón", 8, 30),
            new Producto("Zapatilla/Champión", 7, 20),
            new Producto("Campera", 25, 100),
            new Producto("Gorra", 16, 10)
        };

        static int precio_dolar = 500;
        static Barrier barr = new Barrier(4);

        static void Tarea1()
        {
            lock (productos)
            {
                foreach (Producto producto in productos)
                {
                    producto.CantidadEnStock += 10;
                }
            }
            lock (Console.Out)
            {
                Console.WriteLine("Stock actualizado: \n");
                foreach (Producto producto in productos)
                {
                    Console.WriteLine($" Producto: {producto.Nombre}, Stock: {producto.CantidadEnStock}, Precio: {producto.PrecioUnitarioDolares}");
                }
            }
            barr.SignalAndWait();
        }
        static void Tarea2()
        {
            precio_dolar = 750;
            lock (Console.Out)
            {
                Console.WriteLine("\nNuevo precio dolar: " + precio_dolar);
            }
            barr.SignalAndWait();
        }
        static void Tarea3()
        {
            Thread.Sleep(3000);
            barr.SignalAndWait();
           
            Console.WriteLine("\nInforme de inventario:\n");
            decimal precioTotalInventario = 0;
            lock (Console.Out)
            {
                foreach (Producto producto in productos)
                {
                    Console.WriteLine($" Producto: {producto.Nombre}, Stock: {producto.CantidadEnStock}, Precio: {producto.PrecioUnitarioDolares}");
                    precioTotalInventario += producto.PrecioUnitarioDolares * producto.CantidadEnStock;
                }

                Console.WriteLine($"\nPrecio total del inventario: {precioTotalInventario}");
            }
            
        }

        static void Tarea4()
        {
            barr.SignalAndWait();
            lock (productos)
            {
                foreach (Producto producto in productos)
                {
                    decimal aumento = producto.PrecioUnitarioDolares * 0.1m;
                    producto.PrecioUnitarioDolares += aumento;
                }
            }
            lock (Console.Out)
            {
                Console.WriteLine("\nTarea 4: Precios post 10% de inflacion: \n");
                foreach (Producto producto in productos)
                {
                    Console.WriteLine($" Producto: {producto.Nombre}, Precio: {producto.PrecioUnitarioDolares}");
                }
            }

        }


        internal static void Excecute()
        {
            Thread hilo1 = new Thread(new ThreadStart(Tarea1));
            Thread hilo2 = new Thread(new ThreadStart(Tarea2));
            Thread hilo3 = new Thread(new ThreadStart(Tarea3));
            Thread hilo4 = new Thread(new ThreadStart(Tarea4));


            hilo1.Start();
            hilo2.Start();
            hilo3.Start();
            hilo4.Start();

            Console.ReadLine();
        }
    }
}