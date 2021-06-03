using CalculatorProxyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeClient
{
    class Program
    {
        static void Main(string[] args)
        {
            CalculatorServiceProxy calc = CalculatorProxyFactory.GetPipePorxy();
            Console.WriteLine($"Pipe Client : {calc.Add(10, 20)}");
            Console.Read();

        }
    }
}
