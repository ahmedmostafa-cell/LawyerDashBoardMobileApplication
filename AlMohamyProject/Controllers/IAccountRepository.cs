using AlMohamyProject.Dtos;
using AlMohamyProject.Models;
using BL;
using BL.Migrations;
using Domains;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlMohamyProject.Controllers
{
    public interface IAccountRepository
    {


        Task<string> pHONEcON(SignUpModel signUpModel);

        Task<string> SignUpAsync(SignUpModel signUpModel);


       

        Task<ActionResult<ApplicationUser>> uploadImage(SignUpModel signUpModel);


        Task<ActionResult<ApplicationUser>> uploadDoms(SignUpModel signUpModel);

        Task<ActionResult<ApplicationUser>> UpdatePersonalData(SignUpModel signUpModel);

        Task<ActionResult<ApplicationUser>> UpdateUserToken(ApplicationUser user);

        Task<ActionResult<MyModelStringObject>> EstablishConsult(ConsultingEstablishDtos model);

        Task<ActionResult<MyModelStringObject>> CheckBeforeEstablishConsult(ConsultingEstablishDtos model);
        Task<ActionResult<MyModelStringObject>> documentationRequest(ConsultingEstablishDtos model);
        
        Task<ActionResult<MyModelStringObject>> askDelegationOffer(ConsultingEstablishDtos model);

        Task<ActionResult<MyModelStringObject>> CustomerOfficialDelegationAsk(ConsultingEstablishDtos model);

        
        Task<ActionResult<MyModelStringObject>> replyDelegationOffer(ConsultingEstablishDtos model);


        Task<ActionResult<MyModelStringObject4>> SaveChat(ChatHistoryDtos model);

        Task<ActionResult<MyModelStringObject2>> OfferApprovedOfficeRequest(ApproveOfficeRequestDtos model);


        

        Task<ActionResult<MyModelStringObject>> customerReplyDelegationOffer(ConsultingEstablishDtos model);

        
        Task<ActionResult<TbConsultingEstablish>> EstablishConsultWithoutLawyer(ConsultingEstablishDtos model);


       
        Task<ActionResult<IEnumerable<WaitingCustomerConsultingDtos>>> LawyerGeneralConsult(ApplicationUser user);

        Task<ActionResult<IEnumerable<WaitingCustomerConsultingDtos>>> SearchForUser(string id);

        Task<ActionResult<IEnumerable<WaitingCustomerConsultingDtos>>> LawyerDelegationData(ApplicationUser user);

        
        Task<ActionResult<IEnumerable<WaitingCustomerConsultingDtos>>> LawyerSpecificConsult(ApplicationUser user);


        Task<ActionResult<TbEvaluation>> Evaluate(EaluationDTos model);


        Task<ActionResult<TbEvaluationApprovedOffice>> EvaluateApproveOffice(ApproveOfficeEvalutionDtos model);

        Task<ActionResult<TbConsultingEstablish>> Cancell(CancellConsultDtos model);

        Task<string> LoginAsync(SignInModel signInModel);

        Task<string> ForgotPassword(ForgotPasswordViewModel model);

    }
}
