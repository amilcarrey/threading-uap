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
  public class Solution //reference: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/lock
  {
    static Barrier barriera = new Barrier(4);

    static List<Producto> productos = new List<Producto>
        {
            new Producto("Camisa", 10, 50),
            new Producto("Pantalón", 8, 30),
            new Producto("Zapatilla/Champión", 7, 20),
            new Producto("Campera", 25, 100),
            new Producto("Gorra", 16, 10)
        };

    static int precio_dolar = 500;

    public static void Tarea1()
    {
      System.Console.WriteLine("Tarea 1 - Actualizando Stock...");
      for (int i = 0; i < productos.Count(); i++)
      {
        System.Console.WriteLine("Stock Actual de " + productos[i].Nombre + " :" + productos[i].CantidadEnStock);
        productos[i].CantidadEnStock += 10;
        System.Console.WriteLine("T1: Stock Actualizado de " + productos[i].Nombre + " :" + productos[i].CantidadEnStock);
      }
      Thread.Sleep(3000);
      barriera.SignalAndWait();
    }
    public static void Tarea2()
    {
      System.Console.WriteLine("Tarea 2 - Actualizando precio Dolar...");
      precio_dolar = 1000;
      Thread.Sleep(3000);
      barriera.SignalAndWait();
    }
    public static void Tarea2b()
    {
      System.Console.WriteLine("Tarea 2b - Actualización de precio unitario...");
      for (int i = 0; i < productos.Count(); i++)
      {
        productos[i].PrecioUnitarioDolares = productos[i].PrecioUnitarioDolares * (decimal)1.1;
      
      }
      Thread.Sleep(3000);
      barriera.SignalAndWait();
    }
    public static void Tarea3()
    {
      barriera.SignalAndWait();
      System.Console.WriteLine("Tarea 3 - Listado de precios actualizados");
      // Debe generar un informe que muestre la lista de productos junto con su cantidad en stock actualizada y el precio total del inventario. 
      for (int i = 0; i < productos.Count(); i++)
      {
        System.Console.WriteLine(productos[i].Nombre + " : " + (productos[i].PrecioUnitarioDolares * precio_dolar));
      }
    }

    internal static void Excecute()
    {
      // nase
      Thread t1 = new Thread(Tarea1);
      Thread t2 = new Thread(Tarea2);
      Thread t2b = new Thread(Tarea2b);
      Thread t3 = new Thread(Tarea3);

      t1.Start();
      t2.Start();
      t2b.Start();
      t3.Start();
    }
  }
}