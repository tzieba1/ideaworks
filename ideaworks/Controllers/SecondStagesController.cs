using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ideaworks.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace ideaworks.Models
{
    [Authorize]
    public class SecondStagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SecondStagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SecondStages/Create
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


                //testing
                //needed to be set in the details page
                //i/*d = 3000;*/
                SecondStage ss = _context.SecondStages.Find(id);
                
                //string projectWorthDeveloping = fs.ProjectWorthDeveloping;
                if (!(ss==null))
                {
                    //project has already been evaluated 
                    return View(ss);

                }
                ViewData["ProjectId"] = new SelectList(_context.Evaluations, "ProjectId", "ProjectId");
                return View();
            }
            catch (Exception ex)
            {

                ViewData["ProjectId"] = new SelectList(_context.Evaluations, "ProjectId", "ProjectId");
                return View();
            }
            //ViewData["ProjectId"] = new SelectList(_context.Evaluations, "ProjectId", "ProjectId");
            //return View();
        }

        // POST: SecondStages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MulitcentreCollaboration,InterestAreaLink,InterestedFacultyParticipation,RelatedToIot,MoreInfomationRequired,LeadCentres,GeneralScope,GeneralScopeConfirmed,FeasibleForIdeaworks")] SecondStage secondStage)
        {
            if (ModelState.IsValid)
            {
                string projectIdString = HttpContext.Session.GetString("projectId");

                if (string.IsNullOrEmpty(projectIdString))
                {
                    return RedirectToAction("Index", "projects");
                }

                long id = long.Parse(projectIdString);
                secondStage.ProjectId = id;
                Evaluation evulation = _context.Evaluations.Where(i => i.ProjectId == secondStage.ProjectId).FirstOrDefault();
                if (secondStage is not null && secondStage.FeasibleForIdeaworks.Contains("n", StringComparison.OrdinalIgnoreCase))
                {
                    evulation.EvaluationStage = -2;
                }
                if (secondStage is not null && secondStage.FeasibleForIdeaworks.Contains("y", StringComparison.OrdinalIgnoreCase))
                {
                    evulation.EvaluationStage = 2;
                }
                try
                {
                    _context.Update(secondStage);
                    _context.Update(evulation);
                    await _context.SaveChangesAsync();
                    return Redirect($"/projects/details/{secondStage.ProjectId}");
                }
                catch (Exception ex)
                {
                    //first stage evaluation record doesnt exist for the project yet
                    //Need to set the evalutionStage for the evaluation
                    _context.Add(secondStage);
                    evulation = _context.Evaluations.Where(i => i.ProjectId == secondStage.ProjectId).FirstOrDefault();
                    evulation.EvaluationStage = (secondStage is null ? evulation.EvaluationStage : (secondStage.FeasibleForIdeaworks.Contains("y", StringComparison.OrdinalIgnoreCase) ? 2 : -2));
                    _context.Update(evulation);
                    await _context.SaveChangesAsync();
                    return Redirect($"/projects/details/{secondStage.ProjectId}");
                }


            }
            //display the same page if invalid model
            ViewData["ProjectId"] = new SelectList(_context.Evaluations, "ProjectId", "ProjectId", secondStage.ProjectId);
            return View(secondStage);
        }
    }
}
