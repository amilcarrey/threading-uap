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
        static void Listar()
        {
            lock (productos)
            {
                foreach (var item in productos)
                {
                    Console.WriteLine($"Producto: {item.Nombre} - Precio: {item.PrecioUnitarioDolares} - Cantidad: {item.CantidadEnStock}");

                }
            }
        }
        static Barrier barrera = new Barrier(2);
        static Mutex mutex = new Mutex();

        public static void Tarea1()
      {
            lock (productos)
            {
                foreach (var item in productos)
                {
                    item.CantidadEnStock += 10;

                }
            }
            barrera.SignalAndWait();
                //throw new NotImplementedException();
            
            }
      static void Tarea2()
      {
            precio_dolar = 520;

            barrera.SignalAndWait();
         //throw new NotImplementedException();
      }
        static void punto2()
        {
            mutex.WaitOne();

            foreach (var item in productos)
            {
                item.PrecioUnitarioDolares = item.PrecioUnitarioDolares + (item.PrecioUnitarioDolares * 10 / 100);


            }
            mutex.ReleaseMutex(); 

        }
        static void Tarea3()
      {
        decimal totalInventario=0;
            foreach(var item in productos)
            {
                Console.WriteLine($"Producto: {item.Nombre}");
                decimal precioTotal = precio_dolar * item.CantidadEnStock * item.PrecioUnitarioDolares;
                totalInventario += precioTotal;
            }
            Console.WriteLine(totalInventario);
            


            //throw new NotImplementedException();
      }
       
      

      internal static void Excecute()
      {
            Thread hilo = new Thread(new ThreadStart(Tarea1));
            Thread hilo1 = new Thread(new ThreadStart(Tarea2));
            Thread hilo2=new Thread(new ThreadStart(punto2));
            Thread hilo3 = new Thread(new ThreadStart(Tarea3));
            hilo.Name = "Tarea1";
            hilo.Name = "tarea 2";
            hilo.Start();
            hilo1.Start();
            hilo2.Start();
            hilo3.Start();
            //throw new NotImplementedException();
        }
   }
}