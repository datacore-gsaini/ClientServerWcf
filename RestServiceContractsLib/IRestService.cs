using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace RestServiceContractsLib
{
    [ServiceContract]
    public interface IRestService
    {
        [OperationContract]
        [WebInvoke(
            Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "/add?a={a}&b={b}")]
        int Add(int a, int b);
    }
}
