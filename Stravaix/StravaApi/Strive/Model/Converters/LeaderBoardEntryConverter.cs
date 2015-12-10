namespace Stravaix.StravaApi.Strive.Model.Converters
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    //using Stravaix.StravaApi.Strive.Model.Interfaces.Segments;
    using Stravaix.StravaApi.Strive.Model.Segments;

    public class LeaderBoardEntryConverter : JsonArrayConverter<IList<ILeaderboardEntry>>
    {
        protected override IList<ILeaderboardEntry> Create(Type @type, JArray json)
        {
            if (json == null) { return null; }

            var entries = new List<ILeaderboardEntry>();

            foreach (var entry in json)
            {
                entries.Add(JsonConvert.DeserializeObject<LeaderboardEntry>(entry.ToString()));
            }

            return entries;
        }
    }
}
