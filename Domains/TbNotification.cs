using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Domains
{
    public partial class TbNotification
    {
        public Guid? NotificationId { get; set; }
        public string SenderId { get; set; }
        public string SenderName { get; set; }
        [Required(ErrorMessage = "من فضلك ادخل اسم المرسل اليه")]
        public string ToWhomId { get; set; }
        public string ToWhomName { get; set; }
        [Required(ErrorMessage = "من فضلك ادخل نص الاشعار ")]
        public string Text { get; set; }
        public string NotificationType { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Notes { get; set; }
        public int? CurrentState { get; set; }
    }
}
