using BL;
using Microsoft.AspNetCore.Identity;
using System.IO;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc;
using AlMohamyProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using AlMohamyProject.Dtos;
using AlMohamyProject.Services;
using System.Linq;
using Domains;
using System.Collections.Generic;
using System.Xml.Linq;
using System.IdentityModel.Tokens.Jwt;
using Twilio.Jwt.AccessToken;
using Microsoft.EntityFrameworkCore;

using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using AlMohamyProject.Helpers;
using BL.Interfaces;
using Microsoft.AspNetCore.Components.Forms;

namespace AlMohamyProject.Controllers
{
    public class MyModelStringObject
    {
        public TbConsultingEstablish MyModel { get; set; }
        public string MyString { get; set; }
    }
    public class MyModelStringObject2
    {
        public TbApprovedOffice MyModel { get; set; }
        public string MyString { get; set; }
    }
    public class MyModelStringObject3
    {
        public List<TbConsultingEstablish> MyModel { get; set; }
        public string MyString { get; set; }
    }
    public class MyModelStringObject4
    {
        public TbChat MyModel { get; set; }
        public string MyString { get; set; }
    }
   
    public class AccountRepository : IAccountRepository
    {
        private readonly NotificationService notificationService;
        private readonly LawyersMainConsultingsService _mainConsultingsService;
        private readonly INotificationServiceqq notificationServiceqq;
        private readonly Jwt _jwt;
        ChatService chatService;
        DocumentationOfContractService documentationOfContractService;
        EvaluationApprovedOfficeService evaluationApprovedOfficeService;
        ApprovedOfficeService approvedOfficeService;
        EvaluationService evaluationService;
        SettingService settingService;
        SubMainConsultingService _subMainConsultingService;
        MainConsultingService mainConsultingService;
        ConsultingTypeService consultingTypeService;
        ConsultingEstablishService consultingEstablishService;
        SignInManager<ApplicationUser> SignInManager;
        UserManager<ApplicationUser> Usermanager;
        AlMohamyDbContext Ctx;
        private readonly ISMSService _smsService;
        public AccountRepository(NotificationService NotificationService,LawyersMainConsultingsService mainConsultingsService,INotificationServiceqq NotificationServiceqq,IOptions<Jwt> jwt, ChatService ChatService, DocumentationOfContractService DocumentationOfContractServic, EvaluationApprovedOfficeService EvaluationApprovedOfficeService, ApprovedOfficeService ApprovedOfficeService, EvaluationService EvaluationService, SettingService SettingService, SubMainConsultingService SubMainConsultingService, MainConsultingService MainConsultingService, ConsultingTypeService ConsultingTypeService, ConsultingEstablishService ConsultingEstablishService, ISMSService smsService, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> usermanager, AlMohamyDbContext ctx, ISMSService sMSService)
        {
            notificationService = NotificationService;
            notificationServiceqq = NotificationServiceqq;
            chatService = ChatService;
            documentationOfContractService = DocumentationOfContractServic;
            evaluationService = EvaluationService;
            consultingEstablishService = ConsultingEstablishService;
            Usermanager = usermanager;
            Ctx = ctx;
            SignInManager = signInManager;
          
            consultingTypeService = ConsultingTypeService;
            mainConsultingService = MainConsultingService;
            _subMainConsultingService = SubMainConsultingService;
            settingService = SettingService;
            approvedOfficeService = ApprovedOfficeService;
            evaluationApprovedOfficeService = EvaluationApprovedOfficeService;
            _jwt = jwt.Value;
            _smsService = sMSService;
            _mainConsultingsService = mainConsultingsService;
        }









        public async Task<string> SignUpAsync(SignUpModel signUpModel)
        {

            //if (signUpModel.PersonalImage != null)
            //{
            //    string ImageName = Guid.NewGuid().ToString() + ".jpg";
            //    var filePaths = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads", ImageName);
            //    using (var stream = System.IO.File.Create(filePaths))
            //    {
            //        await signUpModel.PersonalImage.CopyToAsync(stream);
            //    }
            //    signUpModel.ImageProfile = ImageName;
            //}
            //else
            //{
            //    signUpModel.ImageProfile = "6bfaa416-900f-478b-a44d-984e099bd723.jpg";

            //}

            var user = new ApplicationUser()
            {

                UserName = signUpModel.UserName,
                FirstName = signUpModel.FirstName,
                FamilyName = signUpModel.FamilyName,
                Email = signUpModel.Email,
                Image = signUpModel.ImageProfile,
                UserType = signUpModel.UserType,
                Status = "غير مفعل",
                IsApprovedOffice = false,
                ShortDescription = signUpModel.ShortDescription


            };
            if(signUpModel.ImageProfile == null) 
            {
                user.Image = "img_avatar.png";
            }
            user.DeviceToken = new JwtSecurityTokenHandler().WriteToken(GenerateJwtToken(user).Result);
            
            
                var r = await Usermanager.CreateAsync(user, signUpModel.Password);
                return r.ToString();
           
              
            

           
        }

        

        public async Task<ActionResult<ApplicationUser>> uploadImage(SignUpModel signUpModel)
        {

            if (signUpModel.PersonalImage != null)
            {
                string ImageName = Guid.NewGuid().ToString() + ".jpg";
                var filePaths = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads", ImageName);
                using (var stream = System.IO.File.Create(filePaths))
                {
                    await signUpModel.PersonalImage.CopyToAsync(stream);
                }
                signUpModel.ImageProfile = ImageName;
            }
            else
            {
                signUpModel.ImageProfile = "img_avatar.png";

            }

            var user = await Usermanager.FindByIdAsync(signUpModel.Id);




            user.Image = signUpModel.ImageProfile;
           








            var r = await Usermanager.UpdateAsync(user);

            return user;
        }




        public async Task<ActionResult<ApplicationUser>> uploadDoms(SignUpModel signUpModel)
        {

            if (signUpModel.DocumentA != null)
            {
                string ImageName = Guid.NewGuid().ToString() + ".pdf";
                var filePaths = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads", ImageName);
                using (var stream = System.IO.File.Create(filePaths))
                {
                    await signUpModel.DocumentA.CopyToAsync(stream);
                }
                signUpModel.DocumentAName = ImageName;
            }
            if (signUpModel.DocumentB != null)
            {
                string ImageName = Guid.NewGuid().ToString() + ".pdf";
                var filePaths = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads", ImageName);
                using (var stream = System.IO.File.Create(filePaths))
                {
                    await signUpModel.DocumentB.CopyToAsync(stream);
                }
                signUpModel.DocumentBName = ImageName;
            }
            if (signUpModel.DocumentC != null)
            {
                string ImageName = Guid.NewGuid().ToString() + ".pdf";
                var filePaths = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads", ImageName);
                using (var stream = System.IO.File.Create(filePaths))
                {
                    await signUpModel.DocumentC.CopyToAsync(stream);
                }
                signUpModel.DocumentCName = ImageName;
            }

            var user = await Usermanager.FindByIdAsync(signUpModel.Id);




            user.DocumentA = signUpModel.DocumentAName;
            user.DocumentB = signUpModel.DocumentBName;
            user.DocumentC = signUpModel.DocumentCName;









            var r = await Usermanager.UpdateAsync(user);

            return user;
        }


        public async Task<ActionResult<ApplicationUser>> UpdatePersonalData(SignUpModel signUpModel)
        {

            if (signUpModel.IdentityDocumentA != null)
            {
                string ImageName = Guid.NewGuid().ToString() + ".pdf";
                var filePaths = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads", ImageName);
                using (var stream = System.IO.File.Create(filePaths))
                {
                    await signUpModel.IdentityDocumentA.CopyToAsync(stream);
                }
                signUpModel.IdentityDocument = ImageName;
            }
          
            

            var user = await Usermanager.FindByIdAsync(signUpModel.Id);




            user.FirstName = signUpModel.FirstName;
            user.FamilyName = signUpModel.FamilyName;
            user.Email = signUpModel.Email;
            user.PhoneNumber = signUpModel.PhoneNumber;
            user.IdentityDocument = signUpModel.IdentityDocument;
            user.IdentityId = signUpModel.IdentityId;








            var r = await Usermanager.UpdateAsync(user);

            return user;
        }

