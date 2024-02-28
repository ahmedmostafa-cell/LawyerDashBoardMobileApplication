using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using Twilio.TwiML.Voice;

namespace AlMohamyProject.Models
{
    public class MyFatoorah
    {
        // You can get test token from this page  https://myfatoorah.readme.io/docs/test-token
        static string token = "rLtt6JWvbUHDDhsZnfpAhpYk4dxYDQkbcPTyGaKp2TYqQgG7FGZ5Th_WD53Oq8Ebz6A53njUoo1w3pjU1D4vs_ZMqFiz_j0urb_BH9Oq9VZoKFoJEDAbRZepGcQanImyYrry7Kt6MnMdgfG5jn4HngWoRdKduNNyP4kzcp3mRv7x00ahkm9LAK7ZRieg7k1PDAnBIOG3EyVSJ5kK4WLMvYr7sCwHbHcu4A5WwelxYK0GMJy37bNAarSJDFQsJ2ZvJjvMDmfWwDVFEVe_5tOomfVNt6bOg9mexbGjMrnHBnKnZR1vQbBtQieDlQepzTZMuQrSuKn-t5XZM7V6fCW7oP-uXGX-sMOajeX65JOf6XVpk29DP6ro8WTAflCDANC193yof8-f5_EYY-3hXhJj7RBXmizDpneEQDSaSz5sFk0sV5qPcARJ9zGG73vuGFyenjPPmtDtXtpx35A-BVcOSBYVIWe9kndG3nclfefjKEuZ3m4jL9Gg1h2JBvmXSMYiZtp9MR5I6pvbvylU_PP5xJFSjVTIz7IQSjcVGO41npnwIxRXNRxFOdIUHn0tjQ-7LwvEcTXyPsHXcMD8WtgBh-wxR8aKX7WPSsT1O8d8reb2aR7K3rkV3K82K_0OgawImEpwSvp9MNKynEAJQS6ZHe_J_l77652xwPNxMRTMASk1ZsJL";
        static string baseURL = "https://apitest.myfatoorah.com";
        //static async Task Main(string[] args)
        //{
        //    //var intiateResponse = await InitiatePayment(string paymentvalue).ConfigureAwait(false);
        //    Console.WriteLine("Initiate Payment Response :");
        //    //Console.WriteLine(intiateResponse);

        //    //var executeResponse = await ExecutePayment().ConfigureAwait(false);
        //    Console.WriteLine("Execute Payment Response :");
        //    //Console.WriteLine(executeResponse);

        //    Console.ReadLine();
        //}

        public static async Task<string> InitiatePayment(string paymentvalue)
        {
            var intiatePaymentRequest = new
            {
                InvoiceAmount = int.Parse(paymentvalue),
                CurrencyIso = "kwd"
            };

            var intitateRequestJSON = JsonConvert.SerializeObject(intiatePaymentRequest);
            return await PerformRequest(intitateRequestJSON, "InitiatePayment").ConfigureAwait(false);

        }

        public static async Task<string> ExecutePayment()
        {
           
            var executePaymentRequest = new
            {
                //required fields
                PaymentMethodId = "20",
                InvoiceValue = 1000,
                CallBackUrl = "https://example.com/callback",
                ErrorUrl = "https://example.com/error",
                //optional fields 
                CustomerName = "Customer Name",
                DisplayCurrencyIso = "KWD",
                MobileCountryCode = "965",
                CustomerMobile = "12345678",
                CustomerEmail = "email@example.com",
                Language = "En",
                CustomerReference = "",
                CustomerCivilId = "",
                UserDefinedField = "",
                ExpiryDate = DateTime.Now.AddYears(1),
                // to add suppliers
                Suppliers = new[] {
                        new {
                          SupplierCode = 1, InvoiceShare = 1000, ProposedShare = 500
                        }
                 }

            };
            var executeRequestJSON = JsonConvert.SerializeObject(executePaymentRequest);
            return await PerformRequest(executeRequestJSON, "ExecutePayment").ConfigureAwait(false);
        }
        public static async Task<string> ExecutePayment(string PaymentMethodId , string invoiceValue , string ConsultingId , string PaymentGateId , string UserFirstName , string UserEmail)
        {
            var executePaymentRequest = new
            {
                //required fields
                PaymentMethodId = PaymentMethodId,
                InvoiceValue = int.Parse(invoiceValue),
                CallBackUrl = "https://habibaahmedm-002-site10.atempurl.com/api/MyFatoorahGetPaymentStatusApi?id=" + ConsultingId,  //https://65dd-156-218-17-106.eu.ngrok.io
                ErrorUrl = "https://example.com/error",
                //optional fields 
                CustomerName = UserFirstName,//
                DisplayCurrencyIso = "KWD",
                MobileCountryCode = "965",
                CustomerMobile = "",
                CustomerEmail = UserEmail,//
                Language = "En",
                CustomerReference = ConsultingId,//
                CustomerCivilId = "",
                UserDefinedField = PaymentGateId,//
                ExpiryDate = DateTime.Now.AddYears(1),
                // to add suppliers
                Suppliers = new[] {
                        new {
                          SupplierCode = 1, InvoiceShare = int.Parse(invoiceValue), ProposedShare = int.Parse(invoiceValue)
                        }
                 }

            };
            var executeRequestJSON = JsonConvert.SerializeObject(executePaymentRequest);
            return await PerformRequest(executeRequestJSON, "ExecutePayment").ConfigureAwait(false);
        }




        public static async Task<string> ExecutePayment2(string PaymentMethodId, string invoiceValue, string chargeId, string PaymentGateId, string UserId, string UserEmail , string UserFirstName)
        {
            var executePaymentRequest = new
            {
                //required fields
                PaymentMethodId = PaymentMethodId,
                InvoiceValue = int.Parse(invoiceValue),
                CallBackUrl = "https://habibaahmedm-002-site10.atempurl.com/api/MyFatoorahGetPaymentStatusWalletApi?id=" + chargeId,  //https://habibaahmedm-002-site10.atempurl.com//replace consultingid with user id
                ErrorUrl = "https://example.com/error",
                //optional fields 
                CustomerName = UserFirstName,//
                DisplayCurrencyIso = "KWD",
                MobileCountryCode = "965",
                CustomerMobile = "",
                CustomerEmail = UserEmail,//
                Language = "En",
                CustomerReference = UserId,//
                CustomerCivilId = "",
                UserDefinedField = PaymentGateId,//
                ExpiryDate = DateTime.Now.AddYears(1),
                // to add suppliers
                Suppliers = new[] {
                        new {
                          SupplierCode = 1, InvoiceShare = int.Parse(invoiceValue), ProposedShare = int.Parse(invoiceValue)
                        }
                 }

            };
            var executeRequestJSON = JsonConvert.SerializeObject(executePaymentRequest);
            return await PerformRequest(executeRequestJSON, "ExecutePayment").ConfigureAwait(false);
        }
        public static async Task<string> PerformRequest(string requestJSON, string endPoint)
        {
            string url = baseURL + $"/v2/{endPoint}";
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var httpContent = new StringContent(requestJSON, System.Text.Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync(url, httpContent).ConfigureAwait(false);
            string response = string.Empty;
            if (!responseMessage.IsSuccessStatusCode)
            {
                response = JsonConvert.SerializeObject(new
                {
                    IsSuccess = false,
                    Message = responseMessage.StatusCode.ToString()
                });
            }
            else
            {
                response = await responseMessage.Content.ReadAsStringAsync();
            }

            return response;
        }
    }
}
