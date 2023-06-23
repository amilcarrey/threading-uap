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
    internal class Solution
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
        static Mutex precioMutex = new();
        static Barrier informesBarrier = new(4);
        static SemaphoreSlim semaphore = new(1, 1);

        static void Tarea1()
        {
            semaphore.Wait();
            foreach(var product in productos)
            {
                product.CantidadEnStock += 10;
            }
            semaphore.Release();
            Console.WriteLine("stock actualizado");
            informesBarrier.SignalAndWait();
        }
        static void Tarea2()
        {
            precioMutex.WaitOne();
            precio_dolar = new Random().Next(450, 550);
            precioMutex.ReleaseMutex();
            Console.WriteLine($"dolar actualizado a AR$ {precio_dolar:C}");
            informesBarrier.SignalAndWait();
        }
        static void Tarea3()
        {
            informesBarrier.SignalAndWait();
            Console.WriteLine("::::Informes::::");
            decimal total = 0;

            Tabla.PrintLine();
            Tabla.PrintRow("Producto", "Stock", "Precio AR$");
            Tabla.PrintLine();
            foreach (var product in productos)
            {
                decimal precioArs = product.PrecioUnitarioDolares * precio_dolar;
                total += precioArs * product.CantidadEnStock;
                Tabla.PrintRow(product.Nombre, product.CantidadEnStock.ToString(), precioArs.ToString("C"));
            }
            Tabla.PrintLine();

            Console.WriteLine($"---- Total: {total:C}");

        }

        static void Tarea4()
        {
            semaphore.Wait();
            foreach (var product in productos)
            {
                product.PrecioUnitarioDolares += product.PrecioUnitarioDolares * 0.1m;
            }
            semaphore.Release();
            Console.WriteLine("precios actualizados");
            informesBarrier.SignalAndWait();
        }
        
        internal static void Execute ()
        {
            var tarea1 = new Thread(new ThreadStart(Tarea1));
            var tarea2 = new Thread(new ThreadStart(Tarea2));
            var tarea3 = new Thread(new ThreadStart(Tarea3));
            var tarea4 = new Thread(new ThreadStart(Tarea4));

            tarea3.Start();
            tarea4.Start();
            tarea2.Start();
            tarea1.Start();

            Console.WriteLine("hello floppa, this is StockManager by BigNodes");
        }
    }
}