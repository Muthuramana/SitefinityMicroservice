using System.Threading.Tasks;
using DFC.Sitefinity.MicroService.API.Models;

namespace DFC.Sitefinity.MicroService.API.Interfaces
{
    public interface IJobProfileRepository
    {
        Task<JobProfile> GetJobProfileByUrlName(string urlName);
    }
}
