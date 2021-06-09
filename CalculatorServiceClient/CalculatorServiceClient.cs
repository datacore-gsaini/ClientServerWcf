using CalculatorProxyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorServiceClient
{
    class CalculatorServiceClient
    {
        static void Main(string[] args)
        {

            CalculatorServiceProxy calc = CalculatorProxyFactory.GetPipeProxy();
            Console.WriteLine($"Pipe Client : {calc.Add(10, 20)}");
            Console.Read();

            calc = CalculatorProxyFactory.GetTcpProxy();
            Console.WriteLine($"TCP Client : {calc.Add(10, 20)}");
            Console.Read();

        }
    }
}
