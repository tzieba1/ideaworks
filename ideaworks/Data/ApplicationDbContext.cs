using ideaworks.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ideaworks.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<CompanyInformation> CompanyInformations { get; set; }
        public DbSet<Evaluation> Evaluations { get; set; }
        public DbSet<FirstStage> FirstStages { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectInformation> ProjectInformations { get; set; }
        public DbSet<ProposalInformation> ProposalInformations { get; set; }
        public DbSet<SecondStage> SecondStages { get; set; }
        public DbSet<ThirdStage> ThirdStages { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CompanyInformation>(entity =>
            {
                entity.HasKey(e => e.StageId)
                    .HasName("PK_proposal_first_stage");

                entity.ToTable("company_information");

                entity.Property(e => e.StageId)
                    .ValueGeneratedNever()
                    .HasColumnName("stageId");

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("company_name");

                entity.Property(e => e.ContactName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("contact_name");

                entity.Property(e => e.CoreCompanyBusiness)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("core_company_business");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.IntentionToHireStudents)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("intention_to_hire_students")
                    .IsFixedLength(true);

                entity.Property(e => e.InternalResearchExists)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("internal_research_exists")
                    .IsFixedLength(true);

                entity.Property(e => e.IsCanadianBased)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("is_canadian_based")
                    .IsFixedLength(true);

                entity.Property(e => e.IsOntarioBased)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("is_ontario_based")
                    .IsFixedLength(true);

                entity.Property(e => e.NumberOfEmployees).HasColumnName("number_of_employees");

                entity.Property(e => e.OrganizationLocation)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("organization_location");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.PreviousCollaborationWithResearch)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("previous_collaboration_with_research")
                    .IsFixedLength(true);

                entity.Property(e => e.PriorCollaborationWithIdeaworks)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("prior_collaboration_with_ideaworks")
                    .IsFixedLength(true);

                entity.Property(e => e.Website)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("website");

                entity.Property(e => e.YearsOfExistance).HasColumnName("years_of_existance");

                entity.HasOne(d => d.Stage)
                    .WithOne(p => p.CompanyInformation)
                    .HasForeignKey<CompanyInformation>(d => d.StageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_company_information_projects");
            });

            modelBuilder.Entity<Evaluation>(entity =>
            {
                entity.HasKey(e => e.ProjectId);

                entity.ToTable("evaluations");

                entity.Property(e => e.ProjectId)
                    .ValueGeneratedNever()
                    .HasColumnName("project_id");

                entity.Property(e => e.DiscussedWithPartner)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("discussed_with_partner")
                    .IsFixedLength(true);

                entity.Property(e => e.EvaluationStage).HasColumnName("evaluation_stage");

                entity.Property(e => e.Feedback)
                    .HasColumnType("text")
                    .HasColumnName("feedback");

                entity.HasOne(d => d.Project)
                    .WithOne(p => p.Evaluation)
                    .HasForeignKey<Evaluation>(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_evaluations_projects");
            });

            modelBuilder.Entity<FirstStage>(entity =>
            {
                entity.HasKey(e => e.ProjectId);

                entity.ToTable("first_stage");

                entity.Property(e => e.ProjectId)
                    .ValueGeneratedNever()
                    .HasColumnName("project_id");

                entity.Property(e => e.ImpactOnCommunity)
                    .HasColumnType("text")
                    .HasColumnName("impact_on_community");

                entity.Property(e => e.ProjectPaidFor)
                    .HasColumnType("text")
                    .HasColumnName("project_paid_for");

                entity.Property(e => e.ProjectWorthDeveloping)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("project_worth_developing")
                    .IsFixedLength(true);

                entity.Property(e => e.TechnicalFeasibility)
                    .HasColumnType("text")
                    .HasColumnName("technical_feasibility");

                entity.Property(e => e.TimingAndCriticality)
                    .HasColumnType("text")
                    .HasColumnName("timing_and_criticality");

                entity.Property(e => e.WorkableByFaculty)
                    .HasColumnType("text")
                    .HasColumnName("workable_by_faculty");

                entity.HasOne(d => d.Project)
                    .WithOne(p => p.FirstStage)
                    .HasForeignKey<FirstStage>(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_first_stage_evaluations");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("projects");

                entity.Property(e => e.ProjectId).HasColumnName("project_id");

                entity.Property(e => e.ProjectStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("project_status");

                entity.Property(e => e.StartDate).HasColumnName("start_date");

                entity.Property(e => e.SubmissionDate).HasColumnName("submission_date");
            });

            modelBuilder.Entity<ProjectInformation>(entity =>
            {
                entity.HasKey(e => e.StageId)
                    .HasName("PK_proposal_second_stage");

                entity.ToTable("project_information");

                entity.Property(e => e.StageId)
                    .ValueGeneratedNever()
                    .HasColumnName("stageId");

                entity.Property(e => e.Challenges)
                    .HasColumnType("text")
                    .HasColumnName("challenges");

                entity.Property(e => e.ProjectImpact)
                    .HasColumnType("text")
                    .HasColumnName("project_impact");

                entity.Property(e => e.ProjectOutcome)
                    .HasColumnType("text")
                    .HasColumnName("project_outcome");

                entity.Property(e => e.ProjectUsage)
                    .HasColumnType("text")
                    .HasColumnName("project_usage");

                entity.HasOne(d => d.Stage)
                    .WithOne(p => p.ProjectInformation)
                    .HasForeignKey<ProjectInformation>(d => d.StageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_project_information_projects");
            });

            modelBuilder.Entity<ProposalInformation>(entity =>
            {
                entity.HasKey(e => e.StageId)
                    .HasName("PK_proposal_stage3");

                entity.ToTable("proposal_information");

                entity.Property(e => e.StageId)
                    .ValueGeneratedNever()
                    .HasColumnName("stageId");

                entity.Property(e => e.AreaOfResearch)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("area_of_research");

                entity.Property(e => e.AuthorizedRepresentativeEmail)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("authorized_representative_email");

                entity.Property(e => e.AuthorizedRepresentativeName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("authorized_representative_name");

                entity.Property(e => e.AuthorizedRepresentativePhone)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("authorized_representative_phone");

                entity.Property(e => e.AuthorizedRepresentativePosition)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("authorized_representative_position");

                entity.Property(e => e.ExtraInformation)
                    .HasColumnType("text")
                    .HasColumnName("extra_information");

                entity.Property(e => e.NumberOfEmployeesInCanada).HasColumnName("number_of_employees_in_canada");

                entity.Property(e => e.NumberOfResearchStaffInCanada).HasColumnName("number_of_research_staff_in_canada");

                entity.Property(e => e.ParentOrSubsidiary)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("parent_or_subsidiary");

                entity.Property(e => e.SuggestedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("suggested_by");

                entity.Property(e => e.Urgency)
                    .HasMaxLength(10)
                    .HasColumnName("urgency")
                    .IsFixedLength(true);

                entity.HasOne(d => d.Stage)
                    .WithOne(p => p.ProposalInformation)
                    .HasForeignKey<ProposalInformation>(d => d.StageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_proposal_information_projects");
            });

            modelBuilder.Entity<SecondStage>(entity =>
            {
                entity.HasKey(e => e.ProjectId);

                entity.ToTable("second_stage");

                entity.Property(e => e.ProjectId)
                    .ValueGeneratedNever()
                    .HasColumnName("project_id");

                entity.Property(e => e.FeasibleForIdeaworks)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("feasible_for_ideaworks")
                    .IsFixedLength(true);

                entity.Property(e => e.GeneralScope)
                    .HasColumnType("text")
                    .HasColumnName("general_scope");

                entity.Property(e => e.GeneralScopeConfirmed)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("general_scope_confirmed")
                    .IsFixedLength(true);

                entity.Property(e => e.InterestAreaLink)
                    .HasColumnType("text")
                    .HasColumnName("interest_area_link");

                entity.Property(e => e.InterestedFacultyParticipation)
                    .HasColumnType("text")
                    .HasColumnName("interested_faculty_participation");

                entity.Property(e => e.LeadCentres)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("lead_centres");

                entity.Property(e => e.MoreInfomationRequired)
                    .HasColumnType("text")
                    .HasColumnName("more_infomation_required");

                entity.Property(e => e.MulitcentreCollaboration)
                    .HasColumnType("text")
                    .HasColumnName("mulitcentre_collaboration");

                entity.Property(e => e.RelatedToIot)
                    .HasColumnType("text")
                    .HasColumnName("related_to_iot");

                entity.HasOne(d => d.Project)
                    .WithOne(p => p.SecondStage)
                    .HasForeignKey<SecondStage>(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_second_stage_evaluations");
            });

            modelBuilder.Entity<ThirdStage>(entity =>
            {
                entity.HasKey(e => e.ProjectId);

                entity.ToTable("third_stage");

                entity.Property(e => e.ProjectId)
                    .ValueGeneratedNever()
                    .HasColumnName("project_id");

                entity.Property(e => e.ApplicationForFunding)
                    .HasColumnType("text")
                    .HasColumnName("application_for_funding");

                entity.Property(e => e.IdentifiedProjectProblems)
                    .HasColumnType("text")
                    .HasColumnName("identified_project_problems");

                entity.Property(e => e.InternalApproval)
                    .HasColumnType("text")
                    .HasColumnName("internal_approval");

                entity.Property(e => e.IsApplicable)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("is_applicable")
                    .IsFixedLength(true);

                entity.Property(e => e.IsFundingApproved)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("is_funding_approved")
                    .IsFixedLength(true);

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.HasOne(d => d.Project)
                    .WithOne(p => p.ThirdStage)
                    .HasForeignKey<ThirdStage>(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_third_stage_evaluations");
            });

        }

    }
}
