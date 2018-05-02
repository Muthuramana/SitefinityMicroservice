using System.Collections.Concurrent;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace DFC.Sitefinity.MicroService.API.Controllers
{
    [Route("api/[controller]")]
    public class JobProfileController : Controller
    {
        // GET api/jobprofile/plumber
        [HttpGet("{urlname}")]
        public IDictionary<string, string> Get(string urlName)
        {
            var result = new ConcurrentDictionary<string, string> ();

            result.TryAdd("HowToBecome", "<div></div>");
            result.TryAdd("EntryQualifications", "<div></div>");
            result.TryAdd("WhatYouWillDo", "<div></div>");

            return result;
        }
    }
}
