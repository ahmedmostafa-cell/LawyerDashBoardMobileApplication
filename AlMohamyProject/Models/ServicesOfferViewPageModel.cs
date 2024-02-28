using System;

namespace AlMohamyProject.Models
{
    public class ServicesOfferViewPageModel
    {
        public Guid? OfferId { get; set; }
        public Guid? ConsultingId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string LawyerId { get; set; }
        public string LawyerName { get; set; }
        public string LawyerImage { get; set; }
        public string LawyerEvalNoStarts { get; set; }
        public string LawyersEvalNumerical { get; set; }
        public string LawyerShortDescription { get; set; }
        public string LawyersExperinceYears { get; set; }
        public string OfferEndDate { get; set; }
        public string OfferStatus { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Notes { get; set; }
        public int? CurrentState { get; set; }
    }
}
