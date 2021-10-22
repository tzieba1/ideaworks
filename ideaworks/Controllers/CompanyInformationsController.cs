using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ideaworks.Models;
using ideaworks.Data;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace ideaworks.Controllers
{
    public class CompanyInformationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CompanyInformationsController(ApplicationDbContext context)
        {
            _context = context;
        }



        // GET: CompanyInformations/Create
        public IActionResult Create()
        {
            string companyInformationString = HttpContext.Session.GetString("companyInformation");
            if (String.IsNullOrEmpty(companyInformationString))
            {
                ViewData["StageId"] = new SelectList(_context.Projects, "ProjectId", "ProjectId");
                return View();
            }
            else
            {
                CompanyInformation companyInformationExists = (CompanyInformation)JsonSerializer.Deserialize(companyInformationString, typeof(CompanyInformation));
                //setting the string to empty to be able to validate any changes made
                HttpContext.Session.SetString("companyInformation", "");
                return View(companyInformationExists);
            }
        }

        // POST: CompanyInformations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StageId,ContactName,CompanyName,Email,Phone,Website,YearsOfExistance,NumberOfEmployees,IsCanadianBased,IsOntarioBased,OrganizationLocation,PriorCollaborationWithIdeaworks,InternalResearchExists,IntentionToHireStudents,CoreCompanyBusiness,PreviousCollaborationWithResearch")] CompanyInformation companyInformation)
        {
            //getting the data when the back button has been pressed from the next proposal page
            string companyInformationString = HttpContext.Session.GetString("companyInformation");
            //check for the session variable
            if (!String.IsNullOrEmpty(companyInformationString))
            {
                //get the information and fill the page
                CompanyInformation companyInformationExists = (CompanyInformation)JsonSerializer.Deserialize(companyInformationString, typeof(CompanyInformation));
               
                    //setting the string to empty to be able to validate any changes made
                    HttpContext.Session.SetString("companyInformation", "");
                    //clear all the errors for the already existing validated object
                    ModelState.Clear();
                
                return View(companyInformationExists);
            }
            else
            {
                //companyInformation.StageId = 1;
                if (ModelState.IsValid)
                {
                    HttpContext.Session.SetString("companyInformation", JsonSerializer.Serialize(companyInformation));
                    HttpContext.Session.SetString("comingFrom", "1");
                    //_context.Add(companyInformation);
                    //await _context.SaveChangesAsync();
                    return RedirectToAction("Create", "ProposalInformations");
                }
                ViewData["StageId"] = new SelectList(_context.Projects, "ProjectId", "ProjectId", companyInformation.StageId);
                return View(companyInformation);
            }
        }

    }
}
