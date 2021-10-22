using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ideaworks.Models
{
    public partial class ProposalInformation
    {
        public long StageId { get; set; }
        [Required]
        [DisplayName("Does the Project require urgent attention?")]
        public string Urgency { get; set; }
        [Required]
        [DisplayName("How did you learn about IDEAWORKS?")]
        public string SuggestedBy { get; set; }
        [DisplayName("Would you like to provide any additional information?")]
        public string ExtraInformation { get; set; } = "";
        [Required]
        [DisplayName("Parent or Subsidiary organization?")]
        public string ParentOrSubsidiary { get; set; }
        [Required]
        [DisplayName("Authorized Representative Name")]
        public string AuthorizedRepresentativeName { get; set; }
        [Required]
        [DisplayName("Authorized Representative Position")]
        public string AuthorizedRepresentativePosition { get; set; }
        [Required]
        [Phone]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number.")]
        [DisplayName("Authorized Representative Phone")]
        public string AuthorizedRepresentativePhone { get; set; }
        [Required]
        [EmailAddress]
        [DisplayName("Authorized Representative Email")]
        public string AuthorizedRepresentativeEmail { get; set; }
        [Required]
        [DisplayName("Number of Employees in Canada")]
        public int? NumberOfEmployeesInCanada { get; set; }
        [Required]
        [DisplayName("Number of Research Staff in Canada")]
        public int? NumberOfResearchStaffInCanada { get; set; }
        [Required]
        [DisplayName("Area of Research")]
        public string AreaOfResearch { get; set; }

        public virtual Project Stage { get; set; }
    }
}
