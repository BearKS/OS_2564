using System;
using System.Diagnostics;
using System.Threading;

namespace EX05
{
    class EX05
    {
        private static string x = "";
        private static int exitflag = 0;
       // private static int updateFlag = 0;
        
        //---------------Add Lock---------------//
        private static object _Lock = new object();
        static Semaphore _sem = new Semaphore(1,1);

        static void ThReadX(object i)
        {
            while (exitflag == 0)
            {
                Monitor.Enter(_Lock);
                try
                {
                    Monitor.Wait(_Lock); //waits here until thread pulses, releasing the lock  
                    if (x != "exit") //If get input X
                    {
                        Console.WriteLine("***Thread {0} : x = {1}***", i, x);
                    }
                }
                finally
                {
                    Monitor.Wait(_Lock,100);
                    Monitor.Exit(_Lock);
                }
            }
            Console.WriteLine("---Thread {0} exit---", i);
        }

        static void ThWriteX()
        {
            string xx;
            while (exitflag == 0)
            {
                lock (_Lock)
                {  
                    Console.Write("Input: ");
                    Monitor.Pulse(_Lock);
                    xx = Console.ReadLine();
                    //Monitor.Wait(_Lock);
                    if (xx == "exit")
                    {
                        exitflag = 1;
                        Monitor.PulseAll(_Lock);
                    }  
                    else
                        x = xx;
                }                   
            }
        }


        static void Main(string[] args)
        {
            Thread A = new Thread(ThWriteX);
            Thread B = new Thread(ThReadX);
            Thread C = new Thread(ThReadX);
            Thread D = new Thread(ThReadX);
           
            A.Start();
            B.Start(1);
            C.Start(2);
            D.Start(3);
        }
    }
}