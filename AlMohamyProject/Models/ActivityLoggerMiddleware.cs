using AlMohamyProject.Dtos;
using BL;
using BL.Migrations;
using Domains;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp.Serialization.Json;
using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Twilio.Http;
using Twilio.TwiML.Messaging;

namespace AlMohamyProject.Models
{
    public class Person
    {
        public string UserName { get; set; }
        public string Password { get; set; }
       
    }
    public class ActivityLoggerMiddleware
    {

      
        private readonly RequestDelegate _next;
        private readonly ILogger<ActivityLoggerMiddleware> _logger;
        ActivityLogService activityLogService;
        public ActivityLoggerMiddleware(RequestDelegate next, ILogger<ActivityLoggerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
            //activityLogService = ActivityLogService;


        }

        public async Task InvokeAsync(HttpContext context, AlMohamyDbContext dbContext)
        {
            // Log the current user's activity
            //var userName = _usermanager.GetUserAsync(context.User).Result.FirstName;
            var userName="";
            var activityLog = new TbActivityLog();
            var action = context.Request.Path;
            //var userName2 = context.User.FindFirstValue(ClaimTypes.Name);
            //var userName = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userName != null)
            {

                if (action == "/api/OfferLawyerApi/offerService")
                {

                    userName = context.Request.Form.ToList()[3].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المحامي",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();
                }
                else if (action == "/api/LawyerApprovalConsultationApi/approveConsult")
                {

                    userName = context.Request.Form.ToList()[1].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المحامي",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();
                }
                else if (action == "/api/UserLoginApi/loginn")
                {
                    userName = context.Request.Form.ToList()[0].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserName = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                       
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/UserLoginApi/signup")
                {
                    userName = context.Request.Form.ToList()[1].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserName = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/UserLoginApi/forget")
                {
                    userName = context.Request.Form.ToList()[0].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserName = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                       
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/UserLoginApi/PhoneCon")
                {
                    userName = context.Request.Form.ToList()[0].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserName = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                       
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/UserLoginApi/reset-Password")
                {
                    userName = context.Request.Form.ToList()[0].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserName = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                       
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/UserLoginApi/reset-PasswordWithoutToken")
                {
                    userName = context.Request.Form.ToList()[0].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserName = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,

                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/UserLoginApi/deleteAccount")
                {
                    userName = context.Request.Form.ToList()[1].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserName = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المستخدم",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/ChargeApi")
                {
                    userName = context.Request.Form.ToList()[0].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المستخدم",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/PayLinkRecievePaymentLink/OfferApprovedOfficeRequest")
                {
                    userName = context.Request.Form.ToList()[1].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المستخدم",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/PayLinkRecievePaymentLink/WalletOfferApprovedOfficeRequest")
                {
                    userName = context.Request.Form.ToList()[0].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المستخدم",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/PayCosultingDataApi")
                {
                    userName = context.Request.Form.ToList()[0].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المستخدم",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/PromocodeActivationCodeApi/PromocodeActivate")
                {
                    userName = context.Request.Form.ToList()[2].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المستخدم",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/WaitingCustomerConsultingApi")
                {
                    userName = context.Request.Form.ToList()[0].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المستخدم",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/CurrentCustomerConsultingApi")
                {
                    userName = context.Request.Form.ToList()[0].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المستخدم",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/FinishedCustomerConsultingApi")
                {
                    userName = context.Request.Form.ToList()[0].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المستخدم",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/CancelledCustomerConsultationApi")
                {
                    userName = context.Request.Form.ToList()[0].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المستخدم",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/CancelledCustomerConsultationApi")
                {
                    userName = context.Request.Form.ToList()[0].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المستخدم",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/OfferCustomerApi")
                {
                    userName = context.Request.Form.ToList()[1].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المستخدم",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/LawyerChooseApi")
                {
                    userName = context.Request.Form.ToList()[1].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المستخدم",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/LawyerHighestPriceApi")
                {
                    userName = context.Request.Form.ToList()[1].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المستخدم",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/LawyerLowestPriceApi")
                {
                    userName = context.Request.Form.ToList()[1].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المستخدم",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/LawyerHighestExperienceApi")
                {
                    userName = context.Request.Form.ToList()[1].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المستخدم",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/LawyerLowestExperienceApi")
                {
                    userName = context.Request.Form.ToList()[1].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المستخدم",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/LawyerHighestEvaluationApi")
                {
                    userName = context.Request.Form.ToList()[1].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المستخدم",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/LawyerLowestEvaluationApi")
                {
                    userName = context.Request.Form.ToList()[1].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المستخدم",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/LawyerCostConsultApi")
                {
                    userName = context.Request.Form.ToList()[1].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المستخدم",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/ConsultingEstablishApi/EstablishConsultation")
                {
                    userName = context.Request.Form.ToList()[1].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المستخدم",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/ConsultingEstablishApi/CheckBeforeEstablishConsultation")
                {
                    userName = context.Request.Form.ToList()[1].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المستخدم",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/CustomerDocumnetationRequestApi/documentationRequest")
                {
                    userName = context.Request.Form.ToList()[0].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المستخدم",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/CustomerAskDelegationOfferApi/askDelegationOffer")
                {
                    userName = context.Request.Form.ToList()[2].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المستخدم",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/CustomerOfficialDelegationAskApi/CustomerOfficialDelegationAsk")
                {
                    userName = context.Request.Form.ToList()[6].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المستخدم",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/CustomerReplyDelegationOfferApi/customerReplyDelegationOffer")
                {
                    userName = context.Request.Form.ToList()[4].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المستخدم",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/ConsultingEstablishWithoutLawyerApi/EstablishConsultation")
                {
                    userName = context.Request.Form.ToList()[1].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المستخدم",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/CustomerEvaluationApi/Evaluate")
                {
                    userName = context.Request.Form.ToList()[0].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المستخدم",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/ApproveOfficeCustomerAEvaluationApi/Evaluate")
                {
                    userName = context.Request.Form.ToList()[0].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المستخدم",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/CancelledCustomerConsultationApi/Cancell")
                {
                    userName = context.Request.Form.ToList()[1].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المستخدم",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/CustomerApprovalOfferApi")
                {
                    userName = context.Request.Form.ToList()[1].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المستخدم",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/SearchForLawyersApi")
                {
                    userName = context.Request.Form.ToList()[1].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المستخدم",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }

                else if (action == "/api/SearchForUserApi")
                {
                    userName = context.Request.Form.ToList()[1].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المحامي",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/SearchByConsultationApi")
                {
                    userName = context.Request.Form.ToList()[1].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المستخدم",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/DetailsConsiltationApi")
                {
                    userName = context.Request.Form.ToList()[1].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المستخدم",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/ComplainsApi/SendComplain")
                {
                    userName = context.Request.Form.ToList()[0].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المستخدم",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/MainConsultingApi")
                {
                    userName = context.Request.Form.ToList()[0].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المستخدم",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/ConsultingTypeApi")
                {
                    userName = context.Request.Form.ToList()[0].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المستخدم",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/SubMainConsultingApi")
                {
                    userName = context.Request.Form.ToList()[1].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المستخدم",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/DocumentationContractApi")
                {
                    userName = context.Request.Form.ToList()[0].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المستخدم",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/CityApi")
                {
                    userName = context.Request.Form.ToList()[0].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المستخدم",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/AreaApi")
                {
                    userName = context.Request.Form.ToList()[1].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المستخدم",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/LawyerUploadDomsApi/uploadDoms")
                {
                    userName = context.Request.Form.ToList()[0].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المحامي",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }


                else if (action == "/api/UserLoginApi/upload-image")
                {
                    userName = context.Request.Form.ToList()[0].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                      
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/MyPersonalDataUpdateApi/UpdatePersonalData")
                {
                    userName = context.Request.Form.ToList()[0].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                       
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/LawyerGeneralConsultApi")
                {
                    userName = context.Request.Form.ToList()[0].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المحامي",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/LawyerSpecificConsultApi")
                {
                    userName = context.Request.Form.ToList()[0].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المحامي",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/OfferLawyerApi/offerService")
                {
                    userName = context.Request.Form.ToList()[3].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المحامي",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/LawyerApprovalConsultationApi/approveConsult")
                {
                    userName = context.Request.Form.ToList()[1].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المحامي",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/LawyerRejectConsultationApi/rejectConsult")
                {
                    userName = context.Request.Form.ToList()[1].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المحامي",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/LawyerDelegationDataApi")
                {
                    userName = context.Request.Form.ToList()[0].Value;
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المحامي",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/LawyerAppointApi")
                {
                    userName = context.Request.Form.ToList()[0].Value;
                   
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المحامي",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/LawyerAppointApi/SendAppoints")
                {
                   
                    // Enable buffering of the request body
                    context.Request.EnableBuffering();

                    // Read the request body
                    var requestBody = await new StreamReader(context.Request.Body).ReadToEndAsync();

                    // Reset the position of the request body stream so that it can be read again
                    context.Request.Body.Position = 0;

                    LawyerAppointDtos Trans_res = JsonConvert.DeserializeObject<LawyerAppointDtos>(requestBody);
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = Trans_res.LawyerId,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المحامي",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/LawyerMainConsultingApi/SendLawyerConsultWCostWTime")
                {

                    // Enable buffering of the request body
                    context.Request.EnableBuffering();

                    // Read the request body
                    var requestBody = await new StreamReader(context.Request.Body).ReadToEndAsync();

                    // Reset the position of the request body stream so that it can be read again
                    context.Request.Body.Position = 0;

                    LawyerConsultDtos Trans_res = JsonConvert.DeserializeObject<LawyerConsultDtos>(requestBody);
                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = Trans_res.LawyerId,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المحامي",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/LawyerDelegationOfferReplyApi/replyDelegationOffer")
                {
                    userName = context.Request.Form.ToList()[3].Value;

                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المحامي",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/LawyerEvaluationBringDataApi")
                {
                    userName = context.Request.Form.ToList()[1].Value;

                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المحامي",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/CurrentLawyerConsultingApi")
                {
                    userName = context.Request.Form.ToList()[0].Value;

                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المحامي",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/FinishedLawyerConsultingApi")
                {
                    userName = context.Request.Form.ToList()[0].Value;

                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المحامي",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/CancelledLawyerConsutingApi")
                {
                    userName = context.Request.Form.ToList()[0].Value;

                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المحامي",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/LawyerChargeApi")
                {
                    userName = context.Request.Form.ToList()[0].Value;

                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المحامي",
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/PolicyAndPrivacyApi")
                {
                    userName = context.Request.Form.ToList()[0].Value;

                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                       
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/TermsAndConditionsApi")
                {
                    userName = context.Request.Form.ToList()[0].Value;

                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                       
                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/AboutAppApi")
                {
                    userName = context.Request.Form.ToList()[0].Value;

                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,

                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/PaymentGateApi")
                {
                    userName = context.Request.Form.ToList()[0].Value;

                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,

                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/ApprovedOfficeApi")
                {
                    userName = context.Request.Form.ToList()[0].Value;

                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,

                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/OfferApprovedOfficeRequestApi/OfferApprovedOfficeRequest")
                {
                    userName = context.Request.Form.ToList()[4].Value;

                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "المحامي",

                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/UserLogInApi/bringdata")
                {
                    userName = context.Request.Form.ToList()[0].Value;

                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,
                       

                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/DeactivateLawyer/deactivateAccount")
                {
                    userName = context.Request.Form.ToList()[0].Value;

                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,


                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/ActivateLawyerApi/actvateAccount")
                {
                    userName = context.Request.Form.ToList()[0].Value;

                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,


                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }
                else if (action == "/api/ChatHistoryApi/SaveChat")
                {
                    userName = context.Request.Form.ToList()[1].Value;

                    var timestamp = DateTime.Now;

                    activityLog = new TbActivityLog
                    {
                        UserId = userName,
                        Action = action,
                        Timestamp = timestamp,
                        CreatedDate = DateTime.Now,


                    };
                    dbContext.TbActivityLogs.Add(activityLog);
                    await dbContext.SaveChangesAsync();

                }


            }

            // Call the next middleware in the pipeline
            await _next(context);
        }
    }
}
