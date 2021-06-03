using CalculatorContractsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace CalculatorProxyLib
{

    public static class CalculatorProxyFactory
    {
        public static CalculatorServiceProxy GetTcpPorxy()
        { 
            return new CalculatorServiceProxy(new NetTcpBinding(),
                new EndpointAddress("net.tcp://localhost:8009/CalculatorService"));
        }

        public static CalculatorServiceProxy GetPipePorxy()
        {
            return new CalculatorServiceProxy(new NetNamedPipeBinding(),
                new EndpointAddress("net.pipe://localhost/CalculatorService"));
        }
    }
    public class CalculatorServiceProxy :  ClientBase<ICalculatorService>, ICalculatorService
    {
        public CalculatorServiceProxy(Binding binding, EndpointAddress remoteAddress) 
            : base(binding, remoteAddress) 
        { 
        
        }
        public int Add(int a, int b) => Channel.Add(a, b);
    }
}
