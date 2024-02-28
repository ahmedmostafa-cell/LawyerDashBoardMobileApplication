using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace AlMohamyProject.Dtos
{
    public class ConsultingEstablishDtos
    {
        public Guid? ConsultingId { get; set; }
        public string RequestNo { get; set; }
        public string RequestStatus { get; set; }
        public string RequestText { get; set; }
        public string RequestDocument { get; set; }

        public string RequestDocument2 { get; set; }

        public string RequestDocument3 { get; set; }

        public string RequestDocument4 { get; set; }

        public string RequestDocument5 { get; set; }

        public string ImageOne { get; set; }
        public string ImageTwo { get; set; }
        public string ImageThree { get; set; }
        public string ImageFour { get; set; }
        public string ImageFive { get; set; }

        public List<IFormFile> Images { get; set; }

        public List<IFormFile> RequestDocumentt { get; set; }
        public string RequestAudio { get; set; }


        public IFormFile RequestAudioo { get; set; }
        public string RequestType { get; set; }
        public string UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserFamilyName { get; set; }
        public string UserImage { get; set; }
        public string UserEmail { get; set; }
        public string LawyerId { get; set; }
        public string LawyerName { get; set; }
        public string LawyerImage { get; set; }
        public string ConsultingTypeId { get; set; }
        public string ConsultingType { get; set; }
        public string MainConsultingId { get; set; }
        public string MainConsultingName { get; set; }
        public string SubConsultingId { get; set; }
        public string SubConsultingName { get; set; }
        public string ConsultingPeriod { get; set; }
        public string ConsultingPeriodCost { get; set; }
        public string PromocodeDiscountValue { get; set; }
        public string ConsultingVatvalue { get; set; }
        public string TheTotal { get; set; }
        public string TheConsultingPaidValue { get; set; }
        public string TransactionType { get; set; }
        public string ConsultingDateAndTime { get; set; }
        public string TimeRemainingForConsultingToStart { get; set; }
        public string AreaName { get; set; }
        public string CityName { get; set; }
        public string IsDelegationAsked { get; set; }
        public string DelegationValueSentFromUser { get; set; }
        public string DelegationValueSentFromLawyer { get; set; }
        public string DelegationValueApproved { get; set; }
        public string DelegationReplyBack { get; set; }
        public string DelegationRejectionCause { get; set; }
        public string NoOfOffers { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Notes { get; set; }
        public int? CurrentState { get; set; }

        public string ProposedCustomerPay { get; set; }


        public string DelegationAskLawyerReplyBack { get; set; }


        public string UserPhoneNumber { get; set; }


        public string CaseShortDescription { get; set; }



        public string DocumnetationRequestShortDescription { get; set; }


        public Guid? DocumentationOfContractId { get; set; }

        public string DocumentationOfContractTilte { get; set; }


        public string UnDefinedSubConsultingName { get; set; }


    }
}
