
using BL;
using AlMohamyProject.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailService;
using Microsoft.AspNetCore.Authorization;
using AlMohamyProject.Controllers;
using AlMohamyProject.Helpers;
using AlMohamyProject.Services;
using AlMohamyProject.Hubs;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using System.IO;
using AlMohamyProject.Extensions;
using AlMohamyProject.Interfaces;
using BL.Interfaces;
using AlMohamyProject.Dtos;
using BL.Helpers;
using CorePush.Apple;
using BL.Repositories;


namespace AlMohamyProject
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; } // Add this property
        public Startup(IConfiguration configuration , IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ISMSService, SMSService>();
            services.AddScoped<ConsultingEstablishServicee, scopedService>();

            services.AddHostedService<MyBackgroundTask>();
            services.AddScoped<INotificationServiceqq, ClsNotification1>();
            services.AddHostedService<MyBackgroundNotification>();

            var emailConfig = Configuration
         .GetSection("EmailConfiguration")
         .Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);
            services.AddScoped<IEmailSender, EmailSender>();

            services.AddScoped<AboutAppService, ClsAboutApp>();
            services.AddScoped<ApprovedOfficeService, ClsApprovedOffice>();
            services.AddScoped<ChargeService, ClsCharge>();
            services.AddScoped<ConsultingEstablishService, ClsConsultingEstablish>();
            services.AddScoped<ConsultingTypeService, ClsConsultingType>();
            services.AddScoped<DocumentationOfContractService, ClsDocumentationOfContract>();
            services.AddScoped<EvaluationService, ClsEvaluation>();
            services.AddScoped<MainConsultingService, ClsMainConsulting>();
            services.AddScoped<NotificationService, ClsNotification>();
            services.AddScoped<OfferService, ClsOffer>();
            services.AddScoped<PoliciesAndPrivacyService, ClsPoliciesAndPrivacy>();
            services.AddScoped<PromocodeService, ClsPromocode>();
            services.AddScoped<SettingService, ClsSetting>();
            services.AddScoped<SubMainConsultingService, ClsSubMainConsulting>();
            services.AddScoped<TermAndConditionService, ClsTermAndCondition>();
            services.AddScoped<ComplainsAndSuggestionsService, ClsComplainsAndSuggestions>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<RealTimeNotifcationService, ClsRealTimeNotifcation>();
            services.AddScoped<LawyerPeriodCostConsultService, ClsLawyerPeriodCostConsult>();
            services.AddScoped<LogHistoryService, ClsLogHistory>();
            services.AddScoped<PaymentGateService, ClsPaymentGates>();
            services.AddScoped<ChatService, ClsChats>();
            services.AddScoped<LawyerAppintmentsService, ClsLawyerAppintments>();
            services.AddScoped<EvaluationApprovedOfficeService, ClsEvaluationApprovedOffice>();
            services.AddScoped<CityService, ClsCity>();
            services.AddScoped<AreaService, ClsArea>();
            services.AddScoped<ServicesService, ClsServices>();
            services.AddScoped<LawyersMainConsultingsService, ClsLawyersMainConsultings>();

            services.AddScoped<ActivityLogService, ClsActivityLog>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<BaseResponse>();
            services.Configure<FcmNotificationSetting>(Configuration.GetSection("FcmNotification"));
            services.AddHttpClient<FcmSender>();
            services.AddHttpClient<ApnSender>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
           
            services.Configure<TwilioSettings>(Configuration.GetSection("Twilio"));
           


            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder => {
                    builder.AllowAnyOrigin();
                    //URLs are from the front-end (note that they changed
                    //since posting my original question due to scrapping
                    //the original projects and starting over)
                    builder.WithOrigins("https://localhost:44398/", "https://habibaahmedm-002-site10.atempurl.com", "http://ismguk.com/", "https://eibtek2-001-site1.atempurl.com/", "http://localhost:60097/", "http://localhost:4200/", "https://restpilot.paylink.sa/api/auth", "https://restpilot.paylink.sa/api/getInvoice/")
                                     .AllowAnyHeader()
                                     .AllowAnyMethod()
                                     .AllowCredentials();
                });
            });
            services.AddControllersWithViews();
            services.AddSignalR();
            services.AddMvc().AddSessionStateTempDataProvider();
            services.AddSession();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.MaxAge = TimeSpan.FromHours(3);
                options.Cookie.Name = "SessionNameBlaBlaBla";
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;


            });

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 1;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = false;
                options.User.AllowedUserNameCharacters = "";
                options.Password.RequireDigit = false;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(30);
                options.Lockout.MaxFailedAccessAttempts = 5;

                options.User.AllowedUserNameCharacters = string.Empty;

            }).AddErrorDescriber<CustomIdentityErrorDescriber>().AddEntityFrameworkStores<AlMohamyDbContext>().AddDefaultTokenProviders();    ///.AddDefaultUI();



           

            services.AddAuthentication().AddGoogle(options =>
            {
                options.ClientId = "975214719409-pp37jcmifi7bg33254ve18ku83telt9r.apps.googleusercontent.com";
                options.ClientSecret = "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";


            });
            services.AddAuthentication().AddFacebook(options =>
            {
                options.AppId = "1387677424973135";
                options.AppSecret = "de6fc7e479121219c97a2e079eee1b3e";
            });


            services.ConfigureApplicationCookie(opt =>
            {


                opt.Cookie.MaxAge = TimeSpan.FromHours(3);
                opt.Cookie.Name = "CookieNameBlaBlaBla";
                opt.Cookie.HttpOnly = true;

                //opt.LoginPath = new PathString("/login/login");
                opt.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Home/Accessdenied");
                opt.SlidingExpiration = true;
            });


            services.Configure<Jwt>(Configuration.GetSection("JWT"));
            services.AddDbContext<AlMohamyDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            // Swagger Service
            //services.AddSwaggerDocumentation();
            var credential = GoogleCredential.FromFile(Path.Combine(Environment.ContentRootPath, "wwwroot", "App_Data", "serviceAccountKey.json"));
            FirebaseApp.Create(new AppOptions()
            {
                Credential = credential
            });




           
          

          
            
           
           
           
           

           
         


          




           
          
          



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ActivityLoggerMiddleware>();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //app.UseSwaggerDocumentation();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseCors(MyAllowSpecificOrigins);
            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://habibaahmedm-002-site10.atempurl.com"));
            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(

              name: "areas",
              pattern: "{area:exists}/{controller=Home}/{action=Index}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");


                endpoints.MapHub<UserHub>("/hubs/userCount");

                endpoints.MapHub<DeathlyHallowsHub>("hubs/deathyhallows");


                //endpoints.MapHub<DeathlyHallowsHub>("/hubs/houseGroup");


                endpoints.MapHub<NotificationHub>("/hubs/notification");

                endpoints.MapHub<BasicChatHub>("/hubs/basicchat");

                endpoints.MapHub<ChatHub>("/hubs/chat");

                endpoints.MapHub<OrderHub>("/hubs/order");
            });
        }
    }
}
