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

namespace ideaworks.Controllers
{
    public class ThirdStagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ThirdStagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ThirdStages/Create
        public IActionResult Create()
        {
            try
            {
                //getting the project id from the session varibale
                string projectIdString = HttpContext.Session.GetString("projectId");
                //if projectIdString is null - Request is not coming from the project details page
                if (string.IsNullOrEmpty(projectIdString))
                {
                    return RedirectToAction("Index", "projects");
                }
                //checking id the details have already been filled by the user
                //Project worth developing is a deciding factor for the approval or rejection of the project
                long id = long.Parse(projectIdString);

                ThirdStage ts = _context.ThirdStages.Find(id);
               
                if (!(ts==null))
                {
                    //project has already been evaluated 
                    return View(ts);
                }
                ViewData["ProjectId"] = new SelectList(_context.Evaluations, "ProjectId", "ProjectId");
                return View();
            }
            catch (Exception ex)
            {
                ViewData["ProjectId"] = new SelectList(_context.Evaluations, "ProjectId", "ProjectId");
                return View();
            }
        }

        // POST: ThirdStages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InternalApproval,IdentifiedProjectProblems,IsFundingApproved,IsApplicable,ApplicationForFunding,Status")] ThirdStage thirdStage)
        {
            if (ModelState.IsValid)
            {
                string projectIdString = HttpContext.Session.GetString("projectId");

                if (string.IsNullOrEmpty(projectIdString))
                {
                    return RedirectToAction("Index", "projects");
                }


                long id = long.Parse(projectIdString);
                thirdStage.ProjectId = id;
                Evaluation evulation = _context.Evaluations.Where(i => i.ProjectId == thirdStage.ProjectId).FirstOrDefault();
                if (thirdStage is not null && thirdStage.IsFundingApproved is not null && thirdStage.IsFundingApproved.Contains("n", StringComparison.OrdinalIgnoreCase))
                {
                    evulation.EvaluationStage = -3;
                }
                if (thirdStage is not null && thirdStage.IsFundingApproved is not null && thirdStage.IsFundingApproved.Contains("y", StringComparison.OrdinalIgnoreCase))
                {
                    evulation.EvaluationStage = 3;
                }

                evulation.EvaluationStage = (thirdStage is null ? evulation.EvaluationStage : (thirdStage.IsApplicable.Contains("y", StringComparison.OrdinalIgnoreCase) ? evulation.EvaluationStage : 3));
                try
                {
                    _context.Update(thirdStage);
                    _context.Update(evulation);
                    await _context.SaveChangesAsync();
                    return Redirect($"/projects/details/{thirdStage.ProjectId}");
                }
                catch (Exception ex)
                {
                    //first stage evaluation record doesnt exist for the project yet
                    //Need to set the evalutionStage for the evaluation
                    _context.Add(thirdStage);
                    evulation.EvaluationStage = (thirdStage is null ? evulation.EvaluationStage : (thirdStage.IsFundingApproved is not null && thirdStage.IsFundingApproved.Contains("y", StringComparison.OrdinalIgnoreCase) ? 3 : -3));
                    evulation.EvaluationStage = (thirdStage is null ? evulation.EvaluationStage : (thirdStage.IsApplicable.Contains("y", StringComparison.OrdinalIgnoreCase) ? evulation.EvaluationStage : 3));
                    _context.Update(evulation);
                    await _context.SaveChangesAsync();
                    return Redirect($"/projects/details/{thirdStage.ProjectId}");
                }


            }
            //display the same page if invalid model
            ViewData["ProjectId"] = new SelectList(_context.Evaluations, "ProjectId", "ProjectId", thirdStage.ProjectId);
            return View(thirdStage);
        }
    }
}