        public async Task<ActionResult<ApplicationUser>> UpdateUserToken(ApplicationUser user) 
        {
            var result = await Usermanager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return user;
            }
            else
            {
                return null;
            }
        }
        public async Task<ActionResult<MyModelStringObject>> EstablishConsult(ConsultingEstablishDtos model)
        {
            var user = Usermanager.Users.Where(a => a.Id == model.UserId).FirstOrDefault();
            if(user == null) 
            {
                var MyModelStringObject = new MyModelStringObject();
                var myString = "لم يتم  حجز الاستشارة لعدم وجود مستخدم";
                MyModelStringObject.MyString = myString;
                MyModelStringObject.MyModel = new TbConsultingEstablish();
                return MyModelStringObject;

            }
            var lawyer = Usermanager.Users.Where(a => a.Id == model.UserId).FirstOrDefault();
            if (lawyer == null)
            {
                var MyModelStringObject = new MyModelStringObject();
                var myString = "لم يتم  حجز الاستشارة لعدم وجود محامي";
                MyModelStringObject.MyString = myString;
                MyModelStringObject.MyModel = new TbConsultingEstablish();
                return MyModelStringObject;
            }
            TbSetting oTbSetting = settingService.getAll().FirstOrDefault();
            int leastHoursBetweenConsultation = (int.Parse(oTbSetting.TimeBetweenTwoConsultation) * 60);
            List<TbConsultingEstablish> list = consultingEstablishService.getAll().Where(a=> a.ServiceId == Guid.Parse("39a0999b-c8b8-4fa0-aec9-434285da8ea6")).Where(a=> a.RequestStatus =="حالية" && a.LawyerId == model.LawyerId).ToList();
            foreach(var i in list) 
            {
                TimeSpan? duration = DateTime.Parse(model.ConsultingDateAndTime)-DateTime.Parse(i.ConsultingDateAndTime);
                int hours = (int)duration.Value.TotalMinutes;
                if (hours>0 &&  hours < leastHoursBetweenConsultation) 
                {
                    var MyModelStringObject =  new MyModelStringObject();
                    var myString = "لم يتم الحجز لوجود استشارة بعد الميعاد المطلوب باقل من الوقت المحدد من الادمن";
                    MyModelStringObject.MyString = myString;
                    MyModelStringObject.MyModel = new TbConsultingEstablish();
                    return MyModelStringObject;
                }
                else if(hours<0 && hours > (leastHoursBetweenConsultation *-1)) 
                {
                    var MyModelStringObject = new MyModelStringObject();
                    var myString = "لم يتم الحجز لوجود استشارة قبل الميعاد المطلوب باقل من الوقت المحدد من الادمن";
                    MyModelStringObject.MyString = myString;
                    MyModelStringObject.MyModel = new TbConsultingEstablish();
                    return MyModelStringObject;
                }
                else if(hours ==0) 
                {
                    var MyModelStringObject = new MyModelStringObject();
                    var myString = "توجد استشارة بنفس الميعاد";
                    MyModelStringObject.MyString = myString;
                    MyModelStringObject.MyModel = new TbConsultingEstablish();
                    return MyModelStringObject;
                }
               
            }
            if (model.RequestAudioo != null)
            {
                string ImageName = Guid.NewGuid().ToString() + ".mp4";
                var filePaths = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads", ImageName);
                using (var stream = System.IO.File.Create(filePaths))
                {
                    await model.RequestAudioo.CopyToAsync(stream);
                }
                model.RequestAudio = ImageName;
            }

            if (model.RequestDocumentt != null)
            {
                int count = 0;
                foreach (var file in model.RequestDocumentt)
                {
                    string ImageName = Guid.NewGuid().ToString() + ".pdf";
                    var filePaths = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads", ImageName);
                    using (var stream = System.IO.File.Create(filePaths))
                    {
                        await file.CopyToAsync(stream);
                    }
                    count++;
                    if (count == 1)
                    {
                        model.RequestDocument = ImageName;
                    }
                    else if (count == 2)
                    {
                        model.RequestDocument2 = ImageName;
                    }
                    else if (count == 3)
                    {
                        model.RequestDocument3 = ImageName;
                    }
                    else if (count == 4)
                    {
                        model.RequestDocument4 = ImageName;
                    }
                    else if (count == 5)
                    {
                        model.RequestDocument5 = ImageName;
                    }


                }

            }

            if (model.Images != null)
            {
                int count = 0;
                foreach (var file in model.Images)
                {
                    string ImageName = Guid.NewGuid().ToString() + ".jpg";
                    var filePaths = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads", ImageName);
                    using (var stream = System.IO.File.Create(filePaths))
                    {
                        await file.CopyToAsync(stream);
                    }
                    count++;
                    if (count == 1)
                    {
                        model.ImageOne = ImageName;
                    }
                    else if (count == 2)
                    {
                        model.ImageTwo = ImageName;
                    }
                    else if (count == 3)
                    {
                        model.ImageThree = ImageName;
                    }
                    else if (count == 4)
                    {
                        model.ImageFour = ImageName;
                    }
                    else if (count == 5)
                    {
                        model.ImageFive = ImageName;
                    }


                }

            }
            Random rnd = new Random();
            int num = rnd.Next();
            TbConsultingEstablish oTbConsultingEstablish = new TbConsultingEstablish();
            oTbConsultingEstablish.RequestNo = num.ToString();
            oTbConsultingEstablish.RequestStatus = "بانتظار الرد";
            oTbConsultingEstablish.OrderStatus = "بانتظار موافقة المحامي";
            oTbConsultingEstablish.RequestText = model.RequestText;
            oTbConsultingEstablish.UserId = model.UserId;
            
            oTbConsultingEstablish.UserFirstName = Usermanager.Users.Where(a => a.Id == model.UserId).FirstOrDefault().FirstName;
            oTbConsultingEstablish.UserFamilyName = Usermanager.Users.Where(a => a.Id == model.UserId).FirstOrDefault().FamilyName;
            oTbConsultingEstablish.UserImage = Usermanager.Users.Where(a => a.Id == model.UserId).FirstOrDefault().Image;
            oTbConsultingEstablish.UserEmail = Usermanager.Users.Where(a => a.Id == model.UserId).FirstOrDefault().Email;
            oTbConsultingEstablish.LawyerId = model.LawyerId;
            oTbConsultingEstablish.LawyerName = Usermanager.Users.Where(a => a.Id == model.LawyerId).FirstOrDefault().FirstName;
            oTbConsultingEstablish.LawyerFamilyName = Usermanager.Users.Where(a => a.Id == model.LawyerId).FirstOrDefault().FamilyName;
            oTbConsultingEstablish.LawyerImage = Usermanager.Users.Where(a => a.Id == model.UserId).FirstOrDefault().Image;
            oTbConsultingEstablish.ConsultingTypeId = model.ConsultingTypeId;
            oTbConsultingEstablish.ConsultingType = consultingTypeService.getAll().Where(a => a.ConsultingTypeId == Guid.Parse(model.ConsultingTypeId)).FirstOrDefault().ConsultingTypeTitle;
            oTbConsultingEstablish.MainConsultingId = model.MainConsultingId;
            oTbConsultingEstablish.MainConsultingName = mainConsultingService.getAll().Where(a => a.MainConsultingId == Guid.Parse(model.MainConsultingId)).FirstOrDefault().MainConsultingTitle;
            if(model.SubConsultingId!=null) 
            {
                oTbConsultingEstablish.SubConsultingId = model.SubConsultingId;
                oTbConsultingEstablish.SubConsultingName = _subMainConsultingService.getAll().Where(a => a.SubMainConsultingId == Guid.Parse(model.SubConsultingId)).FirstOrDefault().SubMainConsultingTitle;
            }
            
            oTbConsultingEstablish.ConsultingDateAndTime = model.ConsultingDateAndTime;
            oTbConsultingEstablish.ConsultingPeriod = model.ConsultingPeriod;
            oTbConsultingEstablish.ConsultingPeriodCost = model.ConsultingPeriodCost;
            oTbConsultingEstablish.RequestAudio = model.RequestAudio;
            oTbConsultingEstablish.RequestDocument = model.RequestDocument;
            oTbConsultingEstablish.ConsultingVatvalue = settingService.getAll().FirstOrDefault().ValueAddedTax;
            oTbConsultingEstablish.RequestDocument = model.RequestDocument;
            oTbConsultingEstablish.RequestAudio = model.RequestAudio;
            oTbConsultingEstablish.Notes = model.Notes;
            oTbConsultingEstablish.RequestDocument2 = model.RequestDocument2;
            oTbConsultingEstablish.RequestDocument3 = model.RequestDocument3;
            oTbConsultingEstablish.RequestDocument4 = model.RequestDocument4;
            oTbConsultingEstablish.RequestDocument5 = model.RequestDocument5;
            oTbConsultingEstablish.ServiceId = Guid.Parse("39a0999b-c8b8-4fa0-aec9-434285da8ea6");
            oTbConsultingEstablish.ServiceName = "الاستشارات";
            oTbConsultingEstablish.ProposedCustomerPay = model.ProposedCustomerPay;  //تسجيل المبلغ المالي المقترح
            oTbConsultingEstablish.UnDefinedSubConsultingName = model.UnDefinedSubConsultingName;
            oTbConsultingEstablish.ImageOne = model.ImageOne;
            oTbConsultingEstablish.ImageTwo = model.ImageTwo;
            oTbConsultingEstablish.ImageThree = model.ImageThree;
            oTbConsultingEstablish.ImageFour = model.ImageFour;
            oTbConsultingEstablish.ImageFive = model.ImageFive;
            if (model.ConsultingPeriod == "ثلاثون دقيقة") 
            {
                oTbConsultingEstablish.propsedTimeFinishConsult = DateTime.Parse(model.ConsultingDateAndTime).AddMinutes(30);
            }
            else if(model.ConsultingPeriod == "ستون دقيقة") 
            {
                oTbConsultingEstablish.propsedTimeFinishConsult = DateTime.Parse(model.ConsultingDateAndTime).AddMinutes(60);
            }
            else if (model.ConsultingPeriod == "تسعون دقيقة")
            {
                oTbConsultingEstablish.propsedTimeFinishConsult = DateTime.Parse(model.ConsultingDateAndTime).AddMinutes(90);
            }

            var r = await consultingEstablishService.Add2(oTbConsultingEstablish);
            if (r == true)
            {
                var MyModelStringObject = new MyModelStringObject();
                var myString = "تم حجز الاستشارة";
                MyModelStringObject.MyString = myString;
                MyModelStringObject.MyModel = oTbConsultingEstablish;
                return MyModelStringObject;
            }
            else
            {
                var MyModelStringObject = new MyModelStringObject();
                var myString = "لم يتم  حجز الاستشارة";
                MyModelStringObject.MyString = myString;
                MyModelStringObject.MyModel = new TbConsultingEstablish();
                return MyModelStringObject;

            }



        }

