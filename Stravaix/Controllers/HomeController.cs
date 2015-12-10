﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stravaix.Controllers
{
    using System.Threading.Tasks;

    using Stravaix.StravaApi.Activities;
    using Stravaix.StravaApi.Athletes;
    using Stravaix.StravaApi.Authentication;
    using Stravaix.StravaApi.Clients;
    using Stravaix.StravaApi.Streams;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> About()
        {
            ViewBag.Message = "Your application description page.";
            
            var b = StravaTest();

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            var b = getAthleteSummary();

            return View();
        }

        //private async static Task<AthleteSummary> StravaTest()
        private async static Task<List<Comment>> StravaTest()
        {
            StaticAuthentication auth = new StaticAuthentication("72da1a028657d1d0a5c628c87373bebdf15151db");
            StravaClient client = new StravaClient(auth);

            //List<Comment> comments = await client.GetCommentsAsync("102162300");
            //Activity athlete = await client.GetActivityAsync("102162300");
            //Athlete athlete = await client.GetAthleteAsync("1985994");
            //AthleteSummary a = await client.Athletes.GetAthleteAsync();
            var ass = getAthleteSummary();
            List<Comment> comments = await client.Activities.GetCommentsAsync("447602106");
            //AthleteSummary a = await client.Athletes.GetAthleteAsync(ass.Id.ToString());
            return comments;
        }

        private static AthleteSummary getAthleteSummary()
        {
            StaticAuthentication auth = new StaticAuthentication("72da1a028657d1d0a5c628c87373bebdf15151db");
            StravaClient client = new StravaClient(auth);
            StreamClient stream = new StreamClient(auth);

            //List<Comment> comments = await client.GetCommentsAsync("102162300");
            //Activity athlete = await client.GetActivityAsync("102162300");
            //Athlete athlete = await client.GetAthleteAsync("1985994");
            //AthleteSummary a = await client.Athletes.GetAthleteAsync();
            AthleteSummary a = client.Athletes.GetAthlete();
            List<Comment> c = client.Activities.GetComments("447602106");
            AthleteSummary athlete = client.Athletes.GetAthlete("70857");
            Activity activity = client.Activities.GetActivity("447132037", true);
            //ActivityMeta am = client.Activities.GetActivity()
            List<ActivityStream> streams = stream.GetActivityStream("447132037", StreamType.Watts, StreamResolution.All);
            return a;
        }
    }
}