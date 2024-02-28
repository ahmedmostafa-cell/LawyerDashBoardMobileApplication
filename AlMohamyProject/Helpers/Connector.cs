using Newtonsoft.Json;


using RestSharp;
using RestSharp.Serialization.Json;

namespace MadmounMobileApp.Helpers
{
    public class Connector
    {
        public Connector()
        {
        }

        //public Transaction_Response Send(Transaction transaction)
        //{
        //    var client = new RestClient("https://restpilot.paylink.sa/api/auth");
        //    var request = new RestRequest(Method.POST);
        //    request.AddHeader("accept", "*/*");
        //    request.AddHeader("content-type", "application/json");
        //    request.AddParameter("application/json", "{\"persistToken\":true,\"apiId\":\"APP_ID_1632514366953\",\"secretKey\":\"b3fd0e4c-f406-4fd8-8773-ab2ea6007f11\"}", ParameterType.RequestBody);
        //    IRestResponse response = client.Execute(request);


        //    return tran_res;
        //}
    }
}
