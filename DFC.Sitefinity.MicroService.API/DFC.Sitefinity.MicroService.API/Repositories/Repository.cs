using System.Threading.Tasks;
using DFC.Sitefinity.MicroService.API.Interfaces;
using DFC.Sitefinity.MicroService.API.Models;
using MongoDB.Driver;

namespace DFC.Sitefinity.MicroService.API.Repositories
{
    public class Repository : IRepository
    {
        public async Task<JobProfile> GetJobProfileByUrlNameAsync(string urlName)
        {
            var client = new MongoClient();
            var mongoDatabase = client.GetDatabase("SifefinityMicroService");

            var jobProfileCollection = mongoDatabase.GetCollection<JobProfile>("JobProfiles");

            var result = await jobProfileCollection.FindAsync(doc => doc.UrlName.Equals(urlName));

            return await result.FirstAsync();
        }

        public async Task SaveJobProfileAsync(JobProfile jobProfile)
        {
            var client = new MongoClient();
            var mongoDatabase = client.GetDatabase("SifefinityMicroService");

            var jobProfileCollection = mongoDatabase.GetCollection<JobProfile>("JobProfiles");

             await jobProfileCollection.InsertOneAsync(jobProfile);

        }
    }
}
