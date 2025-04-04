using System.Text.Json.Serialization;

namespace LibraryManager.Application.Models.ViewModels;

public class ResultViewModel(bool isSuccess = true, string message = "")
{
    [JsonIgnore]
    public bool IsSuccess { get; private set; } = isSuccess;
    public string Message { get; private set; } = message;

    public static ResultViewModel Success()
        => new();
    
    public static ResultViewModel Error(string message)
        => new(false, message);
}

public class ResultViewModel<T>(T? data, bool isSuccess = true, string message = "")
    : ResultViewModel(isSuccess, message)
{
    public T? Data { get; private set; } = data;

    public static ResultViewModel<T> Success(T data, string message = "")
        => new(data, message: message);

    public new static ResultViewModel<T> Error(string message)
        => new(default, false, message);
}