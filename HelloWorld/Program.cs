using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server(int.Parse(args[0]));

            
            // Console.ReadKey(true);
            // Container container = new Container();
            // Console.WriteLine(container.Load_c());
            // Console.ReadKey(true);
              
                
                
            
           
            Console.ReadKey(true);

            server.Stop();
            
        }
    }
}
