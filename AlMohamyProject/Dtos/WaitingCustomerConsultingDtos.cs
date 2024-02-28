using BL;
using Domains;
using System;
using System.Collections.Generic;

namespace AlMohamyProject.Dtos
{
    public class WaitingCustomerConsultingDtos
    {
        public Guid? ConsultingId { get; set; }
        public string RequestNo { get; set; }
        public string RequestStatus { get; set; }

        public string UserFirstName { get; set; }

        public string UserFamilyName { get; set; }

        public string UserId { get; set; }

        public string UserPhone { get; set; }


        public string UserImage { get; set; }

        public string Notes { get; set; }
        public string MainConsultingName { get; set; }
    
        public string SubConsultingName { get; set; }
       
        public string NoOfOffers { get; set; }
        public string CreatedBy { get; set; }
      
        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string TimeRemainingForConsultingToStart { get; set; }

        public string LawyerId { get; set; }
        public string LawyerName { get; set; }
        public string LawyerImage { get; set; }

        public string LawyerFamilyName { get; set; }
        public List<TbOffer> lstOffers { get; set; }

        public string ProposedCustomerPay { get; set; }


        public string UnDefinedSubConsultingName { get; set; }

        public string MainConsultingId { get; set; }

        public string ConsultingTypeId { get; set; }

        public DateTime? propsedTimeFinishConsult { get; set; }


        public string propsedTimeFinishConsultFormatted { get; set; }


        public string ConsultingDateAndTime { get; set; }


        public string OrderStatus { get; set; }

    }
}
