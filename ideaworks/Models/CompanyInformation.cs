using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ideaworks.Models
{
    public partial class CompanyInformation
    {
        
        public long StageId { get; set; }
        [Required]
        [DisplayName("Contact Name")]
        public string ContactName { get; set; }
        [Required]
        [DisplayName("Organization Name")]
        public string CompanyName { get; set; }
        [Required]
        [EmailAddress]
        [DisplayName("Email Address")]
        public string Email { get; set; }
        [Required]
        [Phone]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number.")]
        [DisplayName("Phone Number")]
        public string Phone { get; set; }
        [Required]
        [RegularExpression(@"^(http:\/\/www\.|https:\/\/www\.|http:\/\/|https:\/\/)?[a-z0-9]+([\-\.]{1}[a-z0-9]+)*\.[a-z]{2,5}(:[0-9]{1,5})?(\/.*)?$",
         ErrorMessage = "Incorrect Website")]
        [DisplayName("Website")]
        public string Website { get; set; }
        [Required]
        [DisplayName("Years of Existance")]
        public int YearsOfExistance { get; set; }
        [Required]
        [DisplayName("Number of Employees")]
        public int NumberOfEmployees { get; set; }
        [Required]
        [DisplayName("Organization based in Canada?")]
        public string IsCanadianBased { get; set; }
        //[Required]
        [DisplayName("Organization based In Ontario")]
        public string IsOntarioBased { get; set; } = "";
        //[Required]
        [DisplayName("Where is the Organization based?")] 
        public string OrganizationLocation { get; set; } = "";
        [Required]
        [DisplayName("Has the Organization collaborated with IDEAWORKS before?")]
        public string PriorCollaborationWithIdeaworks { get; set; }
        [Required]
        [DisplayName("Does the Organization have Internal Research & Development department?")]
        public string InternalResearchExists { get; set; }
        [Required]
        [DisplayName("Does the Organization intend to hire Students or Recent Graduates?")]
        public string IntentionToHireStudents { get; set; }
        [Required]
        [DisplayName("Core Business")]
        public string CoreCompanyBusiness { get; set; }
        [Required]
        [DisplayName("Has the organization worked in the Research field before?")]
        public string PreviousCollaborationWithResearch { get; set; }

        public virtual Project Stage { get; set; }
    }
}
