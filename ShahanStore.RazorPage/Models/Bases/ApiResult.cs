using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis;
using System.Net;

namespace ShahanStore.RazorPage.Models.Bases;

public class ApiResult
{
    public string Message { get; set; }
    public OperationResultStatus Status { get; set; }
    public bool IsSuccess { get; set; }


    public static ApiResult Success(string message) => new() { IsSuccess = true, Message = message ,Status = OperationResultStatus.Success};
    public static ApiResult Error(string message) => new() { IsSuccess = false, Message = message ,Status = OperationResultStatus.Error};
    public static ApiResult NotFound(string message) => new() { IsSuccess = false, Message = message ,Status = OperationResultStatus.NotFound};
}
public class ApiResult<TData> : ApiResult
{
    public TData? Data { get; set; }

    public static ApiResult<TData> Success(TData data, string message)
    {
        return new() { IsSuccess = true, Message = message, Data = data,Status = OperationResultStatus.Success};
    }
    public static new ApiResult<TData> Error(string message)
    {
        return new() { IsSuccess = false, Message = message, Data = default,Status = OperationResultStatus.Error};
    }
}

public enum OperationResultStatus
{
    Success,
    Error,
    NotFound,
    Warning
}