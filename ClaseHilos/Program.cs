using ClaseHilos;

Solution.Excecute();

//1 - Uso basico de hilos con Threads
//_1Basico.Excecute();

//2 - Uso de hilos con ThreadPool
//_2ThreadPool.Excecute();

//3- Uso de Barrier
//_3Barrier.Excecute();

//4- Uso de Semaphore
//_4semaphore.Excecute();

//5- Uso de Mutex
//_5Mutex.Excecute();

//6- Uso de lock
// _6lock.Excecute();

/*
Salida esperada:

Tarea 1
Tarea 2     -> (consideramos que son simultaneas)

(sleep de 3 segundos que es lo que demoran las dos anteriores en terminarse)
 
 3...
 2...
 1...

Tarea 3 


*/


