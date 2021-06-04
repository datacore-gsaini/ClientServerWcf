using CalculatorContractsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorServiceLib
{
    public class CalculatorService : ICalculatorService
    {
        public int Add(int a, int b)
        {
            /*
            if (OperationContext.Current != null)
            {
                var headers = OperationContext.Current.IncomingMessageHeaders;
                int index = headers.FindHeader("Source", "http://datacore.com/plugins/");

                string src = "Not-Found";

                if (index != -1)
                {
                    string user = headers.GetHeader<string>(index);
                    src += user;
                    return 1000;
                }
                 
                return -1;
            }*/
            return a + b;
        }
    }
}
