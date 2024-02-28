using System;
using System.Collections.Generic;
using System.Text;

namespace Domains
{
    public class TbChat
    {
        public Guid ChatId { get; set; }

        public Guid? ConsultingId { get; set; }

        public string? SenderId { get; set; }

        public string? SenderFirstName { get; set; }


        public string? SenderEmail { get; set; }


        public string? SenderText { get; set; }


        public string? SenderAudio { get; set; }


        public string? SenderDocument { get; set; }


        public string? SenderUserType { get; set; }


        public string? RecieverId { get; set; }

        public string? RecieverFirstName { get; set; }


        public string? RecieverEmail { get; set; }


        public string? RecieverUserType { get; set; }


        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? Notes { get; set; }
        public int? CurrentState { get; set; }
    }
}
