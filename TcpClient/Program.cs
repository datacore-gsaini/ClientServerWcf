using CalculatorProxyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            CalculatorServiceProxy calc = CalculatorProxyFactory.GetTcpPorxy();
            Console.WriteLine($"TCP Client : {calc.Add(10, 20)}");
            Console.Read();
        }
    }
}
