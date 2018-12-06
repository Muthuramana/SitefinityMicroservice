using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DFC.Sitefinity.MicroService.API.Interfaces;
using DFC.Sitefinity.MicroService.API.Models;
using Newtonsoft.Json;

namespace DFC.Sitefinity.MicroService.API.Services
{
    public class JobProfileService : IJobProfileService
    {
        private string apiUrl = "https://lab.nationalcareersservice.org.uk/api/bespoke-ui/jobprofiles?$orderby=UrlName&$expand=SOC($select=SOCCode)";
        public async Task<JobProfile> GetJobProfileByUrlNameAsync(string urlName)
        {
            var jobProfile = new JobProfile();
            using (var client = new HttpClient())
            {
                var result = await client.GetStringAsync($"{apiUrl}?$filter=UrlName eq '{urlName}'");

                jobProfile = JsonConvert.DeserializeObject<OdataResult<JobProfile>>(result)?.Value?.FirstOrDefault();
            }

            return jobProfile;
        }

        public async Task<IEnumerable<JobProfile>> GetJobProfilesAsync()
        {
            var nextPage = apiUrl;
            var jobProfileList = new List<JobProfile>();
            while (!string.IsNullOrEmpty(nextPage))
            {
                using (var client = new HttpClient())
                {
                    var result = await client.GetStringAsync(nextPage);

                    jobProfileList.AddRange(JsonConvert.DeserializeObject<OdataResult<JobProfile>>(result)?.Value);

                    nextPage = JsonConvert.DeserializeObject<OdataResult<JobProfile>>(result)?.NextLink;
                }

            }
        
            return jobProfileList;
        }
    }
}
