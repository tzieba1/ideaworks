using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ideaworks.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "projects",
                columns: table => new
                {
                    project_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    project_status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    submission_date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    start_date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_projects", x => x.project_id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "company_information",
                columns: table => new
                {
                    stageId = table.Column<long>(type: "bigint", nullable: false),
                    contact_name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    company_name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    phone = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    website = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    years_of_existance = table.Column<int>(type: "int", nullable: false),
                    number_of_employees = table.Column<int>(type: "int", nullable: false),
                    is_canadian_based = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false),
                    is_ontario_based = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false),
                    organization_location = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    prior_collaboration_with_ideaworks = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false),
                    internal_research_exists = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false),
                    intention_to_hire_students = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false),
                    core_company_business = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    previous_collaboration_with_research = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_proposal_first_stage", x => x.stageId);
                    table.ForeignKey(
                        name: "FK_company_information_projects",
                        column: x => x.stageId,
                        principalTable: "projects",
                        principalColumn: "project_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "evaluations",
                columns: table => new
                {
                    project_id = table.Column<long>(type: "bigint", nullable: false),
                    discussed_with_partner = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    feedback = table.Column<string>(type: "text", nullable: true),
                    evaluation_stage = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_evaluations", x => x.project_id);
                    table.ForeignKey(
                        name: "FK_evaluations_projects",
                        column: x => x.project_id,
                        principalTable: "projects",
                        principalColumn: "project_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "project_information",
                columns: table => new
                {
                    stageId = table.Column<long>(type: "bigint", nullable: false),
                    challenges = table.Column<string>(type: "text", nullable: false),
                    project_impact = table.Column<string>(type: "text", nullable: false),
                    project_usage = table.Column<string>(type: "text", nullable: false),
                    project_outcome = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_proposal_second_stage", x => x.stageId);
                    table.ForeignKey(
                        name: "FK_project_information_projects",
                        column: x => x.stageId,
                        principalTable: "projects",
                        principalColumn: "project_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "proposal_information",
                columns: table => new
                {
                    stageId = table.Column<long>(type: "bigint", nullable: false),
                    urgency = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: false),
                    suggested_by = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    extra_information = table.Column<string>(type: "text", nullable: true),
                    parent_or_subsidiary = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    authorized_representative_name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    authorized_representative_position = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    authorized_representative_phone = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    authorized_representative_email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    number_of_employees_in_canada = table.Column<int>(type: "int", nullable: false),
                    number_of_research_staff_in_canada = table.Column<int>(type: "int", nullable: false),
                    area_of_research = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_proposal_stage3", x => x.stageId);
                    table.ForeignKey(
                        name: "FK_proposal_information_projects",
                        column: x => x.stageId,
                        principalTable: "projects",
                        principalColumn: "project_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "first_stage",
                columns: table => new
                {
                    project_id = table.Column<long>(type: "bigint", nullable: false),
                    technical_feasibility = table.Column<string>(type: "text", nullable: true),
                    timing_and_criticality = table.Column<string>(type: "text", nullable: true),
                    workable_by_faculty = table.Column<string>(type: "text", nullable: true),
                    project_paid_for = table.Column<string>(type: "text", nullable: true),
                    impact_on_community = table.Column<string>(type: "text", nullable: true),
                    project_worth_developing = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_first_stage", x => x.project_id);
                    table.ForeignKey(
                        name: "FK_first_stage_evaluations",
                        column: x => x.project_id,
                        principalTable: "evaluations",
                        principalColumn: "project_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "second_stage",
                columns: table => new
                {
                    project_id = table.Column<long>(type: "bigint", nullable: false),
                    mulitcentre_collaboration = table.Column<string>(type: "text", nullable: true),
                    interest_area_link = table.Column<string>(type: "text", nullable: true),
                    interested_faculty_participation = table.Column<string>(type: "text", nullable: true),
                    related_to_iot = table.Column<string>(type: "text", nullable: true),
                    more_infomation_required = table.Column<string>(type: "text", nullable: true),
                    lead_centres = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    general_scope = table.Column<string>(type: "text", nullable: true),
                    general_scope_confirmed = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    feasible_for_ideaworks = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_second_stage", x => x.project_id);
                    table.ForeignKey(
                        name: "FK_second_stage_evaluations",
                        column: x => x.project_id,
                        principalTable: "evaluations",
                        principalColumn: "project_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "third_stage",
                columns: table => new
                {
                    project_id = table.Column<long>(type: "bigint", nullable: false),
                    internal_approval = table.Column<string>(type: "text", nullable: true),
                    identified_project_problems = table.Column<string>(type: "text", nullable: true),
                    is_funding_approved = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    is_applicable = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false),
                    application_for_funding = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_third_stage", x => x.project_id);
                    table.ForeignKey(
                        name: "FK_third_stage_evaluations",
                        column: x => x.project_id,
                        principalTable: "evaluations",
                        principalColumn: "project_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "company_information");

            migrationBuilder.DropTable(
                name: "first_stage");

            migrationBuilder.DropTable(
                name: "project_information");

            migrationBuilder.DropTable(
                name: "proposal_information");

            migrationBuilder.DropTable(
                name: "second_stage");

            migrationBuilder.DropTable(
                name: "third_stage");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "evaluations");

            migrationBuilder.DropTable(
                name: "projects");
        }
    }
}
