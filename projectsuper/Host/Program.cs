using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfServiceNEW;
using System.ServiceModel;

namespace Host
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(SuperService));
            host.Open();
            Console.WriteLine("This is a server don't close");
            Console.ReadLine();
        }
    }
}
