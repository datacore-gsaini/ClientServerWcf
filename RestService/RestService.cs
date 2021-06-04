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
using WCFExtensibility;

namespace RestService
{
    class RestService : IRestService
    {
        [ThreadStatic]
        public static string RequestSource;

        CalculatorServiceProxy calcProxy = CalculatorProxyFactory.GetTcpPorxy();

        public RestService()
        {
            calcProxy.Endpoint.EndpointBehaviors.Add(new ClientMessageInspectorEndpointBehavior());
        }

        public int Add(int a, int b)
        {

            if (OperationContext.Current != null)
            {

                var hcon = WebOperationContext.Current.IncomingRequest;

                var con = OperationContext.Current.RequestContext;

                try
                {
                    using (OperationContextScope scope = new OperationContextScope(calcProxy.InnerChannel))
                    {
                        string src = "Plugin";
                        var header = System.ServiceModel.Channels.MessageHeader.CreateHeader("Source",
                            "http://datacore.com/plugins/", src);
                        OperationContext.Current.OutgoingMessageHeaders.Add(header);
                        OperationContext.Current.OutgoingMessageProperties.Add("Source", src);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
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
               ep.EndpointBehaviors.Add(new RESTServerMessageInspectorEndpointBehavior());
            //    ep.EndpointBehaviors.Add(new ClientMessageInspectorEndpointBehavior());
            }

            svc.Open();
            Console.WriteLine("Service Started");
            Console.Read();
        }
    }


    public class RESTServerMessageInspector : IDispatchMessageInspector
    {
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            var hcon = WebOperationContext.Current.IncomingRequest;
            var src = hcon.Headers["Source"];
            RestService.RequestSource = src;
            return null;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {

        }
    }


    public class RESTServerMessageInspectorEndpointBehavior : IEndpointBehavior
    {
        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new RESTServerMessageInspector());
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }
    }


    public class ClientSideMessageInspector : IClientMessageInspector
    {
        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            System.IO.StreamWriter wr = new System.IO.StreamWriter("servicemessagelog.txt", true);
            wr.WriteLine($"***************Client After Receive Reply For : {correlationState}*********************");
            wr.WriteLine(reply.ToString());
            wr.Flush();
            wr.Close();
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            var hcon = WebOperationContext.Current.IncomingRequest;
            var src = hcon.Headers["Source"];

            var header = MessageHeader.CreateHeader("Source",
                        "http://datacore.com/plugins/", src);
            request.Headers.Add(header);

            int requestNumber = new Random().Next(1, 100);
            System.IO.StreamWriter wr = new System.IO.StreamWriter("servicemessagelog.txt", true);
            wr.WriteLine($"******************** Client Before Send Request {requestNumber}*********************");
            wr.WriteLine(request.ToString());
            wr.Flush();
            wr.Close();
            return requestNumber;
        }
    }


    public class ClientMessageInspectorEndpointBehavior : IEndpointBehavior
    {
        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.MessageInspectors.Add(new ClientSideMessageInspector());
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }
    }





}
