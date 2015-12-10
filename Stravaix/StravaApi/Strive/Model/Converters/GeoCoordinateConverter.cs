namespace Stravaix.StravaApi.Strive.Model.Converters
{
    using System;
    using System.Device.Location;
    using Newtonsoft.Json.Linq;

    public class GeoCoordinateConverter : JsonArrayConverter<GeoCoordinate>
    {
        protected override GeoCoordinate Create(Type @type, JArray json)
        {
            return json != null ? new GeoCoordinate((double)json.First, (double)json.Last) : null;
        }
    }
}
