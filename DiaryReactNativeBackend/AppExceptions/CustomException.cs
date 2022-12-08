namespace DiaryReactNativeBackend.AppExceptions;

public class CustomException : Exception
{
    public CustomException () { }

    public CustomException(string message) : base (message) { }
}
