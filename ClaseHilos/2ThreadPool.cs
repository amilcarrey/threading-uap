namespace ClaseHilos
{
   internal class _2ThreadPool
   {
      static void HolaUAP(object id)
      {
         Console.WriteLine($"Proceso {id}");
         Thread.Sleep(15000);
         Console.WriteLine("Hola desde " + id);
      }

      internal static void Excecute()
      {
         for (int i = 0; i < 5; i++)
         {
            ThreadPool.QueueUserWorkItem(HolaUAP, i);
         }

         Console.WriteLine("Hola desde el hilo principal");
         Console.ReadLine();
      }
   }
}
