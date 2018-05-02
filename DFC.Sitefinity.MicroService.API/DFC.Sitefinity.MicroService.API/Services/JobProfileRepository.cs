using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DFC.Sitefinity.MicroService.API.Interfaces;
using DFC.Sitefinity.MicroService.API.Models;
using Newtonsoft.Json;

namespace DFC.Sitefinity.MicroService.API.Services
{
    public class JobProfileRepository : IJobProfileRepository
    {
        public async Task<JobProfile> GetJobProfileByUrlName(string urlName)
        {
            var jobProfile = new JobProfile();
            using (var client = new HttpClient())
            {

                var result = await client.GetStringAsync($"http://dev-beta.nationalcareersservice.org.uk/api/bespoke-ui/jobprofiles?$filter=UrlName eq '{urlName}'");

                jobProfile = JsonConvert.DeserializeObject<OdataResult<JobProfile>>(result)?.Value?.FirstOrDefault();
                
            }

            return jobProfile;
        }
    }
}
