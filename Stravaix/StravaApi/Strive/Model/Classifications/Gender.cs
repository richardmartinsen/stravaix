namespace Stravaix.StravaApi.Strive.Model.Classifications
{
    using System.Runtime.Serialization;

    public enum Gender
    {
        [EnumMember(Value = "U")]
        Unknown,
        [EnumMember(Value = "M")]
        Male,
        [EnumMember(Value = "F")]
        Female,
        [EnumMember(Value = "B")]
        Both = Male & Female
    }
}