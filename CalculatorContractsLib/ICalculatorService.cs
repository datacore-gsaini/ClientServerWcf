using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorContractsLib
{
    [ServiceContract]
    public interface ICalculatorService
    {
        [OperationContract]
        int Add(int a, int b);
    }
}
