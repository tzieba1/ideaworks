using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ideaworks.Data;
using ideaworks.Models;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace ideaworks.Controllers
{
    public class ProjectInformationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectInformationsController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult ThankYou()
        {
            HttpContext.Session.SetString("proposalInformation", "");
            HttpContext.Session.SetString("companyInformation", "");
            HttpContext.Session.SetString("projectInformation", "");
            return View();
        }


        // GET: ProjectInformations/Create
        public IActionResult Create()
        {
            string projectInformationString = HttpContext.Session.GetString("projectInformation");
            string comingFrom = HttpContext.Session.GetString("comingFrom");
            if (!String.IsNullOrEmpty(comingFrom))
            {
                //if the user hasn't been through the second form page
                //redirect to the second form page
                if (!(int.Parse(comingFrom) >= 2))
                    return RedirectToAction("Create", "ProposalInformations");
            }
            else
            {
                return RedirectToAction("Create", "ProposalInformations");
            }

            if (String.IsNullOrEmpty(projectInformationString))
            {
                ViewData["StageId"] = new SelectList(_context.Projects, "ProjectId", "ProjectId");
                return View();

            }
            else
            {
                ProjectInformation projectInformationExists = (ProjectInformation)JsonSerializer.Deserialize(projectInformationString, typeof(ProjectInformation));
                //setting the string to empty to be able to validate any changes made
                HttpContext.Session.SetString("projectInformation", "");
                return View(projectInformationExists);
            }
        }

        // POST: ProjectInformations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StageId,Challenges,ProjectImpact,ProjectUsage,ProjectOutcome")] ProjectInformation projectInformation)
        {

            //getting the data when the back button has been pressed from the next proposal page
            string projectInformationString = HttpContext.Session.GetString("projectInformation");
            //check for the session variable
            if (!String.IsNullOrEmpty(projectInformationString))
            {
                //get the information and fill the page
                ProjectInformation projectInformationExists = (ProjectInformation)JsonSerializer.Deserialize(projectInformationString, typeof(ProjectInformation));

                //setting the string to empty to be able to validate any changes made
                HttpContext.Session.SetString("projectInformation", "");
                //clear all the errors for the already existing validated object
                ModelState.Clear();

                return View(projectInformationExists);
            }

            else {
                if (ModelState.IsValid)
                {
                    HttpContext.Session.SetString("projectInformation", JsonSerializer.Serialize(projectInformation));
                    string companyInformationString = HttpContext.Session.GetString("companyInformation");
                    CompanyInformation companyInformation = (CompanyInformation)JsonSerializer.Deserialize(companyInformationString, typeof(CompanyInformation));

                    string proposalInformationString = HttpContext.Session.GetString("proposalInformation");
                    ProposalInformation proposalInformation = (ProposalInformation)JsonSerializer.Deserialize(proposalInformationString, typeof(ProposalInformation));

                    Project project = new Project()
                    {
                        SubmissionDate = DateTimeOffset.Now,
                        ProjectStatus = "PENDING"
                    };
                    //creating a new Evaluation object associated with the project
                    Evaluation evaluation = new Evaluation();

                    //need to await the task and save the changes to the db
                    //cannot add the proposal without the project due to Foreign Key restrictions
                    await _context.AddAsync(project);
                    _context.SaveChanges();

                    //Get the last id to add to the proposals
                    long max = _context.Projects.DefaultIfEmpty().Max(p => p == null ? 0 : p.ProjectId);
                    companyInformation.StageId = max;
                    proposalInformation.StageId = max;
                    projectInformation.StageId = max;
                    evaluation.ProjectId = max;
                    //setting the evaluation to the first stage
                    evaluation.EvaluationStage = 0;

                    _context.Add(companyInformation);
                    _context.Add(proposalInformation);
                    _context.Add(projectInformation);
                    _context.Add(evaluation);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("ThankYou", "ProjectInformations");
                }
                ViewData["StageId"] = new SelectList(_context.Projects, "ProjectId", "ProjectId", projectInformation.StageId);
                return View(projectInformation);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PreviousButtonClicked([Bind("StageId,Challenges,ProjectImpact,ProjectUsage,ProjectOutcome")] ProjectInformation projectInformation)
        {

            ModelState.Clear();
            HttpContext.Session.SetString("projectInformation", JsonSerializer.Serialize(projectInformation));
            return RedirectToAction("Create", "ProposalInformations");
        }
    }
}
