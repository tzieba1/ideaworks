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
using System.Text.Json;

namespace ideaworks.Controllers
{
    public class ProposalInformationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProposalInformationsController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: ProposalInformations/Create
        public IActionResult Create()
        {
            string proposalInformationString = HttpContext.Session.GetString("proposalInformation");
            string comingFrom = HttpContext.Session.GetString("comingFrom");
            if (!String.IsNullOrEmpty(comingFrom))
            {
                //if the user hasn't been through the first form page
                //redirect to the first form page
               if(!(int.Parse(comingFrom) >= 1))
                return RedirectToAction("Create", "CompanyInformations");
            }
            else
            {
                return RedirectToAction("Create", "CompanyInformations");
            }


            if (String.IsNullOrEmpty(proposalInformationString))
            {
                ViewData["StageId"] = new SelectList(_context.Projects, "ProjectId", "ProjectId");
                return View();
            }
            else
            {
                ProposalInformation proposalInformationExists = (ProposalInformation)JsonSerializer.Deserialize(proposalInformationString, typeof(ProposalInformation));
                //setting the string to empty to be able to validate any changes made
                HttpContext.Session.SetString("proposalInformation", "");
                return View(proposalInformationExists);
            }

        }

        // POST: ProposalInformations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StageId,Urgency,SuggestedBy,ExtraInformation,ParentOrSubsidiary,AuthorizedRepresentativeName,AuthorizedRepresentativePosition,AuthorizedRepresentativePhone,AuthorizedRepresentativeEmail,NumberOfEmployeesInCanada,NumberOfResearchStaffInCanada,AreaOfResearch")] ProposalInformation proposalInformation)
        {
            //getting the data when the back button has been pressed from the next proposal page
            string proposalInformationString = HttpContext.Session.GetString("proposalInformation");
            //check for the session variable
            if (!String.IsNullOrEmpty(proposalInformationString))
            {
                //get the information and fill the page
                ProposalInformation proposalInformationExists = (ProposalInformation)JsonSerializer.Deserialize(proposalInformationString, typeof(ProposalInformation));
               
                    //setting the string to empty to be able to validate any changes made
                    HttpContext.Session.SetString("proposalInformation", "");
                    //clear all the errors for the already existing validated object
                    ModelState.Clear();

                return View(proposalInformationExists);
            }
            else
            {

                if (ModelState.IsValid)
                {
                    //_context.Add(proposalInformation);
                    //await _context.SaveChangesAsync();
                    HttpContext.Session.SetString("proposalInformation", JsonSerializer.Serialize(proposalInformation));
                    HttpContext.Session.SetString("comingFrom", "2");
                    return RedirectToAction("Create", "ProjectInformations");
                }
                ViewData["StageId"] = new SelectList(_context.Projects, "ProjectId", "ProjectId", proposalInformation.StageId);
                return View(proposalInformation);
            }
        }
        [HttpPost]
        [Route("ProposalInformations/PreviousButtonClicked")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PreviousButtonClicked([Bind("StageId,Urgency,SuggestedBy,ExtraInformation,ParentOrSubsidiary,AuthorizedRepresentativeName,AuthorizedRepresentativePosition,AuthorizedRepresentativePhone,AuthorizedRepresentativeEmail,NumberOfEmployeesInCanada,NumberOfResearchStaffInCanada,AreaOfResearch")] ProposalInformation proposalInformation)
        {
            ProposalInformation prop = proposalInformation;
            //clearing the model state to allow the user pass to the prev page
            ModelState.Clear();
            HttpContext.Session.SetString("proposalInformation", JsonSerializer.Serialize(proposalInformation));
            return RedirectToAction("Create", "CompanyInformations");
        }
    }
}
