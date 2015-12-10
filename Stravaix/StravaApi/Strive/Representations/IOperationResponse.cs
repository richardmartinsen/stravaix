namespace Stravaix.StravaApi.Strive.Representations
{
    //using Stravaix.StravaApi.Strive.Representations;
    public interface IOperationResponse
    {
        bool OperationSucceeded { get; set; }
        OperationStatus Status { get; set; }
    }

    public interface IOperationResponse<T> : IOperationResponse
    {
        T Data { set; get; }
        string DataAsJson();
    }
}