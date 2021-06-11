using CalculatorProxyLib;
using RestServiceContractsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using CalculatorContractsLib;
using WCFExtensibility;

namespace RestService
{
    class RestService : IRestService
    {
        [ThreadStatic]
        public static string RequestSource;

        //readonly CalculatorServiceProxy calcProxy = CalculatorProxyFactory.GetTcpProxy();
        //readonly AbstractProxy calcProxy = CalculatorProxyFactory.GetDynamicTcpProxy();
        readonly AbstractProxy calcProxy = CalculatorProxyFactory.GetDynamicTcpProxy(new ClientMessageInspectorEndpointBehavior());

        public RestService()
        {
            //calcProxy.Endpoint.EndpointBehaviors.Add(new ClientMessageInspectorEndpointBehavior());
        }

        public int Add(int a, int b)
        {

            //if (OperationContext.Current != null)
            //{

            //    if (WebOperationContext.Current != null)
            //    {
            //        var hcon = WebOperationContext.Current.IncomingRequest;
            //    }

            //    var con = OperationContext.Current.RequestContext;

            //    try
            //    {
            //        using (OperationContextScope scope = new OperationContextScope(calcProxy.InnerChannel))
            //        {
            //            string src = "Plugin";
            //            var header = System.ServiceModel.Channels.MessageHeader.CreateHeader("Source",
            //                "http://datacore.com/plugins/", src);
            //            OperationContext.Current.OutgoingMessageHeaders.Add(header);
            //            OperationContext.Current.OutgoingMessageProperties.Add("Source", src);
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(ex.Message);
            //    }
            //}
            return calcProxy.Add(a, b);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost svc = new ServiceHost(typeof(RestService));

            foreach(ServiceEndpoint ep in svc.Description.Endpoints)
            {
               //ep.EndpointBehaviors.Add(new RestServerMessageInspectorEndpointBehavior());
            //    ep.EndpointBehaviors.Add(new ClientMessageInspectorEndpointBehavior());
            }

            svc.Open();
            Console.WriteLine("Service Started");
            Console.Read();
        }
    }





}
