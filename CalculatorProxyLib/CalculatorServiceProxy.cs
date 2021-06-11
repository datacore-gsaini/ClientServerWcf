using CalculatorContractsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using WCFExtensibility;

namespace CalculatorProxyLib
{
    public class AbstractProxy : ICalculatorService
    {
        public ICalculatorService Channel { get; set; }

        public int Add(int a, int b) => Channel.Add(a, b);

        public ChannelFactory Factory { get; set; }
        public ServiceEndpoint Endpoint => Factory.Endpoint;
    }


    public static class CalculatorProxyFactory
    {
        public static AbstractProxy GetDynamicTcpProxy(IEndpointBehavior endpointBehavior = null)
        {
            Binding binding = new NetTcpBinding();
            var factory = new ChannelFactory<ICalculatorService>(binding);

            //factory.Endpoint.EndpointBehaviors.Add(new ClientMessageInspectorEndpointBehavior());
            
            var address = new EndpointAddress("net.tcp://localhost:8009/CalculatorService");

            if(endpointBehavior != null)
                factory.Endpoint.EndpointBehaviors.Add(endpointBehavior);

            var channel = factory.CreateChannel(address);
            var proxy = new AbstractProxy()
            {
                Channel = channel,
                Factory = factory
            };

            return proxy;
        }

        public static CalculatorServiceProxy GetTcpProxy()
        {
            return new CalculatorServiceProxy(new NetTcpBinding(),
                new EndpointAddress("net.tcp://localhost:8009/CalculatorService"));
        }

        public static CalculatorServiceProxy GetPipeProxy()
        {
            return new CalculatorServiceProxy(new NetNamedPipeBinding(),
                new EndpointAddress("net.pipe://localhost/CalculatorService"));
        }
    }
    public class CalculatorServiceProxy : ClientBase<ICalculatorService>, ICalculatorService
    {
        public CalculatorServiceProxy(Binding binding, EndpointAddress remoteAddress)
            : base(binding, remoteAddress)
        {

        }
        public int Add(int a, int b) => Channel.Add(a, b);
    }
}
