using CalculatorContractsLib;
using CalculatorServiceLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CalculatorServiceHost
{
    class Program
    {
        static readonly AutoResetEvent handle = new AutoResetEvent(false);

        static void Main(string[] args)
        {
            var communicationObject = new ServiceHost(typeof(CalculatorService));

            communicationObject.AddServiceEndpoint(typeof(ICalculatorService),
                new NetNamedPipeBinding(),
                "net.pipe://localhost/CalculatorService");

            communicationObject.AddServiceEndpoint(typeof(ICalculatorService),
                new NetTcpBinding(),
                "net.tcp://localhost:8009/CalculatorService");

            communicationObject.Closed += CommunicationObject_Closed;

            communicationObject.Open();

            Console.WriteLine("Calculator Service Started...");

            handle.WaitOne();
        }

        private static void CommunicationObject_Closed(object sender, EventArgs e)
        {
            handle.Set();
        }
    }
}
