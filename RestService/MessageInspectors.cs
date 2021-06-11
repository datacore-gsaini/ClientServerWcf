using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace RestService
{

    /// <summary>
    /// 
    /// </summary>
    public class DispatchMessageInspector : IDispatchMessageInspector
    {/// <summary>
     /// 
     /// </summary>
     /// <param name="request"></param>
     /// <param name="channel"></param>
     /// <param name="instanceContext"></param>
     /// <returns></returns>
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            Console.WriteLine("After Receive Request");
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reply"></param>
        /// <param name="correlationState"></param>
        public void BeforeSendReply(ref Message reply, object correlationState)
        {
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class DispatchMessageInspectorBehavior : IEndpointBehavior
    {/// <summary>
     /// 
     /// </summary>
     /// <param name="endpoint"></param>
     /// <param name="bindingParameters"></param>
        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="clientRuntime"></param>
        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="endpointDispatcher"></param>
        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new DispatchMessageInspector());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="endpoint"></param>
        public void Validate(ServiceEndpoint endpoint)
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class DispatchMessageInspectorBehaviorElement : BehaviorExtensionElement
    {
        /// <summary>
        /// 
        /// </summary>
        public override Type BehaviorType
        {
            get { return typeof(DispatchMessageInspectorBehavior); }
        }

        /// <summary>
        /// 
        /// </summary>
        protected override object CreateBehavior()
        {
            return new DispatchMessageInspectorBehavior();
        }
    }




    public class RestServerMessageInspector : IDispatchMessageInspector
    {
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            if (WebOperationContext.Current != null)
            {
                var hcon = WebOperationContext.Current.IncomingRequest;
                var src = hcon.Headers["Source"];
                RestService.RequestSource = src;
            }

            return null;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
        }
    }

    public class RestServerMessageInspectorEndpointBehavior : IEndpointBehavior
    {
        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new RestServerMessageInspector());
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }
    }

}
