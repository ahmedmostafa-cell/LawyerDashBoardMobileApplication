
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;
using AlMohamyProject.Helpers;

namespace AlMohamyProject.Services
{
    public  class SMSService : ISMSService
    {
        private const string ApiUrl = "https://api.taqnyat.sa/v1/messages?";
        private const string bearerTokens = "93154f093385198444b70ef0a8704dcb";
        private const string sender = "ALMUSTASHAR";
        //private const string recipients = "966591351435";
        private const string Url = "{0}bearerTokens={1}&sender={2}&recipients={3}&body={4}";
        //private readonly TwilioSettings _twilio;

        public  async Task<string> SendMessage(string phoneNumber, string message)
        {
           

           
            try
            {
                var endPoint = string.Format(Url, ApiUrl, bearerTokens, sender, phoneNumber, message);

                using var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // var bodyJS = JsonConvert.SerializeObject(new PaymentStatusDto());
                var body = new StringContent("SMS", Encoding.UTF8, "application/json");
                var response = client.PostAsync(endPoint, body).GetAwaiter().GetResult();
                var x = await response.Content.ReadAsStringAsync();

                var smsResponse = JsonConvert.DeserializeObject<SmsResponse>(x);
                if (smsResponse.statusCode == 201)
                {
                    return "تم ارسال الرسالة بنجاح";
                }
                if (smsResponse.statusCode == 400)
                {
                    return "لم يتم ارسال الرسالة";
                }
                //var statusMassage = smsResponse.Status.message;
                //var smsMassage = smsResponse.Data.message;
                return null;
            }
            catch (Exception)
            {
                return null;
                //  return "برجاء المحاوله مره اخرى";
            }
        }
    }
}
