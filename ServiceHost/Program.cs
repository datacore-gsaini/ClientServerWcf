using CalculatorContractsLib;
using CalculatorServiceLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WCFExtensibility;

namespace CalculatorServiceHost
{
    class Program
    {
        static readonly AutoResetEvent handle = new AutoResetEvent(false);

        static void Main(string[] args)
        {
            var svc = new ServiceHost(typeof(CalculatorService));
            svc.Closed += CommunicationObject_Closed;

            foreach (ServiceEndpoint ep in svc.Description.Endpoints)
            { 
                ep.EndpointBehaviors.Add(new ServerMessageInspectorEndpointBehavior());
                //ep.EndpointBehaviors.Add(new ClientMessageInspectorEndpointBehavior());
            }

            svc.Open();

            Console.WriteLine("Calculator Service Started...");

            handle.WaitOne();
        }

        private static void CommunicationObject_Closed(object sender, EventArgs e)
        {
            handle.Set();
        }
    }
}
