using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Domains
{
    public partial class TbSetting
    {
        public Guid? SettingId { get; set; }
        [Required(ErrorMessage = "من فضلك ادخل ضريبة القيمة المضافة    ")]
        public string ValueAddedTax { get; set; }
        [Required(ErrorMessage = "من فضلك ادخل نسبة ربح التطبيق    ")]
        public string AppProfitPercent { get; set; }
        [Required(ErrorMessage = "من فضلك ادخل عدد ايام صلاحية العرض      ")]
        public string OffersValidityDays { get; set; }


        [Required(ErrorMessage = "من فضلك ادخل اقل وقت بالساعة للمحامي بين استشارتين متتاليتين       ")]
        public string TimeBetweenTwoConsultation { get; set; }

        [Required(ErrorMessage = "من فضلك ادخل اقل مبلغ يستطيع المستخدم وضعه لقيمة استشارة غير محددة للمحامي       ")]
        public string LowestConsultUnSpecifiedValue { get; set; }

        [Required(ErrorMessage = "من فضلك ادخل اعلي قيمة استشارة ثلاثون دقيقة ")]
        public string Consulting30MinutesCost { get; set; }
        [Required(ErrorMessage = "من فضلك ادخل اعلي قيمة استشارة ستون دقيقة ")]
        public string Consulting60MinutesCost { get; set; }
        [Required(ErrorMessage = "من فضلك ادخل اعلي قيمة استشارة تسعون دقيقة ")]
        public string Consulting90MinutesCost { get; set; }


        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Notes { get; set; }
        public int? CurrentState { get; set; }
    }
}
