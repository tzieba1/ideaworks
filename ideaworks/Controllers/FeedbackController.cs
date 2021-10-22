using ideaworks.Data;
using ideaworks.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ideaworks.Controllers
{
    /// <summary>
    /// Controller to get the feedback on rejection  and send it to the client via email
    /// </summary>
    public class FeedbackController : Controller
    {
        private readonly ApplicationDbContext _context;
        private static string[] ProjectStatus = { "PENDING", "ONGOING", "REJECTED", "APPROVED" };

        private static MailAddress senderEmail = new MailAddress("postmaster@sandbox80104595969a4f7d905c1713816fc96f.mailgun.org", "IDEAWORKS");
        private static String senderPassword = "**** Put your MailGun password here ****";
        private static String senderHostname = "smtp.mailgun.org";
        private static int senderPort = 587;


        public FeedbackController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FeedbackController/Create
        public async Task<IActionResult> Create()
        {
            //getting the project id from the session varibale

            IEnumerable<Project> projectList = await _context.Projects.Include(c => c.CompanyInformation)
                                                                        .Include(c => c.ProjectInformation)
                                                                        .Include(c => c.ProposalInformation)
                                                                        .Include(c => c.Evaluation)
                                                                        .AsNoTracking().ToListAsync();


            string projectIdString = HttpContext.Session.GetString("projectId");

            ViewBag.Statuses = ProjectStatus;

            // If projectIdString is null - Request is not coming from the project details page
            if (string.IsNullOrEmpty(projectIdString))
            {
                return RedirectToAction("Index", "projects");
            }

            long id = long.Parse(projectIdString);

            var project = projectList.Where(m => m.ProjectId == id).FirstOrDefault();
            if (project == null)
            {
                return RedirectToAction("Index", "projects");
            }

            Evaluation currentevaluation = _context.Evaluations.Where(i => i.ProjectId == id).FirstOrDefault();
            ViewBag.SuggestedStatus = (currentevaluation is null ? "PENDING" : (currentevaluation.EvaluationStage < 0 ? "REJECTED" : "APPROVED"));

            FirstStage firstStage = _context.FirstStages.Where(i => i.ProjectId == id).FirstOrDefault();
            ViewBag.firstStageStatus = setStatus(firstStage is null ? "U" : firstStage.ProjectWorthDeveloping);

            SecondStage secondStage = _context.SecondStages.Where(i => i.ProjectId == id).FirstOrDefault();
            ViewBag.secondStageStatus = setStatus(secondStage is null ? "U" : secondStage.FeasibleForIdeaworks);

            ThirdStage thirdStage = _context.ThirdStages.Where(i => i.ProjectId == id).FirstOrDefault();
            ViewBag.thirdStageStatus = setStatus(thirdStage is null ? "U" : thirdStage.IsFundingApproved);
            ViewBag.thirdStageRequired = setStatus(thirdStage is null ? "U" : thirdStage.IsApplicable);

            return View(project); 
        }

        public string setStatus(string status = " ")
        {
            if (status != null && status.Contains("Y")) { return "Yes"; }
            else if (status != null && status.Contains("N")) { return "No"; }
            else { return "Unknown"; }
        }

        // POST: FeedbackController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection collection)
        {
            string feedbackText = collection["feedbackText"];

            string projectIdString = HttpContext.Session.GetString("projectId");

            // If projectIdString is null - Request is not coming from the project details page
            if (string.IsNullOrEmpty(projectIdString))
            {
                return RedirectToAction("Index", "Projects");
            }

            long id = long.Parse(projectIdString);


            Project currentProject = _context.Projects.Where(i => i.ProjectId == id).FirstOrDefault();
            Evaluation currentevaluation = _context.Evaluations.Where(i => i.ProjectId == id).FirstOrDefault();
            CompanyInformation company = _context.CompanyInformations.Where(i => i.StageId == id).FirstOrDefault();
            ProposalInformation proposal = _context.ProposalInformations.Where(i => i.StageId == id).FirstOrDefault();

            try
            {
                //changing the evaluation feedback and the evalutionStage 
                currentevaluation.Feedback = feedbackText;
                //currentProject.ProjectStatus = (currentevaluation.EvaluationStage < 0 ? "REJECTED" : "APPROVED");
                _context.Update(currentevaluation);
                //_context.Update(currentProject);
                _context.SaveChanges();


                // Sends emails here

                var contactEmail = new MailAddress(company.Email.ToString(), company.ContactName.ToString());
                var representativeEmail = new MailAddress(proposal.AuthorizedRepresentativeEmail.ToString(), proposal.AuthorizedRepresentativeName.ToString());

                string subject = "You received a feedback on your project submission to IDEAWORKS";

                Console.WriteLine(contactEmail + " " + representativeEmail);

                var smtp = new SmtpClient
                {
                    Host = senderHostname,
                    Port = senderPort,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderEmail.Address, senderPassword)
                };

                if(collection["contactEmailCheckBox"].Contains("on")) {
                    using (var mess = new MailMessage(senderEmail, contactEmail)
                    {
                        Subject = subject,
                        Body = feedbackText
                    })
                    {
                        smtp.Send(mess);
                    }
                }

                if (collection["authroizedEmailCheckBox"].Contains("on")) {
                    using (var mess = new MailMessage(senderEmail, representativeEmail)
                    {
                        Subject = subject,
                        Body = feedbackText
                    })
                    {
                        smtp.Send(mess);
                    }
                }

                System.Threading.Thread.Sleep(5000);
                return RedirectToAction("Create", "Feedback");

            }
            catch(Exception)
            {
                return RedirectToAction("Index", "Projects");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeStatus(IFormCollection collection)
        {
            string selectedStatus = collection["projectStatus"];

            string projectIdString = HttpContext.Session.GetString("projectId");

            // If projectIdString is null - Request is not coming from the project details page
            if (string.IsNullOrEmpty(projectIdString))
            {
                return RedirectToAction("Index", "Projects");
            }

            long id = long.Parse(projectIdString);


            Project currentProject = _context.Projects.Where(i => i.ProjectId == id).FirstOrDefault();

            try
            {

                currentProject.ProjectStatus = selectedStatus;
                _context.Update(currentProject);
                _context.SaveChanges();

                return RedirectToAction("Create", "Feedback");

            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Projects");
            }
        }
    }
}
