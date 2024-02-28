
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domains
{
    public class NotificationConfirmed
    {
        public int Id { get; set; }

        [ForeignKey("Notification")]
        public int NotificationId { get; set; }

        public virtual Notification Notification { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        
    }
}
