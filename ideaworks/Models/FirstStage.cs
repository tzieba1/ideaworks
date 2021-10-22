using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable

namespace ideaworks.Models
{
    public partial class FirstStage
    {
        [DisplayName("Project ID")]
        public long ProjectId { get; set; }
        [DisplayName("Project idea and technical feasibility")]
        public string TechnicalFeasibility { get; set; }
        [DisplayName("Timing and criticality to partner")]
        public string TimingAndCriticality { get; set; }
        [DisplayName("Suitability for students and facility")]
        public string WorkableByFaculty { get; set; }
        [DisplayName("How would the project be paid for")]
        public string ProjectPaidFor { get; set; }
        [DisplayName("Positive impact on the community")]
        public string ImpactOnCommunity { get; set; }
        [DisplayName("Is this project feasible for IDEAWORKS?")]
        public string ProjectWorthDeveloping { get; set; }

        public virtual Evaluation Project { get; set; }
    }
}
