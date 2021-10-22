﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ideaworks.Data;

namespace ideaworks.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("ideaworks.Models.CompanyInformation", b =>
                {
                    b.Property<long>("StageId")
                        .HasColumnType("bigint")
                        .HasColumnName("stageId");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("company_name");

                    b.Property<string>("ContactName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("contact_name");

                    b.Property<string>("CoreCompanyBusiness")
                        .IsRequired()
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("varchar(500)")
                        .HasColumnName("core_company_business");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("email");

                    b.Property<string>("IntentionToHireStudents")
                        .IsRequired()
                        .HasMaxLength(1)
                        .IsUnicode(false)
                        .HasColumnType("char(1)")
                        .HasColumnName("intention_to_hire_students")
                        .IsFixedLength(true);

                    b.Property<string>("InternalResearchExists")
                        .IsRequired()
                        .HasMaxLength(1)
                        .IsUnicode(false)
                        .HasColumnType("char(1)")
                        .HasColumnName("internal_research_exists")
                        .IsFixedLength(true);

                    b.Property<string>("IsCanadianBased")
                        .IsRequired()
                        .HasMaxLength(1)
                        .IsUnicode(false)
                        .HasColumnType("char(1)")
                        .HasColumnName("is_canadian_based")
                        .IsFixedLength(true);

                    b.Property<string>("IsOntarioBased")
                        .IsRequired()
                        .HasMaxLength(1)
                        .IsUnicode(false)
                        .HasColumnType("char(1)")
                        .HasColumnName("is_ontario_based")
                        .IsFixedLength(true);

                    b.Property<int>("NumberOfEmployees")
                        .HasColumnType("int")
                        .HasColumnName("number_of_employees");

                    b.Property<string>("OrganizationLocation")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("organization_location");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)")
                        .HasColumnName("phone");

                    b.Property<string>("PreviousCollaborationWithResearch")
                        .IsRequired()
                        .HasMaxLength(1)
                        .IsUnicode(false)
                        .HasColumnType("char(1)")
                        .HasColumnName("previous_collaboration_with_research")
                        .IsFixedLength(true);

                    b.Property<string>("PriorCollaborationWithIdeaworks")
                        .IsRequired()
                        .HasMaxLength(1)
                        .IsUnicode(false)
                        .HasColumnType("char(1)")
                        .HasColumnName("prior_collaboration_with_ideaworks")
                        .IsFixedLength(true);

                    b.Property<string>("Website")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("website");

                    b.Property<int>("YearsOfExistance")
                        .HasColumnType("int")
                        .HasColumnName("years_of_existance");

                    b.HasKey("StageId")
                        .HasName("PK_proposal_first_stage");

                    b.ToTable("company_information");
                });

            modelBuilder.Entity("ideaworks.Models.Evaluation", b =>
                {
                    b.Property<long>("ProjectId")
                        .HasColumnType("bigint")
                        .HasColumnName("project_id");

                    b.Property<string>("DiscussedWithPartner")
                        .HasMaxLength(1)
                        .IsUnicode(false)
                        .HasColumnType("char(1)")
                        .HasColumnName("discussed_with_partner")
                        .IsFixedLength(true);

                    b.Property<int?>("EvaluationStage")
                        .HasColumnType("int")
                        .HasColumnName("evaluation_stage");

                    b.Property<string>("Feedback")
                        .HasColumnType("text")
                        .HasColumnName("feedback");

                    b.HasKey("ProjectId");

                    b.ToTable("evaluations");
                });

            modelBuilder.Entity("ideaworks.Models.FirstStage", b =>
                {
                    b.Property<long>("ProjectId")
                        .HasColumnType("bigint")
                        .HasColumnName("project_id");

                    b.Property<string>("ImpactOnCommunity")
                        .HasColumnType("text")
                        .HasColumnName("impact_on_community");

                    b.Property<string>("ProjectPaidFor")
                        .HasColumnType("text")
                        .HasColumnName("project_paid_for");

                    b.Property<string>("ProjectWorthDeveloping")
                        .HasMaxLength(1)
                        .IsUnicode(false)
                        .HasColumnType("char(1)")
                        .HasColumnName("project_worth_developing")
                        .IsFixedLength(true);

                    b.Property<string>("TechnicalFeasibility")
                        .HasColumnType("text")
                        .HasColumnName("technical_feasibility");

                    b.Property<string>("TimingAndCriticality")
                        .HasColumnType("text")
                        .HasColumnName("timing_and_criticality");

                    b.Property<string>("WorkableByFaculty")
                        .HasColumnType("text")
                        .HasColumnName("workable_by_faculty");

                    b.HasKey("ProjectId");

                    b.ToTable("first_stage");
                });

            modelBuilder.Entity("ideaworks.Models.Project", b =>
                {
                    b.Property<long>("ProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("project_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ProjectStatus")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("project_status");

                    b.Property<DateTimeOffset?>("StartDate")
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("start_date");

                    b.Property<DateTimeOffset?>("SubmissionDate")
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("submission_date");

                    b.HasKey("ProjectId");

                    b.ToTable("projects");
                });

            modelBuilder.Entity("ideaworks.Models.ProjectInformation", b =>
                {
                    b.Property<long>("StageId")
                        .HasColumnType("bigint")
                        .HasColumnName("stageId");

                    b.Property<string>("Challenges")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("challenges");

                    b.Property<string>("ProjectImpact")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("project_impact");

                    b.Property<string>("ProjectOutcome")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("project_outcome");

                    b.Property<string>("ProjectUsage")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("project_usage");

                    b.HasKey("StageId")
                        .HasName("PK_proposal_second_stage");

                    b.ToTable("project_information");
                });

            modelBuilder.Entity("ideaworks.Models.ProposalInformation", b =>
                {
                    b.Property<long>("StageId")
                        .HasColumnType("bigint")
                        .HasColumnName("stageId");

                    b.Property<string>("AreaOfResearch")
                        .IsRequired()
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("varchar(500)")
                        .HasColumnName("area_of_research");

                    b.Property<string>("AuthorizedRepresentativeEmail")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("authorized_representative_email");

                    b.Property<string>("AuthorizedRepresentativeName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("authorized_representative_name");

                    b.Property<string>("AuthorizedRepresentativePhone")
                        .IsRequired()
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)")
                        .HasColumnName("authorized_representative_phone");

                    b.Property<string>("AuthorizedRepresentativePosition")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("authorized_representative_position");

                    b.Property<string>("ExtraInformation")
                        .HasColumnType("text")
                        .HasColumnName("extra_information");

                    b.Property<int?>("NumberOfEmployeesInCanada")
                        .IsRequired()
                        .HasColumnType("int")
                        .HasColumnName("number_of_employees_in_canada");

                    b.Property<int?>("NumberOfResearchStaffInCanada")
                        .IsRequired()
                        .HasColumnType("int")
                        .HasColumnName("number_of_research_staff_in_canada");

                    b.Property<string>("ParentOrSubsidiary")
                        .IsRequired()
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)")
                        .HasColumnName("parent_or_subsidiary");

                    b.Property<string>("SuggestedBy")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("suggested_by");

                    b.Property<string>("Urgency")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nchar(10)")
                        .HasColumnName("urgency")
                        .IsFixedLength(true);

                    b.HasKey("StageId")
                        .HasName("PK_proposal_stage3");

                    b.ToTable("proposal_information");
                });

            modelBuilder.Entity("ideaworks.Models.SecondStage", b =>
                {
                    b.Property<long>("ProjectId")
                        .HasColumnType("bigint")
                        .HasColumnName("project_id");

                    b.Property<string>("FeasibleForIdeaworks")
                        .HasMaxLength(1)
                        .IsUnicode(false)
                        .HasColumnType("char(1)")
                        .HasColumnName("feasible_for_ideaworks")
                        .IsFixedLength(true);

                    b.Property<string>("GeneralScope")
                        .HasColumnType("text")
                        .HasColumnName("general_scope");

                    b.Property<string>("GeneralScopeConfirmed")
                        .HasMaxLength(1)
                        .IsUnicode(false)
                        .HasColumnType("char(1)")
                        .HasColumnName("general_scope_confirmed")
                        .IsFixedLength(true);

                    b.Property<string>("InterestAreaLink")
                        .HasColumnType("text")
                        .HasColumnName("interest_area_link");

                    b.Property<string>("InterestedFacultyParticipation")
                        .HasColumnType("text")
                        .HasColumnName("interested_faculty_participation");

                    b.Property<string>("LeadCentres")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("lead_centres");

                    b.Property<string>("MoreInfomationRequired")
                        .HasColumnType("text")
                        .HasColumnName("more_infomation_required");

                    b.Property<string>("MulitcentreCollaboration")
                        .HasColumnType("text")
                        .HasColumnName("mulitcentre_collaboration");

                    b.Property<string>("RelatedToIot")
                        .HasColumnType("text")
                        .HasColumnName("related_to_iot");

                    b.HasKey("ProjectId");

                    b.ToTable("second_stage");
                });

            modelBuilder.Entity("ideaworks.Models.ThirdStage", b =>
                {
                    b.Property<long>("ProjectId")
                        .HasColumnType("bigint")
                        .HasColumnName("project_id");

                    b.Property<string>("ApplicationForFunding")
                        .HasColumnType("text")
                        .HasColumnName("application_for_funding");

                    b.Property<string>("IdentifiedProjectProblems")
                        .HasColumnType("text")
                        .HasColumnName("identified_project_problems");

                    b.Property<string>("InternalApproval")
                        .HasColumnType("text")
                        .HasColumnName("internal_approval");

                    b.Property<string>("IsApplicable")
                        .IsRequired()
                        .HasMaxLength(1)
                        .IsUnicode(false)
                        .HasColumnType("char(1)")
                        .HasColumnName("is_applicable")
                        .IsFixedLength(true);

                    b.Property<string>("IsFundingApproved")
                        .HasMaxLength(1)
                        .IsUnicode(false)
                        .HasColumnType("char(1)")
                        .HasColumnName("is_funding_approved")
                        .IsFixedLength(true);

                    b.Property<string>("Status")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("status");

                    b.HasKey("ProjectId");

                    b.ToTable("third_stage");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ideaworks.Models.CompanyInformation", b =>
                {
                    b.HasOne("ideaworks.Models.Project", "Stage")
                        .WithOne("CompanyInformation")
                        .HasForeignKey("ideaworks.Models.CompanyInformation", "StageId")
                        .HasConstraintName("FK_company_information_projects")
                        .IsRequired();

                    b.Navigation("Stage");
                });

            modelBuilder.Entity("ideaworks.Models.Evaluation", b =>
                {
                    b.HasOne("ideaworks.Models.Project", "Project")
                        .WithOne("Evaluation")
                        .HasForeignKey("ideaworks.Models.Evaluation", "ProjectId")
                        .HasConstraintName("FK_evaluations_projects")
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("ideaworks.Models.FirstStage", b =>
                {
                    b.HasOne("ideaworks.Models.Evaluation", "Project")
                        .WithOne("FirstStage")
                        .HasForeignKey("ideaworks.Models.FirstStage", "ProjectId")
                        .HasConstraintName("FK_first_stage_evaluations")
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("ideaworks.Models.ProjectInformation", b =>
                {
                    b.HasOne("ideaworks.Models.Project", "Stage")
                        .WithOne("ProjectInformation")
                        .HasForeignKey("ideaworks.Models.ProjectInformation", "StageId")
                        .HasConstraintName("FK_project_information_projects")
                        .IsRequired();

                    b.Navigation("Stage");
                });

            modelBuilder.Entity("ideaworks.Models.ProposalInformation", b =>
                {
                    b.HasOne("ideaworks.Models.Project", "Stage")
                        .WithOne("ProposalInformation")
                        .HasForeignKey("ideaworks.Models.ProposalInformation", "StageId")
                        .HasConstraintName("FK_proposal_information_projects")
                        .IsRequired();

                    b.Navigation("Stage");
                });

            modelBuilder.Entity("ideaworks.Models.SecondStage", b =>
                {
                    b.HasOne("ideaworks.Models.Evaluation", "Project")
                        .WithOne("SecondStage")
                        .HasForeignKey("ideaworks.Models.SecondStage", "ProjectId")
                        .HasConstraintName("FK_second_stage_evaluations")
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("ideaworks.Models.ThirdStage", b =>
                {
                    b.HasOne("ideaworks.Models.Evaluation", "Project")
                        .WithOne("ThirdStage")
                        .HasForeignKey("ideaworks.Models.ThirdStage", "ProjectId")
                        .HasConstraintName("FK_third_stage_evaluations")
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("ideaworks.Models.Evaluation", b =>
                {
                    b.Navigation("FirstStage");

                    b.Navigation("SecondStage");

                    b.Navigation("ThirdStage");
                });

            modelBuilder.Entity("ideaworks.Models.Project", b =>
                {
                    b.Navigation("CompanyInformation");

                    b.Navigation("Evaluation");

                    b.Navigation("ProjectInformation");

                    b.Navigation("ProposalInformation");
                });
#pragma warning restore 612, 618
        }
    }
}
