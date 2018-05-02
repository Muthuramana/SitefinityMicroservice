using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DFC.Sitefinity.MicroService.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DFC.Sitefinity.MicroService.API.Controllers
{
    [Route("api/[controller]")]
    public class JobProfileController : Controller
    {

        private readonly IJobProfileRepository jobProfileRepository;

        public JobProfileController(IJobProfileRepository jobProfileRepository)
        {
            this.jobProfileRepository = jobProfileRepository;
        }
      // GET api/jobprofile/plumber
      [HttpGet("{urlname}")]
        public async Task<IDictionary<string, string>> Get(string urlName)
        {
            var result = new ConcurrentDictionary<string, string> ();

            var jobProfile = await jobProfileRepository.GetJobProfileByUrlName(urlName);
           
            if (jobProfile != null)
            {
                var properties = jobProfile.GetType().GetProperties()
                    .Where(prop => prop.PropertyType == typeof(string));
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
    }
}
