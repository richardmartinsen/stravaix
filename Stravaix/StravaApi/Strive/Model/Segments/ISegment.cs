namespace Stravaix.StravaApi.Strive.Model.Segments
{
    using System;
    using Stravaix.StravaApi.Strive.Model.Activities;

    public interface ISegment : ISegmentBase
    {
        DateTime Created { get; set; }
        bool IsHazardous { get; set; }
        IMap Map { get; set; }
        int NumberOfAthletes { get; set; }
        int NumberOfEfforts { get; set; }
        int NumberOfStars { get; set; }
        float TotalElevationGain { get; set; }
        DateTime Updated { get; set; }
    }
}