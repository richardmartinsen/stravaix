﻿namespace Stravaix.StravaApi.Strive.Model.Segments
{
    using System.Device.Location;
    using Stravaix.StravaApi.Strive.Model.Activities;
    //using SwimBikeRun.Strive.Model.Enums.Segments;

    public interface ISegmentBase
    {
         int Id { get; set; }
         ActivityType ActivityType { get; set; }
         float AverageGrade { get; set; }
         ClimbType ClimbType { get; set; }
         string City { get; set; }
         string Country { get; set; }
         float Distance { get; set; }
         GeoCoordinate EndCoordinates { get; set; }
         bool IsPrivate { get; set; }
         float MaximumElevation { get; set; }
         float MaximumGrade { get; set; }
         float MinimumElevation { get; set; }
         string Name { get; set; }
         int ResourceState { get; set; }
         bool IsStarred { get; set; }
         GeoCoordinate StartCoordinates { get; set; }
         string State { get; set; }
    }
}