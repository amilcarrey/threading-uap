namespace ClaseHilos
{
  internal class _6lock //reference: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/lock
  {
    static string[] names = { "Juan", "Pedro", "Maria", "Jose", "Ana", "Carlos", "Luis", "Luisa", "Rosa", "Raul" };
    static void HolaUAP()
    {
      lock (names)
      {
        Console.WriteLine($"{Thread.CurrentThread.Name} Entrando");
        Console.WriteLine($"Names count: {names.Length}");
        Thread.Sleep(3000);
        Console.WriteLine($"{Thread.CurrentThread.Name} Saliendo");
      }
    }
    internal static void Excecute()
    {
      Thread task1 = new Thread(HolaUAP);
      task1.Name = "Hilo 1";
      Thread task2 = new Thread(HolaUAP);
      task2.Name = "Hilo 2";
      Thread task3 = new Thread(HolaUAP);
      task3.Name = "Hilo 3";
      Thread task4 = new Thread(HolaUAP);
      task4.Name = "Hilo 4";

      //Excecute all tasks
      task1.Start();
      task2.Start();
      task3.Start();
      task4.Start();

      Console.ReadLine();
    }
  }
}
