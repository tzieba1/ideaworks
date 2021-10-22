using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable

namespace ideaworks.Models
{
    public partial class SecondStage
    {
        [DisplayName("Project ID")]
        public long ProjectId { get; set; }
        [DisplayName("Possibility of multi-center collaboration")]
        public string MulitcentreCollaboration { get; set; }
        [DisplayName("Link to academic areas of interest")]
        public string InterestAreaLink { get; set; }
        [DisplayName("Interest in faculty participation")]
        public string InterestedFacultyParticipation { get; set; }
        [DisplayName("Relation to IoT")]
        public string RelatedToIot { get; set; }
        [DisplayName("Additional information required")]
        public string MoreInfomationRequired { get; set; }
        [DisplayName("Lead Center")]
        public string LeadCentres { get; set; }
        [DisplayName("General scope of project idea")]
        public string GeneralScope { get; set; }
        [DisplayName("Is general scope confirmed?")]
        public string GeneralScopeConfirmed { get; set; }
        [DisplayName("Can an IDEAWORKS center take the project on?")]
        public string FeasibleForIdeaworks { get; set; }

        public virtual Evaluation Project { get; set; }
    }
}
