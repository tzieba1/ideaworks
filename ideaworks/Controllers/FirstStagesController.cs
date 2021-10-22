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
    /// <summary>
    /// First Stage of the evaluation process
    /// Roshan Shah
    /// </summary>
    public class FirstStagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FirstStagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// This is the first stage for evaluating the project
        /// the project id is passed using the session variable
        /// </summary>
        /// <returns></returns>
        // GET: FirstStages/Create
        public IActionResult Create()
        {
            try
            {
                
                //getting the project id from the session varibale
               string projectIdString = HttpContext.Session.GetString("projectId");
                //if projectIdString is null - Request is not coming from the project details page
                if (string.IsNullOrEmpty(projectIdString))
                {
                    return RedirectToAction("Index","projects");
                }
                //checking id the details have already been filled by the user
                //Project worth developing is a deciding factor for the approval or rejection of the project
                long id = long.Parse(projectIdString);
               
                
                //testing
                //needed to be set in the details page
                //i/*d = 3000;*/
                FirstStage fs = _context.FirstStages.Find(id);
                fs.ProjectId = id;
                string projectWorthDeveloping = fs.ProjectWorthDeveloping;
                if (!string.IsNullOrEmpty(projectWorthDeveloping))
                {
                    //project has already been evaluated 
                    return View(fs);

                }
                ViewData["ProjectId"] = new SelectList(_context.Evaluations, "ProjectId", "ProjectId");
                return View();
            }
            catch (Exception ex) {

                ViewData["ProjectId"] = new SelectList(_context.Evaluations, "ProjectId", "ProjectId");
                return View();
            }
            
        }

        /// <summary>
        /// Loads the previous values for each field if the record already exists otherwise creates a new one
        /// Updates the evaluationStage in the evaluation table for the associated record in case of the latter.
        /// </summary>
        /// <param name="firstStage">is the varibale for model binding</param>
        /// <returns>IActionResult</returns>
        // POST: FirstStages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TechnicalFeasibility,TimingAndCriticality,WorkableByFaculty,ProjectPaidFor,ImpactOnCommunity,ProjectWorthDeveloping")] FirstStage firstStage)
        {
            if (ModelState.IsValid)
            {
                string projectIdString = HttpContext.Session.GetString("projectId");

                if (string.IsNullOrEmpty(projectIdString))
                {
                    return RedirectToAction("Index", "projects");
                }

                long id = long.Parse(projectIdString);
                firstStage.ProjectId = id;
                Evaluation evulation = _context.Evaluations.Where(i => i.ProjectId == firstStage.ProjectId).FirstOrDefault();
                //need to explicitly add this condition since if record already exists, it will throw an error
                //we dont want to change the evaluationStage on updates since the user can update the values for an earlier stage
                if (firstStage is not null && firstStage.ProjectWorthDeveloping.Contains("n", StringComparison.OrdinalIgnoreCase))
                {
                    evulation.EvaluationStage = -1;
                }
                if (firstStage is not null && firstStage.ProjectWorthDeveloping.Contains("y", StringComparison.OrdinalIgnoreCase))
                {
                    evulation.EvaluationStage = 1;
                }
                try
                {
                    _context.Update(firstStage);
                    _context.Update(evulation);
                    await _context.SaveChangesAsync();
                    return Redirect($"/projects/details/{firstStage.ProjectId}");
                }
                catch(Exception ex)
                {
                    //first stage evaluation record doesnt exist for the project yet
                    //Need to set the evalutionStage for the evaluation
                    _context.Add(firstStage);
                    evulation.EvaluationStage = (firstStage is null ? evulation.EvaluationStage : (firstStage.ProjectWorthDeveloping.Contains("y", StringComparison.OrdinalIgnoreCase) ? 1 : -1));
                    _context.Update(evulation);
                    await _context.SaveChangesAsync();
                    return Redirect($"/projects/details/{firstStage.ProjectId}");
                }

            
            }
            //display the same page if invalid model
            ViewData["ProjectId"] = new SelectList(_context.Evaluations, "ProjectId", "ProjectId", firstStage.ProjectId);
            return View(firstStage);
        }

    }
}
