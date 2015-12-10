namespace Stravaix.StravaApi.Strive.Workflows
{
	using System.Collections.Generic;
	using Stravaix.StravaApi.Strive.Model.Segments;
	using Stravaix.StravaApi.Strive.Representations;

    public interface ISegmentWorkflow
    {
        IOperationResponse<ISegment> GetById(int segmentId);
        IOperationResponse<ILeaderboard> GetSegmentLeaderboard(int segmentId, IDictionary<string, string> queryParameters);   
    }
}