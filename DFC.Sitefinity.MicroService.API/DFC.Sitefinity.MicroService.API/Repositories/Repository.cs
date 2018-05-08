using System.Threading.Tasks;
using DFC.Sitefinity.MicroService.API.Interfaces;
using DFC.Sitefinity.MicroService.API.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace DFC.Sitefinity.MicroService.API.Repositories
{
    public class Repository : IRepository
    {
        private readonly IMongoDatabase mongoDatabase;
        public Repository(IConfiguration configuration)
        {
            var mongoCs = configuration["mongo-cs"];
            var client = new MongoClient(mongoCs);
            mongoDatabase = client.GetDatabase("SFMicroservices");
        }
        public async Task<JobProfile> GetJobProfileByUrlNameAsync(string urlName)
        {
            var jobProfileCollection = mongoDatabase.GetCollection<JobProfile>("JobProfiles");

            var result = await jobProfileCollection.FindAsync(doc => doc.UrlName.Equals(urlName));

            return await result.FirstOrDefaultAsync();
        }

        public async Task SaveJobProfileAsync(JobProfile jobProfile)
        { 
            var jobProfileCollection = mongoDatabase.GetCollection<JobProfile>("JobProfiles");

            var result = await jobProfileCollection.FindAsync(doc => doc.UrlName.Equals(jobProfile.UrlName)).Result.FirstOrDefaultAsync();
            if (result == null)
            {
                await jobProfileCollection.InsertOneAsync(jobProfile);
            }
        }
    }
}
