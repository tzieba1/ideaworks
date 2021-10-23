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
    public class ProjectsController : Controller
    {
        //statuses of the projects
        private static string[] ProjectStatus = { "All", "PENDING", "ONGOING", "REJECTED", "APPROVED"};
        private readonly ApplicationDbContext _context;

        public ProjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Projects
        public async Task<IActionResult> Index(string ProjectStatusFilter, int? pageNumber)
        {
            var projectList = from p in _context.Projects select p;
            Task t;

            //using linq to generate the list from the context
            switch (ProjectStatusFilter)
            {
                case "ONGOING":
                    projectList = projectList.Where(p => p.ProjectStatus.Equals("ONGOING")).Include(c => c.CompanyInformation).AsNoTracking().Include(c => c.ProposalInformation);
                    ViewBag.CurrentFilter = "ONGOING";
                    break;
                case "PENDING":
                    projectList = projectList.Where(p => p.ProjectStatus.Equals("PENDING")).Include(c => c.CompanyInformation).AsNoTracking().Include(c => c.ProposalInformation);
                    ViewBag.CurrentFilter = "PENDING";
                    break;
                case "REJECTED":
                    projectList = projectList.Where(p => p.ProjectStatus.Equals("REJECTED")).Include(c => c.CompanyInformation).AsNoTracking().Include(c => c.ProposalInformation);
                    ViewBag.CurrentFilter = "REJECTED";
                    break;
                case "APPROVED":
                    projectList = projectList.Where(p => p.ProjectStatus.Equals("APPROVED")).Include(c => c.CompanyInformation).AsNoTracking().Include(c => c.ProposalInformation);
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

            var project = projectList.Where(m => m.ProjectId == id).FirstOrDefault();
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        /// <summary>
        /// Update the status for a rejected proposal to pending for re-assessment
        /// </summary>
        /// 
        [Authorize]
        public IActionResult ResetStatusToPending(long id)
        {
            //fetch the project with the id 
            Project p =  _context.Projects.Where(i=>i.ProjectId==id).Include(c => c.CompanyInformation).AsNoTracking().Include(c => c.ProposalInformation).Include(c => c.ProjectInformation).Include(c=>c.Evaluation).AsNoTracking().FirstOrDefault();

            //reset the evaluation stage to 0
            p.Evaluation.EvaluationStage = 0;
            // reset the status to pending
            p.ProjectStatus = "PENDING";
            _context.Update(p);
            _context.SaveChanges();
            return  RedirectToAction(nameof(Index));

        }


        private bool ProjectExists(long id)
        {
            return _context.Projects.Any(e => e.ProjectId == id);
        }
    }
}
