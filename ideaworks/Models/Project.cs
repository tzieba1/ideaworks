using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ideaworks.Models
{
    public partial class Project
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [DisplayName("Project ID")]
        public long ProjectId { get; set; }
        [DisplayName("Project Status")]
        public string ProjectStatus { get; set; }
        [DisplayName("Submission Date")]
        [DataType(DataType.Date)]
        public DateTimeOffset? SubmissionDate { get; set; }
        [DisplayName("Start Date")]
        [DataType(DataType.Date)]
        public DateTimeOffset? StartDate { get; set; }

        public virtual CompanyInformation CompanyInformation { get; set; }
        public virtual Evaluation Evaluation { get; set; }
        public virtual ProjectInformation ProjectInformation { get; set; }
        public virtual ProposalInformation ProposalInformation { get; set; }
    }
}
