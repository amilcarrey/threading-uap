namespace ClaseHilos
{
    internal static class _1Basico
    {
        static void HolaUAP()
        {
            Thread.Sleep(2000);
            Console.WriteLine("Hola desde" + Thread.CurrentThread.Name);
        }

        internal static void Excecute()
        {
            Thread hilo = new Thread(new ThreadStart(HolaUAP));
            hilo.Name = "Hilo 1";
            hilo.Start();
            //hilo.Join();
            Console.WriteLine("Hola desde el hilo principal");
            Console.ReadLine();
        }
    }
}
