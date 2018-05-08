using System.Collections.Generic;
using System.Threading.Tasks;
using DFC.Sitefinity.MicroService.API.Models;

namespace DFC.Sitefinity.MicroService.API.Interfaces
{
    public interface IJobProfileService
    {
        Task<JobProfile> GetJobProfileByUrlNameAsync(string urlName);

        Task<IEnumerable<JobProfile>> GetJobProfilesAsync();
    }
}
