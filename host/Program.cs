using chatService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace host
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var host = new ServiceHost(typeof(ServiceChat)))
            {
                host.Open();
                Console.WriteLine("Server is working!");
                Console.ReadLine();
            }
        }
    }
}