        public async Task<ActionResult<MyModelStringObject>> CheckBeforeEstablishConsult(ConsultingEstablishDtos model)
        {

            TbSetting oTbSetting = settingService.getAll().FirstOrDefault();
            int leastHoursBetweenConsultation = (int.Parse(oTbSetting.TimeBetweenTwoConsultation) * 60);
            List<TbConsultingEstablish> list = consultingEstablishService.getAll().Where(a => a.ServiceId == Guid.Parse("39a0999b-c8b8-4fa0-aec9-434285da8ea6")).Where(a => a.RequestStatus == "حالية" && a.LawyerId == model.LawyerId).ToList();
            var MyModelStringObject = new MyModelStringObject();
            var myString = "";
            foreach (var i in list)
            {
                TimeSpan? duration = DateTime.Parse(model.ConsultingDateAndTime) - DateTime.Parse(i.ConsultingDateAndTime);
                int hours = (int)duration.Value.TotalMinutes;
                if (hours > 0 && hours < leastHoursBetweenConsultation)
                {
                   
                     myString = "لم يتم الحجز لوجود استشارة بعد الميعاد المطلوب باقل من الوقت المحدد من الادمن";
                    MyModelStringObject.MyString = myString;
                    MyModelStringObject.MyModel = new TbConsultingEstablish();
                    return MyModelStringObject;
                }
                else if (hours < 0 && hours > (leastHoursBetweenConsultation * -1))
                {
                     MyModelStringObject = new MyModelStringObject();
                     myString = "لم يتم الحجز لوجود استشارة قبل الميعاد المطلوب باقل من الوقت المحدد من الادمن";
                    MyModelStringObject.MyString = myString;
                    MyModelStringObject.MyModel = new TbConsultingEstablish();
                    return MyModelStringObject;
                }
                else if (hours == 0)
                {
                     MyModelStringObject = new MyModelStringObject();
                     myString = "توجد استشارة بنفس الميعاد";
                    MyModelStringObject.MyString = myString;
                    MyModelStringObject.MyModel = new TbConsultingEstablish();
                    return MyModelStringObject;
                }

            }
                MyModelStringObject = new MyModelStringObject();
                myString = "يمكن  حجز الاستشارة";
                MyModelStringObject.MyString = myString;
                MyModelStringObject.MyModel = new TbConsultingEstablish();
                return MyModelStringObject;

            



        }
        public async Task<ActionResult<MyModelStringObject>> documentationRequest(ConsultingEstablishDtos model)
        {
            if (model.RequestDocumentt != null)
            {
                int count = 0;
                foreach (var file in model.RequestDocumentt)
                {
                    string ImageName = Guid.NewGuid().ToString() + ".pdf";
                    var filePaths = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads", ImageName);
                    using (var stream = System.IO.File.Create(filePaths))
                    {
                        await file.CopyToAsync(stream);
                    }
                    count++;
                    if (count == 1)
                    {
                        model.RequestDocument = ImageName;
                    }
                    else if (count == 2)
                    {
                        model.RequestDocument2 = ImageName;
                    }
                    else if (count == 3)
                    {
                        model.RequestDocument3 = ImageName;
                    }
                    else if (count == 4)
                    {
                        model.RequestDocument4 = ImageName;
                    }
                    else if (count == 5)
                    {
                        model.RequestDocument5 = ImageName;
                    }


                }

            }

            Random rnd = new Random();
            int num = rnd.Next();
            TbConsultingEstablish oTbConsultingEstablish = new TbConsultingEstablish();
            oTbConsultingEstablish.RequestNo = num.ToString();
            oTbConsultingEstablish.RequestStatus = "بانتظار الرد";
            oTbConsultingEstablish.DocumnetationRequestShortDescription = model.DocumnetationRequestShortDescription;
            oTbConsultingEstablish.UserId = model.UserId;
            oTbConsultingEstablish.UserFirstName = model.UserFirstName;
            oTbConsultingEstablish.UserFamilyName = model.UserFamilyName;
            oTbConsultingEstablish.UserImage = Usermanager.Users.Where(a => a.Id == model.UserId).FirstOrDefault().Image;
            oTbConsultingEstablish.UserEmail = Usermanager.Users.Where(a => a.Id == model.UserId).FirstOrDefault().Email;
            oTbConsultingEstablish.LawyerId = model.LawyerId;
            oTbConsultingEstablish.LawyerName = Usermanager.Users.Where(a => a.Id == model.LawyerId).FirstOrDefault().FirstName;
            oTbConsultingEstablish.LawyerImage = Usermanager.Users.Where(a => a.Id == model.UserId).FirstOrDefault().Image;
           
          
            oTbConsultingEstablish.RequestDocument = model.RequestDocument;
            oTbConsultingEstablish.ConsultingVatvalue = settingService.getAll().FirstOrDefault().ValueAddedTax;
            oTbConsultingEstablish.RequestDocument = model.RequestDocument;
           
            oTbConsultingEstablish.RequestDocument2 = model.RequestDocument2;
            oTbConsultingEstablish.RequestDocument3 = model.RequestDocument3;
            oTbConsultingEstablish.RequestDocument4 = model.RequestDocument4;
            oTbConsultingEstablish.RequestDocument5 = model.RequestDocument5;
            oTbConsultingEstablish.DocumentationOfContractId = model.DocumentationOfContractId;
            oTbConsultingEstablish.DocumentationOfContractTilte = documentationOfContractService.getAll().Where(a => a.DocumentationOfContractId == model.DocumentationOfContractId).FirstOrDefault().DocumentationOfContractTilte;
            oTbConsultingEstablish.ConsultingPeriodCost = documentationOfContractService.getAll().Where(a => a.DocumentationOfContractId == model.DocumentationOfContractId).FirstOrDefault().DocumentationOfContractCost;
            oTbConsultingEstablish.ServiceId = Guid.Parse("e871c68d-f8fe-4d66-8739-9c01c3bc3c29");
            oTbConsultingEstablish.ServiceName = "توثيق العقود";
            var r = await consultingEstablishService.Add2(oTbConsultingEstablish);
            if (r == true)
            {
                var MyModelStringObject = new MyModelStringObject();
                var myString = "تم حجز توثيق العقد";
                MyModelStringObject.MyString = myString;
                MyModelStringObject.MyModel = oTbConsultingEstablish;
                return MyModelStringObject;
            }
            else
            {
                var MyModelStringObject = new MyModelStringObject();
                var myString = "لم يتم  حجز توثيق العقد";
                MyModelStringObject.MyString = myString;
                MyModelStringObject.MyModel = new TbConsultingEstablish();
                return MyModelStringObject;

            }



        }

        
        public async Task<ActionResult<MyModelStringObject>> askDelegationOffer(ConsultingEstablishDtos model)
        {


            TbConsultingEstablish oTbConsultingEstablish = consultingEstablishService.getAll().Where(a => a.ConsultingId == model.ConsultingId).FirstOrDefault();
            oTbConsultingEstablish.IsDelegationAsked = model.IsDelegationAsked;
           
            
           

            var r = await consultingEstablishService.Edit3(oTbConsultingEstablish);
            if (r == true)
            {
                var MyModelStringObject = new MyModelStringObject();
                var myString = "تم ارسال طلب عرض سعر للتفويض";
                MyModelStringObject.MyString = myString;
                MyModelStringObject.MyModel = oTbConsultingEstablish;
                TbNotification oTbNotification = new TbNotification();
                oTbNotification.SenderId = oTbConsultingEstablish.UserId;
                oTbNotification.SenderName = oTbConsultingEstablish.UserFirstName;
                oTbNotification.ToWhomId = oTbConsultingEstablish.LawyerId;
                oTbNotification.ToWhomName = oTbConsultingEstablish.LawyerName;
                oTbNotification.Text = "تم ارسال طلب عرض سعر للتفويض";
                oTbNotification.NotificationType = "تم ارسال طلب عرض سعر للتفويض";


                notificationService.Add(oTbNotification);
                return MyModelStringObject;
            }
            else
            {
                var MyModelStringObject = new MyModelStringObject();
                var myString = "لم يتم  ارسال طلب عرض سعر للتفويض";
                MyModelStringObject.MyString = myString;
                MyModelStringObject.MyModel = new TbConsultingEstablish();
                return MyModelStringObject;

            }



        }


