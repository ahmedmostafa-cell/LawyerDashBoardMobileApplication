using AlMohamyProject.Helpers;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;
using Twilio.Rest.Api.V2010.Account;

namespace AlMohamyProject.Services
{
    public interface ISMSService
    {
        private const string ApiUrl = "https://api.taqnyat.sa/v1/messages?";
        private const string bearerTokens = "93154f093385198444b70ef0a8704dcb";
        private const string sender = "Taqnyat.sa";
        //private const string recipients = "966591351435";
        private const string Url = "{0}bearerTokens={1}&sender={2}&recipients={3}&body={4}";
        //private readonly TwilioSettings _twilio;

        Task<string> SendMessage(string phoneNumber, string message);
       
    }
}
