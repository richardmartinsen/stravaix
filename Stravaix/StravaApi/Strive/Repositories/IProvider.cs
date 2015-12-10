namespace Stravaix.StravaApi.Strive.Repositories
{
	using System.Collections.Generic;
	using Stravaix.StravaApi.Strive.Model.Segments;

    public interface IRepository
    {
        void Create(ISegment segment);
        ISegment Read(int segmentId);
        IList<ISegment> Read();
        void Update(int segmentId, ISegment updatedSegment);
        void Delete(int segmentId);
    }
}