        public async Task<ActionResult<MyModelStringObject>> CustomerOfficialDelegationAsk(ConsultingEstablishDtos model)
        {


            TbConsultingEstablish oTbConsultingEstablish = consultingEstablishService.getAll().Where(a => a.ConsultingId == model.ConsultingId).FirstOrDefault();
            oTbConsultingEstablish.UserPhoneNumber = model.UserPhoneNumber;

            oTbConsultingEstablish.UserFirstName = model.UserFirstName;

            oTbConsultingEstablish.UserFamilyName = model.UserFamilyName;

            oTbConsultingEstablish.UserEmail = model.UserEmail;

            oTbConsultingEstablish.CaseShortDescription = model.CaseShortDescription;
            oTbConsultingEstablish.ServiceId = Guid.Parse("d6902f37-784d-4d2a-ae16-cdf8666d0adf");
            oTbConsultingEstablish.ServiceName = "الاستشارات و التفويضات";

            var r = await consultingEstablishService.Edit3(oTbConsultingEstablish);
            if (r == true)
            {
                var MyModelStringObject = new MyModelStringObject();
                var myString = "تم ارسال طلب  للتفويض";
                MyModelStringObject.MyString = myString;
                MyModelStringObject.MyModel = oTbConsultingEstablish;
                TbNotification oTbNotification = new TbNotification();
                oTbNotification.SenderId = oTbConsultingEstablish.UserId;
                oTbNotification.SenderName = oTbConsultingEstablish.UserFirstName;
                oTbNotification.ToWhomId = oTbConsultingEstablish.LawyerId;
                oTbNotification.ToWhomName = oTbConsultingEstablish.LawyerName;
                oTbNotification.Text = "تم ارسال طلب  للتفويض";
                oTbNotification.NotificationType = "تم ارسال طلب  للتفويض";


                notificationService.Add(oTbNotification);
                return MyModelStringObject;
            }
            else
            {
                var MyModelStringObject = new MyModelStringObject();
                var myString = "لم يتم  ارسال طلب  للتفويض";
                MyModelStringObject.MyString = myString;
                MyModelStringObject.MyModel = new TbConsultingEstablish();
                return MyModelStringObject;

            }



        }

        
        public async Task<ActionResult<MyModelStringObject>> customerReplyDelegationOffer(ConsultingEstablishDtos model)
        {

         
            TbConsultingEstablish oTbConsultingEstablish = consultingEstablishService.getAll().Where(a => a.ConsultingId == model.ConsultingId).FirstOrDefault();
            var lawyer = Usermanager.Users.Where(a => a.Id == oTbConsultingEstablish.LawyerId).FirstOrDefault();
            oTbConsultingEstablish.DelegationReplyBack = model.DelegationReplyBack;
            TbNotification oTbNotification = new TbNotification();
            if (model.DelegationReplyBack == "مقبول") 
            {
                oTbConsultingEstablish.DelegationValueApproved = oTbConsultingEstablish.DelegationValueSentFromLawyer;
                oTbConsultingEstablish.ServiceName = "الاستشارات و التفويضات";
                oTbConsultingEstablish.ServiceId = Guid.Parse("d6902f37-784d-4d2a-ae16-cdf8666d0adf");
                oTbNotification.Text = oTbConsultingEstablish.DelegationReplyBack;
                oTbNotification.CreatedBy = "";
                oTbNotification.UpdatedBy = "";
            }
            else 
            {
                oTbConsultingEstablish.DelegationRejectionCause = model.DelegationRejectionCause;
                oTbConsultingEstablish.DelegationValueSentFromUser = model.DelegationValueSentFromUser;
                oTbNotification.Text = oTbConsultingEstablish.DelegationReplyBack;
                oTbNotification.CreatedBy = oTbConsultingEstablish.DelegationRejectionCause;
                oTbNotification.UpdatedBy = oTbConsultingEstablish.DelegationValueSentFromUser;
            }
           


            var r = await consultingEstablishService.Edit3(oTbConsultingEstablish);
            if (r == true)
            {
                var MyModelStringObject = new MyModelStringObject();
                var myString = "تم الرد عل  عرض سعر للتفويض";
                MyModelStringObject.MyString = myString;
                MyModelStringObject.MyModel = oTbConsultingEstablish;
                NotificationModel _notificationModel = new NotificationModel();
                _notificationModel.Title = lawyer.FirstName + lawyer.FamilyName;
                _notificationModel.Body = model.DelegationValueSentFromUser + model.DelegationRejectionCause;
                _notificationModel.DeviceId = lawyer.DeviceToken;
                var notificationResult = notificationServiceqq.SendNotification(_notificationModel);
               
                oTbNotification.SenderId = oTbConsultingEstablish.UserId;
                oTbNotification.SenderName = oTbConsultingEstablish.UserFirstName;
                oTbNotification.ToWhomId = oTbConsultingEstablish.LawyerId;
                oTbNotification.ToWhomName = oTbConsultingEstablish.LawyerName;
              
                oTbNotification.NotificationType = "تم الرد عل  عرض سعر للتفويض";
              

                notificationService.Add(oTbNotification);
                return MyModelStringObject;
            }
            else
            {
                var MyModelStringObject = new MyModelStringObject();
                var myString = "لم يتم  الرد عل عرض سعر للتفويض";
                MyModelStringObject.MyString = myString;
                MyModelStringObject.MyModel = new TbConsultingEstablish();
                return MyModelStringObject;

            }



        }
        
