using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ideaworks.Data;
using ideaworks.Models;
using Microsoft.AspNetCore.Http;
using System.Dynamic;
using Microsoft.AspNetCore.Authorization;

namespace ideaworks.Controllers
{
    public class SearchController : Controller
    {
        //statuses of the projects
        private static string[] ProjectStatus = { "All", "PENDING", "ONGOING", "REJECTED", "APPROVED" };
        private readonly ApplicationDbContext _context;

        public SearchController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Projects
        public async Task<IActionResult> Index(string ProjectStatusFilter,
                                                int? pageNumber, 
                                                long? searchId,
                                                string? searchCompanyName,
                                                string? searchPartnerName, 
                                                string? searchPartnerPhone, 
                                                string? searchPartnerEmail, 
                                                string? searchKeywords)
        {

            var projectList = from p in _context.Projects select p;

            //using linq to generate the list from the context
            switch (ProjectStatusFilter)
            {
                case "ONGOING":
                    projectList = projectList.Where(p => p.ProjectStatus.Equals("ONGOING")).Include(c => c.CompanyInformation).Include(c => c.ProposalInformation);
                    ViewBag.CurrentFilter = "ONGOING";
                    break;
                case "PENDING":
                    projectList = projectList.Where(p => p.ProjectStatus.Equals("PENDING")).Include(c => c.CompanyInformation).Include(c => c.ProposalInformation);
                    ViewBag.CurrentFilter = "PENDING";
                    break;
                case "REJECTED":
                    projectList = projectList.Where(p => p.ProjectStatus.Equals("REJECTED")).Include(c => c.CompanyInformation).Include(c => c.ProposalInformation);
                    ViewBag.CurrentFilter = "REJECTED";
                    break;
                case "APPROVED":
                    projectList = projectList.Where(p => p.ProjectStatus.Equals("APPROVED")).Include(c => c.CompanyInformation).Include(c => c.ProposalInformation);
                    ViewBag.CurrentFilter = "APPROVED";
                    break;
                default:
                    //NULL OR EMPTY
                    projectList = projectList.Include(c => c.CompanyInformation).Include(c => c.ProposalInformation);
                    ViewBag.CurrentFilter = "All";
                    break;

            }

            //addding the project statuses for filter
            ViewBag.StatusFilters = ProjectStatus;

            if (searchId != null)
            {
                projectList = projectList.Include(c => c.CompanyInformation).Include(c => c.ProposalInformation).Where(s => s.ProjectId == searchId);
                ViewBag.searchId = searchId;
            }

            if (!String.IsNullOrEmpty(searchCompanyName))
            {
                projectList = projectList.Include(c => c.CompanyInformation).Include(c => c.ProposalInformation).Where(s => s.CompanyInformation.CompanyName.Contains(searchCompanyName));
                ViewBag.searchCompanyName = searchCompanyName;
            }

            if (!String.IsNullOrEmpty(searchPartnerName))
            {
                projectList = projectList.Include(c => c.CompanyInformation).Include(c => c.ProposalInformation).Where(s => s.CompanyInformation.ContactName.Contains(searchPartnerName));
                ViewBag.searchPartnerName = searchPartnerName;
            }

            if (!String.IsNullOrEmpty(searchPartnerPhone))
            {
                projectList = projectList.Include(c => c.CompanyInformation).Include(c => c.ProposalInformation).Where(s => s.CompanyInformation.Phone.Contains(searchPartnerPhone));
                ViewBag.searchPartnerPhone = searchPartnerPhone;
            }

            if (!String.IsNullOrEmpty(searchPartnerEmail))
            {
                projectList = projectList.Include(c => c.CompanyInformation).Include(c => c.ProposalInformation).Where(s => s.CompanyInformation.Email.Contains(searchPartnerEmail));
                ViewBag.searchPartnerEmail = searchPartnerEmail;
            }

            if (!String.IsNullOrEmpty(searchKeywords))
            {
                projectList = projectList.Include(c => c.CompanyInformation).Include(c => c.ProposalInformation)
                                         .Where(s => s.CompanyInformation.ContactName.Contains(searchKeywords) ||
                                                s.CompanyInformation.CompanyName.Contains(searchKeywords) ||
                                                s.CompanyInformation.Email.Contains(searchKeywords) ||
                                                s.CompanyInformation.Phone.Contains(searchKeywords) ||
                                                s.CompanyInformation.Website.Contains(searchKeywords) ||
                                                s.CompanyInformation.OrganizationLocation.Contains(searchKeywords) ||
                                                s.CompanyInformation.CoreCompanyBusiness.Contains(searchKeywords) ||
                                                s.ProjectInformation.Challenges.Contains(searchKeywords) ||
                                                s.ProjectInformation.ProjectImpact.Contains(searchKeywords) ||
                                                s.ProjectInformation.ProjectUsage.Contains(searchKeywords) ||
                                                s.ProjectInformation.ProjectOutcome.Contains(searchKeywords) ||
                                                s.ProposalInformation.ExtraInformation.Contains(searchKeywords) ||
                                                s.ProposalInformation.AuthorizedRepresentativeName.Contains(searchKeywords) ||
                                                s.ProposalInformation.AuthorizedRepresentativePhone.Contains(searchKeywords) ||
                                                s.ProposalInformation.AuthorizedRepresentativeEmail.Contains(searchKeywords) ||
                                                s.ProposalInformation.AreaOfResearch.Contains(searchKeywords));

                ViewBag.searchKeywords = searchKeywords;
            }


            // Number of projects to display on a signle page
            int pageSize = 5;

            return View(await PaginatedList<Project>.CreateAsync(projectList.AsNoTracking().AsQueryable(), pageNumber ?? 1, pageSize));
        }

        // GET: Projects/Details/5
        [Authorize]
        public async Task<IActionResult> Details(long? id)
        {

            IEnumerable<Project> projectList = await _context.Projects.Include(c => c.CompanyInformation).AsNoTracking().Include(c => c.ProjectInformation).AsNoTracking().Include(c => c.ProposalInformation).AsNoTracking().ToListAsync();
            //setting the session string value for the project id
            HttpContext.Session.SetString("projectId", id.ToString());
            if (id == null)
            {
                return NotFound();
            }
            return RedirectToAction("Details", "Projects", new { id = id });
        }
    }
}
