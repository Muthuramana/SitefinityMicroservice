using System.Threading.Tasks;
using DFC.Sitefinity.MicroService.API.Models;

namespace DFC.Sitefinity.MicroService.API.Interfaces
{
    public interface IRepository
    {
       Task<JobProfile> GetJobProfileByUrlNameAsync(string urlName);

        Task SaveJobProfileAsync(JobProfile jobProfile);


    }
}
