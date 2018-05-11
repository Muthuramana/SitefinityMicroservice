using System;
using MongoDB.Bson.Serialization.Attributes;

namespace DFC.Sitefinity.MicroService.API.Models
{
    public class JobProfile
    {
        public Guid Id { get; set; }
        [BsonId]
        public string UrlName { get; set; }
        public string Title { get; set; }
        public string AlternativeTitle { get; set; }
        public string Overview { get; set; }
        public string Salary { get; set; }
        public string Skills { get; set; }
        public string WhatYouWillDo { get; set; }
        public string WorkingHoursPatternsAndEnvironment { get; set; }
        public string CareerPathAndProgression { get; set; }
        public string HowToBecome { get; set; }
        public SOC SOC { get; set; }
        public string MinimumHours { get; set; }
        public string MaximumHours { get; set; }
    }

    public class SOC
    {
        public string SOCCode { get; set; }

        public override string ToString()
        {
            return SOCCode;
        }

        public override int GetHashCode()
        {
            return SOCCode.GetHashCode();
        }
    }
}
