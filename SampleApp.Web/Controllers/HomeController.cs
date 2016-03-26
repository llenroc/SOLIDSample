namespace SampleApp.Web.Controllers
{
    using Core.Interfaces;
    using Core.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using ViewModels;
    public class HomeController : BaseController
    {
        private IMarketingLeadRepository LeadRepo;
        public HomeController(IMarketingLeadRepository leadRepo)
        {
            LeadRepo = leadRepo;
        }

        public ActionResult Index()
        {
            return View(LeadsList().Result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddLead(MarketingLeadViewModel model)
        {
            if (ModelState.IsValid)
            {
                MarketingLead NewLead = new MarketingLead();
                NewLead.Email = model.Email;
                NewLead.FirstName = model.FirstName;
                NewLead.LastName = model.LastName;
                await LeadRepo.CreateAsync(NewLead);
            }

            return RedirectToAction("index");
        }

        [HttpGet]
        public PartialViewResult GetLeads()
        {
            return PartialView("_LeadListTemplate", LeadsList().Result);
        }

        private async Task<List<MarketingLeadViewModel>> LeadsList()
        {
            var leads = await LeadRepo.GetAllAsync();
            var list = new List<MarketingLeadViewModel>();

            foreach (var lead in leads)
            {
                var model = new MarketingLeadViewModel();
                model.Email = lead.Email;
                model.FirstName = lead.FirstName;
                model.LastName = lead.LastName;

                list.Add(model);
            }

            return list;
        }

    }
}