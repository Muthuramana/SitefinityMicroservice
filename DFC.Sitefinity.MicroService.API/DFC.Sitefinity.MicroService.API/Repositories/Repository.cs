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
        private readonly IMongoCollection<JobProfile> jobprofileCollection;
        public Repository(IConfiguration configuration)
        {
            var mongoCs = configuration["mongo-cs"];
            var client = new MongoClient(mongoCs);
            mongoDatabase = client.GetDatabase("SFMicroservices");
            jobprofileCollection = mongoDatabase.GetCollection<JobProfile>(nameof(JobProfile));
        }
        public async Task<JobProfile> GetJobProfileByUrlNameAsync(string urlName)
        {
           

            var result = await jobprofileCollection.FindAsync(doc => doc.UrlName.Equals(urlName));

            return await result.FirstOrDefaultAsync();
        }

        public async Task SaveJobProfileAsync(JobProfile jobProfile)
        {  
            var result = await jobprofileCollection.FindAsync(doc => doc.UrlName.Equals(jobProfile.UrlName)).Result.FirstOrDefaultAsync();
            if (result == null)
            {
                await jobprofileCollection.InsertOneAsync(jobProfile);
            }
        }

        public async Task UpdateJobProfileAsync(JobProfile jobProfile)
        {

            await jobprofileCollection.ReplaceOneAsync(doc => doc.UrlName.Equals(jobProfile.UrlName), jobProfile, new UpdateOptions { IsUpsert = true });

            //var result = await jobprofileCollection.FindAsync(doc => doc.UrlName.Equals(jobProfile.UrlName)).Result.FirstOrDefaultAsync();
            //if (result != null)
            //{
            //    //var filter = Builders<JobProfile>.Filter.Where(jp => jp.UrlName.Equals(jobProfile.UrlName));
            //    //var updateAltTitle = Builders<JobProfile>.Update.Set(jp => jp.AlternativeTitles, jobProfile.AlternativeTitles);
            //    //var updateTitle = Builders<JobProfile>.Update.Set(jp => jp.Title, jobProfile.Title);
            //    //var careerDefinition = Builders<JobProfile>.Update.Set(jp => jp.CareerPathAndProgression, jobProfile.CareerPathAndProgression);
            //    //var entryDefinition = Builders<JobProfile>.Update.Set(jp => jp.EntryRequirements, jobProfile.EntryRequirements);
            //    //var howDefinition = Builders<JobProfile>.Update.Set(jp => jp.HowToBecome, jobProfile.HowToBecome);
            //    //var overviewDefinition = Builders<JobProfile>.Update.Set(jp => jp.Overview, jobProfile.Overview);
            //    //var skillsdDefinition = Builders<JobProfile>.Update.Set(jp => jp.Skills, jobProfile.Skills);
            //    //var whatyoulldoDefinition = Builders<JobProfile>.Update.Set(jp => jp.WhatYoullDo, jobProfile.WhatYoullDo);
            //    //var workingDefinition = Builders<JobProfile>.Update.Set(jp => jp.WorkingHoursPatternsAndEnvironment, jobProfile.WorkingHoursPatternsAndEnvironment);
            //    //var combinedDefinitions =
            //    //    Builders<JobProfile>.Update.Combine(updateAltTitle, updateTitle, careerDefinition, entryDefinition, howDefinition, overviewDefinition, skillsdDefinition, whatyoulldoDefinition, workingDefinition);
            //    //await jobprofileCollection.UpdateOneAsync(filter, combinedDefinitions);




            //}
            //else
            //{
            //    await jobprofileCollection.InsertOneAsync(jobProfile);
            //}
        }
    }
}
