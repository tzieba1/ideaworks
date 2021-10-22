using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ideaworks.Models
{
    public partial class ThirdStage
    {
        [DisplayName("Project ID")]
        public long ProjectId { get; set; }
        [DisplayName("Internal approval")]
        public string InternalApproval { get; set; }
        [DisplayName("Define and indentify the project problem, goal, deliverables and proposed solution")]
        public string IdentifiedProjectProblems { get; set; }
        [DisplayName("Is funding approved?")]
        public string IsFundingApproved { get; set; }
        [DisplayName("Is funding required?")]
        [Required]
        public string IsApplicable { get; set; }
        [DisplayName("Application for funding")]
        public string ApplicationForFunding { get; set; }
        public string Status { get; set; }

        public virtual Evaluation Project { get; set; }
    }
}
