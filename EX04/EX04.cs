using System;
using System.Diagnostics;
using System.Threading;

namespace EX04
{
    class EX04
    {
        private static string x = "";
        private static int exitflag = 0;
        
        //---------------Add Lock---------------//
        private static object _Lock = new object();
        //-----------Add Check State------------//
        private static int check = 0;

        static void ThReadX()
        {
            while (exitflag == 0)
            {
                lock (_Lock)
                {   
                    if (check == 1) //If get input X
                    {
                        Console.WriteLine("X = {0}", x);  
                        check = 0; //Change state for the wait to get input
                    }
                }
            }
        }

        static void ThWriteX()
        {
            string xx;
            while (exitflag == 0)
            {
                lock (_Lock)
                {
                    Console.Write("Input: ");
                    xx = Console.ReadLine();
                    if (xx == "exit")
                    {
                        exitflag = 1;
                        Console.Write("Thread 1 exit");
                    }
                    else
                    {
                        x = xx;
                        check = 1; //Change state for show in ThReadX()
                    }  
                }                   
            }
        }

        static void Main(string[] args)
        {
            Thread A = new Thread(new ThreadStart(ThReadX));
            Thread B = new Thread(new ThreadStart(ThWriteX));
           
            A.Start();
            B.Start();
        }
    }
}