using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace OS_2564
{
    class Program
    {
        private static string x = "";
        private static int exitflag = 0;
        
        private static int pint = 0;
        private static object _Lock = new object();

        static void ThReadX()
        {
          
                while (exitflag == 0){
                  lock(_Lock){   
                        if(pint == 1){
                           Console.WriteLine("X = {0}", x);  
                           pint = 0 ;
                        }
                        
                    }
                }
        }

        static void ThWriteX()
        {
            string xx;
            
                while (exitflag == 0)
                {
                    lock (_Lock){
                    Console.Write("Input: ");
                    xx = Console.ReadLine();
                    if (xx == "exit"){
                       Console.WriteLine( "Thread 1 exit ");
                        exitflag = 1; 
                    }
                    else{
                        x = xx;
                        pint =1 ;
                    }
                        
                }
                    
            }

        }

        static void Main(string[] args)
        {
            Thread A = new Thread(new ThreadStart(ThReadX));
            Thread B = new Thread(new ThreadStart(ThWriteX));
            B.Start();
            A.Start();

            
             B.Join();
             A.Join();

        }
    }
}