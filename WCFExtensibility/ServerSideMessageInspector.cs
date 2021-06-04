using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace WCFExtensibility
{

   
    public class ServerSideMessageInspector : IDispatchMessageInspector
    {
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            int requestNumber = new Random().Next(1, 100);
            System.IO.StreamWriter wr = new System.IO.StreamWriter("servicemessagelog.txt", true);
            wr.WriteLine($"********************Server After Receive Request {requestNumber}*********************");
            wr.WriteLine(request.ToString());
            wr.Flush();
            wr.Close();
            return requestNumber;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            System.IO.StreamWriter wr = new System.IO.StreamWriter("servicemessagelog.txt", true);
            wr.WriteLine($"***************Server Before Send Reply For : {correlationState}*********************");
            wr.WriteLine(reply.ToString());
            wr.Flush();
            wr.Close();
        }
    }


    public class ServerMessageInspectorEndpointBehavior : IEndpointBehavior
    {
        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new ServerSideMessageInspector());
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }
    }


}
