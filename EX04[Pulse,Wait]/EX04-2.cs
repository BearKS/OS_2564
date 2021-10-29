using System;
using System.Diagnostics;
using System.Threading;

namespace EX04_2
{
    class EX04_2
    {
        private static string x = "";
        private static int exitflag = 0;
        
        //---------------Add Lock---------------//
        private static object _Lock = new object();

        static void ThReadX()
        {
            while(exitflag == 0)
            { 
                Monitor.Enter(_Lock);
                try
                {
                    Monitor.Wait(_Lock); //waits here until thread pulses, releasing the lock
                    Console.WriteLine("X = {0}", x);
                }
                finally
                {
                    Monitor.Exit(_Lock);
                }
                
            }
            Console.Write("Thread 1 exit");
        }

        static void  ThWriteX()
        {
            string xx;
            while(exitflag == 0)
            {
                lock(_Lock)
                {
                    Monitor.Pulse(_Lock);
                    Console.Write("Input: ");
                    xx = Console.ReadLine();
                    if(xx == "exit")
                        exitflag = 1;
                    else
                        x = xx;
                }
            }
        }

        static void Main(string[] args)
        {
            Thread A = new Thread(ThReadX);
            Thread B = new Thread(ThWriteX);
           
            A.Start();
            B.Start();
        }
    }
}