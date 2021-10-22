using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ideaworks.Models
{
    public partial class ProjectInformation
    {
        public long StageId { get; set; }
        [Required]
        [DisplayName("Describe the Challenge to be addressed")]
        public string Challenges { get; set; }
        [Required]
        [DisplayName("Planned Project impact on the Organization")]
        public string ProjectImpact { get; set; }
        [Required]
        [DisplayName("How will the Project outcome be used by the Organization?")]
        public string ProjectUsage { get; set; }
        [Required]
        [DisplayName("What is the expected Project Outcome?")]
        public string ProjectOutcome { get; set; }

        public virtual Project Stage { get; set; }
    }
}