        public async Task<ActionResult<MyModelStringObject>> replyDelegationOffer(ConsultingEstablishDtos model)
        {


            TbConsultingEstablish oTbConsultingEstablish = consultingEstablishService.getAll().Where(a => a.ConsultingId == model.ConsultingId).FirstOrDefault();
            oTbConsultingEstablish.DelegationAskLawyerReplyBack = model.DelegationAskLawyerReplyBack;
            if(model.DelegationValueSentFromLawyer != null) 
            {
                oTbConsultingEstablish.DelegationValueSentFromLawyer = model.DelegationValueSentFromLawyer;
            }
            


            var r = await consultingEstablishService.Edit3(oTbConsultingEstablish);
           
            var user = Usermanager.Users.Where(a => a.Id == oTbConsultingEstablish.UserId).FirstOrDefault();
            if (r == true)
            {
                var MyModelStringObject = new MyModelStringObject();
                var myString = "تم الرد عل  طلب عرض سعر للتفويض";
                MyModelStringObject.MyString = myString;
                MyModelStringObject.MyModel = oTbConsultingEstablish;
                NotificationModel _notificationModel = new NotificationModel();
                _notificationModel.Title = user.FirstName + user.FamilyName;
                _notificationModel.Body = model.DelegationValueSentFromLawyer;
                _notificationModel.DeviceId = user.DeviceToken;
                var notificationResult = notificationServiceqq.SendNotification(_notificationModel);
                TbNotification oTbNotification = new TbNotification();
                oTbNotification.SenderId = oTbConsultingEstablish.LawyerId;
                oTbNotification.SenderName = oTbConsultingEstablish.LawyerName;
                oTbNotification.ToWhomId = oTbConsultingEstablish.UserId;
                oTbNotification.ToWhomName = oTbConsultingEstablish.UserFirstName;
                oTbNotification.Text = oTbConsultingEstablish.DelegationValueSentFromLawyer;
                oTbNotification.NotificationType = "تم الرد عل  عرض سعر للتفويض";


                notificationService.Add(oTbNotification);
                return MyModelStringObject;
            }
            else
            {
                var MyModelStringObject = new MyModelStringObject();
                var myString = "لم يتم  الرد عل طلب عرض سعر للتفويض";
                MyModelStringObject.MyString = myString;
                MyModelStringObject.MyModel = new TbConsultingEstablish();
                return MyModelStringObject;

            }



        }



        public async Task<ActionResult<MyModelStringObject4>> SaveChat(ChatHistoryDtos model)
        {
            if (model.RequestAudioo != null)
            {
                string ImageName = Guid.NewGuid().ToString() + ".mp4";
                var filePaths = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads", ImageName);
                using (var stream = System.IO.File.Create(filePaths))
                {
                    await model.RequestAudioo.CopyToAsync(stream);
                }
                model.SenderAudio = ImageName;
            }

            if (model.RequestDocumentt != null)
            {

                    string ImageName = Guid.NewGuid().ToString() + ".pdf";
                    var filePaths = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads", ImageName);
                    using (var stream = System.IO.File.Create(filePaths))
                    {
                        await model.RequestDocumentt.CopyToAsync(stream);
                    }
                    model.SenderDocument = ImageName;


            }


            TbChat oTbChat = new TbChat();
            oTbChat.SenderText = model.SenderText;
            oTbChat.ConsultingId = model.ConsultingId;
            oTbChat.SenderFirstName = Usermanager.Users.Where(a => a.Id == model.SenderId).FirstOrDefault().FirstName;
            oTbChat.CreatedDate = DateTime.Now;
            oTbChat.SenderId = model.SenderId;
            oTbChat.RecieverEmail = Usermanager.Users.Where(a => a.Id == model.RecieverId).FirstOrDefault().Email;
            oTbChat.SenderId = model.SenderId;
            oTbChat.RecieverFirstName = Usermanager.Users.Where(a => a.Id == model.RecieverId).FirstOrDefault().FirstName;
            oTbChat.RecieverId = model.RecieverId;
            oTbChat.RecieverUserType = Usermanager.Users.Where(a => a.Id == model.RecieverId).FirstOrDefault().UserType;
            oTbChat.SenderAudio = model.SenderAudio;
            oTbChat.SenderDocument = model.SenderDocument;
            oTbChat.SenderEmail = Usermanager.Users.Where(a => a.Id == model.SenderId).FirstOrDefault().Email;
            oTbChat.SenderUserType = Usermanager.Users.Where(a => a.Id == model.SenderId).FirstOrDefault().UserType;



            var r =  chatService.Add(oTbChat);
            if (r == true)
            {
                var MyModelStringObject = new MyModelStringObject4();
                var myString = "تم حفظ المحادثة";
                MyModelStringObject.MyString = myString;
                MyModelStringObject.MyModel = oTbChat;
                return MyModelStringObject;
            }
            else
            {
                var MyModelStringObject = new MyModelStringObject4();
                var myString = "لم يتم حفظ المحادثة";
                MyModelStringObject.MyString = myString;
                MyModelStringObject.MyModel = new TbChat();
                return MyModelStringObject;

            }



        }


