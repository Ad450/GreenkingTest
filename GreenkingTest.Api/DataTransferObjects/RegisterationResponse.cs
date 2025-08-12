using GreenKingRefactoring.Speaker.Data.Enums;

namespace GreenKingRefactoring.Speaker.DataTransferObjects;

public class RegistrationResponse<T>
{
    public bool Success { get; set; }
    public string? Error { get; set; }
    public T? Data { get; set; }

    public static RegistrationResponse<T> ErrorResponse(string? errorMessage)
    {
        return new RegistrationResponse<T>
        {
            Success = false,
            Error = errorMessage,
            Data = default
        };
    }

    public static RegistrationResponse<T> SuccessResponse(T data)
    {
        return new RegistrationResponse<T>
        {
            Success = true,
            Error = null,
            Data = data
        };
    }
}