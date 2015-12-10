namespace Stravaix.StravaApi.Strive.Model.Segments
{
    using System.Collections.Generic;

    public interface ILeaderboard
    {
        int EntryCount { get; set; }
        IList<ILeaderboardEntry> Entries { get; set; }
    }
}