using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ideaworks.Models
{
    public partial class Evaluation
    {
        [DisplayName("Project ID")]
        public long ProjectId { get; set; }
        public string DiscussedWithPartner { get; set; }
        [DisplayName("Feedback text")]
        public string Feedback { get; set; }
        public int? EvaluationStage { get; set; }

        public virtual Project Project { get; set; }
        public virtual FirstStage FirstStage { get; set; }
        public virtual SecondStage SecondStage { get; set; }
        public virtual ThirdStage ThirdStage { get; set; }
    }
}
