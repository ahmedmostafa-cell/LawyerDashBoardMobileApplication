using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlMohamyProject.Models
{
    public class SendComplainModel
    {
        public Guid ComplainId { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public string ComplaintsAndSuggestionsText { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Notes { get; set; }
    }
}
