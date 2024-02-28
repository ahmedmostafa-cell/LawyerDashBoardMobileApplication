using System;
using System.Collections.Generic;

#nullable disable

namespace Domains
{
    public partial class TbComplainsAndSuggestion
    {
        public Guid? ComplaintsAndSuggestionsId { get; set; }
        public string Idd { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ComplaintsAndSuggestionsText { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Notes { get; set; }
        public int? CurrentState { get; set; }
    }
}