        public async Task<ActionResult<MyModelStringObject2>> OfferApprovedOfficeRequest(ApproveOfficeRequestDtos model)
        {

            if (model.DocumentA != null)
            {
                string ImageName = Guid.NewGuid().ToString() + ".pdf";
                var filePaths = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads", ImageName);
                using (var stream = System.IO.File.Create(filePaths))
                {
                    await model.DocumentA.CopyToAsync(stream);
                }
                model.ApprovedOfficeLicenseDoc = ImageName;
            }

            TbApprovedOffice oTbApprovedOffice = new TbApprovedOffice();
            oTbApprovedOffice.ApprovedOfficeLicenseDoc = model.ApprovedOfficeLicenseDoc;
            oTbApprovedOffice.UserPhoneNumber = model.UserPhoneNumber;
            oTbApprovedOffice.UserFirsName = model.UserFirsName;
            oTbApprovedOffice.UserEmail = model.UserEmail;
            oTbApprovedOffice.ApprovalStatus = "غير مفعل";
            oTbApprovedOffice.CreatedBy = model.userid;
           




            var r =  approvedOfficeService.Add(oTbApprovedOffice);
            if (r == true)
            {
                var MyModelStringObject = new MyModelStringObject2();
                var myString = "تم ارسال طلب اعتماد المكتب";
                MyModelStringObject.MyString = myString;
                MyModelStringObject.MyModel = oTbApprovedOffice;
                return MyModelStringObject;
            }
            else
            {
                var MyModelStringObject = new MyModelStringObject2();
                var myString = "لم يتم  ارسال طلب اعتماد المكتب";
                MyModelStringObject.MyString = myString;
                MyModelStringObject.MyModel = new TbApprovedOffice();
                return MyModelStringObject;

            }



        }


        
        public async Task<ActionResult<TbConsultingEstablish>> EstablishConsultWithoutLawyer(ConsultingEstablishDtos model)
        {
            string lowestconsultvaluepropsed = settingService.getAll().FirstOrDefault().LowestConsultUnSpecifiedValue;
            if (int.Parse(model.ProposedCustomerPay) < int.Parse(lowestconsultvaluepropsed))
            {
                return new TbConsultingEstablish();
            }
            if (model.RequestAudioo != null)
            {
                string ImageName = Guid.NewGuid().ToString() + ".mp4";
                var filePaths = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads", ImageName);
                using (var stream = System.IO.File.Create(filePaths))
                {
                    await model.RequestAudioo.CopyToAsync(stream);
                }
                model.RequestAudio = ImageName;
            }

            if (model.RequestDocumentt != null)
            {
                int count = 0;
                foreach (var file in model.RequestDocumentt)
                {
                    string ImageName = Guid.NewGuid().ToString() + ".pdf";
                    var filePaths = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads", ImageName);
                    using (var stream = System.IO.File.Create(filePaths))
                    {
                        await file.CopyToAsync(stream);
                    }
                    count++;
                    if (count == 1)
                    {
                        model.RequestDocument = ImageName;
                    }
                    else if (count == 2)
                    {
                        model.RequestDocument2 = ImageName;
                    }
                    else if (count == 3)
                    {
                        model.RequestDocument3 = ImageName;
                    }
                    else if (count == 4)
                    {
                        model.RequestDocument4 = ImageName;
                    }
                    else if (count == 5)
                    {
                        model.RequestDocument5 = ImageName;
                    }


                }

            }
            if (model.Images != null)
            {
                int count = 0;
                foreach (var file in model.Images)
                {
                    string ImageName = Guid.NewGuid().ToString() + ".jpg";
                    var filePaths = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads", ImageName);
                    using (var stream = System.IO.File.Create(filePaths))
                    {
                        await file.CopyToAsync(stream);
                    }
                    count++;
                    if (count == 1)
                    {
                        model.ImageOne = ImageName;
                    }
                    else if (count == 2)
                    {
                        model.ImageTwo = ImageName;
                    }
                    else if (count == 3)
                    {
                        model.ImageThree = ImageName;
                    }
                    else if (count == 4)
                    {
                        model.ImageFour = ImageName;
                    }
                    else if (count == 5)
                    {
                        model.ImageFive = ImageName;
                    }


                }

            }
            Random rnd = new Random();
            int num = rnd.Next();
            TbConsultingEstablish oTbConsultingEstablish = new TbConsultingEstablish();
            oTbConsultingEstablish.RequestNo = num.ToString();
            oTbConsultingEstablish.RequestStatus = "بانتظار الرد";
            oTbConsultingEstablish.OrderStatus = "بانتظار عروض المحاميين";
            oTbConsultingEstablish.RequestText = model.RequestText;
            oTbConsultingEstablish.UserId = model.UserId;
            oTbConsultingEstablish.UserFirstName = Usermanager.Users.Where(a => a.Id == model.UserId).FirstOrDefault().FirstName;
            oTbConsultingEstablish.UserFamilyName = Usermanager.Users.Where(a => a.Id == model.UserId).FirstOrDefault().FamilyName;
            oTbConsultingEstablish.UserImage = Usermanager.Users.Where(a => a.Id == model.UserId).FirstOrDefault().Image;
            oTbConsultingEstablish.UserEmail = Usermanager.Users.Where(a => a.Id == model.UserId).FirstOrDefault().Email;

            oTbConsultingEstablish.ConsultingTypeId = model.ConsultingTypeId;
            oTbConsultingEstablish.ConsultingType = consultingTypeService.getAll().Where(a => a.ConsultingTypeId == Guid.Parse(model.ConsultingTypeId)).FirstOrDefault().ConsultingTypeTitle;
            oTbConsultingEstablish.MainConsultingId = model.MainConsultingId;
            oTbConsultingEstablish.MainConsultingName = mainConsultingService.getAll().Where(a => a.MainConsultingId == Guid.Parse(model.MainConsultingId)).FirstOrDefault().MainConsultingTitle;
            if (model.SubConsultingId != null)
            {
                oTbConsultingEstablish.SubConsultingId = model.SubConsultingId;
                oTbConsultingEstablish.SubConsultingName = _subMainConsultingService.getAll().Where(a => a.SubMainConsultingId == Guid.Parse(model.SubConsultingId)).FirstOrDefault().SubMainConsultingTitle;
            }
            oTbConsultingEstablish.ConsultingDateAndTime = model.ConsultingDateAndTime;
            oTbConsultingEstablish.ConsultingPeriod = model.ConsultingPeriod;

            oTbConsultingEstablish.RequestAudio = model.RequestAudio;
            oTbConsultingEstablish.RequestDocument = model.RequestDocument;
            oTbConsultingEstablish.ConsultingVatvalue = settingService.getAll().FirstOrDefault().ValueAddedTax;
            oTbConsultingEstablish.RequestDocument = model.RequestDocument;
            oTbConsultingEstablish.RequestAudio = model.RequestAudio;
            oTbConsultingEstablish.Notes = model.Notes;
           
            oTbConsultingEstablish.ProposedCustomerPay = model.ProposedCustomerPay;  //تسجيل المبلغ المالي المقترح
            oTbConsultingEstablish.RequestDocument2 = model.RequestDocument2;
            oTbConsultingEstablish.RequestDocument3 = model.RequestDocument3;
            oTbConsultingEstablish.RequestDocument4 = model.RequestDocument4;
            oTbConsultingEstablish.RequestDocument5 = model.RequestDocument5;

            oTbConsultingEstablish.ServiceId = Guid.Parse("39a0999b-c8b8-4fa0-aec9-434285da8ea6");
            oTbConsultingEstablish.ServiceName = "الاستشارات";
            oTbConsultingEstablish.UnDefinedSubConsultingName = model.UnDefinedSubConsultingName;
            oTbConsultingEstablish.ImageOne = model.ImageOne;
            oTbConsultingEstablish.ImageTwo = model.ImageTwo;
            oTbConsultingEstablish.ImageThree = model.ImageThree;
            oTbConsultingEstablish.ImageFour = model.ImageFour;
            oTbConsultingEstablish.ImageFive = model.ImageFive;
            var r = await consultingEstablishService.Add2(oTbConsultingEstablish);

            return oTbConsultingEstablish;
        }



        public async Task<ActionResult<IEnumerable<WaitingCustomerConsultingDtos>>> LawyerGeneralConsult(ApplicationUser user)
        {
            List<TbLawyersMainConsultings> lstLawyeerConsult = _mainConsultingsService.getAll().Where(a => a.LawyerId == user.Id && a.Status =="مفعل").ToList();
            List<Guid?> lstIdsMainCinsulting = new List<Guid?>();
            foreach (var con in lstLawyeerConsult) 
            {
                lstIdsMainCinsulting.Add(con.MainConsultingId);
            }

            List<TbConsultingEstablish> lstTbConsultingEstablish = consultingEstablishService.getAll().Where(a => a.RequestStatus == "بانتظار الرد" && a.ConsultingType == "طلب غير محدد" && a.LawyerId == null).ToList();
            List<TbConsultingEstablish> lstTbConsultingEstablish2 = new List<TbConsultingEstablish>();
            foreach (var i in lstIdsMainCinsulting) 
            {
                foreach(var el in lstTbConsultingEstablish) 
                {
                    if(el.MainConsultingId == i.ToString()) 
                    {
                        lstTbConsultingEstablish2.Add(el);
                    }
                }

            }
            List<WaitingCustomerConsultingDtos> lstWaitingCustomerConsultingDtos = new List<WaitingCustomerConsultingDtos>();
            foreach (var item in lstTbConsultingEstablish2)
            {
                WaitingCustomerConsultingDtos oWaitingCustomerConsultingDtos = new WaitingCustomerConsultingDtos();
                oWaitingCustomerConsultingDtos.lstOffers = new List<TbOffer>();
                oWaitingCustomerConsultingDtos.SubConsultingName = item.SubConsultingName;
                oWaitingCustomerConsultingDtos.MainConsultingName = item.MainConsultingName;
                oWaitingCustomerConsultingDtos.ConsultingId = item.ConsultingId;
                oWaitingCustomerConsultingDtos.UserFirstName = item.UserFirstName;
                oWaitingCustomerConsultingDtos.UserFamilyName = item.UserFamilyName;
                oWaitingCustomerConsultingDtos.UserImage = item.UserImage;
                oWaitingCustomerConsultingDtos.CreatedBy = item.CreatedBy;
                oWaitingCustomerConsultingDtos.CreatedDate = item.CreatedDate;
                oWaitingCustomerConsultingDtos.RequestNo = item.RequestNo;
                oWaitingCustomerConsultingDtos.RequestStatus = item.RequestStatus;
                oWaitingCustomerConsultingDtos.Notes = item.Notes; // العرض المالي المقدم
                oWaitingCustomerConsultingDtos.UnDefinedSubConsultingName = item.UnDefinedSubConsultingName;
                oWaitingCustomerConsultingDtos.UnDefinedSubConsultingName = item.UnDefinedSubConsultingName;
                oWaitingCustomerConsultingDtos.ProposedCustomerPay = item.ProposedCustomerPay;
                oWaitingCustomerConsultingDtos.ConsultingDateAndTime = item.ConsultingDateAndTime;
                oWaitingCustomerConsultingDtos.OrderStatus = item.OrderStatus;
                lstWaitingCustomerConsultingDtos.Add(oWaitingCustomerConsultingDtos);
            }
            return lstWaitingCustomerConsultingDtos;
        }




