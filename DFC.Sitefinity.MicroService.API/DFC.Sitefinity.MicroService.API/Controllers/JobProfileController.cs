using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DFC.Sitefinity.MicroService.API.Interfaces;
using DFC.Sitefinity.MicroService.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DFC.Sitefinity.MicroService.API.Controllers
{
    [Route("api/[controller]")]
    public class JobProfileController : Controller
    {

        private readonly IJobProfileService jobProfileService;
        private readonly IRepository repository;

        public JobProfileController(IJobProfileService jobProfileService, IRepository repository)
        {
            this.jobProfileService = jobProfileService;
            this.repository = repository;
        }
      // GET api/jobprofile/plumber
      [HttpGet("{urlname}")]
        public async Task<IDictionary<string, string>> Get(string urlName)
        {
            var result = new ConcurrentDictionary<string, string> ();

            var jobProfile = await repository.GetJobProfileByUrlNameAsync(urlName);
           
            if (jobProfile != null)
            {
                var properties = jobProfile.GetType().GetProperties();
                    //.Where(prop => prop.PropertyType == typeof(string));
                foreach (var property in properties)
                {
                    var item = jobProfile.GetType().GetProperty(property.Name)?.GetValue(jobProfile, null);
                    if (item != null)
                    {
                        result.TryAdd(property.Name, Convert.ToString(item));
                    }
                }
            }

            return result;
        }

        [HttpGet("updatejobprofilerepo")]
        public async Task<bool> GetUpdateJobProfileRepo()
        {
            var jobProfiles = await jobProfileService.GetJobProfilesAsync();

            foreach (var jobProfile in jobProfiles)
            {
                await repository.UpdateJobProfileAsync(jobProfile);
            }

            return true;
        }

        [HttpPost("updatejobprofile")]
        public async Task<bool> PostJobProfile([FromBody]JobProfile jobProfile)
        {
            if (jobProfile != null)
            {
                await repository.UpdateJobProfileAsync(jobProfile);
            }
           
            return true;
        }
    }
}
