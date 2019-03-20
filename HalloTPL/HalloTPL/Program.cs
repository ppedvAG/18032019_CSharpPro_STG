using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HalloTPL
{

    class Program
    {
        static void Main(string[] args)
        {
            //Parallel.Invoke(Zähle, Zähle, Zähle, Zähle, Zähle, Zähle, Zähle, Zähle);
            //Parallel.For(0, 100000, i => Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: {i}"));


            Task t1 = new Task(() =>
            {
                Console.WriteLine("T1 gestartet");
                Thread.Sleep(600);
                throw new ExecutionEngineException();
                Console.WriteLine("T1 fertig");
            });

            Task<long> t2 = new Task<long>(() =>
            {
                Console.WriteLine("T2 gestartet");
                Thread.Sleep(400);
                Console.WriteLine("T2 fertig");
                return 23456789034;
            });

            t1.ContinueWith(t =>
            {
                Console.WriteLine("Task1 OK");
            }, TaskContinuationOptions.OnlyOnRanToCompletion);

            t1.ContinueWith(t =>
            {
                Console.WriteLine($"Task1 failed! {t.Exception.InnerException.Message}");
            }, TaskContinuationOptions.OnlyOnFaulted);

            t1.Start();
            t2.Start();


            Console.WriteLine($"Result of T2:{t2.Result}");

            Console.WriteLine("Ende");
            Console.ReadLine();
        }

        private static void Zähle()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: {i}");
            }
        }
    }
}
