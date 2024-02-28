using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Linq;

namespace Domains
{
    public class Notification
    {
        public int Id { get; set; }

        [Display(Name = "عنوان الاشعار")]
        [Required(ErrorMessage = "يجب أدخال عنوان الاشعار")]
        public string Title { get; set; }
        [Display(Name = "تفاصيل الاشعار")]
        [Required(ErrorMessage = "يجب أدخال تفاصيل الاشعار")]
        public string Body { get; set; }

        [Display(Name = "تاريح أنشاء الاشعار ")]
        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
