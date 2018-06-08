using Microsoft.AspNetCore.Mvc;
using TechJobs.Data;
using TechJobs.Models;
using TechJobs.ViewModels;

namespace TechJobs.Controllers
{
    public class JobController : Controller
    {

        // Our reference to the data store
        private static JobData jobData;

        static JobController()
        {
            jobData = JobData.GetInstance();
        }

        // The detail display for a given Job at URLs like /Job?id=17
        public IActionResult Index(int id)
        {
            // TODO #1 - get the Job with the given ID and pass it into the view
            
            
            Job someJob = jobData.Find(id);
            JobViewModel jobViewModel = new JobViewModel
            {
                Name = someJob.Name,
                Employer = someJob.Employer,
                CoreCompetency = someJob.CoreCompetency,
                Location = someJob.Location,
                PositionType = someJob.PositionType
            };

            return View(jobViewModel);
        }

        public IActionResult New()
        {
            NewJobViewModel newJobViewModel = new NewJobViewModel();
            return View(newJobViewModel);
        }

        [HttpPost]
        public IActionResult New(NewJobViewModel newJobViewModel)
        {
            // TODO #6 - Validate the ViewModel and if valid, create a 
            // new Job and add it to the JobData data store. Then
            // redirect to the Job detail (Index) action/view for the new Job.
            if (ModelState.IsValid)
            {
                JobData jobData = JobData.GetInstance();

                string name = newJobViewModel.Name;
                Employer employer = jobData.Employers.Find(newJobViewModel.EmployerID);
                Location location = jobData.Locations.Find(newJobViewModel.LocationID);
                CoreCompetency coreCompetency = jobData.CoreCompetencies.Find(newJobViewModel.CoreCompetencyID);
                PositionType positionType = jobData.PositionTypes.Find(newJobViewModel.PositionTypeID);

                Job newJob = new Job
                {
                    Name = name,
                    Employer = employer,
                    Location = location,
                    CoreCompetency = coreCompetency,
                    PositionType = positionType
                };

                jobData.Jobs.Add(newJob);

                return Redirect(string.Format("/Job?id={0}", newJob.ID));
                
            }

            return View(newJobViewModel);
        }
    }
}
