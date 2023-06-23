using System.Net;

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
      static Barrier barrera = new Barrier(4, (b) =>
        {
           Console.WriteLine($"Post-Phase action: {b.CurrentPhaseNumber}");
        });
      public static Barrier barrera_
      {
         get { return barrera; }
      }
      static Mutex mutex = new Mutex();
      static List<Producto> productos = new List<Producto>
        {
            new Producto("Camisa", 10, 50),
            new Producto("Pantalón", 8, 30),
            new Producto("Zapatilla/Champión", 7, 20),
            new Producto("Campera", 25, 100),
            new Producto("Gorra", 16, 10)
        };

      static int precio_dolar = 500;

      static void Tarea1()
      {
         mutex.WaitOne();
         for (int i = 0; i < productos.Count; i++)
         {
            productos[i].CantidadEnStock = productos[i].CantidadEnStock + 10;

         }
         mutex.ReleaseMutex();
         barrera.SignalAndWait();

      }
      static void Tarea2()
      {
         precio_dolar = 600;
         barrera.SignalAndWait();
      }
      static void Tarea3()
      {
         barrera.SignalAndWait();
         for (int i = 0; i < productos.Count; i++)
         {
            Console.WriteLine("Nombre: " + productos[i].Nombre + " - Precio: " + (productos[i].PrecioUnitarioDolares * precio_dolar) + " - Stock: " + productos[i].CantidadEnStock);
         }

      }
      static void Tarea4()
      {
         mutex.WaitOne();
         for (int i = 0; i < productos.Count; i++)
         {
            productos[i].PrecioUnitarioDolares = (productos[i].PrecioUnitarioDolares * (decimal)1.10);
         }
         mutex.ReleaseMutex();
         barrera.SignalAndWait();
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




      }
   }
}