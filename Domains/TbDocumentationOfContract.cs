using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Domains
{
    public partial class TbDocumentationOfContract
    {
        public Guid? DocumentationOfContractId { get; set; }
        [Required(ErrorMessage = "من فضلك ادخل عنوان توثيق العقود")]
        public string DocumentationOfContractTilte { get; set; }
        [Required(ErrorMessage = "من فضلك ادخل تفاصيل توثيق العقود")]
        public string DocumentationOfContractDescription { get; set; }
        public string DocumentationOfContractImage { get; set; }
        [Required(ErrorMessage = "من فضلك ادخل تكلفة توثيق العقود")]
        public string DocumentationOfContractCost { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Notes { get; set; }
        public int? CurrentState { get; set; }
    }
}
