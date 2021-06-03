using CalculatorContractsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorServiceLib
{
    public class CalculatorService : ICalculatorService
    {
        public int Add(int a, int b) => a + b;
    }
}
