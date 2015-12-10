﻿namespace Stravaix.StravaApi.Strive.Representations
{
    using System.Linq;
    using Newtonsoft.Json;
    //using Stravaix.StravaApi.Strive.Representations.Enums;
    //using Stravaix.StravaApi.Strive.Representations.Interfaces;

    public class OperationResponse : BaseRepresentation, IOperationResponse
    {
        public virtual bool OperationSucceeded { get { return new[] {OperationStatus.Ok, OperationStatus.Created, OperationStatus.Accepted, OperationStatus.NoContent}.Contains(Status); } set { } }
        public virtual OperationStatus Status { get; set; }
    }

    public class OperationResponse<T> : OperationResponse, IOperationResponse<T> where T : class
    {
        public virtual T Data { set; get; }

        public virtual string DataAsJson()
        {
            return JsonConvert.SerializeObject(Data);
        }
    }
}