        public async Task<ActionResult<IEnumerable<WaitingCustomerConsultingDtos>>> SearchForUser(string id)
        {
            

            List<TbConsultingEstablish> lstTbConsultingEstablish = consultingEstablishService.getAll().Where(a=> a.UserFirstName.Contains(id)).ToList();
           
            List<WaitingCustomerConsultingDtos> lstWaitingCustomerConsultingDtos = new List<WaitingCustomerConsultingDtos>();
            foreach (var item in lstTbConsultingEstablish)
            {
                WaitingCustomerConsultingDtos oWaitingCustomerConsultingDtos = new WaitingCustomerConsultingDtos();
                oWaitingCustomerConsultingDtos.lstOffers = new List<TbOffer>();
                oWaitingCustomerConsultingDtos.SubConsultingName = item.SubConsultingName;
                oWaitingCustomerConsultingDtos.MainConsultingName = item.MainConsultingName;
                oWaitingCustomerConsultingDtos.ConsultingId = item.ConsultingId;
                oWaitingCustomerConsultingDtos.UserFirstName = item.UserFirstName;
                oWaitingCustomerConsultingDtos.UserImage = item.UserImage;
                oWaitingCustomerConsultingDtos.CreatedBy = item.CreatedBy;
                oWaitingCustomerConsultingDtos.CreatedDate = item.CreatedDate;
                oWaitingCustomerConsultingDtos.RequestNo = item.RequestNo;
                oWaitingCustomerConsultingDtos.RequestStatus = item.RequestStatus;
                oWaitingCustomerConsultingDtos.Notes = item.Notes; // العرض المالي المقدم
                oWaitingCustomerConsultingDtos.UnDefinedSubConsultingName = item.UnDefinedSubConsultingName;
                oWaitingCustomerConsultingDtos.UnDefinedSubConsultingName = item.UnDefinedSubConsultingName;
                oWaitingCustomerConsultingDtos.ProposedCustomerPay = item.ProposedCustomerPay;

                lstWaitingCustomerConsultingDtos.Add(oWaitingCustomerConsultingDtos);
            }
            return lstWaitingCustomerConsultingDtos;
        }

        public async Task<ActionResult<IEnumerable<WaitingCustomerConsultingDtos>>> LawyerDelegationData(ApplicationUser user)
        {

            List<TbConsultingEstablish> LstTbConsultingEstablish = consultingEstablishService.getAll().Where(a => a.IsDelegationAsked == "نعم" && a.DelegationAskLawyerReplyBack == null && a.LawyerId == user.Id).ToList();



            List<WaitingCustomerConsultingDtos> lstWaitingCustomerConsultingDtos = new List<WaitingCustomerConsultingDtos>();
            foreach (var item in LstTbConsultingEstablish)
            {
                WaitingCustomerConsultingDtos oWaitingCustomerConsultingDtos = new WaitingCustomerConsultingDtos();
                oWaitingCustomerConsultingDtos.lstOffers = new List<TbOffer>();
                oWaitingCustomerConsultingDtos.SubConsultingName = item.SubConsultingName;
                oWaitingCustomerConsultingDtos.MainConsultingName = item.MainConsultingName;
                oWaitingCustomerConsultingDtos.ConsultingId = item.ConsultingId;
                oWaitingCustomerConsultingDtos.UserFirstName = item.UserFirstName;
                oWaitingCustomerConsultingDtos.UserFamilyName = item.UserFamilyName;
                oWaitingCustomerConsultingDtos.UserImage = item.UserImage;
                oWaitingCustomerConsultingDtos.CreatedBy = item.CreatedBy;
                oWaitingCustomerConsultingDtos.CreatedDate = item.CreatedDate;
                oWaitingCustomerConsultingDtos.RequestNo = item.RequestNo;
                
                oWaitingCustomerConsultingDtos.Notes = item.Notes; // العرض المالي المقدم
                oWaitingCustomerConsultingDtos.UnDefinedSubConsultingName = item.UnDefinedSubConsultingName;

                lstWaitingCustomerConsultingDtos.Add(oWaitingCustomerConsultingDtos);
            }
            return lstWaitingCustomerConsultingDtos;
        }

        

        public async Task<ActionResult<IEnumerable<WaitingCustomerConsultingDtos>>> LawyerSpecificConsult(ApplicationUser user)
        {
            List<WaitingCustomerConsultingDtos> lstWaitingCustomerConsultingDtos = new List<WaitingCustomerConsultingDtos>();
           
            List<TbConsultingEstablish> lstTbConsultingEstablish = consultingEstablishService.getAll().Where(A => A.LawyerId == user.Id).Where(a => a.RequestStatus == "بانتظار الرد").ToList();
           
            foreach (var item in lstTbConsultingEstablish)
            {
                WaitingCustomerConsultingDtos oWaitingCustomerConsultingDtos = new WaitingCustomerConsultingDtos();
                oWaitingCustomerConsultingDtos.lstOffers = new List<TbOffer>();
                oWaitingCustomerConsultingDtos.SubConsultingName = item.SubConsultingName;
                oWaitingCustomerConsultingDtos.MainConsultingName = item.MainConsultingName;
                oWaitingCustomerConsultingDtos.ConsultingId = item.ConsultingId;
                oWaitingCustomerConsultingDtos.UserFirstName = item.UserFirstName;
                oWaitingCustomerConsultingDtos.UserFamilyName = item.UserFamilyName;
                oWaitingCustomerConsultingDtos.UserImage = item.UserImage;
                oWaitingCustomerConsultingDtos.CreatedBy = item.CreatedBy;
                oWaitingCustomerConsultingDtos.CreatedDate = item.CreatedDate;
                oWaitingCustomerConsultingDtos.RequestNo = item.RequestNo;
                oWaitingCustomerConsultingDtos.RequestStatus = item.RequestStatus;
                oWaitingCustomerConsultingDtos.Notes = item.Notes; // العرض المالي المقدم
                
                DateTime dateTimeConsultingDateAndTime = DateTime.Parse(item.ConsultingDateAndTime);
                oWaitingCustomerConsultingDtos.ConsultingDateAndTime = dateTimeConsultingDateAndTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
               
               
                oWaitingCustomerConsultingDtos.UnDefinedSubConsultingName = item.UnDefinedSubConsultingName;
                oWaitingCustomerConsultingDtos.OrderStatus = item.OrderStatus;
                if (item.ConsultingPeriod == "ثلاثون دقيقة")
                {
                    oWaitingCustomerConsultingDtos.propsedTimeFinishConsult = DateTime.Parse(item.ConsultingDateAndTime).AddMinutes(30);
                    string inputDate = oWaitingCustomerConsultingDtos.propsedTimeFinishConsult.ToString();

                    // Parse the input date string into a DateTime object
                    DateTime dateTime = DateTime.Parse(inputDate);

                    // Convert the DateTime object to the desired format
                    oWaitingCustomerConsultingDtos.propsedTimeFinishConsultFormatted = dateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");


                }
                else if (item.ConsultingPeriod == "ستون دقيقة")
                {
                    oWaitingCustomerConsultingDtos.propsedTimeFinishConsult = DateTime.Parse(item.ConsultingDateAndTime).AddMinutes(60);
                    string inputDate = oWaitingCustomerConsultingDtos.propsedTimeFinishConsult.ToString();

                    // Parse the input date string into a DateTime object
                    DateTime dateTime = DateTime.Parse(inputDate);

                    // Convert the DateTime object to the desired format
                    oWaitingCustomerConsultingDtos.propsedTimeFinishConsultFormatted = dateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");

                }
                else if (item.ConsultingPeriod == "تسعون دقيقة")
                {
                    oWaitingCustomerConsultingDtos.propsedTimeFinishConsult = DateTime.Parse(item.ConsultingDateAndTime).AddMinutes(90);
                    string inputDate = oWaitingCustomerConsultingDtos.propsedTimeFinishConsult.ToString();

                    // Parse the input date string into a DateTime object
                    DateTime dateTime = DateTime.Parse(inputDate);

                    // Convert the DateTime object to the desired format
                    oWaitingCustomerConsultingDtos.propsedTimeFinishConsultFormatted = dateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");

                }
                lstWaitingCustomerConsultingDtos.Add(oWaitingCustomerConsultingDtos);
            }
            return lstWaitingCustomerConsultingDtos;
        }

        public async Task<string> LoginAsync(SignInModel signInModel)
        {
            //var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);
            var result = await SignInManager.PasswordSignInAsync(signInModel.UserName, signInModel.Password, true, true);
            var user = await Usermanager.Users.Where(a => a.UserName == signInModel.UserName).FirstOrDefaultAsync();
            user.DeviceToken = new JwtSecurityTokenHandler().WriteToken(GenerateJwtToken(user).Result);
            await Usermanager.UpdateAsync(user);
            return result.ToString();






        }


        [AllowAnonymous, HttpPost("forgot-password")]
        public async Task<string> ForgotPassword(ForgotPasswordViewModel model)
        {

            //if (user != null && await Usermanager.IsEmailConfirmedAsync(user))
            //{
            //    var token = await Usermanager.GeneratePasswordResetTokenAsync(user);
            //    var passwordResetLink = Url.Action("ResetPassword", "Account", new { email = model.Email, token = token }, Request.Scheme);
            //    logger.Log(LogLevel.Warning, passwordResetLink);
            //    return View("ForgotPasswordConfirmation");

            //}

            var user = await Usermanager.FindByNameAsync(model.UserName);


            if (user != null)
            {
                var code = await Usermanager.GenerateTwoFactorTokenAsync(user, "Phone");

                user.OTP = code;



                var message = "Your security code is: " + code;
                var smsResult = await _smsService.SendMessage(model.phonenumber, message);
              
                if (smsResult != null)
                {
                    if (smsResult == "لم يتم ارسال الرسالة")
                    {
                        return "The Message Has Not Been Sent";
                    }
                    await Usermanager.UpdateAsync(user);
                    var token = await Usermanager.GeneratePasswordResetTokenAsync(user);
                    return token;

                }
                else
                {
                    return "The Phone number is Wrong";
                }

            }
            else
            {
                return "There is No User By This Name";
            }








        }


        public async Task<string> pHONEcON(SignUpModel signUpModel)
        {
            var res2 = Usermanager.Users.Where(a => a.UserName == signUpModel.UserName).FirstOrDefault();

            if (res2.OTP == signUpModel.OTP)
            {
                res2.OTP = "";
                await Usermanager.UpdateAsync(res2);
                return "Success";



            }
            else
            {
                return "wrong code";
            }


        }



        public async Task GenerateForgotPasswordTokenAsync(ApplicationUser user)
        {
            var token = await Usermanager.GeneratePasswordResetTokenAsync(user);
            if (!string.IsNullOrEmpty(token))
            {
                //await SendForgotPasswordEmail(user, token);
            }



        }






        public async Task<ActionResult<TbEvaluation>> Evaluate(EaluationDTos model)
        {




            TbEvaluation oTbEvaluation = new TbEvaluation();
            oTbEvaluation.EvaluaterId = model.EvaluaterId;
            oTbEvaluation.CreatedBy = Usermanager.Users.Where(a => a.Id == model.EvaluaterId).FirstOrDefault().FirstName;
            oTbEvaluation.ToBeEvaluatedId = model.ToBeEvaluatedId;
            oTbEvaluation.UpdatedBy = Usermanager.Users.Where(a => a.Id == model.ToBeEvaluatedId).FirstOrDefault().FirstName;
            oTbEvaluation.EvaluaterImage = Usermanager.Users.Where(a => a.Id == model.EvaluaterId).FirstOrDefault().Image;
            oTbEvaluation.ToBeEvaluatedImage = Usermanager.Users.Where(a => a.Id == model.ToBeEvaluatedId).FirstOrDefault().Image;
            oTbEvaluation.StartsNo = model.StartsNo;
            oTbEvaluation.ConsultationServiceId = model.ConsultationServiceId;
            oTbEvaluation.Notes = consultingEstablishService.getAll().Where(a => a.ConsultingId == Guid.Parse(model.ConsultationServiceId)).FirstOrDefault().MainConsultingName;
            oTbEvaluation.EvaluationText = model.EvaluationText;


            var r = await evaluationService.Add2(oTbEvaluation);

            return oTbEvaluation;
        }



        public async Task<ActionResult<TbEvaluationApprovedOffice>> EvaluateApproveOffice(ApproveOfficeEvalutionDtos model)
        {




            TbEvaluationApprovedOffice oTbEvaluation = new TbEvaluationApprovedOffice();
            oTbEvaluation.EvaluaterId = model.EvaluaterId;
            oTbEvaluation.EvaluaterName = Usermanager.Users.Where(a => a.Id == model.EvaluaterId).FirstOrDefault().FirstName;
            oTbEvaluation.ApprovedOfficeId = model.ApprovedOfficeId;
            oTbEvaluation.ApprovedOfficeName = approvedOfficeService.getAll().Where(a => a.ApprovedOfficeId == Guid.Parse(model.ApprovedOfficeId)).FirstOrDefault().ApprovedOfficeName;
            oTbEvaluation.EvaluaterImage = Usermanager.Users.Where(a => a.Id == model.EvaluaterId).FirstOrDefault().Image;
            oTbEvaluation.ApprovedOfficeLogo = approvedOfficeService.getAll().Where(a => a.ApprovedOfficeId == Guid.Parse(model.ApprovedOfficeId)).FirstOrDefault().ApprovedOfficeLogo;
            oTbEvaluation.StartsNo = model.StartsNo;

            oTbEvaluation.EvaluationApprovedOfficeText = model.EvaluationApprovedOfficeText;

            var r = await evaluationApprovedOfficeService.Add2(oTbEvaluation);

            return oTbEvaluation;
        }



        public async Task<ActionResult<TbConsultingEstablish>> Cancell(CancellConsultDtos model)
        {


            // get the time zone info for a specific time zone (in this example, Eastern Standard Time)
            TimeZoneInfo easternTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Arab Standard Time");

            // get the current time in the specified time zone
            DateTimeOffset easternTime = TimeZoneInfo.ConvertTime(DateTimeOffset.Now, easternTimeZone);

            TbConsultingEstablish oTbConsultingEstablish = consultingEstablishService.getAll().Where(a=> a.ConsultingId == model.ConsultingId).FirstOrDefault();
            oTbConsultingEstablish.RequestStatus = "ملغية";
            oTbConsultingEstablish.DayOfCancellation = easternTime.ToString();
             var r =  consultingEstablishService.Edit2(oTbConsultingEstablish);

            return r;
        }


        #region create and validate JWT token

        private async Task<JwtSecurityToken> GenerateJwtToken(ApplicationUser user, int? time = null)
        {
            var userClaims = await Usermanager.GetClaimsAsync(user);
            var roles = await Usermanager.GetRolesAsync(user);
            var roleClaims = roles.Select(role => new Claim("roles", role)).ToList();
            var userType = user.UserType.ToString();

            var claims = new[]
                {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("uid", user.Id),
                new Claim("Name", user.FirstName),
                new Claim("userType",userType),
                //(user.IsAdmin) ? new Claim("isAdmin", "true") : new Claim("isAdmin", "false"),
        }
            .Union(userClaims)
                .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwt.Issuer,
            audience: _jwt.Audience,
            claims: claims,
                expires: (time != null) ? DateTime.Now.AddHours((double)time) : DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }


        public string ValidateJwtToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                if (token == null)
                    return null;
                if (token.StartsWith("Bearer "))
                    token = token.Replace("Bearer ", "");

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out var validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var accountId = jwtToken.Claims.First(x => x.Type == "uid").Value;

                return accountId;
            }
            catch
            {
                return null;
            }
        }

        #endregion create and validate JWT token


    }
}



